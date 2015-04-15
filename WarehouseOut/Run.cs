using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace WarehouseOut
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            WarehouseOut warehouseOut = new WarehouseOut();
            warehouseOut.m_frm = frm;
            return frm.LoadFormToPanel(warehouseOut);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            WarehouseOutSearch search = new WarehouseOutSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            WarehouseOutOrder order = new WarehouseOutOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
