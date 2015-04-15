using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.XML;
using System.Collections;
using Commons.Model;
using Newtonsoft.Json;
using Commons.WebService;
using Newtonsoft.Json.Serialization;
using Commons.Model.Stock;
using System.Linq;

namespace Stock
{
    public partial class SerialQuery : BaseForm
    {
        #region 参数
        //商品代码
        string productId = null;
        //商品名称
        string productName = null;
        //条形码
        string barCode = null;
        //仓库
        string facilityId = null;
        //串号
        string sequence = null;
        //标示
        string mag = null;
        #endregion

        #region 构造
        public SerialQuery()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            m_frm.closeTab();
        }
        #endregion

        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            productId = teProductId.Text.Trim();
            productName = teProductName.Text.Trim();
            barCode = teBarCode.Text.Trim();
            facilityId = string.IsNullOrWhiteSpace(cboWarehouse.Text) == true ? "" : cboWarehouse.EditValue.ToString();
            sequence = teSequence.Text.Trim();
            GetSerialData();
        }
        #endregion

        #region 获取数据
        public void GetSerialData()
        {
            try
            {
                GetData.GetUrl("Stock", "SerialSearch", out mag);
                var searchCondition = new
                {
                    productId = productId,
                    productName = productName,
                    idValue = barCode,
                    facilityId = facilityId,
                    productStoreId = LoginInfo.ProductStoreId,
                    mag = mag,
                    sequenceId = sequence
                };
                List<StockDeTailModel> list = null;
                if (DevCommon.getDataByWebService("Stock", "SerialSearch", searchCondition, ref list) == RetCode.OK)
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
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void SerialQuery_Load(object sender, EventArgs e)
        {
            //获得仓库数据
            GetFacilityData();
            //获得库存数据
            //GetSerialData();
        }
        #endregion

        #region 获得仓库数据
        public void GetFacilityData()
        {
            try
            {
                DataTable dtFacility = DevCommon.Facility();
                if (dtFacility != null)
                {
                    DevCommon.initCmb(cboWarehouse, dtFacility, "facilityId", "facilityName", true);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
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
    }
}