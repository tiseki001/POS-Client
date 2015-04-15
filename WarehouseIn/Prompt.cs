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

namespace WarehouseIn
{
    public partial class Prompt : DevExpress.XtraEditors.XtraForm
    {
        #region 参数
        public WarehouseIn whsIn = null;
        public List<WarehouseInItemDetail> item = null;
        #endregion

        #region 构造
        public Prompt()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void Prompt_Load(object sender, EventArgs e)
        {
            try
            {
                Data();
            }
            catch (Exception ex)
            {
            }
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

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 确定事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                WarehouseInItemDetail product = new WarehouseInItemDetail();
                product = (WarehouseInItemDetail)item.FirstOrDefault(p => p.lineNo == gridView1.GetFocusedRowCellValue("lineNo").ToString());
                whsIn.product = product;
                this.Close();
            }
        }
        #endregion

        #region 双击选择行信息
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                WarehouseInItemDetail product = new WarehouseInItemDetail();
                product = (WarehouseInItemDetail)item.FirstOrDefault(p => p.lineNo == gridView1.GetFocusedRowCellValue("lineNo").ToString());
                whsIn.product = product;
                this.Close();
            }
        }
        #endregion
    }
}