using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.Stock;
using Commons.WinForm;
using DevExpress.XtraTab;

namespace WarehouseTransfer
{
    public partial class WarehouseTransferOrderDetial : BaseForm
    {
        #region 参数
        public TransferHeaderDetailCommand commandHeader = null;
        public WarehouseTransferOrder order = null;
        #endregion

        #region 构造
        public WarehouseTransferOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ((XtraTabPage)this.Parent).Text = "移库指令单";
                order.Visible = true;
                order.BringToFront();
                m_frm.PromptInformation("");
                this.Close();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 加载事件
        private void WarehouseTransferOrderDetial_Load(object sender, EventArgs e)
        {
            try
            {
                teDocDate.Text = commandHeader.updateDate;
                teUserName.Text = commandHeader.updateUserName;
                teDocId.Text = commandHeader.docId;
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
                var searchCondition = new { docId = commandHeader.docId };
                List<WarehouseOutItemCommandDetail> list = null;
                if (DevCommon.getDataByWebService("TransferItemCommand", "TransferItemCommand", searchCondition, ref list) == RetCode.OK)
                {
                    gcTransferItem.DataSource = list;
                }
                else
                {
                    gcTransferItem.DataSource = null;
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