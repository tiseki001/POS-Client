using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace PreRefundOrder
{
    class PreRefundOrderBLL
    {
        //设置出库单（根据销售订单得到出库单）
        static public bool getPreRefundOrderFromPreCollectionOrder(PreCollectionOrderModel PCO, List<getPaymentMethodModel> PM, ref PreRefundOrderModel PRFO)
        {
            //清空明细
            PRFO.detail.Clear();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < PCO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(PCO.detail[i].docId))
                {
                    PreRefundOrderDtlModel PRFOdtl = new PreRefundOrderDtlModel();
                    PRFOdtl.type = PCO.detail[i].type;
                    PRFOdtl.typeName = PCO.detail[i].type;
                    PRFOdtl.lineNoBaseEntry = PCO.detail[i].lineNo;
                    PRFOdtl.preCollectionAmount = PCO.detail[i].amount;
                    PRFOdtl.style = PCO.detail[i].style;

                    getPaymentMethodModel resultPM = PM.Find(delegate(getPaymentMethodModel result) { return result.paymentMethodTypeId.Equals(PRFOdtl.type); });
                    if (resultPM != null)
                    {
                        PRFOdtl.typeName = resultPM.description;
                    }

                    PRFO.detail.Add(PRFOdtl);
                }
            }
            return true;
        }
    }
}
