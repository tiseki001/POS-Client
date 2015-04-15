using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model.Stock;
using Commons.XML;
using Newtonsoft.Json;
using System.Collections;
using Commons.WebService;
using Commons.Model;
using DevExpress.XtraTab;
using DevExpress.Utils.Menu;
using System.Linq;
using Print;

namespace GoodsDelivery
{
    public partial class DeliverySearch : BaseForm
    {
        #region 参数
        string startDate = null;
        string endDate = null;
        string docStatus = null;
        string docId = null;
        string receiveStore = null;
        string baseEntry = null;
        //发货数据
        List<DeliveryHeaderDetail> deliveryHeaderList = null;
        #endregion

        #region 构造
        public DeliverySearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!DevCommon.ValidateDate(Convert.ToDateTime(deStartDate.EditValue), Convert.ToDateTime(deEndDate.EditValue)))
            {
                return;
            }
            startDate = Convert.ToDateTime(deStartDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
            endDate = Convert.ToDateTime(deEndDate.Text).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            baseEntry = teCommandNo.Text.Trim();
            docId = teDeliveryNo.Text.Trim();
            docStatus = string.IsNullOrEmpty(cboStatus.Text) ? "" : cboStatus.EditValue.ToString();
            receiveStore = beReveiceStore.Text.Trim();
            Data();
        }
        #endregion

        #region 加载事件
        private void DeliverySearch_Load(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_frm.closeTab();
        }
        #endregion

        #region 发货明细查询
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                DeliveryData();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region  指令信息查询
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                CommandSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 获得状态数据
        public void GetStatusData()
        {
            DataTable dt = DevCommon.GetAllOrderStatus();
            if (dt != null)
            {
                DevCommon.initCmb(cboStatus, dt, true);
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
                   productStoreIdTo = receiveStore,
                   docStatus = docStatus,
                   productStoreId = LoginInfo.ProductStoreId,
                   orderType = MoveType.goodsDelivery
               };
                deliveryHeaderList = null;
                if (DevCommon.getDataByWebService("GoodsDelivery", "GoodsDeliveryHeader", searchConditions, ref deliveryHeaderList) == RetCode.OK)
                {
                    gcDeliverySearch.DataSource = deliveryHeaderList;
                }
                else
                {
                    gcDeliverySearch.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region 点击按钮删除草稿事件
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            if (gvDeliverySearch.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 删除发货单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvDeliverySearch.RowCount > 0)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvDeliverySearch.GetFocusedRowCellValue("docStatus"));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        //发货单号
                        string docId = gvDeliverySearch.GetFocusedRowCellValue("docId").ToString();
                        var searchConditions = new
                        {
                            docId = docId
                        };
                        string result = null;
                        if (DevCommon.getDataByWebService("GoodsDeliveryDelete", "GoodsDeliveryDelete", searchConditions, ref result) == RetCode.OK)
                        {
                            gvDeliverySearch.DeleteSelectedRows();
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
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 右键菜单
        private void gvDeliverySearch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                //单据状态
                int docStatus = Convert.ToInt32(gvDeliverySearch.GetFocusedRowCellValue("docStatus"));
                if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }

        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            DeleteDraft();
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

        #region 发货明细
        private void btnDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDeliverySearch.RowCount > 0)
                {
                    DeliveryData();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 发货明细方法
        public void DeliveryData()
        {
            try
            {
                if (gvDeliverySearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvDeliverySearch.GetFocusedRowCellValue("docId").ToString();
                    DeliveryHeaderDetail headerItem = deliveryHeaderList.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        Delivery delivery = new Delivery();
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
                        ((XtraTabPage)this.Parent).Text = "调拨发货";
                        delivery.Show();
                        delivery.BringToFront();
                        m_frm.PromptInformation("");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 指令单查询
        private void btnCommandSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDeliverySearch.RowCount > 0)
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
                if (gvDeliverySearch.RowCount > 0)
                {
                    //单号
                    string commandNo = gvDeliverySearch.GetFocusedRowCellValue("baseEntry") == null ? "" : gvDeliverySearch.GetFocusedRowCellValue("baseEntry").ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        string status = gvDeliverySearch.GetFocusedRowCellValue("status").ToString();
                        DeliveryHeaderDetail headerItem = deliveryHeaderList.FirstOrDefault(p => p.baseEntry == commandNo);
                        DeliveryOrder order = new DeliveryOrder();
                        order.m_frm = m_frm;
                        order.headerDetailOrder = headerItem;
                        order.search = this;
                        order.Location = new Point(0, 0);
                        order.TopLevel = false;
                        order.TopMost = false;
                        order.ControlBox = false;
                        order.FormBorderStyle = FormBorderStyle.None;
                        order.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(order);
                        ((XtraTabPage)this.Parent).Text = "调拨发货指令单";
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

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDeliverySearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvDeliverySearch.GetFocusedRowCellValue("docId").ToString();
                    //状态
                    string status = gvDeliverySearch.GetFocusedRowCellValue("docStatus").ToString();
                    if (status.Equals(Convert.ToInt32(DocStatus.VALID).ToString()))
                    {
                        //打印并修改打印次数
                        PrintAndUpdatePrintNum(docId);
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 打印并修改打印次数
        private void PrintAndUpdatePrintNum(string docId)
        {
            try
            {
                RequisitionPrint print = new RequisitionPrint(docId, true);
                print.ShowDialog();
                var searchCondition = new
                {
                    docId = docId
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateDeliveryPrintNum", "UpdateDeliveryPrintNum", searchCondition, ref result) != RetCode.OK)
                {
                    m_frm.PromptInformation("打印失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}