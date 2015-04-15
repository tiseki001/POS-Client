using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model;
using Commons.JSON;
using System.Threading;
using DevExpress.Utils;

namespace Tigerite
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        #region 构造方法
        public Login()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginLoad();
        }
        #endregion

        #region 设置事件
        private void btnSet_Click(object sender, EventArgs e)
        {
            Config con = new Config();
            con.ShowDialog();
        }
        #endregion

        #region 退出事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
           Application.Exit();
           // System.Environment.Exit(0);
        }
        #endregion

        #region 登录方法
        public void LoginLoad()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    var searchCondition = new
                    {
                        userLoginId = txtUserName.Text.Trim(),
                        userLoginPwd = txtPassword.Text.Trim()
                    };
                    LoginUser login = null;
                    BasePosModel msg = DevCommon.getDataByWebServiceN("Login", "Login", searchCondition, ref login);
                    if (!string.IsNullOrEmpty(msg.flag) && msg.flag == "S")
                    {
                        
                        #region 用户信息赋值
                        LoginInfo.UserName = login.UserName;
                        LoginInfo.UserLoginId = login.UserLoginId;
                        LoginInfo.StoreName = login.StoreName;
                        LoginInfo.ProductStoreId = login.ProductStoreId;
                        LoginInfo.PartyId = login.PartyId;
                        LoginInfo.CompanyName = login.CompanyName;
                        LoginInfo.OwnerPartyId = login.OwnerPartyId;
                        #endregion
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else if (!string.IsNullOrEmpty(msg.flag) && msg.flag == "I")
                    {

                        WaitDialogForm frm = new WaitDialogForm("提示", " 正在更新服务......");

                        int i = 60;
                        for (int j = 1; j < i; j++)
                        {
                            Thread.Sleep(1000);
                            frm.SetCaption("执行进度（" + j.ToString() + "/" + i.ToString() + "）");
                        }
                        frm.Close();
                        Application.Restart();
                    }
                    else if (!string.IsNullOrEmpty(msg.flag) && msg.flag == "U")
                    {
                        ChangePassword cp = new ChangePassword();
                        cp.m_userLoginId = this.txtUserName.Text;
                        cp.Text = "第一次登陆请修改密码";
                        cp.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(msg.msg);
                    }
                }
                else
                {
                    MessageBox.Show("输入用户名和密码！");
                    //this.DialogResult = System.Windows.Forms.DialogResult.;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            Control c = (Control)sender;
            if (e.KeyChar == 13 && c.Name.Equals("txtPassword"))
            {
                LoginLoad();
            }
            else if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
           
            
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}