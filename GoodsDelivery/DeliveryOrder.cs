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
using Commons.XML;
using System.Collections;
using Commons.WebService;
using Newtonsoft.Json;
using Commons.Model;
using System.Linq;
using Commons.Model.SearchButton;

namespace GoodsDelivery
{
    public partial class DeliveryOrder : BaseForm
    {
        #region 参数
        //调拨发货行数据
        public DeliveryHeaderDetail headerDetailOrder = null;
        //命令头数据源
        List<DeliveryHeaderDetailCommand> commandHeaderList = null;
        public DeliverySearch search = null;
        string startDate = null;
        string endDate = null;
        string commandNo = null;
        string status = null;

        //查询条件时间间隔
        int day = 0;
        #endregion

        #region 构造
        public DeliveryOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region 查询行明细数据
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            SearchDetialData();
        }
        #endregion

        #region 加载事件
        private void DeliveryOrder_Load(object sender, EventArgs e)
        {
            try
            {
                //查询状态
                SearchStatus();
                //行明细状态
                RowStatus();
                day = DevCommon.GetDay();
                if (headerDetailOrder != null)
                {
                    btnDetail.Enabled = false;
                    teOrderNO.Text = headerDetailOrder.baseEntry;
                    cboStatus.EditValue = headerDetailOrder.status;
                    commandNo = headerDetailOrder.baseEntry;
                    btnDelivery.Enabled = false;
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
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                ((XtraTabPage)this.Parent).Text = "发货单查询";
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

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            commandNo = teOrderNO.Text.Trim();
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
                    docId = commandNo,
                    status = status,
                    orderType = MoveType.goodsDelivery,
                    productStoreId = LoginInfo.ProductStoreId
                };
                commandHeaderList = null;
                if (DevCommon.getDataByWebService("GoodsDeliveryCommand", "DeliveryCommandSearch", searchCondition, ref commandHeaderList) == RetCode.OK)
                {
                    gcGoodsDelivery.DataSource = commandHeaderList;
                }
                else
                {
                    gcGoodsDelivery.DataSource = null;
                }
                teOrderNO.Focus();
            }
            catch (Exception ex)
            {
                teOrderNO.Focus();
                MessageBox.Show(ex.Message);
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

        #region 点击按钮进入明细数据事件
        private void btnItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (gvGoodsDelivery.RowCount > 0)
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
                if (gvGoodsDelivery.RowCount > 0)
                {
                    //单号
                    string deliveryOrder = gvGoodsDelivery.GetFocusedRowCellValue("docId").ToString();
                    DeliveryHeaderDetailCommand headerItem = commandHeaderList.FirstOrDefault(p => p.docId == deliveryOrder);
                    DeliveryOrderDetial detail = new DeliveryOrderDetial();
                    detail.headerItem = headerItem;
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
                    ((XtraTabPage)this.Parent).Text = "调拨发货明细";
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

        #region 键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region 调拨发货
        private void btnDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                int status = gvGoodsDelivery.GetFocusedRowCellValue("status") == null ? 0 : Convert.ToInt32(gvGoodsDelivery.GetFocusedRowCellValue("status"));
                if (status != Convert.ToInt32(BusinessStatus.CLEARED))
                {
                    //单号
                    string deliveryOrder = gvGoodsDelivery.GetFocusedRowCellValue("docId").ToString();
                    DeliveryHeaderDetailCommand headerItem = commandHeaderList.FirstOrDefault(p => p.docId == deliveryOrder);
                    Delivery delivery = new Delivery();
                    delivery.headerCommand = headerItem;
                    delivery.m_frm = m_frm;
                    delivery.order = this;
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
                int rowhandel = gvGoodsDelivery.FocusedRowHandle;
                Data();
                gvGoodsDelivery.FocusedRowHandle = rowhandel;
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 强制已清事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvGoodsDelivery.RowCount > 0)
                {
                    int status = gvGoodsDelivery.GetFocusedRowCellValue("status") == null ? 0 : Convert.ToInt32(gvGoodsDelivery.GetFocusedRowCellValue("status"));
                    string deliveryDocId = gvGoodsDelivery.GetFocusedRowCellValue("docId").ToString();
                    if (status != Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        DialogResult dr = MessageBox.Show("是否确认强制已清此单据？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            var searchCondition = new
                            {
                                docId = deliveryDocId,
                                updateUserId = LoginInfo.PartyId
                            };
                            string result = "";
                            if (DevCommon.getDataByWebService("UpdateDeliveryCommandStatus", "UpdateDeliveryCommandStatus", searchCondition, ref result) == RetCode.OK)
                            {
                                Reload();
                                return;
                            }
                            else
                            {
                                m_frm.PromptInformation("操作失败！");
                                return;
                            }
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