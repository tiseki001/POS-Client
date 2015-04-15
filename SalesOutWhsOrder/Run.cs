using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;
using Commons.Model.Order;

namespace SalesOutWhsOrder
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            SalesOutWhsOrder so = new SalesOutWhsOrder(frm, null, null);
            return frm.LoadFormToPanel(so);
        }

        public bool ShowQuery(BaseMainForm frm)
        {
            //主框架显示销售画面
            SalesOutWhsOrderQuery soq = new SalesOutWhsOrderQuery(frm, null);
            return frm.LoadFormToPanel(soq);
            return true;
        }

        //设置出库单
        public SalesOutWhsOrderModel setSalesOutWhsOrderFromSalesOrder(SalesOrderModel SO, bool isUnlocked)
        {
            return SalesOutWhsOrderBLL.setSalesOutWhsOrderFromSalesOrder(SO, isUnlocked);
        }
    }
}
