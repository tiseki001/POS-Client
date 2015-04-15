using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace BackOrder
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            //主框架显示销售画面
            BackOrder bo = new BackOrder(frm, null, null);
            return frm.LoadFormToPanel(bo);
        }

        public bool ShowQuery(BaseMainForm frm)
        {
            //主框架显示销售查询画面
            BackOrderQuery boq = new BackOrderQuery(frm, null);
            return frm.LoadFormToPanel(boq);
            return true;
        }
    }
}
