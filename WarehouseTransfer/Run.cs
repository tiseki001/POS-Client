using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace WarehouseTransfer
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            WarehouseTransfer transfer = new WarehouseTransfer();
            transfer.m_frm = frm;
            return frm.LoadFormToPanel(transfer);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            WarehouseTransferSearch search = new WarehouseTransferSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }

        public bool OrderShow(BaseMainForm frm)
        {
            WarehouseTransferOrder order = new WarehouseTransferOrder();
            order.m_frm = frm;
            return frm.LoadFormToPanel(order);
        }
    }
}
