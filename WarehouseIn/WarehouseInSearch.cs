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
using Print;

namespace WarehouseIn
{
    public partial class WarehouseInSearch : BaseForm
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
        //单据状态
        string docStatus = null;
        //入库单列表数据
        List<WarehouseInHeaderDetail> whsInHeader = null;
        #endregion

        #region 构造
        public WarehouseInSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_frm.closeTab();
        }
        #endregion

        #region 加载事件
        private void WarehouseInSearch_Load(object sender, EventArgs e)
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

        #region 查询入库明细事件
        private void btnWhsIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    WhsInDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询入库指令事件
        private void btnSearchCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    WhsInCommand();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 删除草稿事件
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
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
        private void gvWhsInSearch_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(gvWhsInSearch.GetFocusedRowCellValue(columnDocStatus)) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvWhsInSearch.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 删除入库单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    //单据状态
                    int docStatus = gvWhsInSearch.GetFocusedRowCellValue(columnDocStatus) == null ? 0 : Convert.ToInt32(gvWhsInSearch.GetFocusedRowCellValue(columnDocStatus));
                    if (docStatus == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        //入库单号
                        string docId = gvWhsInSearch.GetFocusedRowCellValue(columnDocId).ToString();
                        var searchConditions = new
                        {
                            docId = docId
                        };
                        string result = null;
                        if (DevCommon.getDataByWebService("DeleteWhsInDraft", "DeleteWhsInDraft", searchConditions, ref result) == RetCode.OK)
                        {
                            gvWhsInSearch.DeleteSelectedRows();
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
                    orderType = MoveType.warehouseIn
                };
                whsInHeader = null;
                if (DevCommon.getDataByWebService("GoodsWhsInHeader", "GoodsWhsInHeader", searchConditions, ref whsInHeader) == RetCode.OK)
                {
                    gcWhsInSearch.DataSource = whsInHeader;
                }
                else
                {
                    gcWhsInSearch.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 指令单查询方法
        public void WhsInCommand()
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    //单号
                    string commandNo = gvWhsInSearch.GetFocusedRowCellValue(columnCommand) == null ? "" : gvWhsInSearch.GetFocusedRowCellValue(columnCommand).ToString();
                    if (!string.IsNullOrEmpty(commandNo))
                    {
                        string status = gvWhsInSearch.GetFocusedRowCellValue(columnDocStatus).ToString();
                        WarehouseInHeaderDetail headerItem = whsInHeader.FirstOrDefault(p => p.baseEntry == commandNo);
                        WarehouseInOrder order = new WarehouseInOrder();
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
                        ((XtraTabPage)this.Parent).Text = "入库指令单";
                        order.Show();
                        order.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 行明细进入入库指令单事件
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                WhsInCommand();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 行明细进入入库明细页面事件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            WhsInDetail();
        }
        #endregion

        #region 进入入库明细方法
        private void WhsInDetail()
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvWhsInSearch.GetFocusedRowCellValue(columnDocId).ToString();
                    WarehouseInHeaderDetail headerItem = whsInHeader.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        WarehouseIn whsIn = new WarehouseIn();
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
                        ((XtraTabPage)this.Parent).Text = "入库";
                        whsIn.Show();
                        whsIn.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsInSearch.RowCount > 0)
                {
                    //发货单号
                    string docId = gvWhsInSearch.GetFocusedRowCellValue("docId").ToString();
                    //状态
                    string status = gvWhsInSearch.GetFocusedRowCellValue("docStatus").ToString();
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
                OtherInStorePrint print = new OtherInStorePrint(docId, true);
                print.ShowDialog();
                var searchCondition = new
                {
                    docId = docId
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateReceivePrintNum", "UpdateReceivePrintNum", searchCondition, ref result) != RetCode.OK)
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