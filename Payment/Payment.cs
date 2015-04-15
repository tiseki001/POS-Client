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

namespace Payment
{
    public partial class Payment : BaseForm
    {
        PaymentInOrderModel PIO = null;                 //缴款单
        PaymentInOrderDtlModel PIODtl;                  //表格中的明细行
        BaseMainForm FormBase;                          //框架窗口
        BaseForm FormParent;                            //上级窗口
        bool bIsOrderSaved;                             //订单是否保存
        List<getPaymentAmountModel> PAList = null;      //缴款金额

        #region 构造
        public Payment(BaseMainForm formb, BaseForm formp, PaymentInOrderModel paymentInOrder)
        {
            FormBase = formb;
            FormParent = formp;
            PIO = paymentInOrder;
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void Payment_Load(object sender, EventArgs e)
        {
            if (PIO == null)             //新建订单
            {
                PIO = new PaymentInOrderModel();
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
        #endregion

        //初始化界面显示
        //初始化界面显示
        private bool initDisplay()
        {
            //新建订单的时候
            if (string.IsNullOrEmpty(PIO.header.docId))
            {
                //调用服务，获得单号，获得销售人员
                string str = null;
                if (RetCode.NG == DevCommon.getDocId(DocType.PAYMENTIN, ref str))
                {
                    return false;
                }
                PIO.header.docId = str;
                bIsOrderSaved = false;

                //制单时间
                PIO.header.createDocDate = DateTime.Now.ToString();
                PIO.header.lastUpdateDocDate = PIO.header.createDocDate;
                PIO.header.postingDate = DevCommon.PostingDate();               

                //缴款金额
                if (!PaymentBLL.getPaymentAmount(PIO.header.postingDate, ref PAList))
                {
                    return false;
                }

                //设置缴款单（根据缴款金额得到缴款单）
                if (!PaymentBLL.getPaymentInOrderFromPaymentAmount(PAList, ref PIO))   
                {
                    return false;
                }

                //控件可用
                //textEdit_BaseEntry.Properties.ReadOnly = false;

            }
            else                           //查询订单的情况
            {
                //根据目前已有的订单状态决定是否能够后续操作
                if (PIO.header.docStatus != ((int)DocStatus.DRAFT).ToString())        //只有草稿状态是可以继续操作的
                {
                    //textEdit_BaseEntry.Properties.ReadOnly = true;
                    //buttonEdit_SerialNo.Properties.ReadOnly = true;
                    //buttonEdit_ProductName.Properties.ReadOnly = true;
                    //searchButton_ProductId.Properties.ReadOnly = true;
                    //buttonEdit_BarCodes.Properties.ReadOnly = true;
                    //textEdit_Quantity.Properties.ReadOnly = true;
                    //textEdit_MemoDtl.Properties.ReadOnly = true;
                    //textEdit_MemoHeader.Properties.ReadOnly = true;
                    //checkEdit_SalesOutWhs.Enabled = false;
                    //checkEdit_Logistics.Enabled = false;
                    //simpleButton_Add.Enabled = false;
                    //simpleButton_Delete.Enabled = false;
                    //simpleButton_Save.Enabled = false;
                    simpleButton_PrePaymentIn.Enabled = false;
                    simpleButton_PaymentIn.Enabled = false;
                }
            }

            //画面显示
            this.textEdit_DocId.Text = PIO.header.docId;
            dateEdit_PostingDate.EditValue = Convert.ToDateTime(PIO.header.postingDate);
            //dateEdit_PostingDate.EditValue = DateTime.Now;
            //this.textEdit_CreateDocDate.Text = WO.header.docDate.ToString();
            //this.textEdit_BaseEntry.Text = WO.header.baseEntry;
            //this.buttonEdit_SerialNo.Text = "";
            //this.buttonEdit_ProductName.Text = "";
            //this.searchButton_ProductId.Text = "";
            //this.textEdit_Quantity.Text = "";
            //this.textEdit_MemoDtl.Text = "";
            this.textEdit_MemoHeader.Text = PIO.header.memo;

            //表格明细数据绑定
            this.gridControl_PaymentIn.DataSource = PIO.detail;
            //updateGridData();

            //焦点在串号上
            //searchButton_SerialNo.Focus();

            return true;
        }

        //输入金额后
        private void gridViewPaymentIn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Equals(this.gridColumn_Amount))
            {
                int nFocusedSourceRowIndex = this.gridView__PaymentIn.GetDataSourceRowIndex(this.gridView__PaymentIn.FocusedRowHandle);
                PIO.detail[nFocusedSourceRowIndex].diffAmount = PIO.detail[nFocusedSourceRowIndex].targetAmount - PIO.detail[nFocusedSourceRowIndex].preAmount - PIO.detail[nFocusedSourceRowIndex].amount;
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
                    PIO.header.memo = this.textEdit_MemoHeader.Text;
                }
            }
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //缴款按钮按下
        private void simpleButton_PrePaymentIn_Click(object sender, EventArgs e)
        {
            try
            {
                //无数据
                if (!PIO.detail.Exists(delegate(PaymentInOrderDtlModel item) { return item.amount != 0; }))
                {
                    return;
                }

                //设置缴款单
                if (!setPaymentInOrder(DocStatus.VALID, PaymentInType.PrePaymentIn))
                {
                    return;
                }

                //保存缴款单
                //写入数据库
                if (bIsOrderSaved)
                {
                    //if (!PaymentBLL.updateSalesOrderAll(true, ref SO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    return;
                    //}
                }
                else
                {
                    if (!PaymentBLL.savePaymentInOrder(ref PIO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        return;
                    }
                }
                
                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    return;
                }

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
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //日结按钮按下
        private void simpleButton_PaymentIn_Click(object sender, EventArgs e)
        {
            try
            {
                //无数据
                if (!PIO.detail.Exists(delegate(PaymentInOrderDtlModel item) { return item.amount != 0; }))
                {
                    return;
                }

                //设置缴款单
                if (!setPaymentInOrder(DocStatus.VALID, PaymentInType.PaymentIn))
                {
                    return;
                }

                //保存缴款单
                //写入数据库
                if (bIsOrderSaved)
                {
                    //if (!PaymentBLL.updateSalesOrderAll(true, ref SO, ref CO, ref WO))
                    //{
                    //    FormBase.PromptInformation("订单数据写入数据库出错！");
                    //    return;
                    //}
                }
                else
                {
                    if (!PaymentBLL.savePaymentInOrder(ref PIO))
                    {
                        FormBase.PromptInformation("订单数据写入数据库出错！");
                        return;
                    }
                    bIsOrderSaved = true;
                }

                //初始化显示
                if (!initDisplay())
                {
                    FormBase.PromptInformation("界面初始化失败！");
                    return;
                }

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
            catch (System.Exception ex)
            {
                FormBase.PromptInformation(ex.Message);
            }
        }

        //将界面内容设置到订单对象
        private bool setPaymentInOrder(DocStatus status, string type)
        {
            //表头
            PIO.header.docStatus = ((int)status).ToString();
            PIO.header.docType = type;
            PIO.header.partyId = LoginInfo.OwnerPartyId;
            PIO.header.storeId = LoginInfo.ProductStoreId;
            PIO.header.amount = Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
            if (!bIsOrderSaved)
            {
                PIO.header.createUserId = LoginInfo.PartyId;
            }
            else
            {
                PIO.header.lastUpdateDocDate = DateTime.Now.ToString();
            }
            PIO.header.lastUpdateUserId = LoginInfo.PartyId;

            //销售明细和价格明细
            for (int i = 0; i < PIO.detail.Count; i++)
            {
                if (PIO.detail[i].amount == 0)
                {
                    PIO.detail.RemoveAt(i);
                    i--;
                    continue;
                }
                PIO.detail[i].docId = PIO.header.docId;
                PIO.detail[i].lineNo = i + 1;
            }

            return true;
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