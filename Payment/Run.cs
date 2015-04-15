using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace Payment
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            Payment payment = new Payment(frm, null, null);
            return frm.LoadFormToPanel(payment);   
        }

        public bool SearchShow(BaseMainForm frm)
        {
            //主框架显示销售查询画面
            PaymentSearch paymentSearch = new PaymentSearch(frm, null);
            return frm.LoadFormToPanel(paymentSearch);
        }
    }
}
