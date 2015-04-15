using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.Order;
using Commons.WinForm;
using DevExpress.XtraTab;
using Commons.Model;

namespace ReturnOrder
{
    public partial class ReturnOrderQuery : BaseForm
    {
        List<ReturnOrderHeaderModel> listReturnOrderHeader = null;
        List<ReturnOrderDtlModel> listReturnOrderDtl = null;
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口

        public ReturnOrderQuery(BaseMainForm formb, BaseForm formp)
        {
            FormBase = formb;
            FormParent = formp;
            InitializeComponent();
        }

        private void ReturnOrderQuery_Load(object sender, EventArgs e)
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

                initImageComboBoxEditMoveType();

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
                if (!string.IsNullOrEmpty(imageComboBoxEdit_MoveType.Text))
                {
                    strSearchCondition = strSearchCondition + " and MOVEMENT_TYPE_ID ='" + this.imageComboBoxEdit_MoveType.EditValue + "'";
                }

                //查询订单头
                queryConditionModel QC = new queryConditionModel();
                QC.where = strSearchCondition;

                if (DevCommon.getDataByWebService("getReturnOrderHeaderByCondition", "getReturnOrderHeaderByCondition", QC, ref listReturnOrderHeader) == RetCode.NG)
                {
                    return;
                }

                //数据绑定
                this.gridControl_ReturnOrder.DataSource = listReturnOrderHeader;
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
                if (this.gridView_ReturnOrder.RowCount == 0)
                {
                    return;
                }

                //取得订单头
                ReturnOrderModel RO = new ReturnOrderModel();
                RO.header = listReturnOrderHeader[this.gridView_ReturnOrder.GetDataSourceRowIndex(this.gridView_ReturnOrder.FocusedRowHandle)];
                //取得订单明细
                queryDocIdModel QDI = new queryDocIdModel();
                QDI.docId = RO.header.docId;

                if (DevCommon.getDataByWebService("getReturnOrderDtlById", "getReturnOrderDtlById", QDI, ref listReturnOrderDtl) == RetCode.NG)
                {
                    return;
                }
                //构造销售政策和BOM
                if (listReturnOrderDtl != null)
                {
                    //foreach (ReturnOrderDtlModel itemDtl in listReturnOrderDtl)
                    //{
                    //    //查询商品信息
                    //    queryProductModel QP = new queryProductModel();
                    //    QP.productId = itemDtl.productId;
                    //    QP.productStoreId = LoginInfo.ProductStoreId;
                    //    //QP.movementTypeId = MoveTypeKey.ReturnOrder;
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

                    RO.detail = listReturnOrderDtl;
                }

                //转入下级窗口
                ReturnOrder ROForm = new ReturnOrder(FormBase, this, RO);
                ROForm.Location = new Point(0, 0);
                ROForm.TopLevel = false;
                ROForm.TopMost = false;
                ROForm.ControlBox = false;
                ROForm.FormBorderStyle = FormBorderStyle.None;
                ROForm.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(ROForm);
                ROForm.Show();
                ROForm.BringToFront();

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
            //    int nFocusedDataSourceRowIndex = this.gridView_ReturnOrder.GetDataSourceRowIndex(this.gridView_ReturnOrder.FocusedRowHandle);
            //    String outStr = null;

            //    //草稿状态
            //    if (listReturnOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
            //    {
            //        queryDocIdModel QDI = new queryDocIdModel();
            //        QDI.docId = listReturnOrderHeader[nFocusedDataSourceRowIndex].docId;
            //        //删除
            //        if (DevCommon.getDataByWebService("deleteReturnOrderById", "deleteReturnOrderById", QDI, ref outStr) == RetCode.NG)
            //        {
            //            return;
            //        }
            //    }
            //    //确定且未付款
            //    else if (listReturnOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.VALID).ToString())
            //        && listReturnOrderHeader[nFocusedDataSourceRowIndex].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
            //    {
            //        updateStatusModel US = new updateStatusModel();
            //        US.docId = listReturnOrderHeader[nFocusedDataSourceRowIndex].docId;
            //        US.docStatus = ((int)DocStatus.INVALID).ToString();
            //        US.lastUpdateDocDate = DateTime.Now.ToString();
            //        US.lastUpdateUserId = LoginInfo.UserLoginId;
            //        //更新状态
            //        if (DevCommon.getDataByWebService("updateReturnOrderStatus", "updateReturnOrderStatus", US, ref outStr) == RetCode.NG)
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

        //表格焦点发生变化
        private void gridViewReturnOrder_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{
            //    int nFocusedSourceRowIndex = this.gridView_ReturnOrder.GetDataSourceRowIndex(this.gridView_ReturnOrder.FocusedRowHandle);
            //    if (nFocusedSourceRowIndex < 0)
            //    {
            //        return;
            //    }
            //    if (this.gridView_ReturnOrder.RowCount > 0
            //        && ((listReturnOrderHeader[nFocusedSourceRowIndex].docStatus.Equals(((int)DocStatus.DRAFT).ToString())
            //            || (listReturnOrderHeader[nFocusedSourceRowIndex].docStatus.Equals(((int)DocStatus.VALID).ToString())
            //            && listReturnOrderHeader[nFocusedSourceRowIndex].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString())))))          //草稿状态或者未付款状态可以退订
            //    {
            //        this.simpleButton_Invalid.Enabled = true;
            //    }
            //    else
            //    {
            //        this.simpleButton_Invalid.Enabled = false;
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    FormBase.PromptInformation(ex.Message);
            //}

        }

        //退货类型combo初始化
        public void initImageComboBoxEditMoveType()
        {
            var searchConditions = new
            {
                orderType = MoveType.returnIn
            };
            DataTable dtMoveType = null;
            if (DevCommon.getDataByWebService("MoveType", "MoveSearch", searchConditions, ref dtMoveType) == RetCode.OK)
            {
                DevCommon.initCmb(imageComboBoxEdit_MoveType, dtMoveType, "movementTypeId", "movementTypeName", true);
            }
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