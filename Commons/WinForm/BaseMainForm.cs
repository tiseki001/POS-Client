using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Commons.WinForm
{
    public partial class BaseMainForm : DevExpress.XtraEditors.XtraForm
    {
        public BaseMainForm()
        {
            InitializeComponent();
        }
        public virtual  bool LoadFormToPanel(BaseForm frm)
        {
            return false;
        }
        public virtual void closeTab()
        {
        }

        public virtual void PromptInformation(string message)
        { }
    }
}