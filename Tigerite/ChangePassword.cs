using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model;
using Commons.WinForm;

namespace Tigerite
{
    public partial class ChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword()
        {
            InitializeComponent();
            m_userLoginId = LoginInfo.UserLoginId;
        }
        public string m_userLoginId { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtNewPassword.Text.Equals(txtRePass.Text))
            {
                dxErrorProvider1.SetError(txtRePass, "密码验证错误");
            }
            else
            {
                var searchCondition = new
                {
                    userLoginId = m_userLoginId,
                    userLoginPwd = this.txtOldPasswd.Text.Trim(),
                    userLoginNewPwd = txtNewPassword.Text.Trim()
                };
               
                string msg = "";
                msg = DevCommon.getDataByWebServiceN("updateUserPassword", "updateUserPassword", searchCondition);
                if (msg.Equals("success"))
                {
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
            }
        }

        private void txtRePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control c = (Control)sender;
            if (e.KeyChar == 13 && c.Name.Equals("txtRePass"))
            {
                btnSave_Click(null,null);
            }
            else if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}