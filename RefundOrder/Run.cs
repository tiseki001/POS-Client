using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace RefundOrder
{
    public class Run
    {
        public getRefundFormResultModel Show(RefundOrderInitModel RFOI, CollectionOrderModel CO)
        {
            //主框架显示销售画面
            getRefundFormResultModel result = new getRefundFormResultModel();
            RefundOrder RFOForm = new RefundOrder(RFOI, CO);
            result.dialogResult = RFOForm.ShowDialog();
            result.RFO = RFOForm.RFO;
            return result;
        }

        //public bool Show()
        //{
        //    return true;
        //}
    }
}
