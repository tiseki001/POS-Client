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
using Newtonsoft.Json;
using DevExpress.XtraTab;
using Commons.Model;

namespace SalesOrder
{
    public partial class SalesOrderQuery : BaseForm
    {
        List<SalesOrderHeaderModel> listSalesOrderHeader = null;
        List<SalesOrderDtlModel> listSalesOrderDtl = null;
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口

        public SalesOrderQuery(BaseMainForm formb, BaseForm formp)
        {
            FormBase = formb;
            FormParent = formp;
            InitializeComponent();
        }

        //窗体加载
        private void SalesOrderQuery_Load(object sender, EventArgs e)
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
                if (!string.IsNullOrEmpty(this.searchButton_SalesPersonId.Text))
                {
                    strSearchCondition = strSearchCondition + " and SALES_ID ='" + this.textEdit_OrderId.Text + "'";
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

                if (DevCommon.getDataByWebService("getSalesOrderHeaderByCondition", "getSalesOrderHeaderByCondition", QC, ref listSalesOrderHeader) == RetCode.NG)
                {
                    return;
                }
                //查询后的人名整理
                //if (listSalesOrderHeader != null)
                //{
                //    foreach (SalesOrderHeaderModel hd in listSalesOrderHeader)
                //    {
                //        hd.createUserName = hd.createUserNameL + hd.createUserNameM + hd.createUserNameF;       //创建人员
                //        hd.memberName = hd.memberNameL + hd.memberNameM + hd.memberNameF;                       //会员
                //        hd.salesPersonName = hd.salesPersonNameL + hd.salesPersonNameM + hd.salesPersonNameF;   //销售人员
                //    }
                //}

                //数据绑定
                this.gridControl_SalesOrder.DataSource = listSalesOrderHeader;
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
                if (this.gridView_SalesOrder.RowCount == 0)
                {
                    return;
                }
                
                //取得订单头
                SalesOrderModel SO = new SalesOrderModel();
                SO.header = listSalesOrderHeader[this.gridView_SalesOrder.GetDataSourceRowIndex(this.gridView_SalesOrder.FocusedRowHandle)];
                //取得订单明细
                queryDocIdModel QDI = new queryDocIdModel();
                QDI.docId = SO.header.docId;

                if (DevCommon.getDataByWebService("getSalesOrderDtlById", "getSalesOrderDtlById", QDI, ref listSalesOrderDtl) == RetCode.NG)
                {
                    return;
                }
                //构造销售政策和BOM
                if (listSalesOrderDtl != null)
                {
                    foreach (SalesOrderDtlModel itemDtl in listSalesOrderDtl)
                    {
                        //查询商品信息
                        queryProductModel QP = new queryProductModel();
                        QP.productId = itemDtl.productId;
                        QP.productStoreId = LoginInfo.ProductStoreId;
                        QP.movementTypeId = MoveTypeKey.salesOrder;
                        getProductModel product = new getProductModel();
                        if (DevCommon.getDataByWebService("getProductByCond", "getProductByCond", QP, ref product) == RetCode.OK && product != null)
                        {
                            itemDtl.productSalesPolicys.items = product.productSalesPolicys;
                            ProductSalesPolicyModel productSalesPolicy = itemDtl.productSalesPolicys.items.Find(delegate(ProductSalesPolicyModel item) { return item.productSalesPolicyId.Equals(itemDtl.productSalesPolicyId); });
                            itemDtl.productSalesPrices.items = productSalesPolicy.productPriceTypes;
                        }

                        //查询BOM
                        queryBOMModel QB = new queryBOMModel();
                        QB.productStoreId = LoginInfo.ProductStoreId;
                        QB.productSalesPolicyId = itemDtl.productSalesPolicyId;
                        List<getBOMModel> BOM = new List<getBOMModel>();
                        if (DevCommon.getDataByWebService("getBomByCond", "getBomByCond", QB, ref BOM) == RetCode.NG)
                        {
                            return;
                        }
                        else
                        {
                            itemDtl.boms.items = BOM;
                        }
                    }
                }
                SO.detail = listSalesOrderDtl;

                //转入下级窗口
                SalesOrder SOForm = new SalesOrder(FormBase, this, SO);
                SOForm.Location = new Point(0, 0);
                SOForm.TopLevel = false;
                SOForm.TopMost = false;
                SOForm.ControlBox = false;
                SOForm.FormBorderStyle = FormBorderStyle.None;
                SOForm.Dock = DockStyle.Fill;
                this.Visible = false;
                ((XtraTabPage)this.Parent).Controls.Add(SOForm);
                SOForm.Show();
                SOForm.BringToFront();

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
            try
            {
                int nFocusedDataSourceRowIndex = this.gridView_SalesOrder.GetDataSourceRowIndex(this.gridView_SalesOrder.FocusedRowHandle);
                String outStr = null;

                //草稿状态
                if (listSalesOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    queryDocIdModel QDI = new queryDocIdModel();
                    QDI.docId = listSalesOrderHeader[nFocusedDataSourceRowIndex].docId;
                    //删除
                    if (DevCommon.getDataByWebService("deleteSalesOrderById", "deleteSalesOrderById", QDI, ref outStr) == RetCode.NG)
                    {
                        return;
                    }
                }
                //确定且未付款
                else if (listSalesOrderHeader[nFocusedDataSourceRowIndex].docStatus.Equals(((int)DocStatus.VALID).ToString())
                    && listSalesOrderHeader[nFocusedDataSourceRowIndex].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                {
                    updateStatusModel US = new updateStatusModel();
                    US.docId = listSalesOrderHeader[nFocusedDataSourceRowIndex].docId;
                    US.docStatus = ((int)DocStatus.INVALID).ToString();
                    US.lastUpdateDocDate = DateTime.Now.ToString();
                    US.lastUpdateUserId = LoginInfo.UserLoginId;
                    //更新状态
                    if (DevCommon.getDataByWebService("updateSalesOrderStatus", "updateSalesOrderStatus", US, ref outStr) == RetCode.NG)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);             	
            }

        }

        //表格焦点发生变化
        private void gridViewSalesOrder_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                int nFocusedSourceRowIndex = this.gridView_SalesOrder.GetDataSourceRowIndex(this.gridView_SalesOrder.FocusedRowHandle);
                if (nFocusedSourceRowIndex < 0)
                {
                    return;
                }
                if (this.gridView_SalesOrder.RowCount > 0
                    && ((listSalesOrderHeader[nFocusedSourceRowIndex].docStatus.Equals(((int)DocStatus.DRAFT).ToString())
                        || (listSalesOrderHeader[nFocusedSourceRowIndex].docStatus.Equals(((int)DocStatus.VALID).ToString())
                        && listSalesOrderHeader[nFocusedSourceRowIndex].fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString())))))          //草稿状态或者未付款状态可以退订
                {
                    this.simpleButton_Invalid.Enabled = true;
                }
                else
                {
                    this.simpleButton_Invalid.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);             	
            }

        }

        //销售人员输入后
        private void searchButtonSalesPersonId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(this.searchButton_SalesPersonId.Text))
                    {
                        textEdit_OrderId.Focus();
                        return;
                    }

                    this.textEdit_SalesPersonName.Text = "";

                    //查询销售人员
                    querySalesPersonModel SOI = new querySalesPersonModel();
                    SOI.partyId = this.searchButton_SalesPersonId.Text;
                    getSalesPersonModel salesPerson = new getSalesPersonModel();
                    if (DevCommon.getDataByWebService("getSalesPersonBySalesPersonId", "getSalesPersonBySalesPersonId", SOI, ref salesPerson) == RetCode.OK && salesPerson != null)
                    {
                        this.textEdit_SalesPersonName.Text = salesPerson.name;
                        textEdit_OrderId.Focus();
                    }
                    else
                    {
                        ((SearchButton)sender).ShowForm();
                    }

                }
            }
            catch (System.Exception ex)
            {
                this.searchButton_SalesPersonId.Text = "";
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