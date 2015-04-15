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

namespace RefundOrder
{
    public partial class RefundOrder : BaseForm
    {
        public RefundOrderModel RFO = new RefundOrderModel();                               //退款单
        RefundOrderInitModel RefundOrderInit;
        RefundOrderDtlModel RFODtl;                                                         //表格中的明细行
        RefundOrderDtlModel RFODtlNew;                                                      //界面新增的明细 
        List<getPaymentMethodModel> PM = null;
        CollectionOrderModel CO;                                                            //收款单

        public RefundOrder(RefundOrderInitModel RFOI, CollectionOrderModel COIn)
        {
            CO = COIn;
            RefundOrderInit = RFOI;
            InitializeComponent();
        }

        private void RefundOrder_Load(object sender, EventArgs e)
        {
            initDisplay();
            textEdit_TypeNo.Focus();
        }

        //初始化界面显示
        private void initDisplay()
        {
            try
            {
                //新建订单的时候
                if (string.IsNullOrEmpty(RFO.header.docId))
                {
                    //
                    RFO.header.baseEntry = RefundOrderInit.baseEntry;

                    //制单时间
                    RFO.header.createDocDate = DateTime.Now.ToString();
                    RFO.header.lastUpdateDocDate = RFO.header.createDocDate;

                    RFO.header.amount = RefundOrderInit.refundAmount;
                    RFO.header.unRefundAmount = RefundOrderInit.refundAmount;
                    RFO.header.amount = RefundOrderInit.refundAmount;

                }
                else                           //查询订单的情况
                {
                    //查询时显示状态和过账时间
                    //ComboxTable cbTable = new ComboxTable();
                    //DataTable dtDocStatusTable = cbTable.CreateDocStatusTable();
                    //DevCommon.initCmb(this.imageComboBoxEdit_DocStatus, dtDocStatusTable, "Key", "Value", true);
                    //labelControl_DocStatus.Visible = true;
                    //imageComboBoxEdit_DocStatus.Visible = true;
                    //labelControl_PostingDate.Visible = true;
                    //textEdit_PostingDate.Visible = true;

                    ////根据目前已有的订单状态决定是否能够后续操作
                    //if (SO.header.docStatus != ((int)DocStatus.DRAFT).ToString())        //只有草稿状态是可以继续操作的
                    //{
                    //    textEdit_BaseEntry.Properties.ReadOnly = true;
                    //    buttonEdit_MemberPhoneNo.Properties.ReadOnly = true;
                    //    buttonEdit_SalesPersonId.Properties.ReadOnly = true;
                    //    buttonEdit_SerialNo.Properties.ReadOnly = true;
                    //    buttonEdit_ProductName.Properties.ReadOnly = true;
                    //    buttonEdit_ProductId.Properties.ReadOnly = true;
                    //    buttonEdit_BarCodes.Properties.ReadOnly = true;
                    //    imageComboBoxEdit_ProductSalesPolicy.Properties.ReadOnly = true;
                    //    imageComboBoxEdit_SubProducts.Properties.ReadOnly = true;
                    //    textEdit_Quantity.Properties.ReadOnly = true;
                    //    textEdit_RebateAmountDtl.Properties.ReadOnly = true;
                    //    textEdit_MemoDtl.Properties.ReadOnly = true;
                    //    textEdit_MemoHeader.Properties.ReadOnly = true;
                    //    checkEdit_Refund.Enabled = false;
                    //    checkEdit_SalesOutWhs.Enabled = false;
                    //    checkEdit_Logistics.Enabled = false;
                    //    simpleButton_Add.Enabled = false;
                    //    simpleButton_Delete.Enabled = false;
                    //    simpleButton_Save.Enabled = false;
                    //    simpleButton_Account.Enabled = false;
                    //    simpleButton_OK.Enabled = false;
                    //}
                }


                //this.textEdit_UnRefundAmount.EditValue = RFO.header.UnRefundAmount;

                queryStoreIdModel QS = new queryStoreIdModel();
                QS.storeId = LoginInfo.ProductStoreId;

                if (DevCommon.getDataByWebService("getPaymentMethod", "getPaymentMethod", QS, ref PM) == RetCode.NG)
                {
                    return;
                }
                this.gridControl_RefundType.DataSource = PM;
                

                //表格明细数据绑定
                if (!RefundOrderBLL.getRefundOrderFromCollectionOrder(CO, PM, ref RFO))
                {
                    MessageBox.Show("获得原付款单数据失败！");
                }

                //表头明细行数据
                RFODtlNew = new RefundOrderDtlModel();

                //默认显示空行
                RFODtl = new RefundOrderDtlModel();
                RFO.detail.Add(RFODtl);

                //画面显示
                this.textEdit_RefundAmount.EditValue = RFO.header.amount;
                this.gridControl_Refund.DataSource = RFO.detail;
                updateGridData();
                this.gridView_Refund.MoveFirst();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                //串号检查
                foreach (RefundOrderDtlModel item in RFO.detail)
                {
                    if (!string.IsNullOrEmpty(item.serialNo))
                    {
                        string serialNo = RefundOrderInit.serialNoList.Find(delegate(string no) { return no.Equals(item.serialNo); });
                        if (string.IsNullOrEmpty(serialNo))
                        {
                            MessageBox.Show("串码" + item.serialNo + "输入不正确！");
                            return;
                        }
                    }
                }
                
                if (RFO.header.unRefundAmount == 0)
                {
                    //设置退款单
                    setRefundOrderData(DocStatus.VALID);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("收款金额不正确！");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        //将显示的表头信息设置设置进表头 
        private void setRefundOrderData(DocStatus status)
        {
            //设置头数据
            //调用服务，获得单号
            string str = null;
            if (RetCode.NG == DevCommon.getDocId(DocType.REFUND, ref str))
            {
                return;
            }
            RFO.header.docId = str;
            RFO.header.docStatus = ((int)status).ToString();
            RFO.header.partyId = LoginInfo.OwnerPartyId;
            RFO.header.storeId = LoginInfo.ProductStoreId;
            RFO.header.createUserId = LoginInfo.PartyId;
            RFO.header.lastUpdateUserId = LoginInfo.PartyId;
            //过账日期设置
            RFO.header.postingDate = DevCommon.PostingDate();

            //设置明细数据
            //删除空白行
            RFO.detail.RemoveAt(RFO.detail.Count - 1);
            for (int i = 0; i < RFO.detail.Count; i++)
            {
                RFO.detail[i].docId = RFO.header.docId;
                RFO.detail[i].lineNo = i + 1;
            }
        }

        private void textEditTypeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    getPaymentMethodModel resultPM = PM.Find(delegate(getPaymentMethodModel result) { return result.paymentMethodTypeId.Equals(this.textEdit_TypeNo.Text); });
                    if (resultPM != null)
                    {
                        this.textEdit_TypeName.Text = resultPM.description;
                        textEdit_Amount.Focus();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            try
            {                
                //设置付款明细
                RFODtl.type = this.textEdit_TypeNo.Text;
                RFODtl.typeName = this.textEdit_TypeName.Text;
                RFODtl.amount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
                RFODtl.code = this.textEdit_Code.Text;
                RFODtl.serialNo = this.textEdit_SerialNo.Text;
                RFODtl.cardCode = this.textEdit_CardCode.Text;

                //没有内容，则返回
                if (string.IsNullOrEmpty(RFODtl.type) || RFODtl.amount <= 0)
                {
                    MessageBox.Show("输入项不正确！");
                    return;
                }

                if (this.gridView_Refund.IsLastRow == true)   //在最后一行，添加时候
                {                   
                    //明细表中增加当前显示的商品信息
                    RFO.detail.Insert(RFO.detail.Count - 1, RFODtl);
                    RFODtlNew = new RefundOrderDtlModel();
                }
                else                                               //不在最后一行
                {
                    //明细表中更新当前显示的商品信息
                    RFO.detail[this.gridView_Refund.GetDataSourceRowIndex(this.gridView_Refund.FocusedRowHandle)] = RFODtl;
                }
                //更新表格中数据并跳转到最后一行空白行
                updateGridData();
                textEdit_TypeNo.Focus();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //将明细内容更新至表格数据中
        private void updateGridData()
        {
            this.gridControl_Refund.RefreshDataSource();
            this.gridView_Refund.MoveLast();

            //设置表头
            RFO.header.unRefundAmount = RFO.header.amount - Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
            this.textEdit_UnRefundAmount.EditValue = RFO.header.unRefundAmount;
        }

        private void gridViewRefund_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.gridView_Refund.IsLastRow == true)            //在最后一行是增加明细功能
            {
                RFODtl = RFODtlNew;
                this.simpleButton_Add.Text = "添加";
            }
            else if (this.gridView_Refund.FocusedRowHandle < this.gridView_Refund.RowCount)   //不在最后一行是修改明细功能
            {
                RFODtl = (RefundOrderDtlModel)RFO.detail[this.gridView_Refund.GetDataSourceRowIndex(this.gridView_Refund.FocusedRowHandle)].Clone();
                this.simpleButton_Add.Text = "更新";
            }
            //更新表头的明细信息
            displayDetailData();
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.textEdit_TypeNo.Text = RFODtl.type;
            this.textEdit_TypeName.Text = RFODtl.typeName;
            this.textEdit_Amount.EditValue = RFODtl.amount;
            this.textEdit_Code.Text = RFODtl.code;
            this.textEdit_SerialNo.Text = RFODtl.serialNo;
            this.textEdit_CardCode.Text = RFODtl.cardCode;
        }

        private void simpleButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_Refund.IsLastRow == false)           //最后一行空白行不能删除
                {
                    int nFoucusedRowHandle = this.gridView_Refund.FocusedRowHandle;
                    if (nFoucusedRowHandle == this.gridView_Refund.RowCount - 2)       //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    {
                        this.gridView_Refund.MoveLast();
                    }
                    //删除当前行数据
                    RFO.detail.RemoveAt(nFoucusedRowHandle);
                    //更新表格中数据并跳转到最后一行空白行
                    updateGridData();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            RFO = null;
            this.Close();
        }

        private void gridViewRefundType_DoubleClick(object sender, EventArgs e)
        {
            int nDataSourceRowIndex = this.gridView_RefundType.GetDataSourceRowIndex(this.gridView_RefundType.FocusedRowHandle);
            this.textEdit_TypeNo.Text = PM[nDataSourceRowIndex].paymentMethodTypeId;
            this.textEdit_TypeName.Text = PM[nDataSourceRowIndex].description;
            textEdit_Amount.Focus();
        }

        private void textEditAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    textEdit_Code.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void texteditCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    textEdit_SerialNo.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textEditSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    textEdit_CardCode.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textEditCardCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    this.simpleButton_Add.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KeyPreview_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F4))
                {
                    //确定
                    this.simpleButton_OK.PerformClick();
                }
                else if (e.KeyCode.Equals(Keys.Escape))
                {
                    //取消
                    this.simpleButton_Cancel.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}