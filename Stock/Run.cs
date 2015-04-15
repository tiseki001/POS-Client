using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace Stock
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            StockQuery stock = new StockQuery();
            stock.m_frm = frm;
            return frm.LoadFormToPanel(stock);
        }

        public bool SerialShow(BaseMainForm frm)
        {
            SerialQuery serial = new SerialQuery();
            serial.m_frm = frm;
            return frm.LoadFormToPanel(serial);
        }
    }
}
