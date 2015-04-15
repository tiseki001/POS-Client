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
using System.Linq;
using DevExpress.XtraTab;
using DevExpress.Utils.Menu;
using Commons.Model;
using Newtonsoft.Json;

namespace WarehouseReplenishment
{
    public partial class ReplenishmentSearch : BaseForm
    {

        #region 参数
        string startDate = null;
        string endDate = null;
        string docStatus = null;
        string docId = null;
        string userId = null;
        int day = 0;
        List<ReplenishmentHeaderDetail> headerList = null;
        DataTable dtPerson = new DataTable();
        #endregion

        #region 构造
        public ReplenishmentSearch()
        {
            InitializeComponent();
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
                //单号
                docId = teDocId.Text.Trim();
                //状态
                docStatus = cboDocStatus.EditValue.ToString();
                //
                userId = string.IsNullOrEmpty(cboPerson.Text.Trim()) ? null : cboPerson.EditValue.ToString();
                //加载数据
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_frm.closeTab();
        }
        #endregion

        #region 查询方法
        public void Data()
        {
            try
            {
                var searchConditions = new
                {
                    startDate = startDate,
                    endDate = endDate,
                    docId = docId,
                    docStatus = docStatus,
                    updateUserId=userId
                };
                headerList = null;
                if (DevCommon.getDataByWebService("ReplenishmentHeader", "ReplenishmentHeader", searchConditions, ref headerList) == RetCode.OK)
                {
                    gcReplenishment.DataSource = headerList;
                }
                else
                {
                    gcReplenishment.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void ReplenishmentSearch_Load(object sender, EventArgs e)
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
            PersonData();
            //收货单数据
            //Data();
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

        #region 补货明细
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvReplenishment.RowCount>0)
                {
                    ReplenishmentDetail();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 行点击进入补货明细页面
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            ReplenishmentDetail();
        }
        #endregion

        #region 进入补货明细方法
        private void ReplenishmentDetail()
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    //发货单号
                    string docId = gvReplenishment.GetFocusedRowCellValue("docId").ToString();
                    ReplenishmentHeaderDetail headerItem = headerList.FirstOrDefault(p => p.docId == docId);
                    if (headerItem != null)
                    {
                        this.Visible = false;
                        ReplenishmentApply apply = new ReplenishmentApply();
                        apply.m_frm = m_frm;
                        apply.search = this;
                        apply.headerItem = headerItem;
                        apply.Location = new Point(0, 0);
                        apply.TopLevel = false;
                        apply.TopMost = false;
                        apply.ControlBox = false;
                        apply.FormBorderStyle = FormBorderStyle.None;
                        apply.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(apply);
                        ((XtraTabPage)this.Parent).Text = "补货申请";
                        apply.Show();
                        apply.BringToFront();
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
            int rowHandel = gvReplenishment.FocusedRowHandle;
            Data();
            gvReplenishment.FocusedRowHandle = rowHandel;
        }
        #endregion

        #region 删除草稿
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
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

        #region 删除补货单草稿数据
        public void DeleteDraft()
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    //单据状态
                    string status = gvReplenishment.GetFocusedRowCellValue(columnDocStatus) == null ? "" : gvReplenishment.GetFocusedRowCellValue(columnDocStatus).ToString();
                    if (!string.IsNullOrEmpty(status))
                    {
                        if (Convert.ToInt32(gvReplenishment.GetFocusedRowCellValue(columnDocStatus)) == Convert.ToInt32(DocStatus.DRAFT))
                        {
                            //出库单号
                            string docId = gvReplenishment.GetFocusedRowCellValue(columnDocId).ToString();
                            var searchConditions = new
                            {
                                docId = docId
                            };
                            string result = null;
                            if (DevCommon.getDataByWebService("DeleteReplenishmentDraft", "DeleteReplenishmentDraft", searchConditions, ref result) == RetCode.OK)
                            {
                                gvReplenishment.DeleteSelectedRows();
                                m_frm.PromptInformation("删除成功！");
                            }
                            else
                            {
                                m_frm.PromptInformation("删除失败！");
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

        #region 右键菜单
        private void gvReplenishment_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(gvReplenishment.GetFocusedRowCellValue(columnDocStatus)) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvReplenishment.RowCount > 0)
            {
                DeleteDraft();
            }
        }
        #endregion

        #region 根据店铺编号获得人员信息
        public void PersonData()
        {
            try
            {
                var searchConditions = new
                {
                    productStoreId=LoginInfo.ProductStoreId
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
                        DevCommon.initCmb(cboPerson, dt, "partyId", "userName", true);
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