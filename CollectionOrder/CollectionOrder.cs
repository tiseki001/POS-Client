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

namespace CollectionOrder
{
    public partial class CollectionOrder : BaseForm
    {
        public CollectionOrderModel CO = new CollectionOrderModel();                           //收款单
        CollectionOrderInitModel CollectionOrderInit;                                          //收款单初始化条件
        CollectionOrderDtlModel CODtl;                                                         //表格中的明细行
        CollectionOrderDtlModel CODtlNew;                                                      //界面新增的明细 
        List<getPaymentMethodModel> PM = null;                                                 //支付方式

        //收款单构造
        public CollectionOrder(CollectionOrderInitModel COI)
        {
            CollectionOrderInit = COI;
            InitializeComponent();
        }

        private void CollectionOrder_Load(object sender, EventArgs e)
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
                if (string.IsNullOrEmpty(CO.header.docId))
                {
                    CO.header.baseEntry = CollectionOrderInit.baseEntry;
                    CO.header.collectionAmount = CollectionOrderInit.collectionAmount;
                    CO.header.uncollectionAmount = CollectionOrderInit.collectionAmount;
                    CO.header.amount = CollectionOrderInit.collectionAmount;
                    CO.header.preCollectionAmount = 0;
                }
                else                           //查询订单的情况
                {

                }

                queryStoreIdModel QS = new queryStoreIdModel();
                QS.storeId = LoginInfo.ProductStoreId;

                if (DevCommon.getDataByWebService("getPaymentMethod", "getPaymentMethod", QS, ref PM) == RetCode.NG)
                {
                    return;
                }

                this.gridControl_CollectionType.DataSource = PM;
                
                
                //表头明细行数据
                CODtlNew = new CollectionOrderDtlModel();

                //默认显示空行
                CODtl = new CollectionOrderDtlModel();
                CO.detail.Add(CODtl);

                //画面显示
                //表格明细数据绑定
                this.textEdit_CollectionAmount.EditValue = CO.header.collectionAmount;
                this.gridControl_Collection.DataSource = CO.detail;
                updateGridData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //确定按钮按下
        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            try
            {
                //串号检查
                foreach (CollectionOrderDtlModel item in CO.detail)
                {
                    if (!string.IsNullOrEmpty(item.serialNo))
                    {
                        string serialNo = CollectionOrderInit.serialNoList.Find(delegate(string no) { return no.Equals(item.serialNo); });
                        if (string.IsNullOrEmpty(serialNo))
                        {
                            MessageBox.Show("串码" + item.serialNo + "输入不正确！");
                            return;
                        }
                    }
                }
                
                if (CO.header.uncollectionAmount == 0)
                {
                    //设付款信息
                    setCollectionOrderData(DocStatus.VALID);
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
        private bool setCollectionOrderData(DocStatus status)
        {
            //设置表头
            //调用服务，获得单号
            string str = null;
            if (RetCode.NG == DevCommon.getDocId(DocType.COLLECTION, ref str))
            {
                return false;
            }
            CO.header.docId = str;
            CO.header.docStatus = ((int)status).ToString();
            CO.header.partyId = LoginInfo.OwnerPartyId;
            CO.header.storeId = LoginInfo.ProductStoreId;
            CO.header.createUserId = LoginInfo.PartyId;
            CO.header.lastUpdateUserId = LoginInfo.PartyId;
            //制单时间
            CO.header.createDocDate = DateTime.Now.ToString();
            CO.header.lastUpdateDocDate = CO.header.createDocDate;
            //过账日期设置
            CO.header.postingDate = DevCommon.PostingDate();

            //设置明细行
            //删除空白行
            CO.detail.RemoveAt(CO.detail.Count - 1);
            for (int i = 0; i < CO.detail.Count; i++)
            {
                CO.detail[i].docId = CO.header.docId;
                CO.detail[i].lineNo = i + 1;
            }

            return true;

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
                CODtl.type = this.textEdit_TypeNo.Text;
                CODtl.typeName = this.textEdit_TypeName.Text;
                CODtl.amount = Convert.ToDecimal(this.textEdit_Amount.EditValue);
                CODtl.code = this.textEdit_Code.Text;
                CODtl.serialNo = this.textEdit_SerialNo.Text;
                CODtl.cardCode = this.textEdit_CardCode.Text;
                CODtl.style = CollectionStyle.Collection;

                //没有内容，则返回
                if (string.IsNullOrEmpty(CODtl.type) || CODtl.amount <= 0)
                {
                    MessageBox.Show("输入项不正确！");
                    return;
                }

                if (this.gridView_Collection.IsLastRow == true)   //在最后一行，添加时候
                {                   
                    //明细表中增加当前显示的商品信息
                    CO.detail.Insert(CO.detail.Count - 1, CODtl);
                    CODtlNew = new CollectionOrderDtlModel();
                }
                else                                               //不在最后一行
                {
                    //明细表中更新当前显示的商品信息
                    CO.detail[this.gridView_Collection.GetDataSourceRowIndex(this.gridView_Collection.FocusedRowHandle)] = CODtl;
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
            this.gridControl_Collection.RefreshDataSource();
            this.gridView_Collection.MoveLast();

            //设置表头
            CO.header.uncollectionAmount = CO.header.collectionAmount - Convert.ToDecimal(gridColumn_Amount.SummaryItem.SummaryValue);
            this.textEdit_UncollectionAmount.EditValue = CO.header.uncollectionAmount;
        }

        private void gridViewCollection_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.gridView_Collection.IsLastRow == true)            //在最后一行是增加明细功能
            {
                CODtl = CODtlNew;
                this.simpleButton_Add.Text = "添加";
            }
            else if (this.gridView_Collection.FocusedRowHandle < this.gridView_Collection.RowCount)   //不在最后一行是修改明细功能
            {
                CODtl = (CollectionOrderDtlModel)CO.detail[this.gridView_Collection.GetDataSourceRowIndex(this.gridView_Collection.FocusedRowHandle)].Clone();
                this.simpleButton_Add.Text = "更新";
            }
            //更新表头的明细信息
            displayDetailData();
        }

        //将明细内容更新至显示数据
        private void displayDetailData()
        {
            this.textEdit_TypeNo.Text = CODtl.type;
            this.textEdit_TypeName.Text = CODtl.typeName;
            this.textEdit_Amount.EditValue = CODtl.amount;
            this.textEdit_Code.Text = CODtl.code;
            this.textEdit_SerialNo.Text = CODtl.serialNo;
            this.textEdit_CardCode.Text = CODtl.cardCode;
        }

        private void simpleButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_Collection.IsLastRow == false)           //最后一行空白行不能删除
                {
                    int nFoucusedRowHandle = this.gridView_Collection.FocusedRowHandle;
                    if (nFoucusedRowHandle == this.gridView_Collection.RowCount - 2)       //倒数第二行在删除后，在将焦点移动到最后一行，不触发FocusedRowChanged的事件，故需要先移动焦点
                    {
                        this.gridView_Collection.MoveLast();
                    }
                    //删除当前行数据
                    CO.detail.RemoveAt(nFoucusedRowHandle);
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
            CO = null;
            this.Close();
        }

        private void gridViewCollectionType_DoubleClick(object sender, EventArgs e)
        {
            int nDataSourceRowIndex = this.gridView_CollectionType.GetDataSourceRowIndex(this.gridView_CollectionType.FocusedRowHandle);
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

        //快捷键响应
        private void CollectionOrder_KeyDown(object sender, KeyEventArgs e)
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