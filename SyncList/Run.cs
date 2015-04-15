using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Commons.WinForm;

namespace SyncList
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            SyncForm sf = new SyncForm();
          //  SalesOutWhsOrder so = new SalesOutWhsOrder(frm, null, null);
            return frm.LoadFormToPanel(sf);
        }
    }
}
