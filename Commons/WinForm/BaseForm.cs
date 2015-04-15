using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace Commons.WinForm
{
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public BaseMainForm m_frm { get; set; }
        private void BaseForm_Load(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Arial"); 
            Font font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            initFont(this.Controls, font);
        }
        private void initFont(Control.ControlCollection ctls,Font font)
        {
            PropertyInfo p ;

            foreach (Control con in ctls)
            {
                p = con.GetType().GetProperty("Font", BindingFlags.Public | BindingFlags.Instance);
                if (p!=null&&p.CanWrite)
                {
                    p.SetValue(con, font,null);
                    //((LabelControl)con).Font = font;
                }
                if (con.Controls.Count > 0)
                    initFont(con.Controls, font);

            }
        }
    }
}