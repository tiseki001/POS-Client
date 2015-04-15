using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Commons.WinForm;
using Commons.Model.Order;
using Commons.Model;

namespace ReturnOrder
{
    public partial class ReturnOrder : BaseForm
    {
        ReturnOrderModel RO = null;                     //退货单
        ReturnOrderDtlModel RODtl;                      //表格中的明细行
        RefundOrderModel RFO = null;                    //退款单 
        ReturnInWhsOrderModel IO = null;                //退货入库单
       

        SalesOrderModel SO = null;                      //销售订单

        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存
        int nProductSalesPolicyNo = 0;                  //商品销售政策+套包序列
        bool bIsRefreshing = false;                     //数据更新中

        public ReturnOrder(BaseMainForm formb, BaseForm formp, ReturnOrderModel returnOrder)
        {
            FormBase = formb;
            FormParent = formp;
            RO = returnOrder;

            InitializeComponent();
        }

        private void ReturnOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (RO == null)             //新建订单
                {
                    RO = new ReturnOrderModel();
                    bIsOrderSaved = false;
                }
                else                        //查询订单
                {
                    bIsOrderSaved = true;
                }

                //初始化显示
                if (!bIsOrderSaved)
                {
                    SendKeys.Send("{tab}");
                }
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }



        //表格中删除一行，最后一行空白行不能删除
        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_ProductList.RowCount > 0)           //有数据时候
                {
                    int nFocusedSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);
                    int nProductSalesPolicyNo = RO.detail[nFocusedSourceRowIndex].productSalesPolicyNo;   
                    //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    int nCount = RO.detail.Sum(dtl => dtl.productSalesPolicyNo == nProductSalesPolicyNo ? 1 : 0) - 1;                    
                    //if (RO.detail[nFocusedSourceRowIndex].subProducts.items != null)
                    //{
                    //    nCount = RO.detail[nFocusedSourceRowIndex].subProducts.items.Count;
                    //}
                    if (nFocusedSourceRowIndex + nCount == this.gridView_ProductList.RowCount - 2)
                    {
                        this.gridView_ProductList.MoveLast();
                    }
                    //删除当前行数据(包括套包)
                    RO.detail.RemoveAll(dtl => dtl.productSalesPolicyNo == nProductSalesPolicyNo ? true : false);
                    //更新表格中数据并跳转到最后一行空白行
                    updateGridData();
                }
                //searchButton_SerialNo.Focus();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //表格焦点变化
        private void gridViewProducetList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int nFocusedSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);
            if (nFocusedSourceRowIndex < 0)
            {
                RODtl = new ReturnOrderDtlModel();
            }
            else
            {
                RODtl = (ReturnOrderDtlModel)RO.detail[nFocusedSourceRowIndex].Clone();
            }

            //更新表头的明细信息
            displayDetailData();
        }

        //将明细内容更新至表格数据中
        private void updateGridData()
        {
            this.gridControl_ProductList.RefreshDataSource();
            this.gridView_ProductList.MoveLast();
            this.simpleButton_OK.Enabled = false;
        }

        //明细内容设置更改属性
        private void setDetailReadOnly(bool isReadOnly)
        {
            textEdit_Quantity.Properties.ReadOnly = isReadOnly;
            textEdit_MemoDtl.Properties.ReadOnly = isReadOnly;
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.searchButton_SerialNo.Text = RODtl.serialNo;
            this.searchButton_ProductName.Text = RODtl.productName;
            this.searchButton_ProductId.Text = RODtl.productId;
            this.searchButton_BarCodes.Text = RODtl.barCodes;
            this.textEdit_Quantity.EditValue = RODtl.quantity;
            this.textEdit_UnitPrice.EditValue = RODtl.unitPrice;
            //this.textEdit_RebateAmountDtl.EditValue = RODtl.bomAmount;
            this.textEdit_RebateAmountDtl.EditValue = RODtl.rebateAmount;
            this.textEdit_MemoDtl.Text = RODtl.memo;
            this.textEdit_ProductSalesPolicy.Text = RODtl.productSalesPolicyName;
            this.textEdit_BOM.Text = RODtl.bomName;
            this.imageComboBoxEdit_FaultType.EditValue = RODtl.faultTypeId;
            this.textEdit_FaultDescription.Text = RODtl.faultDescription;
            //if (RODtl.productSalesPolicys != null)
            //{
            //    initImageCombBoxProductSalesPolicy(SODtl.productSalesPolicys.items);
            //}
            //if (RODtl.boms != null)
            //{
            //    initImageComboBoxEditBOM(RODtl.boms.items);
            //}
        }

        //将明细内容更新至显示数据
        private void displayHeaderData()
        {
            if (RO != null && RO.header != null)
            {
                this.searchButton_MemberPhoneNo.Text = RO.header.memberPhoneMobile;
                this.textEdit_MemberName.Text = RO.header.memberName;
                this.searchButton_SalesPersonId.Text = RO.header.salesId;
                this.textEdit_SalesPersonName.Text = RO.header.salesPersonName;
            }
            else
            {
                this.searchButton_MemberPhoneNo.Text = null;
                this.textEdit_MemberName.Text = null;
                this.searchButton_SalesPersonId.Text = null;
                this.textEdit_SalesPersonName.Text = null;
            }
        }


        //数量输入后
        private void textEditQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    int quantity = Convert.ToInt32(this.textEdit_Quantity.EditValue);
                    if (quantity <= 0)
                    {
                        this.textEdit_Quantity.EditValue = RODtl.quantity;
                        return;
                    }
                    //计算金额
                    RODtl.quantity = Convert.ToInt32(this.textEdit_Quantity.EditValue);
                    RODtl.rebateAmount = RODtl.quantity * RODtl.rebatePrice;
                    //RODtl.bomAmount = RODtl.rebateAmount;

                    //显示
                    //this.textEdit_RebateAmountDtl.EditValue = RODtl.bomAmount;
                    this.textEdit_RebateAmountDtl.EditValue = RODtl.rebateAmount;
                    imageComboBoxEdit_FaultType.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //金额输入后
        private void textEditBomAmoutDtl_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode.Equals(Keys.Enter))
            //    {
            //        //套包金额均摊
            //        if (SODtl.isMainProduct.Equals(MainProductType.yes))
            //        {
            //            decimal subProductPriceSum = 0;
            //            decimal currentBomAmout = Convert.ToDecimal(this.textEdit_BomAmountDtl.EditValue);
            //            SODtl.rebateAmount = SODtl.rebateAmount + (currentBomAmout - SODtl.bomAmount) * SODtl.mainRatio / 100;
            //            SODtl.rebatePrice = SODtl.rebateAmount / SODtl.quantity;
            //            SODtl.bomAmount = currentBomAmout;
            //            if (SODtl.subProducts.items != null)
            //            {
            //                subProductPriceSum = SODtl.subProducts.items.Sum(item => item.unitPrice);
            //                if (subProductPriceSum > 0)
            //                {
            //                    foreach (getProductModel item in SODtl.subProducts.items)
            //                    {
            //                        item.quantity = SODtl.quantity;
            //                        item.rebateAmount = (currentBomAmout - SODtl.rebateAmount) * item.unitPrice / subProductPriceSum;
            //                        item.rebatePrice = item.rebateAmount / item.quantity;
            //                        item.bomAmount = SODtl.bomAmount;
            //                    }
            //                }

            //            }
            //        }

            //        textEdit_MemoDtl.Focus();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    FormBase.PromptInformation(ex.Message);
            //}
        }

        //备注中回车键按下
        private void textEditMemo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //获取界面中的数据
                    RODtl.memo = this.textEdit_MemoDtl.Text;
                    this.simpleButton_Add.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //结算
        private void simpleButton_Account_Click(object sender, EventArgs e)
        {
            try
            {
                //有数据
                if (this.gridView_ProductList.RowCount > 0)
                {
                    this.textEdit_PrimeAmount.EditValue = Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
                    this.textEdit_Amount.EditValue = this.textEdit_PrimeAmount.EditValue;
                    this.simpleButton_OK.Enabled = true;
                    //this.simpleButton_Save.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }

        }

        //销售查询
        private void textEditBaseEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(this.textEdit_BaseEntry.Text))
                    {
                        RO = new ReturnOrderModel();
                        this.gridControl_ProductList.DataSource = RO.detail;
                        updateGridData();
                        displayHeaderData();
                        return;
                    }

                    //调用接口查询销售订单
                    queryDocIdModel QDI = new queryDocIdModel();
                    QDI.docId = this.textEdit_BaseEntry.Text;
                    if (DevCommon.getDataByWebService("getSalesOrderById", "getSalesOrderById", QDI, ref SO) == RetCode.NG || SO.header == null)
                    {
                        this.gridControl_ProductList.DataSource = RO.detail;
                        updateGridData();
                        displayHeaderData();
                        return;
                    }
                    else
                    {
                        if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                            || !SO.header.fundStatus.Equals(((int)BusinessStatus.CLEARED).ToString()))
                        {
                            FormBase.PromptInformation("销售订单状态不正确！");
                            return;
                        }
                    }
                    //界面显示预订单信息
                    //RO.header.baseEntry = this.textEdit_BaseEntry.Text;
                    ReturnOrderBLL.getReturnOrderFromSalesOrder(SO, ref RO);
                    if (SO.header.returnStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    {
                        initImageComboBoxEditMoveType(true);
                    }
                    else
                    {
                        initImageComboBoxEditMoveType(false);                   //退过货的不能做冲红
                    }

                    initImageComboBoxEditFaultType();
                    this.gridControl_ProductList.DataSource = RO.detail;
                    updateGridData();

                    displayHeaderData();
                    imgcmbMoveType_SelectedIndexChanged(null, null);

                    imageComboBoxEdit_FaultType.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //备注输入后
        private void textEditMemoHeader_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //设置备注信息
                    RO.header.memo = this.textEdit_MemoHeader.Text;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //保存为草稿
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (bIsQuery && !SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                if (bIsOrderSaved && !RO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
                {
                    FormBase.PromptInformation("非草稿状态订单不能保存！");
                    return;
                }

                //设置销售订单
                if (!setReturnOrder(DocStatus.DRAFT))
                {
                    FormBase.PromptInformation("销售订单数据不正确！");
                    return;
                }
                //收款单和出库单不保存
                RFO = null;
                IO = null;

                //保存销售订单
                //写入数据库
                if (bIsOrderSaved)
                {
                    //if (!SalesOrderBLL.updateSalesOrderAll(true, ref SO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    return;
                    //}
                }
                else
                {
                    //if (!SalesOrderBLL.saveSalesOrderAll(ref SO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    return;
                    //}
                    bIsOrderSaved = true;
                }

                //初始化显示
                //if (!initDisplay())
                //{
                //    FormBase.PromptInformation("界面初始化失败！");
                //    return;
                //}
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //确定
        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            string currentDocStatus = RO.header.docStatus;
            try
            {
                //数据有效性检查
                //if (!checkSalesOrderValid())
                //{
                //    return;
                //}

                //查询销售订单时候，是否要更新销售订单
                bool bUpdateSalesOrder = false;
                if (bIsOrderSaved && RO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    bUpdateSalesOrder = true;
                }

                //设置退货单
                if (string.IsNullOrEmpty(RO.header.docStatus) || RO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    if (!setReturnOrder(DocStatus.VALID))
                    {
                        FormBase.PromptInformation("退货单数据不正确！");
                        RO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //退款单
                if (ReturnOrderBLL.setRefundOrder(ref RO, ref RFO) == DialogResult.Cancel)
                {
                    RO.header.docStatus = currentDocStatus;
                    return;
                }

                //退货入库单
                if (SO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                {
                    IO = null;
                }
                else
                {
                    if (!ReturnOrderBLL.setReturnInWhsOrder(ref RO, ref IO))
                    {
                        FormBase.PromptInformation("销售出库单数据不正确！");
                        RO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //写入数据库
                if (bIsOrderSaved)
                {
                    
                    //if (!SalesOrderBLL.updateSalesOrderAll(bUpdateSalesOrder, ref SO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    SO.header.docStatus = currentDocStatus;
                    //    return;
                    //}
                }
                else
                {
                    if (!ReturnOrderBLL.saveReturnOrderAll(ref RO, ref RFO, ref IO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }


                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    RO.header.docStatus = currentDocStatus;
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
                RO.header.docStatus = currentDocStatus;
            }
        }

        //取消
        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否确定关闭本画面？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        searchButton_SerialNo.Focus();
                        return;
                    }

                    this.textEdit_SalesPersonName.Text = "";
                    RO.header.salesId = null;

                    //查询销售人员
                    querySalesPersonModel SOI = new querySalesPersonModel();
                    SOI.partyId = this.searchButton_SalesPersonId.Text;
                    getSalesPersonModel salesPerson = new getSalesPersonModel();
                    if (DevCommon.getDataByWebService("getSalesPersonBySalesPersonId", "getSalesPersonBySalesPersonId", SOI, ref salesPerson) == RetCode.OK && salesPerson != null)
                    {
                        this.textEdit_SalesPersonName.Text = salesPerson.name;
                        RO.header.salesId = SOI.partyId;
                        searchButton_SerialNo.Focus();
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

        //将界面内容设置到订单对象
        private bool setReturnOrder(DocStatus status)
        {
            //表头
            RO.header.docStatus = ((int)status).ToString();
            RO.header.rebateAmount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
            RO.header.primeAmount = Convert.ToDecimal(this.textEdit_PrimeAmount.EditValue);
            RO.header.partyId = LoginInfo.OwnerPartyId;
            RO.header.storeId = LoginInfo.ProductStoreId;
            if (!bIsOrderSaved)
            {
                RO.header.createUserId = LoginInfo.PartyId;
            }
            else
            {
               RO.header.lastUpdateDocDate = DateTime.Now.ToString();
            }
            RO.header.lastUpdateUserId = LoginInfo.PartyId;
            if (string.IsNullOrEmpty(RO.header.refundStoreId))
            {
                RO.header.refundStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(RO.header.receiveStoreId))
            {
                RO.header.receiveStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(RO.header.refundStatus))
            {
                RO.header.refundStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            if (string.IsNullOrEmpty(RO.header.warehouseStatus))
            {
                RO.header.warehouseStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            if (string.IsNullOrEmpty(RO.header.exchangeStatus))
            {
                RO.header.exchangeStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            //过账日期设置
            RO.header.postingDate = DevCommon.PostingDate();

            //锁定数量
            //bool bLocked = true;
            //if (SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString())           //草稿
            //    || checkEdit_Logistics.Checked)                                         //物流出库
            //{
            //    bLocked = false;
            //}
            //销售明细和价格明细
            for (int i = 0; i < RO.detail.Count; i++)
            {
                RO.detail[i].docId = RO.header.docId;
                RO.detail[i].lineNo = i + 1;

                if (SO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                {
                    RO.detail[i].unLockedQuantity = RO.detail[i].quantity;
                }
                else
                {
                    RO.detail[i].unLockedQuantity = 0;
                }

                //根据移动类型设置入库仓库
                if (RO.header.movementTypeId.Equals(MoveTypeKey.returnOrderRCW))
                {
                    SalesOrderDtlModel SODtl = SO.detail.Find(delegate(SalesOrderDtlModel result) { return result.lineNo.Equals(RO.detail[i].lineNoBaseEntry); });
                    RO.detail[i].facilityId = SODtl.facilityId;
                }
                else
                {
                    RO.detail[i].facilityId = null;
                }

            }

            return true;
        }

        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(RO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.RETURN, ref str))
                {
                    return false;
                }
                RO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                RO.header.createDocDate = DateTime.Now.ToString();
                RO.header.lastUpdateDocDate = RO.header.createDocDate;

                //控件可用
                textEdit_BaseEntry.Properties.ReadOnly = false;
                searchButton_SalesPersonId.Properties.ReadOnly = false;
                textEdit_Quantity.Properties.ReadOnly = false;
                textEdit_MemoDtl.Properties.ReadOnly = false;
                textEdit_MemoHeader.Properties.ReadOnly = false;
                simpleButton_Add.Enabled = true;
                simpleButton_Delete.Enabled = true;
                //simpleButton_Logistics.Enabled = true;
                //simpleButton_Save.Enabled = true;
                simpleButton_Account.Enabled = true;
                simpleButton_OK.Enabled = true;
                imageComboBoxEdit_FaultType.Properties.Items.Clear();
                imageComboBoxEdit_MoveType.Properties.Items.Clear();
            }
            else                           //查询订单的情况
            {
                //查询时显示状态和过账时间
                //labelControl_DocStatus.Visible = true;
                //imageComboBoxEdit_DocStatus.Visible = true;
                //labelControl_PostingDate.Visible = true;
                //textEdit_PostingDate.Visible = true;

                initImageComboBoxEditMoveType(true);
                initImageComboBoxEditFaultType();


                //根据目前已有的订单状态决定是否能够后续操作
                if (!RO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))        //草稿状态可以任意修改
                {
                    textEdit_BaseEntry.Properties.ReadOnly = true;
                    searchButton_SalesPersonId.Properties.ReadOnly = true;
                    textEdit_Quantity.Properties.ReadOnly = true;
                    textEdit_MemoDtl.Properties.ReadOnly = true;
                    textEdit_MemoHeader.Properties.ReadOnly = true;
                    imageComboBoxEdit_MoveType.ReadOnly = true;
                    imageComboBoxEdit_FaultType.ReadOnly = true;
                    textEdit_FaultDescription.ReadOnly = true;
                    textEdit_Amount.ReadOnly = true;
                    
                    simpleButton_Add.Enabled = false;
                    simpleButton_Delete.Enabled = false;
                    if (!RO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                        || !RO.header.refundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    {
                        if (!RO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()) || RO.header.receiveStoreId != SO.header.storeId)
                        {
                            if (RO.header.receiveStoreId != RO.header.storeId)
                            {
                                //checkEdit_Logistics.Checked = true;
                            }
                            //simpleButton_Logistics.Enabled = false;
                            //simpleButton_Save.Enabled = false;
                            simpleButton_Account.Enabled = false;
                            simpleButton_OK.Enabled = false;
                        }
                        else
                        {
                            //checkEdit_Logistics.Enabled = false;
                            //checkEdit_Collection.Checked = false;           //不需要收款
                        }
                    }
                }
            }

            //画面显示
            this.textEdit_DocId.Text = RO.header.docId;
            this.textEdit_CreateDocDate.Text = RO.header.createDocDate.ToString();
            this.textEdit_BaseEntry.Text = RO.header.baseEntry;
            //this.searchButton_MemberPhoneNo.Text = RO.header.memberPhoneMobile;
            //this.textEdit_MemberName.Text = RO.header.memberName;
            //this.searchButton_SalesPersonId.Text = RO.header.salesId;
            //this.textEdit_SalesPersonName.Text = RO.header.salesPersonName;
            this.searchButton_SerialNo.Text = "";
            this.searchButton_ProductName.Text = "";
            this.searchButton_ProductId.Text = "";
            this.textEdit_ProductSalesPolicy.Text = "";
            this.textEdit_BOM.Text = "";
            this.textEdit_Quantity.Text = "";
            this.textEdit_UnitPrice.Text = "";
            this.textEdit_RebateAmountDtl.Text = "";
            this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = RO.header.memo;
            this.textEdit_PrimeAmount.EditValue = RO.header.rebateAmount;
            this.textEdit_Amount.EditValue = RO.header.rebateAmount;
            textEdit_PostingDate.EditValue = RO.header.postingDate;
            imageComboBoxEdit_MoveType.EditValue = RO.header.movementTypeId;

            //表格明细数据绑定
            this.gridControl_ProductList.DataSource = RO.detail;
            updateGridData();
            
            //initImageComboBoxEditMoveType();
            displayHeaderData();
            FormBase.PromptInformation("");

            //默认显示本门店员工
            DataTable dt = DevCommon.getSalesPersonWhere();
            this.searchButton_SalesPersonId.where = dt;

            //焦点在销售订单上
            textEdit_BaseEntry.Focus();

            return true;
        }

        //退货类型combo初始化
        public void initImageComboBoxEditMoveType(bool bRCW)
        {
            var searchConditions = new
            {
                orderType = MoveType.returnIn
            };
            DataTable dtMoveType = null;

            if (DevCommon.getDataByWebService("MoveType", "MoveSearch", searchConditions, ref dtMoveType) == RetCode.OK)
            {
                if (!bRCW)
                {
                    for (int i = 0; i < dtMoveType.Rows.Count; i++)
                    {
                        if (dtMoveType.Rows[i]["movementTypeId"].ToString().Equals(MoveTypeKey.returnOrderRCW))
                        {
                            dtMoveType.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
                DevCommon.initCmb(imageComboBoxEdit_MoveType, dtMoveType, "movementTypeId", "movementTypeName", false);
            }
        }

        //故障类型combo初始化
        public void initImageComboBoxEditFaultType()
        {
            DataTable faultType = DevCommon.FaultType();
            if (faultType != null)
            {
                DevCommon.initCmb(imageComboBoxEdit_FaultType, faultType, "faultTypeId", "memo", true);
            }
        }

        private void imgcmbMoveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.imageComboBoxEdit_MoveType.EditValue.Equals(MoveTypeKey.returnOrderRCW))
            {
                textEdit_Quantity.Properties.ReadOnly = true;
                textEdit_RebateAmountDtl.Properties.ReadOnly = true;
                simpleButton_Delete.Enabled = false;
                simpleButton_Save.Enabled = false;
                //冲红退货需要从新取一下订单，因为有可能界面上已经改动了
                if (!string.IsNullOrEmpty(RO.header.docId))
                {
                    //调用接口查询销售订单
                    queryDocIdModel QDI = new queryDocIdModel();
                    QDI.docId = RO.header.baseEntry;
                    if (DevCommon.getDataByWebService("getSalesOrderById", "getSalesOrderById", QDI, ref SO) == RetCode.NG || SO.header == null)
                    {
                        this.gridControl_ProductList.DataSource = RO.detail;
                        updateGridData();
                        displayHeaderData();
                        return;
                    }
                    else
                    {
                        if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                            || !SO.header.fundStatus.Equals(((int)BusinessStatus.CLEARED).ToString()))
                        {
                            FormBase.PromptInformation("销售订单状态不正确！");
                            return;
                        }
                    }
                    //界面显示预订单信息
                    //RO.header.baseEntry = this.textEdit_BaseEntry.Text;
                    ReturnOrderBLL.getReturnOrderFromSalesOrder(SO, ref RO);
                    //this.gridControl_ProductList.DataSource = null;
                    //updateGridData();
                    this.gridControl_ProductList.DataSource = RO.detail;
                    updateGridData();
                    displayHeaderData();
                    gridViewProducetList_FocusedRowChanged(null, null);
                    imageComboBoxEdit_FaultType.Focus();
                }
            }
            else
            {
                textEdit_Quantity.Properties.ReadOnly = false;
                textEdit_RebateAmountDtl.Properties.ReadOnly = false;
                simpleButton_Delete.Enabled = true;
                simpleButton_Save.Enabled = true;
            }
            RO.header.movementTypeId = this.imageComboBoxEdit_MoveType.EditValue.ToString();
        }

        //表格中添加一行（当焦点在最后一行时候）
        //表格中修改一行（当焦点不在最后一行）
        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            //没有商品，则返回
            if (string.IsNullOrEmpty(RODtl.productId))
            {
                imageComboBoxEdit_FaultType.Focus();
                return;
            }

            imgcmbFaultType_SelectedIndexChanged(null, null);

            //明细表中更新 备注（要求仅仅备注可以更改）
            int nDataSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);

            RO.detail[nDataSourceRowIndex].quantity = RODtl.quantity;
            RO.detail[nDataSourceRowIndex].rebateAmount = RODtl.rebateAmount;
            RO.detail[nDataSourceRowIndex].bomAmount = RODtl.bomAmount;
            RO.detail[nDataSourceRowIndex].faultTypeId = RODtl.faultTypeId;
            RO.detail[nDataSourceRowIndex].faultType = RODtl.faultType;
            RO.detail[nDataSourceRowIndex].faultDescription = RODtl.faultDescription;
            RO.detail[nDataSourceRowIndex].memo = RODtl.memo;
            //更新表格中数据并跳转到最后一行空白行
            updateGridData();
            imageComboBoxEdit_FaultType.Focus();
        }

        private void imgcmbFaultType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.textEdit_FaultDescription.Focus();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        private void imgcmbFaultType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RODtl != null && imageComboBoxEdit_FaultType.SelectedItem != null)
                {
                    RODtl.faultTypeId = imageComboBoxEdit_FaultType.EditValue.ToString();
                    RODtl.faultType = imageComboBoxEdit_FaultType.Text;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        private void textEditFaultDscpt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //获取界面中的数据
                    RODtl.faultDescription = this.textEdit_FaultDescription.Text;
                    this.textEdit_MemoDtl.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //快捷键响应
        private void KeyPreview_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    //结算
                    this.simpleButton_Account.PerformClick();
                }
                else if (e.KeyCode.Equals(Keys.F4))
                {
                    //确定
                    this.simpleButton_OK.PerformClick();
                }
                else if (e.KeyCode.Equals(Keys.F5))
                {
                    //保存
                    this.simpleButton_Save.PerformClick();
                }
                else if (e.KeyCode.Equals(Keys.Escape))
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

        private void textEditRebateAmoutDtl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //获取界面中的数据
                    RODtl.rebateAmount = Convert.ToDecimal(this.textEdit_RebateAmountDtl.EditValue);
                    RODtl.rebatePrice = RODtl.rebateAmount / RODtl.quantity;
                    this.imageComboBoxEdit_FaultType.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

    }
}