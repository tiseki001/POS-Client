using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Commons.Model;
using Commons.WinForm;

namespace Tigerite
{
    public partial class HomeView : DevExpress.XtraEditors.XtraForm
    {
        private string[] m_shotkey;
        List<MenuTable> m_List= null;
        public HomeView()
        {
            InitializeComponent();
            //tableLayoutPanel1.dou
            Form.CheckForIllegalCrossThreadCalls = false;
            m_shotkey = new string[2];
        }

        public MainForm MForm { get; set; }

        int count = 0;
        private void MainFrm_Load(object sender, EventArgs e)
        {
           // cmbuser.
            this.Dock = DockStyle.None;
            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            Login loginfrm = new Login();
            if (loginfrm.ShowDialog() == DialogResult.OK)
            {
                m_List = Commons.Model.MenuWorker.GetMenuData();

                initMainButton(this.Controls, m_List);
            }
            else
            {
                System.Environment.Exit(0);
            }
            lblStoreName.Text =  LoginInfo.StoreName;
            lblName.Text = LoginInfo.UserName;
        }

        private void initMainButton(Control.ControlCollection ctls, List<MenuTable> mList)
        {
            MainImgButton mib;
            MenuTable mt;
            foreach (Control con in ctls)
            {
                if (con.GetType() == typeof(Commons.WinForm.MainImgButton))
                {
                    mib = (MainImgButton)con;
                    if (!string.IsNullOrEmpty(mib.MenuId))
                    {
                        mt = findMenu(mList, mib.MenuId);
                        mib.menuTitle = mt.MeunShortcuts+" "+mt.MeunName;
                        mib.shortcuts = mt.MeunShortcuts;
                        mib.picture = Image.FromFile(Application.StartupPath + "\\Pic\\" + mt.MeunPic);
                        mib.mt = mt;

                    }
                    continue;
                }
                else if (con.Controls.Count > 0)
                    initMainButton(con.Controls, mList);
                else
                {
                    
                }
            }
        }

        private MenuTable findMenu(List<MenuTable> mList, string id)
        {
            foreach (MenuTable mt in mList)
            {
                if (mt.MeunId.Equals(id))
                    return mt;
            }
            return null;
        }

        private void label56_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void MainFrm_2_FormClosed(object sender, FormClosedEventArgs e)
        {
           // System.Environment.Exit(0);
        }

        private void mainImgButton1_UserControlBtnClicked(object sender, EventArgs e)
        {
            if (MForm == null)
            {
                MForm = new MainForm();
                MForm.m_Home = this;
            }
            MForm.Show();
            Control pic = (Control)sender;
            Control parent = getParent(pic);
            if (parent == null)
                return;
            MainImgButton mb = (MainImgButton)parent;
            
            
            if(!string.IsNullOrEmpty(mb.mt.MeunDll))
                MForm.LoadSubForm(mb.mt);
            this.Hide();
        }

        private Control getParent(Control con)
        {
            if(con.Parent.GetType() == typeof(MainImgButton))
                return con.Parent;
            else if (con.Parent != null)
            {
                return getParent(con.Parent);
            }
            return null;
        }

        private void mainImgButton17_UserControlBtnClk(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void HomeView_KeyDown(object sender, KeyEventArgs e)
        {
            m_shotkey[0] = m_shotkey[1];
            if (e.KeyCode.ToString().Substring(0, 1).Equals("D"))
                m_shotkey[1] = e.KeyCode.ToString().Substring(1, e.KeyCode.ToString().Length - 1);
            else
                m_shotkey[1] = "";
            string shotkey = m_shotkey[0] + m_shotkey[1];
            foreach (MenuTable mt in m_List)
            {
                if (!string.IsNullOrEmpty(mt.MeunShortcuts) && mt.MeunShortcuts.Equals(shotkey))
                {
                    if (MForm == null)
                    {
                        MForm = new MainForm();
                        MForm.m_Home = this;
                    }
                    MForm.Show();



                    if (!string.IsNullOrEmpty(mt.MeunDll))
                        MForm.LoadSubForm(mt);
                    this.Hide();
                    m_shotkey[0] = "";
                    m_shotkey[1] = "";
                    return;
                }
            }
            
            
            //MessageBox.Show(e.KeyCode.ToString());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            popupMenu1.ShowPopup(pictureBox1.PointToScreen(new Point(pictureBox1.Location.X, pictureBox1.Location.Y + pictureBox1.Size.Height)));

        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
           // System.Environment.Exit(0);

            Application.Restart();
        }

        private void btnChangePasswd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.ShowDialog();
        }
    }
}