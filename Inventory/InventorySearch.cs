using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using DevExpress.Utils.Menu;
using Commons.Model;
using Commons.Model.Stock;
using System.Linq;
using DevExpress.XtraTab;

namespace Inventory
{
    public partial class InventorySearch : BaseForm
    {
        #region 参数
        string startDate = null;
        string endDate = null;
        string docId = null;
        string baseEntry = null;
        string docStatus = null;
        List<InventotyDetailHeader> header = null;
        #endregion

        #region 构造
        public InventorySearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void InventorySearch_Load(object sender, EventArgs e)
        {
            //列表状态数据
            AddStatus();
            ////获得状态数据
            GetStatusData();
            //查询事件间隔
            int day = 0;
            day = DevCommon.GetDay();
            //开始时间
            deStartDate.Text = DateTime.Now.AddDays(-day).ToString("yyyy-MM-dd");
            startDate = Convert.ToDateTime(deStartDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
            //结束时间
            deEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            endDate = Convert.ToDateTime(deEndDate.Text).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            //获得发货单数据
            //Data();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_frm.closeTab();
        }
        #endregion

        #region 删除草稿
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    DeleteDraft();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 删除发货单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvInventory.GetFocusedRowCellValue("docStatus"));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        //发货单号
                        string docId = gvInventory.GetFocusedRowCellValue("docId").ToString();
                        var searchConditions = new
                        {
                            docId = docId
                        };
                        string result = null;
                        if (DevCommon.getDataByWebService("InventoryDraftDelete", "InventoryDraftDelete", searchConditions, ref result) == RetCode.OK)
                        {
                            gvInventory.DeleteSelectedRows();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 右键菜单
        private void gvInventory_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DXMenuItem item = new DXMenuItem(" 删除");
                item.Click += new EventHandler(item_Click);
                if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvInventory.GetFocusedRowCellValue("docStatus"));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        e.Menu.Items.Insert(0, item);
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }

        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    DeleteDraft();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 指令查询
        private void btnCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    CommandSearch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 指令单查询
        public void CommandSearch()
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    //单号
                    string commandNo = gvInventory.GetFocusedRowCellValue("baseEntry") == null ? "" : gvInventory.GetFocusedRowCellValue("baseEntry").ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        InventotyDetailHeader headerItem = header.FirstOrDefault(p => p.baseEntry == commandNo);
                        InventoryOrder order = new InventoryOrder();
                        order.m_frm = m_frm;
                        order.headerItem = headerItem;
                        order.search = this;
                        order.Location = new Point(0, 0);
                        order.TopLevel = false;
                        order.TopMost = false;
                        order.ControlBox = false;
                        order.FormBorderStyle = FormBorderStyle.None;
                        order.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(order);
                        ((XtraTabPage)this.Parent).Text = "盘点指令";
                        order.Show();
                        order.BringToFront();
                        m_frm.PromptInformation("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 盘点明细
        private void btnInventory_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    InventoryData();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 发货明细方法
        public void InventoryData()
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    //发货单号
                    string docId = gvInventory.GetFocusedRowCellValue("docId").ToString();
                    InventotyDetailHeader headerItem = header.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        Inventory inventory = new Inventory();
                        inventory.m_frm = m_frm;
                        inventory.search = this;
                        inventory.searchHeader = headerItem;
                        inventory.Location = new Point(0, 0);
                        inventory.TopLevel = false;
                        inventory.TopMost = false;
                        inventory.ControlBox = false;
                        inventory.FormBorderStyle = FormBorderStyle.None;
                        inventory.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(inventory);
                        ((XtraTabPage)this.Parent).Text = "盘点";
                        inventory.Show();
                        inventory.BringToFront();
                        m_frm.PromptInformation("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 添加列表状态数据
        public void AddStatus()
        {
            try
            {
                DataTable status = DevCommon.GetAllOrderStatus();
                if (status != null)
                {
                    DevCommon.initCmb(repositoryItemImageComboBox1, status, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得状态数据
        public void GetStatusData()
        {
            DataTable dt = DevCommon.GetAllOrderStatus();
            if (dt != null)
            {
                DevCommon.initCmb(cboDocStatus, dt, true);
            }
        }
        #endregion

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DevCommon.ValidateDate(Convert.ToDateTime(deStartDate.EditValue), Convert.ToDateTime(deEndDate.EditValue)))
                {
                    return;
                }
                startDate = Convert.ToDateTime(deStartDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                endDate = Convert.ToDateTime(deEndDate.Text).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                baseEntry = teCommandNo.Text.Trim();
                docId = teInventoryNo.Text.Trim();
                docStatus = string.IsNullOrEmpty(cboDocStatus.Text) ? "" : cboDocStatus.EditValue.ToString();
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得发货单数据
        public void Data()
        {
            try
            {
                var searchConditions = new
                {
                    startDate = startDate,
                    endDate = endDate,
                    docId = docId,
                    baseEntry = baseEntry,
                    docStatus = docStatus,
                    productStoreId = LoginInfo.ProductStoreId
                };
                header = null;
                if (DevCommon.getDataByWebService("InventoryHeaderSearch", "InventoryHeaderSearch", searchConditions, ref header) == RetCode.OK)
                {
                    gcInventory.DataSource = header;
                }
                else
                {
                    gcInventory.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 点击行进入盘点页面
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    InventoryData();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击行进入盘点指令页面
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    CommandSearch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}