using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.Stock;
using DevExpress.XtraTab;
using Commons.WinForm;

namespace GoodsReceipt
{
    public partial class ReceiveOrderDetial : BaseForm
    {
        #region 参数
        public ReceiptHeaderCommandDetail headerItem = null;
        public ReceiveOrder order=null;
        #endregion 

        #region 构造
        public ReceiveOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            order.Visible = true;
            order.BringToFront();
            ((XtraTabPage)this.Parent).Text = "调拨收货指令单";
            m_frm.PromptInformation("");
            this.Close();
        }
        #endregion

        #region 加载事件
        private void ReceiveOrderDetial_Load(object sender, EventArgs e)
        {
            try
            {
                //单号
                teCommandNo.Text = headerItem.docId;
                //制单时间
                teDocDate.Text = headerItem.docDate;
                //制单人
                teUserName.Text = headerItem.updateUserName;
                //收货指令明细
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得命令数据
        public void Data()
        {
            try
            {
                var searchCondition = new { docId = headerItem.docId };
                List<ReceiptItemCommandDetail> list = null;
                if (DevCommon.getDataByWebService("GoodsReceiveItemCommand", "GoodsReceiveItemCommand", searchCondition, ref list) == RetCode.OK)
                {
                    gcReceiveCommand.DataSource = list;
                }
                else
                {
                    gcReceiveCommand.DataSource = null;
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