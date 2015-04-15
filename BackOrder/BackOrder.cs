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

namespace BackOrder
{
    public partial class BackOrder : BaseForm
    {
        BackOrderModel BO = null;                       //退订单
        BackOrderDtlModel BODtl;                        //表格中的明细行
        PreRefundOrderModel PRFO = null;                //订金返还单 
        PreOrderModel PO = null;                        //预订单

        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存
        int nProductSalesPolicyNo = 0;                  //商品销售政策+套包序列
        bool bIsRefreshing = false;                     //数据更新中

        public BackOrder(BaseMainForm formb, BaseForm formp, BackOrderModel backOrder)
        {
            FormBase = formb;
            FormParent = formp;
            BO = backOrder;

            InitializeComponent();
        }

        private void BackOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (BO == null)             //新建订单
                {
                    BO = new BackOrderModel();
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

        //表格焦点变化
        private void gridViewProducetList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int nFocusedSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);
            if (nFocusedSourceRowIndex < 0)
            {
                BODtl = new BackOrderDtlModel();
            }
            else
            {
                BODtl = (BackOrderDtlModel)BO.detail[nFocusedSourceRowIndex].Clone();
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
            this.searchButton_SerialNo.Text = BODtl.serialNo;
            this.searchButton_ProductName.Text = BODtl.productName;
            this.searchButton_ProductId.Text = BODtl.productId;
            this.searchButton_BarCodes.Text = BODtl.barCodes;
            this.textEdit_Quantity.EditValue = BODtl.quantity;
            this.textEdit_UnitPrice.EditValue = BODtl.unitPrice;
            //this.textEdit_RebateAmountDtl.EditValue = BODtl.bomAmount;
            this.textEdit_RebateAmountDtl.EditValue = BODtl.rebateAmount;
            this.textEdit_MemoDtl.Text = BODtl.memo;
        }

        //将明细内容更新至显示数据
        private void displayHeaderData()
        {
            if (BO != null && BO.header != null)
            {
                this.searchButton_MemberPhoneNo.Text = BO.header.memberPhoneMobile;
                this.textEdit_MemberName.Text = BO.header.memberName;
                this.searchButton_SalesPersonId.Text = BO.header.salesId;
                this.textEdit_SalesPersonName.Text = BO.header.salesPersonName;
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
                        this.textEdit_Quantity.EditValue = BODtl.quantity;
                        return;
                    }
                    //计算金额
                    BODtl.quantity = Convert.ToInt32(this.textEdit_Quantity.EditValue);
                    BODtl.rebateAmount = BODtl.quantity * BODtl.rebatePrice;
                    //BODtl.bomAmount = BODtl.rebateAmount;

                    //显示
                    //this.textEdit_RebateAmountDtl.EditValue = BODtl.bomAmount;
                    this.textEdit_RebateAmountDtl.EditValue = BODtl.rebateAmount;
                    //imageComboBoxEdit_FaultType.Focus();
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
                    BODtl.memo = this.textEdit_MemoDtl.Text;
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
                        BO = new BackOrderModel();
                        this.gridControl_ProductList.DataSource = BO.detail;
                        updateGridData();
                        displayHeaderData();
                        return;
                    }

                    //调用接口查询销售订单
                    queryDocIdModel QDI = new queryDocIdModel();
                    QDI.docId = this.textEdit_BaseEntry.Text;
                    if (DevCommon.getDataByWebService("getPreOrderById", "getPreOrderById", QDI, ref PO) == RetCode.NG || PO.header == null)
                    {
                        this.gridControl_ProductList.DataSource = BO.detail;
                        updateGridData();
                        displayHeaderData();
                        return;
                    }
                    else
                    {
                        if (!PO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                            || !PO.header.fundStatus.Equals(((int)BusinessStatus.CLEARED).ToString()))
                        {
                            FormBase.PromptInformation("预订单状态不正确！");
                            return;
                        }
                    }
                    //界面显示预订单信息
                    BackOrderBLL.getBackOrderFromPreOrder(PO, ref BO);

                    this.gridControl_ProductList.DataSource = BO.detail;
                    updateGridData();

                    displayHeaderData();

                    //imageComboBoxEdit_FaultType.Focus();
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

        }

        //保存为草稿
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (bIsQuery && !PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                if (bIsOrderSaved && !BO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))     //草稿可以更改
                {
                    FormBase.PromptInformation("非草稿状态订单不能保存！");
                    return;
                }

                //设置销售订单
                if (!setBackOrder(DocStatus.DRAFT))
                {
                    FormBase.PromptInformation("销售订单数据不正确！");
                    return;
                }
                //收款单和出库单不保存
                PRFO = null;

                //保存销售订单
                //写入数据库
                if (bIsOrderSaved)
                {
                    //if (!SalesOrderBLL.updateSalesOrderAll(true, ref PO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    return;
                    //}
                }
                else
                {
                    //if (!SalesOrderBLL.saveSalesOrderAll(ref PO, ref CO, ref WO))
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
            string currentDocStatus = BO.header.docStatus;
            try
            {
                //数据有效性检查
                //if (!checkSalesOrderValid())
                //{
                //    return;
                //}

                //查询销售订单时候，是否要更新销售订单
                bool bUpdateSalesOrder = false;
                if (bIsOrderSaved && BO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    bUpdateSalesOrder = true;
                }

                //设置退货单
                if (string.IsNullOrEmpty(BO.header.docStatus) || BO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    if (!setBackOrder(DocStatus.VALID))
                    {
                        FormBase.PromptInformation("退货单数据不正确！");
                        BO.header.docStatus = currentDocStatus;
                        return;
                    }
                }

                //退款单
                if (BackOrderBLL.setPreRefundOrder(ref BO, ref PRFO) == DialogResult.Cancel)
                {
                    BO.header.docStatus = currentDocStatus;
                    return;
                }

                //写入数据库
                if (bIsOrderSaved)
                {

                    //if (!SalesOrderBLL.updateSalesOrderAll(bUpdateSalesOrder, ref PO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    PO.header.docStatus = currentDocStatus;
                    //    return;
                    //}
                }
                else
                {
                    if (!BackOrderBLL.saveBackOrderAll(ref BO, ref PRFO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        PO.header.docStatus = currentDocStatus;
                        return;
                    }
                }


                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    BO.header.docStatus = currentDocStatus;
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
                BO.header.docStatus = currentDocStatus;
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
                    BO.header.salesId = null;

                    //查询销售人员
                    querySalesPersonModel SOI = new querySalesPersonModel();
                    SOI.partyId = this.searchButton_SalesPersonId.Text;
                    getSalesPersonModel salesPerson = new getSalesPersonModel();
                    if (DevCommon.getDataByWebService("getSalesPersonBySalesPersonId", "getSalesPersonBySalesPersonId", SOI, ref salesPerson) == RetCode.OK && salesPerson != null)
                    {
                        this.textEdit_SalesPersonName.Text = salesPerson.name;
                        BO.header.salesId = SOI.partyId;
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
        private bool setBackOrder(DocStatus status)
        {
            //表头
            BO.header.docStatus = ((int)status).ToString();
            BO.header.rebateAmount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
            BO.header.primeAmount = Convert.ToDecimal(this.textEdit_PrimeAmount.EditValue);
            BO.header.partyId = LoginInfo.OwnerPartyId;
            BO.header.storeId = LoginInfo.ProductStoreId;
            if (!bIsOrderSaved)
            {
                BO.header.createUserId = LoginInfo.PartyId;
            }
            else
            {
                BO.header.lastUpdateDocDate = DateTime.Now.ToString();
            }
            BO.header.lastUpdateUserId = LoginInfo.PartyId;
            if (string.IsNullOrEmpty(BO.header.preRefundStoreId))
            {
                BO.header.preRefundStoreId = LoginInfo.ProductStoreId;
            }
            if (string.IsNullOrEmpty(BO.header.refundStatus))
            {
                BO.header.refundStatus = ((int)BusinessStatus.NOTCLEARED).ToString();
            }
            //过账日期设置
            BO.header.postingDate = DevCommon.PostingDate();

            //锁定数量
            //bool bLocked = true;
            //if (PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString())           //草稿
            //    || checkEdit_Logistics.Checked)                                         //物流出库
            //{
            //    bLocked = false;
            //}
            //销售明细和价格明细
            for (int i = 0; i < BO.detail.Count; i++)
            {
                BO.detail[i].docId = BO.header.docId;
                BO.detail[i].lineNo = i + 1;
            }

            return true;
        }

        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(BO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.BACK, ref str))
                {
                    return false;
                }
                BO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                BO.header.createDocDate = DateTime.Now.ToString();
                BO.header.lastUpdateDocDate = BO.header.createDocDate;

                //控件可用
                textEdit_BaseEntry.Properties.ReadOnly = false;
                searchButton_SalesPersonId.Properties.ReadOnly = false;
                textEdit_MemoDtl.Properties.ReadOnly = false;
                textEdit_MemoHeader.Properties.ReadOnly = false;
                simpleButton_Add.Enabled = true;
                //simpleButton_Logistics.Enabled = true;
                //simpleButton_Save.Enabled = true;
                simpleButton_Account.Enabled = true;
                simpleButton_OK.Enabled = true;
                //imageComboBoxEdit_FaultType.Properties.Items.Clear();
            }
            else                           //查询订单的情况
            {
                //查询时显示状态和过账时间
                //labelControl_DocStatus.Visible = true;
                //imageComboBoxEdit_DocStatus.Visible = true;
                //labelControl_PostingDate.Visible = true;
                //textEdit_PostingDate.Visible = true;

                //根据目前已有的订单状态决定是否能够后续操作
                if (!BO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))        //草稿状态可以任意修改
                {
                    textEdit_BaseEntry.Properties.ReadOnly = true;
                    searchButton_SalesPersonId.Properties.ReadOnly = true;
                    textEdit_MemoDtl.Properties.ReadOnly = true;
                    textEdit_MemoHeader.Properties.ReadOnly = true;
                    textEdit_Amount.ReadOnly = true;

                    simpleButton_Add.Enabled = false;
                    if (!BO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                        || !BO.header.refundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    {
                        //if (!BO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()) || BO.header.receiveStoreId != PO.header.storeId)
                        //{
                        //    if (BO.header.receiveStoreId != BO.header.storeId)
                        //    {
                        //        //checkEdit_Logistics.Checked = true;
                        //    }
                        //    //simpleButton_Logistics.Enabled = false;
                        //    //simpleButton_Save.Enabled = false;
                        //    simpleButton_Account.Enabled = false;
                        //    simpleButton_OK.Enabled = false;
                        //}
                        //else
                        //{
                        //    //checkEdit_Logistics.Enabled = false;
                        //    //checkEdit_Collection.Checked = false;           //不需要收款
                        //}
                    }
                }
            }

            //画面显示
            this.textEdit_DocId.Text = BO.header.docId;
            this.textEdit_CreateDocDate.Text = BO.header.createDocDate.ToString();
            this.textEdit_BaseEntry.Text = BO.header.baseEntry;
            //this.searchButton_MemberPhoneNo.Text = BO.header.memberPhoneMobile;
            //this.textEdit_MemberName.Text = BO.header.memberName;
            //this.searchButton_SalesPersonId.Text = BO.header.salesId;
            //this.textEdit_SalesPersonName.Text = BO.header.salesPersonName;
            this.searchButton_SerialNo.Text = "";
            this.searchButton_ProductName.Text = "";
            this.searchButton_ProductId.Text = "";
            this.textEdit_Quantity.Text = "";
            this.textEdit_UnitPrice.Text = "";
            this.textEdit_RebateAmountDtl.Text = "";
            this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = BO.header.memo;
            this.textEdit_PrimeAmount.EditValue = BO.header.rebateAmount;
            this.textEdit_Amount.EditValue = BO.header.rebateAmount;
            textEdit_PostingDate.EditValue = BO.header.postingDate;

            //表格明细数据绑定
            this.gridControl_ProductList.DataSource = BO.detail;
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

        //表格中添加一行（当焦点在最后一行时候）
        //表格中修改一行（当焦点不在最后一行）
        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            //没有商品，则返回
            if (string.IsNullOrEmpty(BODtl.productId))
            {
                //imageComboBoxEdit_FaultType.Focus();
                return;
            }

            //明细表中更新 备注（要求仅仅备注可以更改）
            int nDataSourceRowIndex = this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle);

            BO.detail[nDataSourceRowIndex].quantity = BODtl.quantity;
            BO.detail[nDataSourceRowIndex].rebateAmount = BODtl.rebateAmount;
            BO.detail[nDataSourceRowIndex].bomAmount = BODtl.bomAmount;
            BO.detail[nDataSourceRowIndex].memo = BODtl.memo;
            //更新表格中数据并跳转到最后一行空白行
            updateGridData();
            //imageComboBoxEdit_FaultType.Focus();
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
                    BODtl.rebateAmount = Convert.ToDecimal(this.textEdit_RebateAmountDtl.EditValue);
                    BODtl.rebatePrice = BODtl.rebateAmount / BODtl.quantity;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }
    }
}