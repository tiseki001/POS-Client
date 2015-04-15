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

namespace PreRefundOrder
{
    public partial class PreRefundOrder : BaseForm
    {
        public PreRefundOrderModel PRFO = new PreRefundOrderModel();                         //退款单
        RefundOrderInitModel RefundOrderInit;
        PreRefundOrderDtlModel PRFODtl;                                                      //表格中的明细行
        PreRefundOrderDtlModel PRFODtlNew;                                                   //界面新增的明细 
        List<getPaymentMethodModel> PM = null;
        PreCollectionOrderModel PCO;                                                            //收款单

        public PreRefundOrder(RefundOrderInitModel RFOI, PreCollectionOrderModel PCOIn)
        {
            PCO = PCOIn;
            RefundOrderInit = RFOI;
            InitializeComponent();
        }

        private void PreRefundOrder_Load(object sender, EventArgs e)
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
                if (string.IsNullOrEmpty(PRFO.header.docId))
                {
                    //
                    PRFO.header.baseEntry = RefundOrderInit.baseEntry;

                    //制单时间
                    PRFO.header.createDocDate = DateTime.Now.ToString();
                    PRFO.header.lastUpdateDocDate = PRFO.header.createDocDate;

                    PRFO.header.amount = RefundOrderInit.refundAmount;
                    PRFO.header.unRefundAmount = RefundOrderInit.refundAmount;
                    PRFO.header.amount = RefundOrderInit.refundAmount;

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


                //this.textEdit_UnRefundAmount.EditValue = PRFO.header.UnRefundAmount;

                queryStoreIdModel QS = new queryStoreIdModel();
                QS.storeId = LoginInfo.ProductStoreId;

                if (DevCommon.getDataByWebService("getPaymentMethod", "getPaymentMethod", QS, ref PM) == RetCode.NG)
                {
                    return;
                }
                this.gridControl_RefundType.DataSource = PM;
                

                //表格明细数据绑定
                if (!PreRefundOrderBLL.getPreRefundOrderFromPreCollectionOrder(PCO, PM, ref PRFO))
                {
                    MessageBox.Show("获得原付款单数据失败！");
                }

                //表头明细行数据
                PRFODtlNew = new PreRefundOrderDtlModel();

                //默认显示空行
                PRFODtl = new PreRefundOrderDtlModel();
                PRFO.detail.Add(PRFODtl);

                //画面显示
                this.textEdit_RefundAmount.EditValue = PRFO.header.amount;
                this.gridControl_PreRefund.DataSource = PRFO.detail;
                updateGridData();
                this.gridView_PreRefund.MoveFirst();
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
                foreach (PreRefundOrderDtlModel item in PRFO.detail)
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
                
                if (PRFO.header.unRefundAmount == 0)
                {
                    //设置退款单
                    setPreRefundOrderData(DocStatus.VALID);
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
        private void setPreRefundOrderData(DocStatus status)
        {
            //设置头数据
            //调用服务，获得单号
            string str = null;
            if (RetCode.NG == DevCommon.getDocId(DocType.PREREFUND, ref str))
            {
                return;
            }
            PRFO.header.docId = str;
            PRFO.header.docStatus = ((int)status).ToString();
            PRFO.header.partyId = LoginInfo.OwnerPartyId;
            PRFO.header.storeId = LoginInfo.ProductStoreId;
            PRFO.header.createUserId = LoginInfo.PartyId;
            PRFO.header.lastUpdateUserId = LoginInfo.PartyId;
            //过账日期设置
            PRFO.header.postingDate = DevCommon.PostingDate();

            //设置明细数据
            //删除空白行
            PRFO.detail.RemoveAt(PRFO.detail.Count - 1);
            for (int i = 0; i < PRFO.detail.Count; i++)
            {
                PRFO.detail[i].docId = PRFO.header.docId;
                PRFO.detail[i].lineNo = i + 1;
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
                PRFODtl.type = this.textEdit_TypeNo.Text;
                PRFODtl.typeName = this.textEdit_TypeName.Text;
                PRFODtl.amount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
                PRFODtl.code = this.textEdit_Code.Text;
                PRFODtl.serialNo = this.textEdit_SerialNo.Text;
                PRFODtl.cardCode = this.textEdit_CardCode.Text;

                //没有内容，则返回
                if (string.IsNullOrEmpty(PRFODtl.type) || PRFODtl.amount <= 0)
                {
                    MessageBox.Show("输入项不正确！");
                    return;
                }

                if (this.gridView_PreRefund.IsLastRow == true)   //在最后一行，添加时候
                {                   
                    //明细表中增加当前显示的商品信息
                    PRFO.detail.Insert(PRFO.detail.Count - 1, PRFODtl);
                    PRFODtlNew = new PreRefundOrderDtlModel();
                }
                else                                               //不在最后一行
                {
                    //明细表中更新当前显示的商品信息
                    PRFO.detail[this.gridView_PreRefund.GetDataSourceRowIndex(this.gridView_PreRefund.FocusedRowHandle)] = PRFODtl;
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
            this.gridControl_PreRefund.RefreshDataSource();
            this.gridView_PreRefund.MoveLast();

            //设置表头
            PRFO.header.unRefundAmount = PRFO.header.amount - Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
            this.textEdit_UnRefundAmount.EditValue = PRFO.header.unRefundAmount;
        }

        private void gridViewRefund_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.gridView_PreRefund.IsLastRow == true)            //在最后一行是增加明细功能
            {
                PRFODtl = PRFODtlNew;
                this.simpleButton_Add.Text = "添加";
            }
            else if (this.gridView_PreRefund.FocusedRowHandle < this.gridView_PreRefund.RowCount)   //不在最后一行是修改明细功能
            {
                PRFODtl = (PreRefundOrderDtlModel)PRFO.detail[this.gridView_PreRefund.GetDataSourceRowIndex(this.gridView_PreRefund.FocusedRowHandle)].Clone();
                this.simpleButton_Add.Text = "更新";
            }
            //更新表头的明细信息
            displayDetailData();
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.textEdit_TypeNo.Text = PRFODtl.type;
            this.textEdit_TypeName.Text = PRFODtl.typeName;
            this.textEdit_Amount.EditValue = PRFODtl.amount;
            this.textEdit_Code.Text = PRFODtl.code;
            this.textEdit_SerialNo.Text = PRFODtl.serialNo;
            this.textEdit_CardCode.Text = PRFODtl.cardCode;
        }

        private void simpleButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_PreRefund.IsLastRow == false)           //最后一行空白行不能删除
                {
                    int nFoucusedRowHandle = this.gridView_PreRefund.FocusedRowHandle;
                    if (nFoucusedRowHandle == this.gridView_PreRefund.RowCount - 2)       //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    {
                        this.gridView_PreRefund.MoveLast();
                    }
                    //删除当前行数据
                    PRFO.detail.RemoveAt(nFoucusedRowHandle);
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
            PRFO = null;
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