using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.XML;
using System.Collections;
using Commons.WebService;
using Commons.Model;
using Newtonsoft.Json;
using Commons.Model.Stock;
using Commons.WinForm;
using DevExpress.XtraTab;

namespace GoodsDelivery
{
    public partial class DeliveryOrderDetial : BaseForm
    {
        #region 参数
        public DeliveryHeaderDetailCommand headerItem = null;
        public DeliveryOrder order = null;
        #endregion

        #region 构造
        public DeliveryOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((XtraTabPage)this.Parent).Text = "调拨发货指令单";
            
            order.Visible = true;
            order.BringToFront();
            m_frm.PromptInformation("");
            this.Close();
        }
        #endregion

        #region 获得命令数据
        public void Data()
        {
            try
            {
                var searchCondition = new { docId = headerItem.docId };
                List<DeliveryItemDetailCommand> list = null;
                if (DevCommon.getDataByWebService("GoodsDeliveryItemCommand", "DeliveryItemCommandSearch", searchCondition, ref list) == RetCode.OK)
                {
                    gcDetial.DataSource = list;
                }
                else
                {
                    gcDetial.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void DeliveryOrderDetial_Load(object sender, EventArgs e)
        {
            //单号
            teCommandNo.Text = headerItem.docId;
            //制单 时间
            teDate.Text = Convert.ToDateTime(headerItem.docDate).ToString("yyyy-MM-dd HH:mm:ss");
            //制单人
            teUserName.Text = headerItem.userName;
            //获取数据
            Data();
        }
        #endregion
    }
}