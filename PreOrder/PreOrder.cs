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
using Newtonsoft.Json;
using Commons.WebService;
using System.Collections;
using Newtonsoft.Json.Converters;
using Commons.JSON;
using Commons.Model;

namespace PreOrder
{
    public partial class PreOrder : BaseForm
    {
        PreOrderModel PO = null;                        //预订单
        PreCollectionOrderModel PCO = null;             //订金预收单  
        PreOrderDtlModel PODtl;                         //表格中的明细行
        PreOrderDtlModel PODtlNew;                      //界面新增的明细
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存
        int nProductSalesPolicyNo = 0;                  //商品销售政策+套包序列
        bool bIsRefreshing = false;                     //数据更新中

        public PreOrder(BaseMainForm formb, BaseForm formp, PreOrderModel preOrder)
        {
            FormBase = formb;
            FormParent = formp;
            PO = preOrder;

            InitializeComponent();
        }

        private void PreOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (PO == null)             //新建订单
                {
                    PO = new PreOrderModel();
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
                if (string.IsNullOrEmpty(PODtl.productId))
                {
                    searchButton_SerialNo.Focus();
                    return;
                }

                if (this.gridView_ProductList.IsLastRow)   //在最后一行，添加时候
                {
                    PODtl.productSalesPolicyNo = ++nProductSalesPolicyNo;
                    //明细表中增加当前显示的商品信息
                    PO.detail.Insert(PO.detail.Count - 1, PODtl);
                    PODtlNew = new PreOrderDtlModel();
                }
                else                                               //不在最后一行
                {
                    PO.detail[this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle)] = PODtl;
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
                    int nProductSalesPolicyNo = PO.detail[nFocusedSourceRowIndex].productSalesPolicyNo;
                    //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    int nCount = 0;
                    if (nFocusedSourceRowIndex + nCount == this.gridView_ProductList.RowCount - 2)
                    {
                        this.gridView_ProductList.MoveLast();
                    }
                    //删除当前行数据(包括套包)
                    PO.detail.RemoveAll(dtl => dtl.productSalesPolicyNo == nProductSalesPolicyNo ? true : false);
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
                PODtl = PODtlNew;
                this.simpleButton_Add.Text = "添加";
                setDetailReadOnly(false);
            }
            else if (this.gridView_ProductList.FocusedRowHandle < this.gridView_ProductList.RowCount)   //不在最后一行是修改明细功能
            {
                PODtl = (PreOrderDtlModel)PO.detail[this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle)].Clone();
                this.simpleButton_Add.Text = "更新";
                if (PODtl.isMainProduct.Equals(MainProductType.yes))            //主商品
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
            //if (bIsQuery && !PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
            if (bIsOrderSaved && !PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
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
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.searchButton_SerialNo.Text = PODtl.serialNo;
            this.searchButton_ProductName.Text = PODtl.productName;
            this.searchButton_ProductId.Text = PODtl.productId;
            this.searchButton_BarCodes.Text = PODtl.barCodes;
            this.textEdit_Quantity.EditValue = PODtl.quantity;
            this.textEdit_UnitPrice.EditValue = PODtl.unitPrice;
            this.textEdit_BomAmountDtl.EditValue = PODtl.bomAmount;
            this.textEdit_MemoDtl.Text = PODtl.memo;
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
                    if (DevCommon.getDataByWebService("getProductByCond", "getProductByCond", QP, ref product) == RetCode.OK && product != null)
                    {
                        //副商品修改时候，可修改串号，其他信息不修改
                        if (!string.IsNullOrEmpty(PODtl.isMainProduct) && PODtl.isMainProduct.Equals(MainProductType.no))
                        {
                            PODtl.serialNo = product.sequenceId;
                            this.searchButton_SerialNo.Text = PODtl.serialNo;
                            simpleButton_Add.PerformClick();
                            return;
                        }
                        PODtl.isMainProduct = MainProductType.yes;
                        PODtl.serialNo = product.sequenceId;
                        PODtl.productName = product.productName;
                        PODtl.productId = product.productId;
                        PODtl.barCodes = product.idValue;
                        PODtl.isSequence = product.isSequence;
                        PODtl.quantity = 1;
                        PODtl.unitPrice = product.suggestedPrice;
                        PODtl.rebatePrice = PODtl.unitPrice;
                        PODtl.rebateAmount = PODtl.rebatePrice * PODtl.quantity;  
                        PODtl.bomAmount = PODtl.rebateAmount;

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
                                this.searchButton_SerialNo.where = PreOrderBLL.getSerialNoWhere(searchButton_ProductId.Text);
                            }
                        }
                        ((SearchButton)sender).ShowForm();
                    }

                }
            }
            catch (System.Exception ex)
            {
                PODtl.serialNo = "";
                PODtl.productName = "";
                PODtl.productId = "";
                PODtl.barCodes = "";
                PODtl.isSequence = "";
                PODtl.quantity = 0;
                PODtl.unitPrice = 0;
                PODtl.rebatePrice = 0;
                PODtl.rebateAmount = 0;
                PODtl.bomAmount = 0;
                displayDetailData();
                return;
            }
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
                    if (quantity <= 0)
                    {
                        this.textEdit_Quantity.EditValue = PODtl.quantity;
                        return;
                    }
                    PODtl.rebatePrice = PODtl.unitPrice;
                    PODtl.quantity = quantity;
                    PODtl.rebateAmount = PODtl.quantity * PODtl.unitPrice;
                    PODtl.bomAmount = PODtl.rebateAmount;

                    //显示
                    this.textEdit_BomAmountDtl.EditValue = PODtl.bomAmount;
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
                    PODtl.bomAmount = Convert.ToDecimal(this.textEdit_BomAmountDtl.EditValue);
                    PODtl.rebateAmount = PODtl.bomAmount;
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
                    PODtl.memo = this.textEdit_MemoDtl.Text;
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
                    PO.header.memberId = null;

                    //会员查询
                    queryMemberModel QM = new queryMemberModel();
                    QM.phoneMobile = this.searchButton_MemberPhoneNo.Text;
                    getMemberModel member = new getMemberModel();
                    if (DevCommon.getDataByWebService("getMemberByPhoneNo", "getMemberByPhoneNo", QM, ref member) == RetCode.OK && member != null)
                    {
                        this.textEdit_MemberName.Text = member.name;
                        PO.header.memberId = member.partyId;
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
                    PO.header.memo = this.textEdit_MemoHeader.Text;
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
                if (bIsOrderSaved && !PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
                {
                    FormBase.PromptInformation("非草稿状态订单不能保存！");
                    return;
                }

                //设置预订单
                if (!setPreOrder(DocStatus.DRAFT))
                {
                    FormBase.PromptInformation("预订单数据不正确！");
                    return;
                }
                //收款单和出库单不保存
                PCO = null;

                //保存预订单
                //写入数据库
                if (bIsOrderSaved)
                {
                    if (!PreOrderBLL.updatePreOrderAll(true, ref PO, ref PCO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        return;
                    }
                }
                else
                {
                    if (!PreOrderBLL.savePreOrderAll(ref PO, ref PCO))
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
            string currentDocStatus = PO.header.docStatus;
            try
            {
                //数据有效性检查
                if (!checkPreOrderValid())
                {
                    return;
                }

                //查询预订单时候，是否要更新预订单
                bool bUpdatePreOrder = false;
                if (bIsOrderSaved && PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    bUpdatePreOrder = true;
                }

                //设置预订单
                if (string.IsNullOrEmpty(PO.header.docStatus) || PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    if (!setPreOrder(DocStatus.VALID))
                    {
                        FormBase.PromptInformation("预订单数据不正确！");
                        PO.header.docStatus = currentDocStatus;
                        return;
                    }
                }


                //收款单
                if (PreOrderBLL.setPreCollectionOrder(ref PO, ref PCO) == DialogResult.Cancel)
                {
                    PO.header.docStatus = currentDocStatus;
                    return;
                }


                //写入数据库
                if (bIsOrderSaved)
                {
                    if (!PreOrderBLL.updatePreOrderAll(bUpdatePreOrder, ref PO, ref PCO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        PO.header.docStatus = currentDocStatus;
                        return;
                    }
                }
                else
                {
                    if (!PreOrderBLL.savePreOrderAll(ref PO, ref PCO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        PO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //打印
                //Sales PreOrderPrint = new Sales(this.textEdit_DocId.Text, false);
                //PreOrderPrint.ShowDialog();

                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    PO.header.docStatus = currentDocStatus;
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
                PO.header.docStatus = currentDocStatus;
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
                    PO.header.salesId = null;

                    //查询销售人员
                    querySalesPersonModel SOI = new querySalesPersonModel();
                    SOI.partyId = this.searchButton_SalesPersonId.Text;
                    getSalesPersonModel salesPerson = new getSalesPersonModel();
                    if (DevCommon.getDataByWebService("getSalesPersonBySalesPersonId", "getSalesPersonBySalesPersonId", SOI, ref salesPerson) == RetCode.OK && salesPerson != null)
                    {
                        this.textEdit_SalesPersonName.Text = salesPerson.name;
                        PO.header.salesId = SOI.partyId;
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
        private bool setPreOrder(DocStatus status)
        {
            //表头
            PO.header.docStatus = ((int)status).ToString();
            PO.header.rebateAmount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
            PO.header.primeAmount = PO.detail.Sum(item => item.unitPrice * item.quantity); ;
            PO.header.partyId = LoginInfo.OwnerPartyId;
            PO.header.storeId = LoginInfo.ProductStoreId;
            if (!bIsOrderSaved)
            {
                PO.header.createUserId = LoginInfo.PartyId;
            }
            else
            {
                PO.header.lastUpdateDocDate = DateTime.Now.ToString();
            }
            PO.header.lastUpdateUserId = LoginInfo.PartyId;
            if (string.IsNullOrEmpty(PO.header.collectionStoreId))
            {
                PO.header.collectionStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(PO.header.fundStatus))
            {
                PO.header.fundStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            if (string.IsNullOrEmpty(PO.header.backStatus))
            {
                PO.header.backStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            //过账日期设置
            PO.header.postingDate = DevCommon.PostingDate();

            //销售明细
            for (int i = 0; i < PO.detail.Count - 1; i++)
            {
                PO.detail[i].docId = PO.header.docId;
                PO.detail[i].lineNo = i + 1;
            }

            return true;
        }

        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(PO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.PRE, ref str))
                {
                    return false;
                }
                PO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                PO.header.createDocDate = DateTime.Now.ToString();
                PO.header.lastUpdateDocDate = PO.header.createDocDate;

                //控件可用
                searchButton_MemberPhoneNo.Properties.ReadOnly = false;
                searchButton_SalesPersonId.Properties.ReadOnly = false;
                searchButton_SerialNo.Properties.ReadOnly = false;
                searchButton_ProductName.Properties.ReadOnly = false;
                searchButton_ProductId.Properties.ReadOnly = false;
                searchButton_BarCodes.Properties.ReadOnly = false;
                textEdit_Quantity.Properties.ReadOnly = false;
                textEdit_BomAmountDtl.Properties.ReadOnly = false;
                textEdit_MemoDtl.Properties.ReadOnly = false;
                textEdit_MemoHeader.Properties.ReadOnly = false;
                simpleButton_Add.Enabled = true;
                simpleButton_Delete.Enabled = true;
                simpleButton_Account.Enabled = true;
                simpleButton_OK.Enabled = true;
            }
            else                           //查询订单的情况
            {
                //查询时显示状态和过账时间


                //根据目前已有的订单状态决定是否能够后续操作
                if (!PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))        //草稿状态可以任意修改
                {
                    searchButton_MemberPhoneNo.Properties.ReadOnly = true;
                    searchButton_SalesPersonId.Properties.ReadOnly = true;
                    searchButton_SerialNo.Properties.ReadOnly = true;
                    searchButton_ProductName.Properties.ReadOnly = true;
                    searchButton_ProductId.Properties.ReadOnly = true;
                    searchButton_BarCodes.Properties.ReadOnly = true;
                    textEdit_Quantity.Properties.ReadOnly = true;
                    textEdit_BomAmountDtl.Properties.ReadOnly = true;
                    textEdit_MemoDtl.Properties.ReadOnly = true;
                    textEdit_MemoHeader.Properties.ReadOnly = true;
                    simpleButton_Add.Enabled = false;
                    simpleButton_Delete.Enabled = false;
                    if (!PO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                        || !PO.header.fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    {
                        //checkEdit_Collection.Enabled = false;
                        //if (!PO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()) || PO.header.deliveryStoreId != PO.header.storeId)
                        //{
                        //    checkEdit_SalesOutWhs.Enabled = false;
                        //    checkEdit_Logistics.Enabled = false;
                        //    if (PO.header.deliveryStoreId != PO.header.storeId)
                        //    {
                        //        checkEdit_Logistics.Checked = true;
                        //    }
                        //    simpleButton_Account.Enabled = false;
                        //    simpleButton_OK.Enabled = false;
                        //}
                        //else
                        //{
                        //    checkEdit_Logistics.Enabled = false;
                        //    checkEdit_Collection.Checked = false;           //不需要收款
                        //}
                        simpleButton_Account.Enabled = false;
                        simpleButton_OK.Enabled = false;
                    }
                }
            }

            //表头明细行数据
            PODtlNew = new PreOrderDtlModel();

            //默认显示空行
            PODtl = new PreOrderDtlModel();
            PO.detail.Add(PODtl);

            //画面显示
            this.textEdit_DocId.Text = PO.header.docId;
            this.textEdit_CreateDocDate.Text = PO.header.createDocDate.ToString();
            this.searchButton_MemberPhoneNo.Text = PO.header.memberPhoneMobile;
            this.textEdit_MemberName.Text = PO.header.memberName;
            this.searchButton_SalesPersonId.Text = PO.header.salesId;
            this.textEdit_SalesPersonName.Text = PO.header.salesPersonName;
            this.searchButton_SerialNo.Text = "";
            this.searchButton_ProductName.Text = "";
            this.searchButton_ProductId.Text = "";
            this.textEdit_Quantity.Text = "";
            this.textEdit_UnitPrice.Text = "";
            this.textEdit_BomAmountDtl.Text = "";
            this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = PO.header.memo;
            this.textEdit_Amount.EditValue = PO.header.rebateAmount;
            imageComboBoxEdit_DocStatus.EditValue = Convert.ToInt32(PO.header.docStatus);
            textEdit_PostingDate.EditValue = PO.header.postingDate;
            FormBase.PromptInformation("");

            //表格明细数据绑定
            this.gridControl_ProductList.DataSource = PO.detail;
            updateGridData();

            //默认显示本门店员工
            DataTable dt = DevCommon.getSalesPersonWhere();
            this.searchButton_SalesPersonId.where = dt;

            //焦点在会员上
            searchButton_MemberPhoneNo.Focus();

            return true;
        }

        //明细有效性检查
        private bool checkPreOrderValid()
        {
            return true;
        }

        //快捷键响应
        private void PreOrder_KeyDown(object sender, KeyEventArgs e)
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