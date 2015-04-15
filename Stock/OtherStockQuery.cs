using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using System.Collections;
using Commons.WebService;
using Commons.Model;
using Newtonsoft.Json;
using Commons.XML;
using System.Linq;
using Commons.Model.Stock;

namespace Stock
{
    public partial class OtherStockQuery : BaseForm
    {
        #region 参数
        /// <summary>
        /// 他店信息
        /// </summary>
        DataTable dtStore = null;
        List<string> list = new List<string>();
        //商品代码
        string productId = null;
        //商品名称
        string productName = null;
        //条形码
        string barCode = null;
        //标示
        string mag = null;
        #endregion

        #region 构造
        public OtherStockQuery()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 获取数据
        public void GetStockData()
        {
            try
            {
                GetData.GetUrl("Stock", "StockOtherSearch", out mag);
                var searchCondition = new { 
                    productId = productId, 
                    productName = productName, 
                    idValue = barCode, 
                    mag = mag, 
                    otherStore = list };
                List<StockDeTailModel> listOther = null;
                if (DevCommon.getDataByWebService("Stock", "StockOtherSearch", searchCondition, ref listOther) == RetCode.OK)
                {
                    gcOtherStock.DataSource = listOther;
                }
                else
                {
                    gcOtherStock.DataSource = null;
                }
                //获得焦点
                teProductId.Focus();
            }
            catch (Exception ex)
            {
                //获得焦点
                teProductId.Focus();
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void OtherStockQuery_Load(object sender, EventArgs e)
        {
            //获得店铺信息
            GetStoreData();
            //获得焦点
            teProductId.Focus();
        }
        #endregion

        #region 获得店铺信息
        public void GetStoreData()
        {
            dtStore = new DataTable();
            dtStore.Columns.Add("storeId", typeof(string));
            dtStore.Columns.Add("storeName", typeof(string));

            DataRow dr = dtStore.NewRow();
            dr["storeId"] = "10001";
            dr["storeName"] = "东单店";
            dtStore.Rows.Add(dr);

            DataRow dr1 = dtStore.NewRow();
            dr1["storeId"] = "10002";
            dr1["storeName"] = "朝阳店";
            dtStore.Rows.Add(dr1);
            DevCommon.initCmb(cboStore, dtStore, true);
        }
        #endregion

        #region 键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teProductId.Text.Trim())
                && string.IsNullOrEmpty(teProductName.Text.Trim())
                && string.IsNullOrEmpty(teBarCode.Text.Trim()))
            {
                MessageBox.Show("请输入查询条件");
                teProductId.Focus();
                return;
            }
            productId = teProductId.Text.Trim();
            productName = teProductName.Text.Trim();
            barCode = teBarCode.Text.Trim();
            list.Clear();
            //获得门店编号
            if (dtStore != null)
            {
                if (string.IsNullOrEmpty(cboStore.Text.Trim()))
                {
                    foreach (DataRow item in dtStore.Rows)
                    {
                        list.Add(item["storeId"].ToString());
                    }
                }
                else
                {
                    list.Add(cboStore.EditValue.ToString());
                }
            }
            GetStockData();
        }
        #endregion
    }
}