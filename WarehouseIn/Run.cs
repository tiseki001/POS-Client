using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace WarehouseIn
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            WarehouseIn warehouseIn = new WarehouseIn();
            warehouseIn.m_frm = frm;
            return frm.LoadFormToPanel(warehouseIn);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            WarehouseInSearch search = new WarehouseInSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            WarehouseInOrder order = new WarehouseInOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
