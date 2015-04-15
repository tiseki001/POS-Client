using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace SalesOrder
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            SalesOrder so = new SalesOrder(frm, null, null);
            return frm.LoadFormToPanel(so);           
        }

        public bool ShowQuery(BaseMainForm frm)
        {
            //主框架显示销售查询画面
            SalesOrderQuery soq = new SalesOrderQuery(frm, null);
            return frm.LoadFormToPanel(soq);
        }
    }
}
