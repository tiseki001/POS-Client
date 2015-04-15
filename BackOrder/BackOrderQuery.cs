using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.Order;
using Commons.WinForm;
using DevExpress.XtraTab;

namespace BackOrder
{
    public partial class BackOrderQuery : BaseForm
    {
        List<BackOrderHeaderModel> listBackOrderHeader = null;
        List<BackOrderDtlModel> listBackOrderDtl = null;
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口

        public BackOrderQuery(BaseMainForm formb, BaseForm formp)
        {
            FormBase = formb;
            FormParent = formp;
            InitializeComponent();
        }

        private void BackOrderQuery_Load(object sender, EventArgs e)
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

                //初始化表格中的combobox
                dtStatus = DevCommon.GetAllBusinessStatus();
                if (dtStatus != null)
                {
                    DevCommon.initCmb(repositoryItemImageComboBox_BusinessStatus, dtStatus, false);
                }

                //初始化显示
                //simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //查询按钮按下
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
                if (!string.IsNullOrEmpty(this.textEdit_BaseEntry.Text))
                {
                    strSearchCondition = strSearchCondition + " and BASE_ENTRY ='" + this.textEdit_BaseEntry.Text + "'";
                }
                if (!string.IsNullOrEmpty(baseCombobox_DocStatus.Text))
                {
                    strSearchCondition = strSearchCondition + " and DOC_STATUS ='" + this.baseCombobox_DocStatus.EditValue + "'";
                }

                //查询订单头
                queryConditionModel QC = new queryConditionModel();
                QC.where = strSearchCondition;

                if (DevCommon.getDataByWebService("getBackOrderHeaderByCondition", "getBackOrderHeaderByCondition", QC, ref listBackOrderHeader) == RetCode.NG)
                {
                    return;
                }

                //数据绑定
                this.gridControl_BackOrder.DataSource = listBackOrderHeader;
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
                if (this.gridView_BackOrder.RowCount == 0)
                {
                    return;
                }

                //取得订单头
                BackOrderModel BO = new BackOrderModel();
                BO.header = listBackOrderHeader[this.gridView_BackOrder.GetDataSourceRowIndex(this.gridView_BackOrder.FocusedRowHandle)];
                //取得订单明细
                queryDocIdModel QDI = new queryDocIdModel();
                QDI.docId = BO.header.docId;

                if (DevCommon.getDataByWebService("getBackOrderDtlById", "getBackOrderDtlById", QDI, ref listBackOrderDtl) == RetCode.NG)
                {
                    return;
                }
                //构造销售政策和BOM
                if (listBackOrderDtl != null)
                {
                    //foreach (BackOrderDtlModel itemDtl in listBackOrderDtl)
                    //{
                    //    //查询商品信息
                    //    queryProductModel QP = new queryProductModel();
                    //    QP.productId = itemDtl.productId;
                    //    QP.productStoreId = LoginInfo.ProductStoreId;
                    //    //QP.movementTypeId = MoveTypeKey.BackOrder;
                    //    getProductModel product = new getProductModel();
                    //    if (DevCommon.getDataByWebService("getProductByCond", "getProductByCond", QP, ref product) == RetCode.OK && product != null)
                    //    {
                    //        itemDtl.productSalesPolicys.items = product.productSalesPolicys;
                    //        ProductSalesPolicyModel productSalesPolicy = itemDtl.productSalesPolicys.items.Find(delegate(ProductSalesPolicyModel item) { return item.productSalesPolicyId.Equals(itemDtl.productSalesPolicyId); });
                    //        itemDtl.productSalesPrices.items = productSalesPolicy.productPriceTypes;
                    //    }

                    //    //查询BOM
                    //    queryBOMModel QB = new queryBOMModel();
                    //    QB.productStoreId = LoginInfo.ProductStoreId;
                    //    QB.productSalesPolicyId = itemDtl.productSalesPolicyId;
                    //    List<getBOMModel> BOM = new List<getBOMModel>();
                    //    if (DevCommon.getDataByWebService("getBomByCond", "getBomByCond", QB, ref BOM) == RetCode.NG)
                    //    {
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        itemDtl.boms.items = BOM;
                    //    }
                    //}

                    BO.detail = listBackOrderDtl;
                }

                //转入下级窗口
                BackOrder BOForm = new BackOrder(FormBase, this, BO);
                BOForm.Location = new Point(0, 0);
                BOForm.TopLevel = false;
                BOForm.TopMost = false;
                BOForm.ControlBox = false;
                BOForm.FormBorderStyle = FormBorderStyle.None;
                BOForm.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(BOForm);
                BOForm.Show();
                BOForm.BringToFront();

                //更新当前显示
                simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }

        }

        //取消按钮按下
        private void simpleButton_Cancel_Click(object sender, EventArgs e)
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

        //退订按钮按下
        private void simpleButton_Invalid_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int nFocusedDataSourceRowIndex = this.gridView_BackOrder.GetDataSourceRowIndex(this.gridView_BackOrder.FocusedRowHandle);
            //    String outStr = null;

            //    //草稿状态
            //    if (listBackOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
            //    {
            //        queryDocIdModel QDI = new queryDocIdModel();
            //        QDI.docId = listBackOrderHeader[nFocusedDataSourceRowIndex].docId;
            //        //删除
            //        if (DevCommon.getDataByWebService("deleteBackOrderById", "deleteBackOrderById", QDI, ref outStr) == RetCode.NG)
            //        {
            //            return;
            //        }
            //    }
            //    //确定且未付款
            //    else if (listBackOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.VALID).ToString())
            //        && listBackOrderHeader[nFocusedDataSourceRowIndex].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
            //    {
            //        updateStatusModel US = new updateStatusModel();
            //        US.docId = listBackOrderHeader[nFocusedDataSourceRowIndex].docId;
            //        US.docStatus = ((int)DocStatus.INVALID).ToString();
            //        US.lastUpdateDocDate = DateTime.Now.ToString();
            //        US.lastUpdateUserId = LoginInfo.UserLoginId;
            //        //更新状态
            //        if (DevCommon.getDataByWebService("updateBackOrderStatus", "updateBackOrderStatus", US, ref outStr) == RetCode.NG)
            //        {
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }

            //    simpleButton_Query.PerformClick();
            //}
            //catch (System.Exception ex)
            //{
            //    FormBase.PromptInformation(ex.Message);
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