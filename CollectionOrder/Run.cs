using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;
using Commons.Model.Order;

namespace CollectionOrder
{
    public class Run
    {
        public getCollectionFormResultModel Show(CollectionOrderInitModel COI)
        {
            //主框架显示销售画面          
            getCollectionFormResultModel result = new getCollectionFormResultModel();
            CollectionOrder COForm = new CollectionOrder(COI);
            result.dialogResult = COForm.ShowDialog();
            result.CO = COForm.CO;
            return result;
        }

        //public bool ShowQuery(BaseMainForm frm)
        //{
        //    //主框架显示销售画面
        //    CollectionOrder coq = new CollectionOrder(frm, null);
        //    return frm.LoadFormToPanel(coq);
        //}
    }
}
