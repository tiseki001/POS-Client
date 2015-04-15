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
using Commons.DLL;
using Commons.Model;
using Commons.Model.Stock;

namespace SalesOutWhsOrder
{
    public partial class SalesOutWhsOrder : BaseForm
    {
        SalesOutWhsOrderModel WO = null;                //销售出库单
        SalesOrderModel SO = null;                      //销售订单

        SalesOutWhsOrderDtlModel WODtl;                 //表格中的明细行
        //SalesOrderDtl WODtlNew;                       //界面新增的明细
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存

        SalesOrderAllModel SOAll = new SalesOrderAllModel();

        public SalesOutWhsOrder(BaseMainForm formb, BaseForm formp, SalesOutWhsOrderModel salesOutWhsOrder)
        {
            FormBase = formb;
            FormParent = formp;
            WO = salesOutWhsOrder;
;
            InitializeComponent();
        }

        private void SalesOutWhsOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (WO == null)             //新建订单
                {
                    WO = new SalesOutWhsOrderModel();
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

       //保存为草稿
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //saveSalesOrder(DocStatus.DRAFT);
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
                if (string.IsNullOrEmpty(WODtl.productId))
                {
                    return;
                }

                //明细表中更新 备注（要求仅仅备注可以更改）
                WO.item[this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle)].memo = WODtl.memo;
                //更新表格中数据并跳转到最后一行空白行
                updateGridData();
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //表格中删除一行(暂不用)
        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_ProductList.RowCount > 0)
                {
                    WO.item.RemoveAt(this.gridView_ProductList.GetDataSourceRowIndex(this.gridView_ProductList.FocusedRowHandle));
                    //更新表格中数据并跳转到最后一行空白行
                    updateGridData();
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
                WODtl = new SalesOutWhsOrderDtlModel();
            }
            else
            {
                WODtl = (SalesOutWhsOrderDtlModel)WO.item[nFocusedSourceRowIndex].Clone();
            }

            //更新表头的明细信息
            displayDetailData();
        }

        //初始化界面显示
        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(WO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.SALESOUTWHS, ref str))
                {
                    return false;
                }
                WO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                WO.header.docDate = DateTime.Now.ToString();
                WO.header.updateDate = WO.header.docDate;

                //控件可用
                //textEdit_BaseEntry.Properties.ReadOnly = false;
                //searchButton_MemberPhoneNo.Properties.ReadOnly = false;
                //searchButton_SalesPersonId.Properties.ReadOnly = false;
                //searchButton_SerialNo.Properties.ReadOnly = false;
                //searchButton_ProductName.Properties.ReadOnly = false;
                //searchButton_ProductId.Properties.ReadOnly = false;
                //searchButton_BarCodes.Properties.ReadOnly = false;
                //imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = false;
                //imageComboBoxEdit_BOM.Properties.ReadOnly = false;
                //textEdit_Quantity.Properties.ReadOnly = false;
                //textEdit_BomAmountDtl.Properties.ReadOnly = false;
                //textEdit_MemoDtl.Properties.ReadOnly = false;
                //textEdit_MemoHeader.Properties.ReadOnly = false;
                //simpleButton_Add.Enabled = true;
                //simpleButton_Delete.Enabled = true;
                //checkEdit_SalesOutWhs.Enabled = true;
                //checkEdit_Logistics.Enabled = true;
                //simpleButton_Logistics.Enabled = true;
                ////simpleButton_Save.Enabled = true;
                //simpleButton_Account.Enabled = true;
                //simpleButton_OK.Enabled = true;
            }
            else                           //查询订单的情况
            {
                //查询时显示状态和过账时间
                //labelControl_DocStatus.Visible = true;
                //imageComboBoxEdit_DocStatus.Visible = true;
                //labelControl_PostingDate.Visible = true;
                //textEdit_PostingDate.Visible = true;

                //根据目前已有的订单状态决定是否能够后续操作
                if (WO.header.docStatus != ((int)DocStatus.DRAFT).ToString())        //只有草稿状态是可以继续操作的
                {
                    //textEdit_BaseEntry.Properties.ReadOnly = true;
                    //searchButton_MemberPhoneNo.Properties.ReadOnly = true;
                    //searchButton_SalesPersonId.Properties.ReadOnly = true;
                    //searchButton_SerialNo.Properties.ReadOnly = true;
                    //searchButton_ProductName.Properties.ReadOnly = true;
                    //searchButton_ProductId.Properties.ReadOnly = true;
                    //searchButton_BarCodes.Properties.ReadOnly = true;
                    //imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = true;
                    //imageComboBoxEdit_BOM.Properties.ReadOnly = true;
                    //textEdit_Quantity.Properties.ReadOnly = true;
                    //textEdit_BomAmountDtl.Properties.ReadOnly = true;
                    //textEdit_MemoDtl.Properties.ReadOnly = true;
                    //textEdit_MemoHeader.Properties.ReadOnly = true;
                    //simpleButton_Add.Enabled = false;
                    //simpleButton_Delete.Enabled = false;
                    //if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                    //    || !SO.header.fundStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    //{
                    //    checkEdit_Collection.Enabled = false;
                    //    if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                    //        || !SO.header.warehouseStatus.Equals(((int)BusinessStatus.NOTCLEARED).ToString()))
                    //    {
                    //        checkEdit_SalesOutWhs.Enabled = false;
                    //        checkEdit_Logistics.Enabled = false;
                    //        simpleButton_Logistics.Enabled = false;
                    //        //simpleButton_Save.Enabled = false;
                    //        simpleButton_Account.Enabled = false;
                    //        simpleButton_OK.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        checkEdit_Collection.Checked = false;           //不需要收款
                    //    }
                    //}
                    textEdit_BaseEntry.Properties.ReadOnly = true;
                    buttonEdit_SerialNo.Properties.ReadOnly = true;
                    buttonEdit_ProductName.Properties.ReadOnly = true;
                    searchButton_ProductId.Properties.ReadOnly = true;
                    buttonEdit_BarCodes.Properties.ReadOnly = true;
                    textEdit_Quantity.Properties.ReadOnly = true;
                    textEdit_MemoDtl.Properties.ReadOnly = true;
                    textEdit_MemoHeader.Properties.ReadOnly = true;
                    checkEdit_SalesOutWhs.Enabled = false;
                    checkEdit_Logistics.Enabled = false;
                    simpleButton_Add.Enabled = false;
                    simpleButton_Delete.Enabled = false;
                    simpleButton_Save.Enabled = false;
                    simpleButton_OK.Enabled = false;
                }
            }

            //画面显示
            //this.textEdit_DocId.Text = SO.header.docId;
            //this.textEdit_CreateDocDate.Text = SO.header.createDocDate.ToString();
            //this.textEdit_BaseEntry.Text = SO.header.baseEntry;
            //this.searchButton_MemberPhoneNo.Text = "";
            //this.textEdit_MemberName.Text = "";
            //this.searchButton_SerialNo.Text = "";
            //this.searchButton_ProductName.Text = "";
            //this.searchButton_ProductId.Text = "";
            //this.imageComboBoxEdit_ProductSalesPolicy.Text = "";
            //this.imageComboBoxEdit_BOM.Text = "";
            //this.textEdit_Quantity.Text = "";
            //this.textEdit_UnitPrice.Text = "";
            //this.textEdit_BomAmountDtl.Text = "";
            //this.textEdit_MemoDtl.Text = "";
            //this.textEdit_MemoHeader.Text = SO.header.memo;
            //this.textEdit_Amout.EditValue = SO.header.rebateAmount;
            //imageComboBoxEdit_DocStatus.EditValue = Convert.ToInt32(SO.header.docStatus);
            //textEdit_PostingDate.EditValue = SO.header.postingDate;
            //imageComboBoxEdit_ProductSalesPolicy.Properties.Items.Clear();
            //imageComboBoxEdit_BOM.Properties.Items.Clear();
            this.textEdit_DocId.Text = WO.header.docId;
            this.textEdit_CreateDocDate.Text = WO.header.docDate.ToString();
            this.textEdit_BaseEntry.Text = WO.header.baseEntry;
            this.buttonEdit_SerialNo.Text = "";
            this.buttonEdit_ProductName.Text = "";
            this.searchButton_ProductId.Text = "";
            this.textEdit_Quantity.Text = "";
            this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = WO.header.memo;

            //表格明细数据绑定
            this.gridControl_ProductList.DataSource = WO.item;
            updateGridData();

            //焦点在串号上
            //searchButton_SerialNo.Focus();

            return true;
        }
        //private void initDisplay2()
        //{
        //    try
        //    {
        //        //新建订单的时候
        //        if (string.IsNullOrEmpty(WO.header.docId))
        //        {
        //            //调用服务，获得单号，获得销售人员
        //            string str = null;
        //            if (RetCode.NG == DevCommon.getDocId(DocType.SALESOUTWHS, ref str))
        //            {
        //                return;
        //            }
        //            WO.header.docId = str;

        //            //制单时间
        //            WO.header.docDate = DateTime.Now.ToString();
        //            WO.header.updateDate = WO.header.docDate;
        //        }
        //        else                           //查询订单的情况
        //        {
        //            //查询时显示状态和过账时间
        //            //ComboxTable cbTable = new ComboxTable();
        //            //DataTable dtDocStatusTable = cbTable.CreateDocStatusTable();
        //            //DevCommon.initCmb(this.imageComboBoxEdit_DocStatus, dtDocStatusTable, "Key", "Value", true);
        //            labelControl_DocStatus.Visible = true;
        //            imageComboBoxEdit_DocStatus.Visible = true;
                    
        //            //根据目前已有的订单状态决定是否能够后续操作
        //            if (WO.header.docStatus != ((int)DocStatus.DRAFT).ToString())        //只有草稿状态是可以继续操作的
        //            {
        //                textEdit_BaseEntry.Properties.ReadOnly = true;
        //                buttonEdit_SerialNo.Properties.ReadOnly = true;
        //                buttonEdit_ProductName.Properties.ReadOnly = true;
        //                searchButton_ProductId.Properties.ReadOnly = true;
        //                buttonEdit_BarCodes.Properties.ReadOnly = true;
        //                textEdit_Quantity.Properties.ReadOnly = true;
        //                textEdit_MemoDtl.Properties.ReadOnly = true;
        //                textEdit_MemoHeader.Properties.ReadOnly = true;
        //                checkEdit_SalesOutWhs.Enabled = false;
        //                checkEdit_Logistics.Enabled = false;
        //                simpleButton_Add.Enabled = false;
        //                simpleButton_Delete.Enabled = false;
        //                simpleButton_Save.Enabled = false;
        //                simpleButton_OK.Enabled = false;
        //            }
        //        }

        //        //表头明细行数据
        //        //WODtlNew = new SalesOrderDtl();
                
        //        //默认显示空行
        //        //WODtl = new SalesOrderDtl();
        //        //SO.detail.Add(WODtl);
                
        //        //画面显示
        //        this.textEdit_DocId.Text = WO.header.docId;
        //        this.textEdit_CreateDocDate.Text = WO.header.docDate.ToString();
        //        this.textEdit_BaseEntry.Text = WO.header.baseEntry;
        //        this.buttonEdit_SerialNo.Text = "";
        //        this.buttonEdit_ProductName.Text = "";
        //        this.searchButton_ProductId.Text = "";
        //        this.textEdit_Quantity.Text = "";
        //        this.textEdit_MemoDtl.Text = "";
        //        this.textEdit_MemoHeader.Text = WO.header.memo;
        //        imageComboBoxEdit_DocStatus.EditValue = Convert.ToInt32(WO.header.docStatus);

        //        //表格明细数据绑定
        //        //this.gridView_ProductList.clear()
        //        //this.gridControl_ProductList.DataSource = WO.item;
        //        updateGridData();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        FormBase.PromptInformation(ex.Message);
        //    }
            
        //}

        //将明细内容更新至表格数据中
        private void updateGridData()
        {
            this.gridControl_ProductList.RefreshDataSource();
            //this.gridView_ProductList.MoveLast();
            //this.simpleButton_OK.Enabled = false;
            //this.simpleButton_Save.Enabled = false;
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.buttonEdit_SerialNo.Text = WODtl.sequenceId;
            this.buttonEdit_ProductName.Text = WODtl.productName;
            this.searchButton_ProductId.Text = WODtl.productId;
            this.buttonEdit_BarCodes.Text = WODtl.idValue;
            this.textEdit_Quantity.EditValue = WODtl.quantity;
            this.textEdit_MemoDtl.Text = WODtl.memo;
        }

        //数量输入后
        private void textEditQuantity_KeyDown(object sender, KeyEventArgs e)
        {
             try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //获取界面中的数据
                    WODtl.quantity = Convert.ToInt32(this.textEdit_Quantity.Text);
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
                    WODtl.memo = this.textEdit_MemoDtl.Text;
                    this.simpleButton_Add.PerformClick();
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
            string currentDocStatus = WO.header.docStatus;
            try
            {
                //查询销售订单时候，是否要更新出库单
                bool bUpdateSalesOutWhsOrder = false;
                if (bIsOrderSaved && WO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    bUpdateSalesOutWhsOrder = true;
                }

                //数据有效性检查
                //if (checkEdit_Logistics.Checked && string.IsNullOrEmpty(SO.header.logisticsAddress))
                //{
                //    LogisticsAddress dialog = new LogisticsAddress();
                //    dialog.logisticsAddress = SO.header.logisticsAddress;
                //    if (dialog.ShowDialog() == DialogResult.OK)
                //    {
                //        SO.header.logisticsAddress = dialog.logisticsAddress;
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}

                //销售出库单
                //if (checkEdit_SalesOutWhs.Checked)
                //{
                //    SO.header.deliveryStoreId = LoginInfo.ProductStoreId;
                //    SO.header.logisticsAddress = null;
                //    if (!SalesOrderBLL.setSalesOutWhsOrder(ref SO, ref WO))
                //    {
                //        FormBase.PromptInformation("销售出库单数据不正确！");
                //        SO.header.docStatus = currentDocStatus;
                //        return;
                //    }
                //}

                //设置销售出库单
                if (!setSalesOutWhsOrder(DocStatus.VALID))
                {
                    FormBase.PromptInformation("销售出库单数据不正确！");
                    WO.header.docStatus = currentDocStatus;
                    return;
                }

                if (bIsOrderSaved)
                {
                    if (!SalesOutWhsOrderBLL.updateSalesOutWhsOrder(ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        WO.header.docStatus = currentDocStatus;
                        return;
                    }
                }
                else
                {
                    if (!SalesOutWhsOrderBLL.createSalesOutWhsOrder(ref WO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        WO.header.docStatus = currentDocStatus;
                        return;
                    }
                }


                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    WO.header.docStatus = currentDocStatus;
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
                WO.header.docStatus = currentDocStatus;
            }
        }

        //取消
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

        //暂时不处理
        private void checkEditSalesOutWhs_CheckedChanged(object sender, EventArgs e)
        {

        }

        //暂时不处理
        private void checkEditLogistics_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textEditBaseEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(this.textEdit_BaseEntry.Text))
                    {
                        textEdit_MemoDtl.Focus();
                        return;
                    }

                    //调用接口查询销售订单
                    queryDocIdModel QDI = new queryDocIdModel();
                    QDI.docId = this.textEdit_BaseEntry.Text;
                    if (DevCommon.getDataByWebService("getSalesOrderById", "getSalesOrderById", QDI, ref SO) == RetCode.NG || SO.header == null)
                    {
                        return;
                    }
                    else
                    {
                        if (!SO.header.docStatus.Equals(((int)DocStatus.VALID).ToString())
                            || !SO.header.fundStatus.Equals(((int)BusinessStatus.CLEARED).ToString())
                            || SO.header.warehouseStatus.Equals(((int)BusinessStatus.CLEARED).ToString()))
                        {
                            FormBase.PromptInformation("销售订单状态不正确！");
                            return;
                        }
                    }
                    //界面显示预订单信息
                    WO.header.baseEntry = this.textEdit_BaseEntry.Text;
                    SalesOutWhsOrderBLL.getSalesOutWhsOrderFromSalesOrder(SO, ref WO);
                    this.gridControl_ProductList.DataSource = WO.item;
                    updateGridData();
                    textEdit_MemoDtl.Focus();
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //将界面内容设置到订单对象
        private bool setSalesOutWhsOrder(DocStatus status)
        {
            WO.header.docStatus = ((int)status).ToString();
            if (WO.header.docStatus.Equals(((int)DocStatus.VALID).ToString()))
            {
                WO.header.status = ((int)BusinessStatus.CLEARED).ToString(); 
            }
            else
            {
                WO.header.status = ((int)BusinessStatus.NOTCLEARED).ToString(); 
            }
            //过账日期设置
            WO.header.postingDate = DevCommon.PostingDate();

            return true;
        }

        private void textEditMemoHeader_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //设置备注信息
                    WO.header.memo = this.textEdit_MemoHeader.Text;
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
                if (e.KeyCode.Equals(Keys.F4))
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