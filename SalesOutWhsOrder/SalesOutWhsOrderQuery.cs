using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model.Order;
using Commons.Model;
using Commons.Model.Stock;
using DevExpress.XtraTab;

namespace SalesOutWhsOrder
{
    public partial class SalesOutWhsOrderQuery : BaseForm
    {
        List<SalesOutWhsOrderHeaderModel> listSalesOutWhsOrderHeader = null;
        List<SalesOutWhsOrderDtlModel> listSalesOutWhsOrderDtl = null;
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口

        public SalesOutWhsOrderQuery(BaseMainForm formb, BaseForm formp)
        {
            FormBase = formb;
            FormParent = formp;
            InitializeComponent();
        }

        private void SalesOutWhsOrderQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //默认时间段为当前时间到前6天时间
                dateEdit_EndDate.EditValue = DateTime.Now;
                dateEdit_StartDate.EditValue = DateTime.Now.AddDays(-DevCommon.GetDay());


                DataTable dtStatus = null;
                dtStatus = DevCommon.GetAllOrderStatus();
                if (dtStatus != null)
                {
                    DevCommon.initCmb(this.baseCombobox_DocStatus, dtStatus, true);
                    DevCommon.initCmb(repositoryItemImageComboBox_Docstatus, dtStatus, false);
                }

                //dtStatus = DevCommon.GetAllBusinessStatus();
                //if (dtStatus != null)
                //{
                //    DevCommon.initCmb(repositoryItemImageComboBox_BusinessStatus, dtStatus, false);
                //}

                //初始化显示
                //simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void simpleButton_Query_Click(object sender, EventArgs e)
        {
            try
            {
                querySalesOutWhsOrderModel QSOWO = new querySalesOutWhsOrderModel();
                QSOWO.orderType = MoveType.saleOut;
                if (this.dateEdit_StartDate.EditValue != null)
                {
                    QSOWO.startDate = this.dateEdit_StartDate.DateTime.Date;
                }
                if (this.dateEdit_EndDate.EditValue != null)
                {
                    QSOWO.endDate = this.dateEdit_EndDate.DateTime.Date.AddDays(1);
                }
                if (!string.IsNullOrEmpty(this.textEdit_OrderId.Text))
                {
                    QSOWO.docId = this.textEdit_OrderId.Text;
                }
                if (!string.IsNullOrEmpty(this.textEdit_BaseEntry.Text))
                {
                    QSOWO.baseEntry = this.textEdit_BaseEntry.Text;
                }
                if (!string.IsNullOrEmpty(baseCombobox_DocStatus.Text))
                {
                    QSOWO.docStatus = this.baseCombobox_DocStatus.EditValue.ToString();
                }

                if (DevCommon.getDataByWebService("getSalesOutWhsOrderHeaderByCondition", "getSalesOutWhsOrderHeaderByCondition", QSOWO, ref listSalesOutWhsOrderHeader) == RetCode.NG)
                {
                    return;
                }
                //查询后的人名整理
                //if (listSalesOrderHeader != null)
                //{
                //    foreach (SalesOrderHeaderModel hd in listSalesOrderHeader)
                //    {
                //        hd.createUserName = hd.createUserNameL + hd.createUserNameM + hd.createUserNameF;
                //        hd.memberName = hd.memberNameL + hd.memberNameM + hd.memberNameF;
                //    }
                //}


                this.gridControl_SalesOutWhsOrder.DataSource = listSalesOutWhsOrderHeader;
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
            
        }

        private void simpleButton_Detail_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_SalesOutWhsOrder.RowCount == 0)
                {
                    return;
                }

                //取得订单头
                SalesOutWhsOrderModel WO = new SalesOutWhsOrderModel();
                WO.header = listSalesOutWhsOrderHeader[this.gridView_SalesOutWhsOrder.GetDataSourceRowIndex(this.gridView_SalesOutWhsOrder.FocusedRowHandle)];
                //取得订单明细
                queryDocIdModel QDI = new queryDocIdModel();
                QDI.docId = WO.header.docId;

                if (DevCommon.getDataByWebService("getSalesOutWhsOrderDtlById", "getSalesOutWhsOrderDtlById", QDI, ref listSalesOutWhsOrderDtl) == RetCode.NG)
                {
                    return;
                }

                WO.item = listSalesOutWhsOrderDtl;


                SalesOutWhsOrder WOForm = new SalesOutWhsOrder(FormBase, this, WO);
                WOForm.Location = new Point(0, 0);
                WOForm.TopLevel = false;
                WOForm.TopMost = false;
                WOForm.ControlBox = false;
                WOForm.FormBorderStyle = FormBorderStyle.None;
                WOForm.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(WOForm);
                WOForm.Show();
                WOForm.BringToFront();

                simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }

            

        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            if (FormParent == null)
            {
                FormBase.closeTab();
            }
            else
            {
                FormParent.Visible = true;
                FormParent.BringToFront();
                this.Close();
            }
        }

        private void simpleButton_Invalid_Click(object sender, EventArgs e)
        {
            try
            {
                //int nFocusedRow = this.gridView_SalesOrder.FocusedRowHandle;
                //String outStr = null;
                //queryDocId QDI = new queryDocId();
                //QDI.docId = listSalesOrderHeader[nFocusedRow].docId;

                //if (listSalesOrderHeader[nFocusedRow].docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                //{
                //    //删除
                //    if (DevCommon.getDataByWebService("deleteSalesOrderById", "deleteSalesOrderById", QDI, ref outStr) == RetCode.NG)
                //    {
                //        return;
                //    }
                //}
                //else if (listSalesOrderHeader[nFocusedRow].docStatus.Equals(((int)DocStatus.VALID).ToString())
                //    && listSalesOrderHeader[nFocusedRow].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                //{
                //    updateStatus US = new updateStatus();
                //    US.docStatus = ((int)DocStatus.INVALID).ToString();
                //    US.lastUpdateDocDate = DateTime.Now.ToString();
                //    US.lastUpdateUserId = LoginInfo.UserLoginId;
                //    //更新状态
                //    if (DevCommon.getDataByWebService("updateSalesOrderStatus", "updateSalesOrderStatus", US, ref outStr) == RetCode.NG)
                //    {
                //        return;
                //    }
                //}
                //else
                //{
                //    return;
                //}
                //simpleButton_Query_Click(null, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);              	
            }

        }

        private void gridViewSalesOrder_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //int nFocusedRow = this.gridView_SalesOrder.FocusedRowHandle;
            //if (this.gridView_SalesOrder.RowCount > 0
            //    && ((listSalesOrderHeader[nFocusedRow].docStatus.Equals(((int)DocStatus.DRAFT).ToString())
            //        || (listSalesOrderHeader[nFocusedRow].docStatus.Equals(((int)DocStatus.VALID).ToString())
            //        && listSalesOrderHeader[nFocusedRow].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString())))))          //草稿状态或者未付款状态可以退订
            //{
            //    this.simpleButton_Invalid.Enabled = true;
            //}
            //else
            //{
            //    this.simpleButton_Invalid.Enabled = false;
            //}
        }

        //快捷键响应
        private void KeyPreview_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Escape))
                {
                    //取消
                    this.simpleButton_Cancel.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

    }
}