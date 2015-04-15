using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model.Stock;
using DevExpress.XtraTab;

namespace WarehouseOut
{
    public partial class WarehouseOutOrderDetial : BaseForm
    {
        #region 参数
        public WarehouseOutHeaderDetailCommand commandHeader = null;
        public WarehouseOutOrder order = null;
        #endregion

        #region 构造
        public WarehouseOutOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ((XtraTabPage)this.Parent).Text = "出库指令单";
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
        private void WarehouseOutOrderDetial_Load(object sender, EventArgs e)
        {
            try
            {
                teCommandNo.Text = commandHeader.docId;
                teDocDate.Text = Convert.ToDateTime(commandHeader.updateDate).ToString("yyyy-MM-dd HH:mm:ss");
                teUserName.Text = commandHeader.updateUserName; ;
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
                if (DevCommon.getDataByWebService("WhsOutItemCommand", "WhsOutItemCommand", searchCondition, ref list) == RetCode.OK)
                {
                    gcWhsOutCommandDetail.DataSource = list;
                }
                else
                {
                    gcWhsOutCommandDetail.DataSource = null;
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