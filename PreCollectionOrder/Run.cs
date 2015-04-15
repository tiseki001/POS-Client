using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace PreCollectionOrder
{
    public class Run
    {
        public getPreCollectionFormResultModel Show(CollectionOrderInitModel COI)
        {
            //主框架显示销售画面          
            getPreCollectionFormResultModel result = new getPreCollectionFormResultModel();
            PreCollectionOrder PCOForm = new PreCollectionOrder(COI);
            result.dialogResult = PCOForm.ShowDialog();
            result.PCO = PCOForm.PCO;
            return result;
        }
    }
}
