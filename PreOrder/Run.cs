using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace PreOrder
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            PreOrder po = new PreOrder(frm, null, null);
            return frm.LoadFormToPanel(po); 
        }

        public bool ShowQuery(BaseMainForm frm)
        {
            //主框架显示销售画面
            PreOrderQuery poq = new PreOrderQuery(frm, null);
            return frm.LoadFormToPanel(poq);
        }
    }
}
