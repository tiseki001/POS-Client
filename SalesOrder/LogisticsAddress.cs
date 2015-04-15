using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;

namespace SalesOrder
{
    public partial class LogisticsAddress : BaseForm
    {
        public string logisticsAddress;                 //物流信息
        public LogisticsAddress()
        {
            InitializeComponent();
        }

        //确定按下
        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textEdit_Address.Text))
            {
                MessageBox.Show("请输入物流信息！");
            }
            else
            {
                logisticsAddress = this.textEdit_Address.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        //窗体加载
        private void LogisticsAddress_Load(object sender, EventArgs e)
        {
            this.textEdit_Address.Text = logisticsAddress;
        }

        //快捷键响应
        private void KeyPreview_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F4))
                {
                    //确定
                    this.simpleButton_OK.PerformClick();
                }
                else if (e.KeyCode.Equals(Keys.Escape))
                {
                    //取消
                    this.simpleButton_Cancel.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}