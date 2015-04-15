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

namespace WarehouseOut
{
    public partial class WarehouseOutOrder : BaseForm
    {
        #region 参数
        public WarehouseOutSearch search = null;
        List<WarehouseOutHeaderDetailCommand> whsOutCommand = null;
        public WarehouseOutHeaderDetail headerItem = null;
        string startDate = null;
        string endDate = null;
        string status = null;
        string commandNo = null;
        int day = 0;
        #endregion

        #region 构造
        public WarehouseOutOrder()
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
                ((XtraTabPage)this.Parent).Text = "出库单查询";
                m_frm.PromptInformation("");
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 获得指令明细数据

        #endregion

        #region 加载事件
        private void WarehouseOutOrder_Load(object sender, EventArgs e)
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
                    btnwhsOut.Enabled = false;
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

        #region 点击按钮进入出库页面事件
        private void btnwhsOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    string status = gvWhsOut.GetFocusedRowCellValue(columnDocStatus) == null ? "" : gvWhsOut.GetFocusedRowCellValue(columnDocStatus).ToString();
                    if (!string.IsNullOrEmpty(status))
                    {
                        if (!gvWhsOut.GetFocusedRowCellValue(columnDocStatus).ToString().Equals(Convert.ToInt32(BusinessStatus.CLEARED).ToString()))
                        {
                            //单号
                            string outOrder = gvWhsOut.GetFocusedRowCellValue(columnDocId).ToString();
                            WarehouseOutHeaderDetailCommand header = whsOutCommand.FirstOrDefault(p => p.docId == outOrder);
                            WarehouseOut warehouseIn = new WarehouseOut();
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
                            ((XtraTabPage)this.Parent).Text = "出库";
                            warehouseIn.Show();
                            warehouseIn.BringToFront();
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

        #region 点击按钮查询出库指令明细数据
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    WhsOutCommandDetail();
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
                status = string.IsNullOrEmpty(b.Text.ToString()) ? "" : b.EditValue.ToString();
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
                    orderType = MoveType.warehouseOut,
                    productStoreId = LoginInfo.ProductStoreId
                };
                whsOutCommand = null;
                if (DevCommon.getDataByWebService("WhsOutCommand", "WhsOutCommand", searchCondition, ref whsOutCommand) == RetCode.OK)
                {
                    gcWhsOut.DataSource = whsOutCommand;
                }
                else
                {
                    gcWhsOut.DataSource = null;
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
                    DevCommon.initCmb(b, dt, true);
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

        #region 出库指令明细查询方法
        public void WhsOutCommandDetail()
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    //单号
                    string whsOutOrder = gvWhsOut.GetFocusedRowCellValue(columnDocId).ToString();
                    WarehouseOutHeaderDetailCommand header = whsOutCommand.FirstOrDefault(p => p.docId == whsOutOrder);
                    WarehouseOutOrderDetial whsOut = new WarehouseOutOrderDetial();
                    whsOut.m_frm = m_frm;
                    whsOut.commandHeader = header;
                    whsOut.order = this;
                    whsOut.Location = new Point(0, 0);
                    whsOut.TopLevel = false;
                    whsOut.TopMost = false;
                    whsOut.ControlBox = false;
                    whsOut.FormBorderStyle = FormBorderStyle.None;
                    whsOut.Dock = DockStyle.Fill;
                    this.Visible = false;
                    ((XtraTabPage)this.Parent).Controls.Add(whsOut);
                    ((XtraTabPage)this.Parent).Text = "出库指令明细";
                    whsOut.Show();
                    whsOut.BringToFront();
                    m_frm.PromptInformation("");
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击行单号进入指令明细页面
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                WhsOutCommandDetail();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 重新加载
        public void Reload()
        {
            int rowHandle = gvWhsOut.FocusedRowHandle;
            Data();
            gvWhsOut.FocusedRowHandle = rowHandle;
        }
        #endregion

        #region 强制已清事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    int status = gvWhsOut.GetFocusedRowCellValue("status") == null ? 0 : Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue("status"));
                    string WhsOutDocId = gvWhsOut.GetFocusedRowCellValue("docId").ToString();
                    if (status != Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                         DialogResult dr = MessageBox.Show("是否确认强制已清此单据？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                         if (dr == DialogResult.OK)
                         {
                             var searchCondition = new
                             {
                                 docId = WhsOutDocId,
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