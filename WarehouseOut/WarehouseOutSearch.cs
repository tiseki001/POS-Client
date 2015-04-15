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
using System.Linq;
using DevExpress.XtraTab;
using Commons.Model;
using Print;

namespace WarehouseOut
{
    public partial class WarehouseOutSearch : BaseForm
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
        List<WarehouseOutHeaderDetail> whsOutHeader = null;
        #endregion

        #region 构造
        public WarehouseOutSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void WarehouseOutSearch_Load(object sender, EventArgs e)
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

        #region 点击按钮删除草稿事件
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteDraft();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 右键菜单
        private void gvWhsInSearch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(gvWhsOutSearch.GetFocusedRowCellValue(columnDocStatus)) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvWhsOutSearch.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 删除出库单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    //单据状态
                    int docStatus = Convert.ToInt32(gvWhsOutSearch.GetFocusedRowCellValue(columnDocStatus));
                    //出库单号
                    string docId = gvWhsOutSearch.GetFocusedRowCellValue(columnDocId).ToString();
                    var searchConditions = new
                    {
                        docId = docId
                    };
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {

                        string result = null;
                        if (DevCommon.getDataByWebService("DeleteWhsOutDraft", "DeleteWhsOutDraft", searchConditions, ref result) == RetCode.OK)
                        {
                            gvWhsOutSearch.DeleteSelectedRows();
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

        #region 点击按钮查询指令单
        private void btnSearchCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    WhsOutCommand();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 指令单查询方法
        public void WhsOutCommand()
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    //单号
                    string commandNo = gvWhsOutSearch.GetFocusedRowCellValue(columnCommand) == null ? "" : gvWhsOutSearch.GetFocusedRowCellValue(columnCommand).ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        string status = gvWhsOutSearch.GetFocusedRowCellValue(columnDocStatus).ToString();
                        WarehouseOutHeaderDetail headerItem = whsOutHeader.FirstOrDefault(p => p.baseEntry == commandNo);
                        WarehouseOutOrder order = new WarehouseOutOrder();
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
                        ((XtraTabPage)this.Parent).Text = "出库指令单";
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

        #region 点击行明细进入出库详细
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            WhsOutDetail();
        }
        #endregion

        #region 点击行明细进入指令页面
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                WhsOutCommand();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮进入出库详细
        private void btnWhsOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    WhsOutDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 出库明细方法
        private void WhsOutDetail()
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvWhsOutSearch.GetFocusedRowCellValue(columnDocId).ToString();
                    WarehouseOutHeaderDetail headerItem = whsOutHeader.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        WarehouseOut whsIn = new WarehouseOut();
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
                        ((XtraTabPage)this.Parent).Text = "出库";
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
                //入库单号
                docId = teWhsIn.Text.Trim();
                //指令单号
                baseEntry = teCommandNo.Text.Trim();
                //状态
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

        #region 入库单数据
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
                    orderType = MoveType.warehouseOut
                };
                whsOutHeader = null;
                if (DevCommon.getDataByWebService("GoodsWhsOutHeader", "GoodsWhsOutHeader", searchConditions, ref whsOutHeader) == RetCode.OK)
                {
                    gcWhOutSearch.DataSource = whsOutHeader;
                }
                else
                {
                    gcWhOutSearch.DataSource = null;
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

        #region 打印事件
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOutSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvWhsOutSearch.GetFocusedRowCellValue("docId").ToString();
                    //状态
                    string status = gvWhsOutSearch.GetFocusedRowCellValue("docStatus").ToString();
                    if (status.Equals(Convert.ToInt32(DocStatus.VALID).ToString()))
                    {
                        //打印并修改打印次数
                        printAndUpdatePrintNum(docId);
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
        private void printAndUpdatePrintNum(string docId)
        {
            try
            {
                OtherOutStorePrint print = new OtherOutStorePrint(docId, true);
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