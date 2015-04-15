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

namespace GoodsReceipt
{
    public partial class ReceiveOrder : BaseForm
    {
        #region 参数
        public ReceiptHeaderDetail headerItem = null;
        public ReveiveSearch search = null;
        string startDate = null;
        string endDate = null;
        string commandNo = null;
        string status = null;
        int day = 0;
        List<ReceiptHeaderCommandDetail> commandHeader = null;
        #endregion

        #region 构造
        public ReceiveOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "收货单查询";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 收货指令明细查询
        private void repositoryItemHyperLinkEdit1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ReceiveCommandDetail();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void ReceiveOrder_Load(object sender, EventArgs e)
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
                    brnReceive.Enabled = false;
                    teCommandNo.Text = headerItem.baseEntry;
                    commandNo = headerItem.baseEntry;
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

        #region 点击按钮进入调拨收货明细事件
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (gvReceive.RowCount > 0)
            {
                ReceiveCommandDetail();
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
                    orderType = MoveType.goodsReceipt,
                    productStoreId=LoginInfo.ProductStoreId
                };
                commandHeader = null;
                if (DevCommon.getDataByWebService("GoodsReceiveCommand", "GoodsReceiveCommand", searchCondition, ref commandHeader) == RetCode.OK)
                {
                    gcReceive.DataSource = commandHeader;
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

        #region 调拨收货指令明细查询方法
        public void ReceiveCommandDetail()
        {
            try
            {
                //单号
                string receiveOrder = gvReceive.GetFocusedRowCellValue(columnDocId).ToString();
                ReceiptHeaderCommandDetail headerItem = commandHeader.FirstOrDefault(p => p.docId == receiveOrder);
                ReceiveOrderDetial detail = new ReceiveOrderDetial();
                detail.m_frm = m_frm;
                detail.headerItem = headerItem;
                detail.order = this;
                detail.Location = new Point(0, 0);
                detail.TopLevel = false;
                detail.TopMost = false;
                detail.ControlBox = false;
                detail.FormBorderStyle = FormBorderStyle.None;
                detail.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(detail);
                ((XtraTabPage)this.Parent).Text = "调拨收货指令单明细";
                detail.Show();
                detail.BringToFront();
                m_frm.PromptInformation(""); 
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮进入收货事件
        private void brnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                string status = gvReceive.GetFocusedRowCellValue("status") == null ? "" : gvReceive.GetFocusedRowCellValue("status").ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    if (Convert.ToInt32(gvReceive.GetFocusedRowCellValue("status")) != Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        //单号
                        string receiveOrder = gvReceive.GetFocusedRowCellValue(columnDocId).ToString();
                        ReceiptHeaderCommandDetail headerItem = commandHeader.FirstOrDefault(p => p.docId == receiveOrder);
                        Receive receive = new Receive();
                        receive.commandHeader = headerItem;
                        receive.m_frm = m_frm;
                        receive.order = this;
                        receive.Location = new Point(0, 0);
                        receive.TopLevel = false;
                        receive.TopMost = false;
                        receive.ControlBox = false;
                        receive.FormBorderStyle = FormBorderStyle.None;
                        receive.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(receive);
                        ((XtraTabPage)this.Parent).Text = "调拨收货";
                        receive.Show();
                        receive.BringToFront();
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

        #region 重新加载数据
        public void Reload()
        {
            try
            {
                int rowhandel = gvReceive.FocusedRowHandle;
                Data();
                gvReceive.FocusedRowHandle = rowhandel;
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }   
        #endregion
    }
}