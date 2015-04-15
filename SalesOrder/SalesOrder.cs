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
using Print;


namespace SalesOrder
{    
    public partial class SalesOrder : BaseForm
    {
        PreOrderModel PO;                               //预订单
        PreOrderHeaderModel POheader;                   //预订单表头

        SalesOrderModel SO = null;                      //销售订单
        CollectionOrderModel CO = null;                 //收款单  
        SalesOutWhsOrderModel WO = null;                //销售出库单
        SalesOrderDtlModel SODtl;                       //表格中的明细行
        SalesOrderDtlModel SODtlNew;                    //界面新增的明细
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存
        int nProductSalesPolicyNo = 0;                  //商品销售政策+套包序列
        bool bIsRefreshing = false;                     //数据更新中

        //窗体构造
        public SalesOrder(BaseMainForm formb, BaseForm formp, SalesOrderModel salesOrder)
        {
            FormBase = formb;
            FormParent = formp;
            SO = salesOrder;

            InitializeComponent();
        }

        //窗体加载
        private void SalesOrder_Load(object sender, EventArgs e)
        {
            try
            {                           
                if (SO == null)             //新建订单
                {
                    SO = new SalesOrderModel();
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

        //表格中添加一行（当焦点在最后一行时候）
        //表格中修改一行（当焦点不在最后一行）
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //没有商品，则返回
                if (string.IsNullOrEmpty(SODtl.productId))
                {
                    searchButton_SerialNo.Focus();
                    return;
                }
                
                if (this.gridView_ProductList.IsLastRow)   //在最后一行，添加时候
                {
                    SODtl.productSalesPolicyNo = ++nProductSalesPolicyNo;
                    //明细表中增加当前显示的商品信息
                    SO.detail.Insert(SO.detail.Count - 1, SODtl);
                    //套包副商品添加
                    if (!SalesOrderBLL.addSubProducts(SO.detail.Count - 1, SODtl, ref SO.detail))
                    {
                        return;
                    }
                    SODtlNew = new SalesOrderDtlModel();
                }
                else                                               //不在最后一行
                {
                    //明细表中更新当前显示的商品信息
                    if (SODtl.isMainProduct.Equals(MainProductType.yes))        //主商品
                    {
                        //删除当前行
                        int nFocusedSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);
                        int nProductSalesPolicyNo = SO.detail[nFocusedSourceRowIndex].productSalesPolicyNo;
                        SO.detail.RemoveAll(dtl => dtl.productSalesPolicyNo == nProductSalesPolicyNo ? true : false);
                        //增加新行
                        //明细表中增加当前显示的商品信息
                        SO.detail.Insert(nFocusedSourceRowIndex, SODtl);
                        //套包副商品添加
                        if (!SalesOrderBLL.addSubProducts(nFocusedSourceRowIndex + 1, SODtl, ref SO.detail))
                        {
                            return;
                        }
                    }
                    else                                                        //副商品
                    {
                        SO.detail[this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle)] = SODtl;
                    }

                }
                //更新表格中数据并跳转到最后一行空白行
                updateGridData();
                searchButton_SerialNo.Focus();
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
                if (!this.gridView_ProductList.IsLastRow)           //最后一行空白行不能删除
                {
                    int nFocusedSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);
                    int nProductSalesPolicyNo = SO.detail[nFocusedSourceRowIndex].productSalesPolicyNo;     
                    //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    int nCount = 0;
                    if (SO.detail[nFocusedSourceRowIndex].subProducts.items != null)
                    {
                        nCount = SO.detail[nFocusedSourceRowIndex].subProducts.items.Count;
                    }
                    if (nFocusedSourceRowIndex + nCount == this.gridView_ProductList.RowCount - 2)
                    {
                        this.gridView_ProductList.MoveLast();
                    }
                    //删除当前行数据(包括套包)
                    SO.detail.RemoveAll(dtl => dtl.productSalesPolicyNo == nProductSalesPolicyNo ? true : false);
                    //更新表格中数据并跳转到最后一行空白行
                    updateGridData();
                }
                searchButton_SerialNo.Focus();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);         	
            }
        }

        //表格焦点变化
        private void gridViewProducetList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {                   
            if (this.gridView_ProductList.IsLastRow)            //在最后一行是增加明细功能
            {
                SODtl = SODtlNew;
                this.simpleButton_Add.Text = "添加";
                setDetailReadOnly(false);
            }
            else if (this.gridView_ProductList.FocusedRowHandle < this.gridView_ProductList.RowCount)   //不在最后一行是修改明细功能
            {
                SODtl = (SalesOrderDtlModel)SO.detail[this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle)].Clone();
                this.simpleButton_Add.Text = "更新";
                if (SODtl.isMainProduct.Equals(MainProductType.yes))            //主商品
                {
                    setDetailReadOnly(false);
                }
                else                                                            //副商品或者查询
                {
                    setDetailReadOnly(true);
                }

                //更新状态不重新获取
                bIsRefreshing = true;
            }
            //if (bIsQuery && !SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
            if (bIsOrderSaved && !SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
            {
                setDetailReadOnly(true);
            }

            //更新表头的明细信息
            displayDetailData();
            bIsRefreshing = false;
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
            searchButton_ProductId.Properties.ReadOnly = isReadOnly;
            searchButton_ProductName.Properties.ReadOnly = isReadOnly;
            searchButton_BarCodes.Properties.ReadOnly = isReadOnly;
            textEdit_Quantity.Properties.ReadOnly = isReadOnly;
            textEdit_BomAmountDtl.Properties.ReadOnly = isReadOnly;
            textEdit_MemoDtl.Properties.ReadOnly = isReadOnly;
            imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = isReadOnly;
            imageComboBoxEdit_BOM.Properties.ReadOnly = isReadOnly;
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.searchButton_SerialNo.Text = SODtl.serialNo;
            this.searchButton_ProductName.Text = SODtl.productName;
            this.searchButton_ProductId.Text = SODtl.productId;
            this.searchButton_BarCodes.Text = SODtl.barCodes;
            this.textEdit_Quantity.EditValue = SODtl.quantity;
            this.textEdit_UnitPrice.EditValue = SODtl.unitPrice;
            this.textEdit_BomAmountDtl.EditValue = SODtl.bomAmount;
            this.textEdit_MemoDtl.Text = SODtl.memo;
            this.imageComboBoxEdit_ProductSalesPolicy.EditValue = SODtl.productSalesPolicyId;
            this.imageComboBoxEdit_BOM.EditValue = SODtl.bomId;
            if (SODtl.productSalesPolicys != null)
            {
                initImageCombBoxProductSalesPolicy(SODtl.productSalesPolicys.items);
            }
            if (SODtl.boms != null)
            {
                initImageComboBoxEditBOM(SODtl.boms.items);
            }
        }

        //商品信息查询
        private void searchButtonProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    queryProductModel QP = new queryProductModel();
                    if (sender.Equals(searchButton_ProductId))
                    {
                        QP.productId = this.searchButton_ProductId.Text;
                    }
                    else if (sender.Equals(searchButton_ProductName))
                    {
                        QP.productName = this.searchButton_ProductName.Text;
                        //经过搜索帮助后
                        if (!this.searchButton_ProductName.Value.Equals(this.searchButton_ProductName.Text))
                        {
                            QP.productId = this.searchButton_ProductName.Value;
                        }                        
                    }
                    else if (sender.Equals(searchButton_SerialNo))
                    {
                        QP.sequenceId = this.searchButton_SerialNo.Text;
                        //在有商品ID的情况下，需要连同商品ID一起查询
                        if (!string.IsNullOrEmpty(searchButton_ProductId.Text))
                        {
                            QP.productId = this.searchButton_ProductId.Text;
                        }
                    }
                    else if (sender.Equals(searchButton_BarCodes))
                    {
                        QP.idValue = this.searchButton_BarCodes.Text;
                    }                

                    //查询商品信息
                    QP.productStoreId = LoginInfo.ProductStoreId;
                    QP.movementTypeId = MoveTypeKey.salesOrder;
                    getProductModel product = new getProductModel();
                    if (DevCommon.getDataByWebService("getProductByCond", "getProductByCond", QP, ref product) == RetCode.OK && product!=null)
                    {
                        //副商品修改时候，可修改串号，其他信息不修改
                        if (!string.IsNullOrEmpty(SODtl.isMainProduct) && SODtl.isMainProduct.Equals(MainProductType.no))
                        {
                            SODtl.serialNo = product.sequenceId;
                            this.searchButton_SerialNo.Text = SODtl.serialNo;
                            simpleButton_Add.PerformClick();
                            return;
                        }
                        SODtl.isMainProduct = MainProductType.yes;
                        SODtl.productSalesPolicys.items = product.productSalesPolicys;
                        SODtl.serialNo = product.sequenceId;
                        SODtl.productName = product.productName;
                        SODtl.productId = product.productId;
                        SODtl.barCodes = product.idValue;
                        SODtl.isSequence = product.isSequence;
                        SODtl.isOccupied = product.isOccupied;
                        SODtl.facilityId = product.facilityId;
                        if (!string.IsNullOrEmpty(SODtl.isSequence) && SODtl.isSequence.Equals(ProductSequenceManager.sequenceY))
                        {
                            if (SODtl.isOccupied.Equals(ProductOccupiedManager.occupiedY))
                            {
                                SODtl.stockQuantity = 0;
                            }
                            else
                            {
                                SODtl.stockQuantity = 1;
                            }
                        }
                        else
                        {
                            SODtl.stockQuantity = product.onhand - product.promise;
                        }
                        SODtl.quantity = 1;

                        initImageCombBoxProductSalesPolicy(SODtl.productSalesPolicys.items);
                        imageComboBoxEditProductSalesPolicy_SelectedIndexChanged(null, null);
                        imageComboBoxEditBOM_SelectedIndexChanged(null, null);
                        imageComboBoxEdit_ProductSalesPolicy.Focus();
                        //画面显示
                        displayDetailData();
                    }
                    else
                    {
                        //输入串号时，如果有ProductId，则作为搜索帮助的初始条件
                        if (sender.Equals(searchButton_SerialNo))
                        {
                            this.searchButton_SerialNo.where = null;
                            if (!string.IsNullOrEmpty(searchButton_ProductId.Text))
                            {                                                            
                                this.searchButton_SerialNo.where = SalesOrderBLL.getSerialNoWhere(searchButton_ProductId.Text);
                            }
                        }
                        ((SearchButton)sender).ShowForm();
                    }

                }
            }
            catch (System.Exception ex)
            {
                imageComboBoxEdit_ProductSalesPolicy.Properties.Items.Clear();
                imageComboBoxEdit_BOM.Properties.Items.Clear();
                SODtl.serialNo = "";
                SODtl.productName = "";
                SODtl.productId = "";
                SODtl.barCodes = "";
                SODtl.isSequence = "";
                SODtl.isOccupied = "";
                SODtl.quantity = 0;
                SODtl.unitPrice = 0;
                SODtl.rebatePrice = 0;
                SODtl.rebateAmount = 0;
                SODtl.bomAmount = 0;
                displayDetailData();
                return;       	
            }          
        }

        //销售政策combo初始化
        void initImageCombBoxProductSalesPolicy(List<ProductSalesPolicyModel> productSalesPolicys)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            if (productSalesPolicys != null)
            {
                foreach (ProductSalesPolicyModel item in productSalesPolicys)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.policyName;
                    dr["Value"] = item.productSalesPolicyId;
                    dt.Rows.Add(dr);
                }
            }
            DevCommon.initCmb(this.imageComboBoxEdit_ProductSalesPolicy, dt, false);
        }

        //数量输入后
        private void textEditQuantity_KeyDown(object sender, KeyEventArgs e)
        {
             try
            {
                if (e.KeyCode.Equals(Keys.Enter)) 
                {
                     //计算金额
                    int quantity = Convert.ToInt32(this.textEdit_Quantity.EditValue);
                    if (quantity <=0)
                    {
                        this.textEdit_Quantity.EditValue = SODtl.quantity;
                        return;
                    }
                    SODtl.rebatePrice = SODtl.unitPrice;
                    SODtl.quantity = quantity;
                    SODtl.rebateAmount = SODtl.quantity * SODtl.unitPrice;
                    SODtl.bomAmount = SODtl.rebateAmount;
                    if (SODtl.subProducts.items != null)
                    {
                        foreach (getProductModel item in SODtl.subProducts.items)
                        {
                            item.rebatePrice = item.unitPrice;
                            item.quantity = SODtl.quantity;
                            item.rebateAmount = item.rebatePrice * item.quantity;
                            SODtl.bomAmount += item.rebateAmount;
                        }
                    }

                    //显示
                    this.textEdit_BomAmountDtl.EditValue = SODtl.bomAmount;
                    textEdit_BomAmountDtl.Focus();
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
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //套包金额均摊
                    if (SODtl.isMainProduct.Equals(MainProductType.yes))
                    {
                        decimal subProductPriceSum = 0;
                        decimal currentBomAmout = Convert.ToDecimal(this.textEdit_BomAmountDtl.EditValue);
                        SODtl.rebateAmount = SODtl.rebateAmount + (currentBomAmout - SODtl.bomAmount) * SODtl.mainRatio / 100;
                        SODtl.rebatePrice = SODtl.rebateAmount / SODtl.quantity;
                        SODtl.bomAmount = currentBomAmout;
                        if (SODtl.subProducts.items != null)
                        {
                            subProductPriceSum = SODtl.subProducts.items.Sum(item => item.unitPrice);
                            if (subProductPriceSum > 0)
                            {
                                foreach (getProductModel item in SODtl.subProducts.items)
                                {
                                    item.quantity = SODtl.quantity;
                                    item.rebateAmount = (currentBomAmout - SODtl.rebateAmount) * item.unitPrice / subProductPriceSum;
                                    item.rebatePrice = item.rebateAmount / item.quantity;
                                    item.bomAmount = SODtl.bomAmount;
                                }
                            }

                        }
                    }

                    textEdit_MemoDtl.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);         	
            }
        }

        //备注中回车键按下
        private void textEditMemo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //获取界面中的数据
                    SODtl.memo = this.textEdit_MemoDtl.Text;
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
                if (this.gridView_ProductList.RowCount > 1)
                {
                    this.textEdit_Amount.EditValue = Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
                    this.simpleButton_OK.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);         	    	
            }

        }

        //预订单查询
        private void textEditBaseEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(this.textEdit_BaseEntry.Text))
                    {
                        searchButton_SerialNo.Focus();
                        return;
                    }
                    
                    //调用接口查询预订单表头
                    //queryDocIdModel QDI = new queryDocIdModel();
                    //if (DevCommon.getDataByWebService("getPreOrderHeaderById", "getPreOrderHeaderById", QDI, ref POheader) == RetCode.NG)
                    //{
                    //    return;
                    //}
                    ////界面显示预订单信息
                    //SO.header.baseEntry = this.textEdit_BaseEntry.Text;
                    searchButton_SerialNo.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);         	
            }
        }

        //会员输入后
        private void searchButtonMemberPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(this.searchButton_MemberPhoneNo.Text))
                    {
                        searchButton_SalesPersonId.Focus();
                        return;
                    }
                    this.textEdit_MemberName.Text = "";
                    SO.header.memberId = null;

                    //会员查询
                    queryMemberModel QM = new queryMemberModel();
                    QM.phoneMobile = this.searchButton_MemberPhoneNo.Text;
                    getMemberModel member = new getMemberModel();
                    if (DevCommon.getDataByWebService("getMemberByPhoneNo", "getMemberByPhoneNo", QM, ref member) == RetCode.OK && member != null)
                    {
                        this.textEdit_MemberName.Text = member.name;
                        SO.header.memberId = member.partyId;
                        searchButton_SalesPersonId.Focus();
                    }
                    else
                    {
                        ((SearchButton)sender).ShowForm();
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.searchButton_MemberPhoneNo.Text = "";
                return;          	
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
                    SO.header.memo = this.textEdit_MemoHeader.Text;
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
                if (bIsOrderSaved && !SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
                {
                    FormBase.PromptInformation("非草稿状态订单不能保存！");
                    return;
                }

                //设置销售订单
                if (!setSalesOrder(DocStatus.DRAFT))
                {
                    FormBase.PromptInformation("销售订单数据不正确！");
                    return;
                }
                //收款单和出库单不保存
                CO = null;
                WO = null;

                //保存销售订单
                //写入数据库
                if (bIsOrderSaved)
                {
                    if (!SalesOrderBLL.updateSalesOrderAll(true, ref SO, ref CO, ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        return;
                    }
                }
                else
                {
                    if (!SalesOrderBLL.saveSalesOrderAll(ref SO, ref CO, ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        return;
                    }
                    bIsOrderSaved = true;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //确定
        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            string currentDocStatus = SO.header.docStatus;
            try
            {
                //数据有效性检查
                if (!checkSalesOrderValid())
                {
                    return;
                }

                //查询销售订单时候，是否要更新销售订单
                bool bUpdateSalesOrder = false;
                if (bIsOrderSaved && SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    bUpdateSalesOrder = true;
                }

                //数据有效性检查---物流数据
                if (checkEdit_Logistics.Checked && string.IsNullOrEmpty(SO.header.logisticsAddress))
                {
                    LogisticsAddress dialog = new LogisticsAddress();
                    dialog.logisticsAddress = SO.header.logisticsAddress;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SO.header.logisticsAddress = dialog.logisticsAddress;
                    }
                    else
                    {
                        return;
                    }
                }

                //设置销售订单
                if (string.IsNullOrEmpty(SO.header.docStatus) || SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    if (!setSalesOrder(DocStatus.VALID))
                    {
                        FormBase.PromptInformation("销售订单数据不正确！");
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }


                //收款单
                if (checkEdit_Collection.Checked)
                {
                    if (SalesOrderBLL.setCollectionOrder(ref SO, ref CO) == DialogResult.Cancel)
                    {
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //销售出库单
                if (checkEdit_SalesOutWhs.Checked)
                {
                    SO.header.deliveryStoreId = LoginInfo.ProductStoreId;
                    SO.header.logisticsAddress = null;
                    
                    //新生成的销售订单，或者从草稿变更成确定的销售订单同时出库的时候不锁库存
                    bool bUnlocked = true;
                    if (string.IsNullOrEmpty(currentDocStatus) || currentDocStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                    {
                        bUnlocked = false;
                    }

                    if (!SalesOrderBLL.setSalesOutWhsOrder(bUnlocked, ref SO, ref WO))
                    {
                        FormBase.PromptInformation("销售出库单数据不正确！");
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }
                //物流出库
                if (checkEdit_Logistics.Checked)
                {
                    WO = null;
                }


                //写入数据库
                if (bIsOrderSaved)
                {
                    if (!SalesOrderBLL.updateSalesOrderAll(bUpdateSalesOrder, ref SO, ref CO, ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }
                else
                {
                    if (!SalesOrderBLL.saveSalesOrderAll(ref SO, ref CO, ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        SO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //打印
                Sales SalesOrderPrint = new Sales(this.textEdit_DocId.Text, false);
                SalesOrderPrint.ShowDialog();

                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    SO.header.docStatus = currentDocStatus;
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
                SO.header.docStatus = currentDocStatus;
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
                    SO.header.salesId = null;

                    //查询销售人员
                    querySalesPersonModel SOI = new querySalesPersonModel();
                    SOI.partyId = this.searchButton_SalesPersonId.Text;
                    getSalesPersonModel salesPerson = new getSalesPersonModel();
                    if (DevCommon.getDataByWebService("getSalesPersonBySalesPersonId", "getSalesPersonBySalesPersonId", SOI, ref salesPerson) == RetCode.OK && salesPerson != null)
                    {
                        this.textEdit_SalesPersonName.Text = salesPerson.name;
                        SO.header.salesId = SOI.partyId;
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

        //付款选择
        private void checkEditCollection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_Collection.Checked)
            {
                checkEdit_SalesOutWhs.Enabled = true;
                checkEdit_Logistics.Enabled = true;
            }
            else if (checkEdit_Collection.Enabled)
            {
                checkEdit_SalesOutWhs.Checked = false;
                checkEdit_Logistics.Checked = false;
                checkEdit_SalesOutWhs.Enabled = false;
                checkEdit_Logistics.Enabled = false;
            }
        }

        //销售出库选择
        private void checkEditSalesOutWhs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_SalesOutWhs.Checked)
            {
                SO.header.deliveryStoreId = LoginInfo.ProductStoreId;
                checkEdit_Logistics.Checked = false;
            }
        }

        //物流出库选择
        private void checkEditLogistics_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_Logistics.Checked)
            {
                simpleButton_Logistics.Enabled = true;
                SO.header.deliveryStoreId = DevCommon.getStoreSetting("OwnerPartyFacility", "ID");
                checkEdit_SalesOutWhs.Checked = false;
            }
            else
            {
                simpleButton_Logistics.Enabled = false;
            }
        }

        //销售政策选择
        private void imageComboBoxEditProductSalesPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bIsRefreshing)
                {
                    this.imageComboBoxEdit_ProductSalesPolicy.EditValue = SODtl.productSalesPolicyId;
                    return;
                }
                
                if (this.imageComboBoxEdit_ProductSalesPolicy.SelectedItem != null)
                {
                    SODtl.productSalesPolicyId = imageComboBoxEdit_ProductSalesPolicy.EditValue.ToString();
                    SODtl.productSalesPolicyName = imageComboBoxEdit_ProductSalesPolicy.Text;
                    ProductSalesPolicyModel productSalesPolicy = SODtl.productSalesPolicys.items.Find(delegate(ProductSalesPolicyModel item) { return item.productSalesPolicyId.Equals(SODtl.productSalesPolicyId); });
                    SODtl.mainRatio = productSalesPolicy.mainRatio;

                    queryBOMModel QB = new queryBOMModel();
                    QB.productStoreId = LoginInfo.ProductStoreId;
                    QB.productSalesPolicyId = this.imageComboBoxEdit_ProductSalesPolicy.EditValue.ToString();
                    List<getBOMModel> BOM = new List<getBOMModel>();
                    if (DevCommon.getDataByWebService("getBomByCond", "getBomByCond", QB, ref BOM) == RetCode.NG)
                    {
                        return;
                    }
                    else
                    {
                        //商品价格确定
                        SODtl.unitPrice = SalesOrderBLL.getProductUnitPrice(SODtl.productSalesPolicyId, SODtl.productSalesPolicys.items, ref SODtl.productSalesPrices.items);
                        SODtl.rebatePrice = SODtl.unitPrice;
                        SODtl.rebateAmount = SODtl.rebatePrice * SODtl.quantity;                        
                        SODtl.boms.items = BOM;
                        initImageComboBoxEditBOM(SODtl.boms.items);
                        imageComboBoxEditBOM_SelectedIndexChanged(null, null);
                    }
                }
                else
                {
                    SODtl.productSalesPolicyId = null;
                    SODtl.productSalesPolicyName = null;
                    SODtl.productSalesPolicys.items = null;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //套包选择
        private void imageComboBoxEditBOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bIsRefreshing)
                {
                    this.imageComboBoxEdit_BOM.EditValue = SODtl.bomId;
                    return;
                }

                if (this.imageComboBoxEdit_BOM.SelectedItem != null)
                {
                    SODtl.bomId = this.imageComboBoxEdit_BOM.EditValue.ToString();
                    SODtl.bomName = imageComboBoxEdit_BOM.Text;
                    querySubProductsModel QSP = new querySubProductsModel();
                    QSP.productStoreId = LoginInfo.ProductStoreId;
                    QSP.bomId = this.imageComboBoxEdit_BOM.EditValue.ToString();
                    QSP.movementTypeId = MoveTypeKey.salesOrder;
                    List<getProductModel> products = new List<getProductModel>();
                    if (DevCommon.getDataByWebService("getProductByBomCond", "getProductByBomCond", QSP, ref products) == RetCode.NG)
                    {
                        return;
                    }
                    else
                    {
                        SODtl.subProducts.items = products;
                        SODtl.bomAmount = SODtl.rebateAmount;
                        //副商品价格确定
                        foreach (getProductModel item in SODtl.subProducts.items)
                        {
                            item.unitPrice = SalesOrderBLL.getProductUnitPrice(SODtl.productSalesPolicyId, item.productSalesPolicys, ref item.priceList);
                            item.rebatePrice = item.unitPrice;
                            item.quantity = SODtl.quantity;
                            item.rebateAmount = item.rebatePrice * SODtl.quantity;
                            //item.bomAmount = item.rebateAmount;
                            SODtl.bomAmount += item.rebateAmount;
                        }
                    }       
                }
                else
                {
                    SODtl.bomId = null;
                    SODtl.bomName = null;
                    SODtl.subProducts.items = null;
                    SODtl.bomAmount = SODtl.rebateAmount;
                }
                //价格显示
                textEdit_BomAmountDtl.EditValue = SODtl.bomAmount;
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //套包Combo初始化
        void initImageComboBoxEditBOM(List<getBOMModel> BOMs)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Text", typeof(string));

            if (BOMs != null)
            {
                foreach (getBOMModel item in BOMs)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.bomName;
                    dr["Value"] = item.bomId;
                    dt.Rows.Add(dr);
                }
            }

            DevCommon.initCmb(this.imageComboBoxEdit_BOM, dt, false);
        }

        //将界面内容设置到订单对象
        private bool setSalesOrder(DocStatus status)
        {
            //表头
            SO.header.docStatus = ((int)status).ToString();
            SO.header.rebateAmount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
            SO.header.primeAmount = SO.detail.Sum(item => item.unitPrice * item.quantity); ;
            SO.header.partyId = LoginInfo.OwnerPartyId;
            SO.header.storeId = LoginInfo.ProductStoreId;
            if (!bIsOrderSaved)
            {
                SO.header.createUserId = LoginInfo.PartyId;
            }
            else
            {
                SO.header.lastUpdateDocDate = DateTime.Now.ToString();
            }
            SO.header.lastUpdateUserId = LoginInfo.PartyId;
            if (string.IsNullOrEmpty(SO.header.collectionStoreId))
            {
                SO.header.collectionStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(SO.header.deliveryStoreId))
            {
                SO.header.deliveryStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(SO.header.fundStatus))
            {
                SO.header.fundStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            if (string.IsNullOrEmpty(SO.header.warehouseStatus))
            {
                SO.header.warehouseStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            if (string.IsNullOrEmpty(SO.header.returnStatus))
            {
                SO.header.returnStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            SO.header.movementTypeId = MoveTypeKey.salesOrder;
            //过账日期设置
            SO.header.postingDate = DevCommon.PostingDate();

            //锁定数量
            bool bLocked = true;
            if (SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString())           //草稿
                || checkEdit_Logistics.Checked                                          //物流出库
                || checkEdit_SalesOutWhs.Checked)                                       //销售出库
            {
                bLocked = false;
            }
            //销售明细和价格明细
            SO.priceDtl.Clear();
            for (int i = 0; i < SO.detail.Count - 1; i++)
            {            
                SO.detail[i].docId = SO.header.docId;
                SO.detail[i].lineNo = i + 1;
                if (bLocked)
                {
                    SO.detail[i].lockedQuantity = SO.detail[i].quantity;
                }
                else
                {
                    SO.detail[i].lockedQuantity = 0;
                }

                //价格列表
                foreach (ProductPriceTypeModel item in SO.detail[i].productSalesPrices.items)
                {
                    SalesOrderPriceDtlModel SOPdtl = new SalesOrderPriceDtlModel();
                    SOPdtl.docId = SO.detail[i].docId;
                    SOPdtl.lineNo = SO.detail[i].lineNo;
                    SOPdtl.productPriceTypeId = item.productPriceTypeId;
                    SOPdtl.productPriceRuleId = item.productPriceRuleId;
                    SOPdtl.price = item.price;
                    SO.priceDtl.Add(SOPdtl);
                }
                //物流出库不写仓库
                if (checkEdit_Logistics.Checked)
                {
                    SO.detail[i].facilityId = null;
                }
            }

            return true;
        }

        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(SO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.SALES, ref str))
                {
                    return false;
                }
                SO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                SO.header.createDocDate = DateTime.Now.ToString();
                SO.header.lastUpdateDocDate = SO.header.createDocDate;

                //控件可用
                textEdit_BaseEntry.Properties.ReadOnly = false;
                searchButton_MemberPhoneNo.Properties.ReadOnly = false;
                searchButton_SalesPersonId.Properties.ReadOnly = false;
                searchButton_SerialNo.Properties.ReadOnly = false;
                searchButton_ProductName.Properties.ReadOnly = false;
                searchButton_ProductId.Properties.ReadOnly = false;
                searchButton_BarCodes.Properties.ReadOnly = false;
                imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = false;
                imageComboBoxEdit_BOM.Properties.ReadOnly = false;
                textEdit_Quantity.Properties.ReadOnly = false;
                textEdit_BomAmountDtl.Properties.ReadOnly = false;
                textEdit_MemoDtl.Properties.ReadOnly = false;
                textEdit_MemoHeader.Properties.ReadOnly = false;
                simpleButton_Add.Enabled = true;
                simpleButton_Delete.Enabled = true;
                checkEdit_Collection.Enabled = true;
                checkEdit_SalesOutWhs.Enabled = true;
                checkEdit_Logistics.Enabled = true;
                simpleButton_Account.Enabled = true;
                simpleButton_OK.Enabled = true;
            }
            else                           //查询订单的情况
            {
                //查询时显示状态和过账时间


                //根据目前已有的订单状态决定是否能够后续操作
                if (!SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))        //草稿状态可以任意修改
                {
                    textEdit_BaseEntry.Properties.ReadOnly = true;
                    searchButton_MemberPhoneNo.Properties.ReadOnly = true;
                    searchButton_SalesPersonId.Properties.ReadOnly = true;
                    searchButton_SerialNo.Properties.ReadOnly = true;
                    searchButton_ProductName.Properties.ReadOnly = true;
                    searchButton_ProductId.Properties.ReadOnly = true;
                    searchButton_BarCodes.Properties.ReadOnly = true;
                    imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = true;
                    imageComboBoxEdit_BOM.Properties.ReadOnly = true;
                    textEdit_Quantity.Properties.ReadOnly = true;
                    textEdit_BomAmountDtl.Properties.ReadOnly = true;
                    textEdit_MemoDtl.Properties.ReadOnly = true;
                    textEdit_MemoHeader.Properties.ReadOnly = true;
                    simpleButton_Add.Enabled = false;
                    simpleButton_Delete.Enabled = false;
                    if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                        || !SO.header.fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    {
                        checkEdit_Collection.Enabled = false;
                        if (!SO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()) || SO.header.deliveryStoreId != SO.header.storeId)
                        {
                            checkEdit_SalesOutWhs.Enabled = false;
                            checkEdit_Logistics.Enabled = false;
                            if (SO.header.deliveryStoreId != SO.header.storeId)
                            {
                                checkEdit_Logistics.Checked = true;
                            }
                            simpleButton_Account.Enabled = false;
                            simpleButton_OK.Enabled = false;
                        }
                        else
                        {
                            checkEdit_Logistics.Enabled = false;
                            checkEdit_Collection.Checked = false;           //不需要收款
                        }
                    }
                }
            }

            //表头明细行数据
            SODtlNew = new SalesOrderDtlModel();

            //默认显示空行
            SODtl = new SalesOrderDtlModel();
            SO.detail.Add(SODtl);

            //画面显示
            this.textEdit_DocId.Text = SO.header.docId;
            this.textEdit_CreateDocDate.Text = SO.header.createDocDate.ToString();
            this.textEdit_BaseEntry.Text = SO.header.baseEntry;
            this.searchButton_MemberPhoneNo.Text = SO.header.memberPhoneMobile;
            this.textEdit_MemberName.Text = SO.header.memberName;
            this.searchButton_SalesPersonId.Text = SO.header.salesId;
            this.textEdit_SalesPersonName.Text = SO.header.salesPersonName;
            this.searchButton_SerialNo.Text = "";
            this.searchButton_ProductName.Text = "";
            this.searchButton_ProductId.Text = "";
            this.imageComboBoxEdit_ProductSalesPolicy.Text = "";
            this.imageComboBoxEdit_BOM.Text = "";
            this.textEdit_Quantity.Text = "";
            this.textEdit_UnitPrice.Text = "";
            this.textEdit_BomAmountDtl.Text = "";
            this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = SO.header.memo;
            this.textEdit_Amount.EditValue = SO.header.rebateAmount;
            imageComboBoxEdit_DocStatus.EditValue = Convert.ToInt32(SO.header.docStatus);
            textEdit_PostingDate.EditValue = SO.header.postingDate;
            imageComboBoxEdit_ProductSalesPolicy.Properties.Items.Clear();
            imageComboBoxEdit_BOM.Properties.Items.Clear();
            FormBase.PromptInformation("");

            //表格明细数据绑定
            this.gridControl_ProductList.DataSource = SO.detail;
            updateGridData();

            //默认显示本门店员工
            DataTable dt = DevCommon.getSalesPersonWhere();
            this.searchButton_SalesPersonId.where = dt;

            //焦点在会员上
            searchButton_MemberPhoneNo.Focus();

            return true;
        }

        //明细有效性检查
        private bool checkSalesOrderValid()
        {
            //物流出库不检查
            if (checkEdit_Logistics.Checked)
            {
                return true;
            }
            
            //数量合计值
            int productSum = 0;

            //明细序列号和数量检查                
            foreach (SalesOrderDtlModel item in SO.detail)
            {
                if (string.IsNullOrEmpty(item.productId))
                {
                    continue;
                }

                //串号检查
                if (!string.IsNullOrEmpty(item.isSequence) && item.isSequence.Equals(ProductSequenceManager.sequenceY) && string.IsNullOrEmpty(item.serialNo))
                {
                    FormBase.PromptInformation(item.productName + "请输入串号！");
                    return false;
                }

                //根据商品ID取数量
                if (bIsOrderSaved)
                {
                    queryProductModel QP = new queryProductModel();
                    QP.productStoreId = LoginInfo.ProductStoreId;
                    QP.movementTypeId = MoveTypeKey.salesOrder;
                    QP.productId = item.productId;
                    getProductModel product = new getProductModel();
                    if (DevCommon.getDataByWebService("getProductByCond", "getProductByCond", QP, ref product) == RetCode.OK && product != null)
                    {
                        if (!string.IsNullOrEmpty(item.isSequence) && item.isSequence.Equals(ProductSequenceManager.sequenceY))
                        {
                            if (!string.IsNullOrEmpty(item.isOccupied) && item.isOccupied.Equals(ProductOccupiedManager.occupiedY))
                            {
                                item.stockQuantity = 0;
                            }
                            else
                            {
                                item.stockQuantity = 1;
                            }
                        }
                        else
                        {
                            item.stockQuantity = product.onhand - product.promise;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                //商品ID数量检查
                if (!string.IsNullOrEmpty(item.isSequence) && item.isSequence.Equals(ProductSequenceManager.sequenceY))
                {
                    productSum = item.quantity;         //串号库存不合计
                }
                else
                {
                    productSum = SO.detail.Sum(dtl => dtl.productId == item.productId ? dtl.quantity : 0);
                }


                if (!string.IsNullOrEmpty(item.facilityId)&&productSum > item.stockQuantity)
                {
                    FormBase.PromptInformation(item.productName + "数量大于可用库存数！");
                    return false;
                }
            }
            return true;
        }

        //销售政策回车
        private void imgCmbProductSalesPolicy_KeyDown(object sender, KeyEventArgs e)
        {
            imageComboBoxEdit_BOM.Focus();
        }

        //副商品list回车
        private void imgCmbBOM_KeyDown(object sender, KeyEventArgs e)
        {
            textEdit_Quantity.Focus();
        }

        //物流地址按钮按下
        private void simpleButton_Logistics_Click(object sender, EventArgs e)
        {
            LogisticsAddress dialog = new LogisticsAddress();
            dialog.logisticsAddress = SO.header.logisticsAddress;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SO.header.logisticsAddress = dialog.logisticsAddress;
            }
        }

        //快捷键响应
        private void SalesOrder_KeyDown(object sender, KeyEventArgs e)
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


    }
}
