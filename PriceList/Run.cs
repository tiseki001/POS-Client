using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace PriceList
{
    public class Run
    {
        public bool ShowChangedPriceListQuery(BaseMainForm frm)
        {
            //主框架显示调价通知画面
            ChangedPriceListForm subfrm = new ChangedPriceListForm(frm, null);
            return frm.LoadFormToPanel(subfrm);
        }

        public bool ShowPriceListQuery(BaseMainForm frm)
        {
            //主框架显示价格列表画面//
            PriceListQueryForm subfrm = new PriceListQueryForm(frm, null);
            return frm.LoadFormToPanel(subfrm);
        }
    }
}
