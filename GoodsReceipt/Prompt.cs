using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.Stock;
using System.Linq;

namespace GoodsReceipt
{
    public partial class Prompt : DevExpress.XtraEditors.XtraForm
    {
        #region 参数
        public List<ReceiptItemDetail> item = null;
        private ReceiptItemDetail product = new ReceiptItemDetail();
        public Receive receive = null;
        #endregion

        #region 构造
        public Prompt()
        {
            InitializeComponent();
        }
        #endregion

        #region 确定
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gridView1.RowCount>0)
            {
                product=(ReceiptItemDetail)item.FirstOrDefault(p => p.lineNo == gridView1.GetFocusedRowCellValue("lineNo").ToString());
                receive.product = product;
                this.Close();
            }
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 加载事件
        private void Prompt_Load(object sender, EventArgs e)
        {
            Data();
        }
        #endregion

        #region 数据加载
        public void Data()
        {
            try
            {
                gridControl1.DataSource = item;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 双击选择行数据
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                product = (ReceiptItemDetail)item.FirstOrDefault(p => p.lineNo == gridView1.GetFocusedRowCellValue("lineNo").ToString());
                receive.product = product;
                this.Close();
            }
        }
        #endregion
    }
}