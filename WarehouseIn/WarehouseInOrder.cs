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
using System.Linq;
using Commons.Model;

namespace WarehouseIn
{
    public partial class WarehouseInOrder : BaseForm
    {
        #region 参数
        public WarehouseInSearch search = null;
        public WarehouseInHeaderDetail headerItem = null;
        List<WarehouseInHeaderDetailCommand> whsInCommand = null;
        string startDate = null;
        string endDate = null;
        string status = null;
        string commandNo = null;
        int day = 0;
        #endregion

        #region 构造
        public WarehouseInOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "入库单查询";
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
        private void WarehouseInOrder_Load(object sender, EventArgs e)
        {
            try
            {
                //查询状态
                SearchStatus();
                //行明细状态
                RowStatus();
                day = DevCommon.GetDay();
                if (headerItem != null)
                {
                    teCommandNo.Text = headerItem.baseEntry;
                    commandNo = headerItem.baseEntry;
                    pcCondition.Enabled = false;
                    btnReceive.Enabled = false;
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
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询明细数据
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                WhsInCommandDetail();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                commandNo = teCommandNo.Text.Trim();
                if (!DevCommon.ValidateDate(Convert.ToDateTime(deStartDate.EditValue), Convert.ToDateTime(deEndDate.EditValue)))
                {
                    return;
                }
                startDate = Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                endDate = (Convert.ToDateTime(deEndDate.EditValue).AddDays(1)).ToString("yyyy-MM-dd HH:mm:ss");
                status = string.IsNullOrEmpty(cboStatus.Text.ToString()) ? "" : cboStatus.EditValue.ToString();
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询明细事件
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                WhsInCommandDetail();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 链接入库页面按钮事件
        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (gvWhsIn.RowCount > 0)
            {
                string status = gvWhsIn.GetFocusedRowCellValue(columnStatus) == null ? "" : gvWhsIn.GetFocusedRowCellValue(columnStatus).ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    if (!gvWhsIn.GetFocusedRowCellValue(columnStatus).ToString().Equals(Convert.ToInt32(BusinessStatus.CLEARED).ToString()))
                    {
                        //单号
                        string receiveOrder = gvWhsIn.GetFocusedRowCellValue(columnDocId).ToString();
                        WarehouseInHeaderDetailCommand header = whsInCommand.FirstOrDefault(p => p.docId == receiveOrder);
                        WarehouseIn warehouseIn = new WarehouseIn();
                        warehouseIn.headerCommand = header;
                        warehouseIn.m_frm = m_frm;
                        warehouseIn.order = this;
                        warehouseIn.Location = new Point(0, 0);
                        warehouseIn.TopLevel = false;
                        warehouseIn.TopMost = false;
                        warehouseIn.ControlBox = false;
                        warehouseIn.FormBorderStyle = FormBorderStyle.None;
                        warehouseIn.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(warehouseIn);
                        ((XtraTabPage)this.Parent).Text = "入库";
                        warehouseIn.Show();
                        warehouseIn.BringToFront();
                        m_frm.PromptInformation("");
                    }
                }
            }
        }
        #endregion

        #region 获得状态数据
        public void SearchStatus()
        {
            try
            {
                DataTable dt = DevCommon.GetAllBusinessStatus();
                if (dt != null)
                {
                    DevCommon.initCmb(cboStatus, dt, true);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
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
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得命令数据
        public void Data()
        {
            try
            {
                var searchCondition = new
                {
                    startDate = startDate,
                    endDate = endDate,
                    docId = commandNo,
                    status = status,
                    orderType=MoveType.warehouseIn,
                    productStoreId = LoginInfo.ProductStoreId
                };
                whsInCommand = null;
                if (DevCommon.getDataByWebService("WhsInCommand", "WhsInCommand", searchCondition, ref whsInCommand) == RetCode.OK)
                {
                    gcWhsIn.DataSource = whsInCommand;
                }
                else
                {
                    gcWhsIn.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 入库指令明细查询方法
        public void WhsInCommandDetail()
        {
            try
            {
                if (gvWhsIn.RowCount > 0)
                {
                    //单号
                    string whsInOrder = gvWhsIn.GetFocusedRowCellValue(columnDocId).ToString();
                    WarehouseInHeaderDetailCommand header = whsInCommand.FirstOrDefault(p => p.docId == whsInOrder);
                    WarehouseInOrderDetial whsIn = new WarehouseInOrderDetial();
                    whsIn.m_frm = m_frm;
                    whsIn.commandHeader = header;
                    whsIn.order = this;
                    whsIn.Location = new Point(0, 0);
                    whsIn.TopLevel = false;
                    whsIn.TopMost = false;
                    whsIn.ControlBox = false;
                    whsIn.FormBorderStyle = FormBorderStyle.None;
                    whsIn.Dock = DockStyle.Fill;
                    this.Visible = false;
                    ((XtraTabPage)this.Parent).Controls.Add(whsIn);
                    ((XtraTabPage)this.Parent).Text = "入库指令明细";
                    whsIn.Show();
                    whsIn.BringToFront();
                    m_frm.PromptInformation("");
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 重新加载数据
        public void Reload()
        {
            try
            {
                int rowhandel = gvWhsIn.FocusedRowHandle;
                Data();
                gvWhsIn.FocusedRowHandle = rowhandel;
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion
    }
}