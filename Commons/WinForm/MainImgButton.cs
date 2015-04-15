using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Commons.Model;

namespace Commons.WinForm
{
    public partial class MainImgButton : UserControl
    {

        public MainImgButton()
        {
            InitializeComponent();
            //if()
            
            foreach (Control cr in this.Controls)
            {
                //if (cr is Button)
                  //  cr.MouseMove += new MouseEventHandler(MainImgButton_MouseMove);
            } 
        }
        public bool isBorder = false;
        public MenuTable mt;
        
       
        [
        Category("Alignment"),
        Description("menu Id.")
        ]
        public string MenuId { get; set; }
        

        [
        Category("Alignment"),
        Description("type Id.")
        ]
        public string TypeId { get; set; }
        

        [
        Category("Alignment"),
        Description("Shortcuts key.")
        ]
        public string shortcuts {
            get
            {
                return lblShortcuts.Text;
            }

            set
            {
                lblShortcuts.Text = value;               
                Invalidate();
            }
        }

        [
        Category("Alignment"),
        Description("menu title.")
        ]
        public string menuTitle
        {
            get
            {
                return lblTitle.Text;
            }

            set
            {
                lblTitle.Text = value;
                Invalidate();
            }
        }

        [
        Category("Alignment"),
        Description("to do.")
        ]
        public string toReady
        {
            get
            {
                return this.lblReady.Text;
            }

            set
            {
                lblReady.Text = value;
                int result;
                if (int.TryParse(lblReady.Text, out result) && int.Parse(lblReady.Text) > 0)
                {
                  //  if (thread==null)
                      //  thread = new Thread(new ThreadStart(ColorChange));
                  //  if (!thread.IsAlive)
                       // thread.Start();
                }
                else
                {
                  //  if (thread != null)
                     //   thread.Suspend();
                   // this.lblReady.ForeColor = Color.FromArgb(0, 0, 0);
                }
            }
        }


        //lable字体颜色变化
        private void ColorChange()
        {
            Random random = new Random();
            while (true)
            {
                int red = random.Next(1, 256);
                int green = random.Next(1, 256);
                int blue = random.Next(1, 256);
                this.lblReady.ForeColor = Color.FromArgb(red, green, blue);
                Thread.Sleep(500);
            }
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
           // this.set = (int.Parse(lblReady.Text) + 1).ToString();
            
        }
        [
        Category("Alignment"),
        Description("picture")
        ]
        public System.Drawing.Image picture
        {
            get
            {
                return this.picMain.BackgroundImage;
            }

            set
            {
                this.picMain.BackgroundImage = value;
               // Invalidate();
            }
        }
        [
        Category("Alignment"),
        Description("back color")
        ]
        public System.Drawing.Color backColor
        {
            get
            {
                return this.tableLayoutPanel1.BackColor;
            }

            set
            {
                this.tableLayoutPanel1.BackColor = value;
              //  Invalidate();
            }
        }

        private void MainImgButton_MouseMove(object sender, MouseEventArgs e)
        {
           // UserControl pel = (UserControl)sender;
           // this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            
            //if (!isBorder)
            //{
            //    isBorder = true;
            //    this.Invalidate();
            //}
            
        }

        private void MainImgButton_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void MainImgButton_MouseLeave(object sender, EventArgs e)
        {
            //UserControl pel = (UserControl)sender;
            isBorder = false;
        }

        private void picMain_Click(object sender, EventArgs e)
        {
            if (UserControlBtnClicked != null)
                UserControlBtnClicked(sender, new EventArgs());//把按钮自身作为参数传递
        }
        //定义委托
        public delegate void BtnClickHandle(object sender, EventArgs e);
        //定义事件
        public event BtnClickHandle UserControlBtnClicked;

        private void tableLayoutPanel1_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TypeId) && TypeId.Equals("2"))
            {
                TableLayoutRowStyleCollection styles =
                this.tableLayoutPanel1.RowStyles;
                styles[1].Height = 30;
                TableLayoutColumnStyleCollection stylesc = this.tableLayoutPanel1.ColumnStyles;
                stylesc[1].Width = 30;
                //int y = panelControl2.Location.Y + panelControl2.Size.Height - this.lblTitle.Size.Height - 2;
                // this.lblTitle.Location = new Point(this.lblTitle.Location.X, this.lblTitle.Location.Y + y);


            }
        }
       
    }
}
