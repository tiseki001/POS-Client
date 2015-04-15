using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace PreRefundOrder
{
    public class Run
    {
        public getPreRefundFormResultModel Show(RefundOrderInitModel RFOI, PreCollectionOrderModel PCO)
        {
            //主框架显示销售画面
            getPreRefundFormResultModel result = new getPreRefundFormResultModel();
            PreRefundOrder PRFOForm = new PreRefundOrder(RFOI, PCO);
            result.dialogResult = PRFOForm.ShowDialog();
            result.PRFO = PRFOForm.PRFO;
            return result;
        }
    }
}
