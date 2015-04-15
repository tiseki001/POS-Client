using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Commons.WinForm;
using Commons.Model.Stock;
using System.Linq;
using Commons.Model;

namespace WarehouseTransfer
{
    public partial class WarehouseTransferOrder : BaseForm
    {
        #region 参数
        List<TransferHeaderDetailCommand> transferCommandHeader = null;
        public WarehouseTransferSearch search = null;
        public TransferHeaderDetail headerItem = null;
        int day = 0;
        string commandNo = null;
        string startDate = null;
        string endDate = null;
        string status = null;
        #endregion

        #region 构造
        public WarehouseTransferOrder()
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
                ((XtraTabPage)this.Parent).Text = "移库单查询";
                m_frm.PromptInformation("");
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 指令单明细数据
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    TransferCommandDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 双击选择指令单数据
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
        }
        #endregion

        #region 加载事件
        private void WarehouseTransferOrder_Load(object sender, EventArgs e)
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
                    btnTransfer.Enabled = false;
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
                    orderType = MoveType.warehouseTransfer,
                    productStoreId = LoginInfo.ProductStoreId
                };
                transferCommandHeader = null;
                if (DevCommon.getDataByWebService("TransfertCommand", "TransfertCommand", searchCondition, ref transferCommandHeader) == RetCode.OK)
                {
                    gcTransfer.DataSource = transferCommandHeader;
                }
                else
                {
                    gcTransfer.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮进入移库页面
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    string status = gvTransfer.GetFocusedRowCellValue(columnDocStatus) == null ? "" : gvTransfer.GetFocusedRowCellValue(columnDocStatus).ToString();
                    if (!string.IsNullOrEmpty(status))
                    {
                        if (!gvTransfer.GetFocusedRowCellValue(columnDocStatus).ToString().Equals(Convert.ToInt32(BusinessStatus.CLEARED).ToString()))
                        {
                            //单号
                            string outOrder = gvTransfer.GetFocusedRowCellValue(columnDocId).ToString();
                            TransferHeaderDetailCommand header = transferCommandHeader.FirstOrDefault(p => p.docId == outOrder);
                            WarehouseTransfer transfer = new WarehouseTransfer();
                            transfer.headerCommand = header;
                            transfer.m_frm = m_frm;
                            transfer.order = this;
                            transfer.Location = new Point(0, 0);
                            transfer.TopLevel = false;
                            transfer.TopMost = false;
                            transfer.ControlBox = false;
                            transfer.FormBorderStyle = FormBorderStyle.None;
                            transfer.Dock = DockStyle.Fill;
                            this.Visible = false;
                            ((XtraTabPage)this.Parent).Controls.Add(transfer);
                            ((XtraTabPage)this.Parent).Text = "移库";
                            transfer.Show();
                            transfer.BringToFront();
                            m_frm.PromptInformation("");
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

        #region 点击按钮进入移库明细页面
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    TransferCommandDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 出库指令明细查询方法
        public void TransferCommandDetail()
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    //单号
                    string whsOutOrder = gvTransfer.GetFocusedRowCellValue(columnDocId).ToString();
                    TransferHeaderDetailCommand header = transferCommandHeader.FirstOrDefault(p => p.docId == whsOutOrder);
                    WarehouseTransferOrderDetial transferHeader = new WarehouseTransferOrderDetial();
                    transferHeader.m_frm = m_frm;
                    transferHeader.commandHeader = header;
                    transferHeader.order = this;
                    transferHeader.Location = new Point(0, 0);
                    transferHeader.TopLevel = false;
                    transferHeader.TopMost = false;
                    transferHeader.ControlBox = false;
                    transferHeader.FormBorderStyle = FormBorderStyle.None;
                    transferHeader.Dock = DockStyle.Fill;
                    this.Visible = false;
                    ((XtraTabPage)this.Parent).Controls.Add(transferHeader);
                    ((XtraTabPage)this.Parent).Text = "移库指令明细";
                    transferHeader.Show();
                    transferHeader.BringToFront();
                    m_frm.PromptInformation("");
                }
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

        #region 从新加载数据
        public void Reload()
        {
            int rowhandle = gvTransfer.FocusedRowHandle;
            Data();
            gvTransfer.FocusedRowHandle = rowhandle;
        }
        #endregion
    }
}