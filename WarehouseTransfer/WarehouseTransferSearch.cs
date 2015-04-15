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
using Commons.Model.Stock;
using Commons.Model;
using System.Linq;
using DevExpress.XtraTab;
using Newtonsoft.Json;

namespace WarehouseTransfer
{
    public partial class WarehouseTransferSearch : BaseForm
    {
        #region 参数
        //查询天数间隔
        int day = 0;
        //开始时间
        string startDate = null;
        //结束时间
        string endDate = null;
        //出库单号
        string docId = null;
        //指令单号
        string baseEntry = null;
        //单据状态
        string docStatus = null;
        //制单人
        string updateUserId = null;
        List<TransferHeaderDetail> transferHeader = null;
        #endregion

        #region 构造
        public WarehouseTransferSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_frm.closeTab();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 删除草稿
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
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

        #region 加载事件
        private void WarehouseTransferSearch_Load(object sender, EventArgs e)
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
                //人员信息
                //PersonData();
                //收货单数据
                //Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮查询指令信息事件
        private void btnCommandItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    TransferCommand();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮查询移库单明细事件
        private void btnTransferDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    TransferDetail();
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
                if (!DevCommon.ValidateDate(Convert.ToDateTime(deStartDate.EditValue), Convert.ToDateTime(deEndDate.EditValue)))
                {
                    return;
                }
                //开始时间
                startDate = string.IsNullOrEmpty(deStartDate.Text) ? "" : Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                //开始时间
                endDate = string.IsNullOrEmpty(deEndDate.Text) ? "" : Convert.ToDateTime(deEndDate.EditValue).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                //移库单号
                docId = teDocId.Text.Trim();
                //指令单号
                baseEntry = teCommandNo.Text.Trim();
                //状态
                docStatus = string.IsNullOrEmpty(cboDocStatus.Text.Trim()) ? "" : cboDocStatus.EditValue.ToString();
                //制单人
                updateUserId = string.IsNullOrEmpty(cboUserName.Text.Trim()) ? "" : cboUserName.EditValue.ToString();
                //加载数据
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 右键菜单
        private void gvTransferSearch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                string docStatus = gvTransferSearch.GetFocusedRowCellValue(columnDocStatus) == null ? "0" : gvTransferSearch.GetFocusedRowCellValue(columnDocStatus).ToString();
                if (!string.IsNullOrEmpty(docStatus))
                {
                    if (Convert.ToInt32(docStatus) == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        e.Menu.Items.Insert(0, item);
                    }
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvTransferSearch.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 删除移库单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvTransferSearch.GetFocusedRowCellValue(columnDocStatus));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        //出库单号
                        string docId = gvTransferSearch.GetFocusedRowCellValue(columnDocId).ToString();
                        var searchConditions = new
                        {
                            docId = docId
                        };
                        string result = null;
                        if (DevCommon.getDataByWebService("DeleteWhsOutDraft", "DeleteWhsOutDraft", searchConditions, ref result) == RetCode.OK)
                        {
                            gvTransferSearch.DeleteSelectedRows();
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

        #region 获得状态数据
        public void GetStatusData()
        {
            try
            {
                DataTable dt = DevCommon.GetAllOrderStatus();
                if (dt != null)
                {
                    DevCommon.initCmb(cboDocStatus, dt, true);
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

        #region 移库单数据
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
                    productStoreId = LoginInfo.ProductStoreId,
                    orderType = MoveType.warehouseTransfer//,
                    //updateUserId=updateUserId
                };
                transferHeader = null;
                if (DevCommon.getDataByWebService("TransferHeader", "TransferHeader", searchConditions, ref transferHeader) == RetCode.OK)
                {
                    gcTransferSearch.DataSource = transferHeader;
                }
                else
                {
                    gcTransferSearch.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 指令单查询方法
        private void TransferCommand()
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    //单号
                    string commandNo = gvTransferSearch.GetFocusedRowCellValue(columnCommand) == null ? "" : gvTransferSearch.GetFocusedRowCellValue(columnCommand).ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        TransferHeaderDetail headerItem = transferHeader.FirstOrDefault(p => p.baseEntry == commandNo);
                        WarehouseTransferOrder order = new WarehouseTransferOrder();
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
                        ((XtraTabPage)this.Parent).Text = "移库指令单";
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

        #region 点击行明细指令单号进入指令单查询页面事件
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    TransferCommand();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 移库明细方法
        private void TransferDetail()
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvTransferSearch.GetFocusedRowCellValue(columnDocId).ToString();
                    TransferHeaderDetail headerItem = transferHeader.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        WarehouseTransfer whsIn = new WarehouseTransfer();
                        whsIn.m_frm = m_frm;
                        whsIn.search = this;
                        whsIn.headerItem = headerItem;
                        whsIn.Location = new Point(0, 0);
                        whsIn.TopLevel = false;
                        whsIn.TopMost = false;
                        whsIn.ControlBox = false;
                        whsIn.FormBorderStyle = FormBorderStyle.None;
                        whsIn.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(whsIn);
                        ((XtraTabPage)this.Parent).Text = "移库";
                        whsIn.Show();
                        whsIn.BringToFront();
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

        #region 点击行移库单号进入移库单页面事件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransferSearch.RowCount > 0)
                {
                    TransferDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 本门店人员信息
        public void PersonData()
        {
            try
            {
                var searchConditions = new
                {
                    productStoreId = LoginInfo.ProductStoreId
                };
                object obj = null;
                if (DevCommon.getDataByWebService("StorePerson", "StorePerson", searchConditions, ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                        dt.Columns.Add("userName", typeof(string));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["userName"] = dt.Rows[i]["lastName"].ToString() + dt.Rows[i]["middleName"].ToString() + dt.Rows[i]["firstName"].ToString();
                        }
                        DevCommon.initCmb(cboUserName, dt, "partyId", "userName", true);
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