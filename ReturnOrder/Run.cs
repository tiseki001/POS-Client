using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace ReturnOrder
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            ReturnOrder so = new ReturnOrder(frm, null, null);
            return frm.LoadFormToPanel(so);
        }

        public bool ShowQuery(BaseMainForm frm)
        {
            //主框架显示销售查询画面
            ReturnOrderQuery roq = new ReturnOrderQuery(frm, null);
            return frm.LoadFormToPanel(roq);
            return true;
        }
    }
}
