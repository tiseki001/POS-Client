using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace WarehouseReplenishment
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            ReplenishmentApply apply = new ReplenishmentApply();
            apply.m_frm = frm;
            return frm.LoadFormToPanel(apply);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            ReplenishmentSearch search = new ReplenishmentSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }
    }
}
