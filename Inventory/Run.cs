using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace Inventory
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            Inventory inventoty = new Inventory();
            inventoty.m_frm = frm;
            return frm.LoadFormToPanel(inventoty);   
        }

        public bool SearchShow(BaseMainForm frm)
        {
            InventorySearch search = new InventorySearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            InventoryOrder order = new InventoryOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
