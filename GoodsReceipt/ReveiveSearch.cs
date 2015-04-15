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

namespace GoodsReceipt
{
    public partial class ReveiveSearch : BaseForm
    {
        #region 参数
        //查询天数间隔
        int day = 0;
        //开始时间
        string startDate = null;
        //结束时间
        string endDate = null;
        //发货单号
        string docId = null;
        //指令单号
        string baseEntry = null;
        //发货门店
        string productStoreIdFrom = null;
        //单据状态
        string docStatus = null;
        //收货单列表数据
        List<ReceiptHeaderDetail> receiveHeader = null;
        #endregion

        #region 构造
        public ReveiveSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void ReveiveSearch_Load(object sender, EventArgs e)
        {
            try
            {
                //列表状态数据
                AddStatus();
                //获得状态数据
                GetStatusData();
                day = DevCommon.GetDay();
                //开始时间
                deStartDate.Text = DateTime.Now.AddDays(-day).ToString("yyyy-MM-dd");
                startDate = Convert.ToDateTime(deStartDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                //结束时间
                deEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = Convert.ToDateTime(deEndDate.Text).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                //收货单数据
                //Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_frm.PromptInformation("");
            m_frm.closeTab();
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
                //开始时间
                startDate = string.IsNullOrEmpty(deStartDate.Text) ? "" : Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                //开始时间
                endDate = string.IsNullOrEmpty(deEndDate.Text) ? "" : Convert.ToDateTime(deEndDate.EditValue).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                //收货单号
                docId = teReceiveNo.Text.Trim();
                //发货门店
                productStoreIdFrom = btnReceiveStore.Value;
                //指令单号
                baseEntry = teOrderNO.Text.Trim();
                //单据状态
                docStatus = cboStatus.EditValue.ToString();
                //加载数据
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮删除草稿数据事件
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceiveSearch.RowCount > 0)
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

        #region 右键菜单
        private void gvReceiveSearch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(gvReceiveSearch.GetFocusedRowCellValue(columnDocStatus)) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvReceiveSearch.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 点击按钮进入收货明细事件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            ReceiveDetail();
        }
        #endregion

        #region 点击按钮进入收货指令事件
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            if (gvReceiveSearch.RowCount > 0)
            {
                ReveiveCommand();
            }
        }
        #endregion

        #region 获得状态数据
        public void GetStatusData()
        {
            try
            {
                DataTable dt = DevCommon.GetAllOrderStatus();
                if (dt != null)
                {
                    DevCommon.initCmb(cboStatus, dt, true);
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

        #region 收货单详细事件
        private void btnReceiveDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceiveSearch.RowCount > 0)
                {
                    ReceiveDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 收货指令查询事件
        private void btnSearchCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceiveSearch.RowCount > 0)
                {
                    ReveiveCommand();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 收货单数据
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
                   productStoreIdFrom = productStoreIdFrom,
                   docStatus = docStatus,
                   productStoreId = LoginInfo.ProductStoreId,
                   orderType = MoveType.goodsReceipt
               };
                receiveHeader = null;
                if (DevCommon.getDataByWebService("GoodsReceive", "GoodsReceive", searchConditions, ref receiveHeader) == RetCode.OK)
                {
                    gcReceive.DataSource = receiveHeader;
                }
                else
                {
                    gcReceive.DataSource = null;
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
                if (gvReceiveSearch.RowCount > 0)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvReceiveSearch.GetFocusedRowCellValue(columnDocStatus));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        //发货单号
                        string docId = gvReceiveSearch.GetFocusedRowCellValue(columnDocId).ToString();
                        var searchConditions = new
                        {
                            docId = docId
                        };
                        string result = null;
                        if (DevCommon.getDataByWebService("DeleteReceiveDraft", "DeleteReceiveDraft", searchConditions, ref result) == RetCode.OK)
                        {
                            gvReceiveSearch.DeleteSelectedRows();
                            m_frm.PromptInformation("删除成功！");
                        }
                        else
                        {
                            m_frm.PromptInformation("删除失败！");
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

        #region 收货明细方法
        public void ReceiveDetail()
        {
            try
            {
                if (gvReceiveSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvReceiveSearch.GetFocusedRowCellValue(columnDocId).ToString();
                    ReceiptHeaderDetail headerItem = receiveHeader.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        Receive delivery = new Receive();
                        delivery.m_frm = m_frm;
                        delivery.search = this;
                        delivery.headerItem = headerItem;
                        delivery.Location = new Point(0, 0);
                        delivery.TopLevel = false;
                        delivery.TopMost = false;
                        delivery.ControlBox = false;
                        delivery.FormBorderStyle = FormBorderStyle.None;
                        delivery.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(delivery);
                        ((XtraTabPage)this.Parent).Text = "调拨收货";
                        delivery.Show();
                        delivery.BringToFront();
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

        #region 指令单查询方法
        public void ReveiveCommand()
        {
            try
            {
                if (gvReceiveSearch.RowCount > 0)
                {
                    //单号
                    string commandNo = gvReceiveSearch.GetFocusedRowCellValue(columnCommand) == null ? "" : gvReceiveSearch.GetFocusedRowCellValue(columnCommand).ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        string status = gvReceiveSearch.GetFocusedRowCellValue(columnDocStatus).ToString();
                        ReceiptHeaderDetail headerItem = receiveHeader.FirstOrDefault(p => p.baseEntry == commandNo);
                        ReceiveOrder order = new ReceiveOrder();
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
                        ((XtraTabPage)this.Parent).Text = "调拨收货指令单";
                        order.Show();
                        order.BringToFront();
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

        #region 选择门店
        private void btnReceiveStore_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(btnReceiveStore.Text.Trim()))
                    {
                        btnReceiveStore.ShowForm();
                    }
                    else
                    {
                        btnReceiveStore.Text = "";
                        btnReceiveStore.Value = "";
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