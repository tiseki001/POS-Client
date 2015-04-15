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

namespace Inventory
{
    public partial class InventoryOrderDetial : BaseForm
    {
        #region 参数
        public InventoryOrder order = null;
        public InventoryHeaderDetailCommand headerCommand = null;
        #endregion 

        #region 构造
        public InventoryOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ((XtraTabPage)this.Parent).Text = "盘点指令";
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
        private void InventoryOrderDetial_Load(object sender, EventArgs e)
        {
            try
            {
                teCommandNo.Text = headerCommand.docId;
                teDocDate.Text = Convert.ToDateTime(headerCommand.docDate).ToString("yyyy-MM-dd HH:mm:ss");
                teInventoryName.Text = headerCommand.description;
                teUserName.Text = headerCommand.userName;
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
                var searchCondition = new { docId = headerCommand.docId };
                List<InventotyDetailItemCommand> list = null;
                if (DevCommon.getDataByWebService("SearchInventiryItemCommand", "SearchInventiryItemCommand", searchCondition, ref list) == RetCode.OK)
                {
                    gcCommandItem.DataSource = list;
                }
                else
                {
                    gcCommandItem.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}