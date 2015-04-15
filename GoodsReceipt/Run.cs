using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace GoodsReceipt
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            Receive receive = new Receive();
            receive.m_frm = frm;
            return frm.LoadFormToPanel(receive);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            ReveiveSearch search = new ReveiveSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            ReceiveOrder order = new ReceiveOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
