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
using DevExpress.XtraTab;

namespace Payment
{
    public partial class PaymentSearch : BaseForm
    {
        List<PaymentInOrderHeaderModel> listPaymentInOrderHeader = null;
        List<PaymentInOrderDtlModel> listPaymentInOrderDtl = null;
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口

        #region 构造
        public PaymentSearch(BaseMainForm formb, BaseForm formp)
        {
            FormBase = formb;
            FormParent = formp;
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭画面
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
        #endregion

        #region 加载事件
        private void PaymentSearch_Load(object sender, EventArgs e)
        {
            try
            {
                //默认时间段为当前时间到前6天时间
                dateEdit_EndDate.EditValue = DateTime.Now;
                dateEdit_StartDate.EditValue = DateTime.Now.AddDays(-DevCommon.GetDay());

                //初始化combobox
                DataTable dtStatus = null;
                dtStatus = DevCommon.GetAllOrderStatus();
                if (dtStatus != null)
                {
                    DevCommon.initCmb(this.baseCombobox_DocStatus, dtStatus, true);
                    DevCommon.initCmb(repositoryItemImageComboBox_Docstatus, dtStatus, false);
                }

                DataTable dtDocType = null;
                dtDocType = DevCommon.GetPostingType();
                if (dtDocType != null)
                {
                    DevCommon.initCmb(repositoryItemImageComboBox_DocType, dtDocType, false);
                }

                //初始化显示
                //simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }
        #endregion

        private void simpleButton_Query_Click(object sender, EventArgs e)
        {
            try
            {
                //组织查询条件
                string strSearchCondition = "1=1";
                if (this.dateEdit_StartDate.EditValue != null)
                {
                    strSearchCondition = strSearchCondition + " and CREATE_DOC_DATE>='" + this.dateEdit_StartDate.DateTime.Date.ToString() + "'";
                }
                if (this.dateEdit_EndDate.EditValue != null)
                {
                    strSearchCondition = strSearchCondition + " and CREATE_DOC_DATE<'" + this.dateEdit_EndDate.DateTime.Date.AddDays(1).ToString() + "'";
                }
                if (!string.IsNullOrEmpty(this.textEdit_OrderId.Text))
                {
                    strSearchCondition = strSearchCondition + " and DOC_ID ='" + this.textEdit_OrderId.Text + "'";
                }
                if (!string.IsNullOrEmpty(baseCombobox_DocStatus.Text))
                {
                    strSearchCondition = strSearchCondition + " and DOC_STATUS ='" + this.baseCombobox_DocStatus.EditValue + "'";
                }

                //查询订单头
                queryConditionModel QC = new queryConditionModel();
                QC.where = strSearchCondition;

                if (DevCommon.getDataByWebService("getPaymentInOrderHeaderByCondition", "getPaymentInOrderHeaderByCondition", QC, ref listPaymentInOrderHeader) == RetCode.NG)
                {
                    return;
                }

                //数据绑定
                this.gridControl_PaymentInOrder.DataSource = listPaymentInOrderHeader;
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //明细按钮按下
        private void simpleButton_Detail_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_PaymentInOrder.RowCount == 0)
                {
                    return;
                }

                //取得订单头
                PaymentInOrderModel PIO = new PaymentInOrderModel();
                PIO.header = listPaymentInOrderHeader[this.gridView_PaymentInOrder.GetDataSourceRowIndex(this.gridView_PaymentInOrder.FocusedRowHandle)];
                //取得订单明细
                queryDocIdModel QDI = new queryDocIdModel();
                QDI.docId = PIO.header.docId;

                if (DevCommon.getDataByWebService("getPaymentInOrderDtlById", "getPaymentInOrderDtlById", QDI, ref listPaymentInOrderDtl) == RetCode.NG)
                {
                    return;
                }
                PIO.detail = listPaymentInOrderDtl;

                //转入下级窗口
                Payment PIOForm = new Payment(FormBase, this, PIO);
                PIOForm.Location = new Point(0, 0);
                PIOForm.TopLevel = false;
                PIOForm.TopMost = false;
                PIOForm.ControlBox = false;
                PIOForm.FormBorderStyle = FormBorderStyle.None;
                PIOForm.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(PIOForm);
                PIOForm.Show();
                PIOForm.BringToFront();

                //更新当前显示
                simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }

        }
    }
}