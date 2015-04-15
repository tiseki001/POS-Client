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
using DevExpress.XtraTab;

using Commons.DLL;
using DevExpress.XtraTreeList.Nodes;

namespace Tigerite
{
    public partial class MainForm : BaseMainForm
    {
        #region 构造
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 参数
        private List<MenuTable> mList;
        public HomeView m_Home { get; set; }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            mList = Commons.Model.MenuWorker.GetMenuData();
            tlMain.DataSource = mList;
            tlMain.ExpandAll();
            this.Text = "恒波零售管理系统" + "  " + LoginInfo.StoreName;
        }

        private void tlMain_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            MenuTable mt = mList[e.Node.Id];
            if (!string.IsNullOrEmpty(mt.MeunDll))
            {
                LoadSubForm(mt);
                e.Node.ParentNode.Selected = true;
                
            }
        }
        
        public void LoadSubForm(MenuTable mt)
        {
            object result;
            DllInvoke.Invoke(mt.MeunDll, mt.MeunClass, mt.MeunMethod, new object[] { this }, out result);
        }
       
        public override bool LoadFormToPanel(BaseForm frm)
        {
            XtraTabPage tp = xtcMain.TabPages.Add(frm.Text);
            frm.Location = new Point(0, 0);
            frm.TopLevel = false;
            frm.TopMost = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            tp.Controls.Add(frm);
                
            frm.Show();
            frm.BringToFront();
            xtcMain.SelectedTabPage = tp;
            tp.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            PromptInformation("");
            return true;
        }

        #region tabPage关闭事件
        private void xtcMain_CloseButtonClick(object sender, EventArgs e)
        {
            closeTab();
        }

        public override void closeTab()
        {
            PromptInformation("");
            xtcMain.SelectedTabPage.Dispose();
            if (xtcMain.TabPages.Count <= 0)
            {
                m_Home.Show();
                this.Hide();
            }
        }
        #endregion

        public override void PromptInformation(string message)
        {
            meMessage.Text = message;
        }

        #region 窗体关闭事件
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion
    }
}