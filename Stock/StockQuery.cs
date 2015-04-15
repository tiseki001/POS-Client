using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using DevExpress.XtraTab;
using Commons.XML;
using Commons.WebService;
using System.Collections;
using Newtonsoft.Json;
using Commons.Model;
using Newtonsoft.Json.Serialization;
using System.Linq;
using Commons.Model.Stock;
using Commons.JSON;

namespace Stock
{
    public partial class StockQuery : BaseForm
    {
        #region 参数
        //商品代码
        string productId = null;
        //商品名称
        string productName = null;
        //条形码
        string barCode = null;
        //仓库编号
        string facilityId = null;
        //标示
        string mag = null;
        //库存存在库存
        string isInventory = null;
        #endregion

        #region 构造
        public StockQuery()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            m_frm.closeTab();
        }
        #endregion

        #region 他店查询
        private void btnOtherSearch_Click(object sender, EventArgs e)
        {
            OtherStockQuery otherStock = new OtherStockQuery();
            otherStock.Location = new Point(0, 0);
            otherStock.TopLevel = false;
            otherStock.TopMost = false;
            otherStock.ControlBox = false;
            otherStock.FormBorderStyle = FormBorderStyle.None;
            otherStock.Dock = DockStyle.Fill;
            this.Visible = true;
            this.Controls.Add(otherStock);
            otherStock.Show();
            otherStock.BringToFront();
        }
        #endregion

        #region 加载事件
        private void StockQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //是否存在库存
                isInventory = isInventoryItem.Checked ? IsInventory.isInventory : IsInventory.notInventory;
                //获得仓库数据
                GetFacilityData();
                //获得库存数据
                GetStockData();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获取库存数据
        public void GetStockData()
        {
            try
            {
                GetData.GetUrl("Stock", "StockSearch", out mag);
                var searchCondition = new
                {
                    productId = productId,
                    productName = productName,
                    idValue = barCode,
                    facilityId = facilityId,
                    productStoreId = LoginInfo.ProductStoreId,
                    mag = mag,
                    isInventory = isInventory
                };
                List<StockDeTailModel> list = null;
                if (DevCommon.getDataByWebService("Stock", "StockSearch", searchCondition, ref list) == RetCode.OK)
                {
                    gcStock.DataSource = list;
                }
                else
                {
                    gcStock.DataSource = null;
                }
                //获得焦点
                teProductId.Focus();
            }
            catch (Exception ex)
            {
                //获得焦点
                teProductId.Focus();
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 库存查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //是否存在库存
            isInventory = isInventoryItem.Checked ? IsInventory.isInventory : IsInventory.notInventory;
            productId = teProductId.Text.Trim();
            productName = teProductName.Text.Trim();
            barCode = teBarCode.Text.Trim();
            facilityId = string.IsNullOrEmpty(cboWarehuose.Text) ? "" : cboWarehuose.EditValue.ToString();
            GetStockData();
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

        #region 获得仓库数据
        public void GetFacilityData()
        {
            try
            {
                DataTable dtFacility = DevCommon.Facility();
                if (dtFacility!=null)
                {
                    DevCommon.initCmb(cboWarehuose, dtFacility, "facilityId", "facilityName", true);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion
    }
}
