using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace GoodsDelivery
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            Delivery delivery = new Delivery();
            delivery.m_frm = frm;
            return frm.LoadFormToPanel(delivery);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            DeliverySearch search = new DeliverySearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            DeliveryOrder order = new DeliveryOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
