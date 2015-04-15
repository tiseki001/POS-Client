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

namespace WarehouseIn
{
    public partial class WarehouseInOrderDetial : BaseForm
    {
        #region 参数
        public WarehouseInOrder order = null;
        public WarehouseInHeaderDetailCommand commandHeader = null;
        #endregion

        #region 构造
        public WarehouseInOrderDetial()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ((XtraTabPage)this.Parent).Text = "入库指令单";
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
        private void WarehouseInOrderDetial_Load(object sender, EventArgs e)
        {
            try
            {
                teCommandNo.Text=commandHeader.docId;
                teDocDate.Text = Convert.ToDateTime(commandHeader.updateDate).ToString("yyyy-MM-dd HH:mm:ss");
                teUserName.Text = commandHeader.updateUserName;
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
                List<WarehouseInItemCommandDetail> list = null;
                if (DevCommon.getDataByWebService("WhsInItemCommand", "WhsInItemCommand", searchCondition, ref list) == RetCode.OK)
                {
                    gcWhsInDetail.DataSource = list;
                }
                else
                {
                    gcWhsInDetail.DataSource = null;
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