using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using DevExpress.XtraTab;
using Commons.Model.Stock;
using Commons.Model;
using System.Linq;
using Newtonsoft.Json;

namespace Inventory
{
    public partial class InventoryOrder : BaseForm
    {
        #region 参数
        public InventorySearch search = null;
        public InventotyDetailHeader headerItem = null;
        List<InventoryHeaderDetailCommand> list = null;
        string startDate = null;
        string endDate = null;
        string docId = null;
        string status = null;
        int day = 0;
        #endregion

        #region 构造
        public InventoryOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                ((XtraTabPage)this.Parent).Text = "盘点查询";
                search.Visible = true;
                search.BringToFront();
                m_frm.PromptInformation("");
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 加载事件
        private void InventoryOrder_Load(object sender, EventArgs e)
        {
            //查询状态
            SearchStatus();
            //行明细状态
            RowStatus();
            day = DevCommon.GetDay();
            if (search != null)
            {
                teDocId.Text = headerItem.baseEntry;
                cboStatus.EditValue = headerItem.status;
                docId = headerItem.baseEntry;
                btnInventory.Enabled = false;
            }
            else
            {
                //开始时间
                deStartDate.Text = DateTime.Now.AddDays(-day).ToString("yyyy-MM-dd");
                //结束时间
                deEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                startDate = Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                endDate = (Convert.ToDateTime(deEndDate.EditValue).AddDays(1)).ToString("yyyy-MM-dd HH:mm:ss");
            }
            //加载数据
            Data();
        }
        #endregion

        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            docId = teDocId.Text.Trim();
            if (!DevCommon.ValidateDate(Convert.ToDateTime(deStartDate.EditValue), Convert.ToDateTime(deEndDate.EditValue)))
            {
                return;
            }
            startDate = Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
            endDate = (Convert.ToDateTime(deEndDate.EditValue).AddDays(1)).ToString("yyyy-MM-dd HH:mm:ss");
            status = string.IsNullOrEmpty(cboStatus.Text.ToString()) ? "" : cboStatus.EditValue.ToString();
            Data();
        }
        #endregion

        #region 获得命令数据
        private void Data()
        {
            try
            {
                var searchCondition = new
                {
                    startDate = startDate,
                    endDate = endDate,
                    docId = docId,
                    status = status
                };
                list = null;
                if (DevCommon.getDataByWebService("InventotyCommandSearch", "InventotyCommandSearch", searchCondition, ref list) == RetCode.OK)
                {
                    gcInventoryCommand.DataSource = list;
                }
                else
                {
                    gcInventoryCommand.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 盘点
        private void btnInventory_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventoryCommand.RowCount > 0)
                {
                    int status = gvInventoryCommand.GetFocusedRowCellValue("status") == null ? 0 : Convert.ToInt32(gvInventoryCommand.GetFocusedRowCellValue("status"));
                    if (status != Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        //单号
                        string inventoryDocId = gvInventoryCommand.GetFocusedRowCellValue("docId").ToString();
                        InventoryHeaderDetailCommand headerItem = list.FirstOrDefault(p => p.docId == inventoryDocId);
                        Inventory detail = new Inventory();
                        detail.orderHeader = headerItem;
                        detail.order = this;
                        detail.m_frm = m_frm;
                        detail.Location = new Point(0, 0);
                        detail.TopLevel = false;
                        detail.TopMost = false;
                        detail.ControlBox = false;
                        detail.FormBorderStyle = FormBorderStyle.None;
                        detail.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(detail);
                        ((XtraTabPage)this.Parent).Text = "盘点";
                        detail.Show();
                        detail.BringToFront();
                        m_frm.PromptInformation("");
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 明细
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventoryCommand.RowCount > 0)
                {
                    SearchDetialData();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询发货明细方法
        public void SearchDetialData()
        {
            try
            {
                if (gvInventoryCommand.RowCount > 0)
                {
                    //单号
                    string inventoryDocId = gvInventoryCommand.GetFocusedRowCellValue("docId").ToString();
                    InventoryHeaderDetailCommand headerItem = list.FirstOrDefault(p => p.docId == inventoryDocId);
                    InventoryOrderDetial detail = new InventoryOrderDetial();
                    detail.headerCommand = headerItem;
                    detail.order = this;
                    detail.m_frm = m_frm;
                    detail.Location = new Point(0, 0);
                    detail.TopLevel = false;
                    detail.TopMost = false;
                    detail.ControlBox = false;
                    detail.FormBorderStyle = FormBorderStyle.None;
                    detail.Dock = DockStyle.Fill;
                    this.Visible = false;
                    ((XtraTabPage)this.Parent).Controls.Add(detail);
                    ((XtraTabPage)this.Parent).Text = "盘点指令明细";
                    detail.Show();
                    detail.BringToFront();
                    m_frm.PromptInformation("");
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得状态数据
        public void SearchStatus()
        {
            DataTable dt = DevCommon.GetAllBusinessStatus();
            if (dt != null)
            {
                DevCommon.initCmb(cboStatus, dt, true);
            }
        }
        #endregion

        #region 添加列表状态数据
        public void RowStatus()
        {
            try
            {
                DataTable status = DevCommon.GetAllBusinessStatus();
                if (status != null)
                {
                    DevCommon.initCmb(repositoryItemImageComboBox1, status, false);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 点击行单据进入盘点指令明细页面
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventoryCommand.RowCount > 0)
                {
                    SearchDetialData();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 重新加载方法
        public void Reload()
        {
            try
            {
                int rowhandel = gvInventoryCommand.FocusedRowHandle;
                Data();
                gvInventoryCommand.FocusedRowHandle = rowhandel;
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据指令单号获得仓库
        private void commandFacility(string docId)
        {
            try
            {
                var searchCondition = new
                {
                    docId = docId
                };
                object obj = null;
                if (DevCommon.getDataByWebService("SearchCommandFacility", "SearchCommandFacility", searchCondition, ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        DataTable dtFacility = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                        gcInventoryFacility.DataSource = dtFacility;
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 页改变事件
        private void xtraTabInventoryCommand_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (xtraTabInventoryCommand.SelectedTabPage == tabCommandFacility)
                {
                    if (gvInventoryCommand.RowCount > 0)
                    {
                        string commandDocId = gvInventoryCommand.GetFocusedRowCellValue("docId") == null ? null : gvInventoryCommand.GetFocusedRowCellValue("docId").ToString();
                        if (!string.IsNullOrEmpty(commandDocId))
                        {
                            commandFacility(commandDocId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion
    }
}