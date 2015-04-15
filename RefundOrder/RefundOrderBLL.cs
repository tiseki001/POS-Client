using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace RefundOrder
{
    class RefundOrderBLL
    {
        //设置出库单（根据销售订单得到出库单）
        static public bool getRefundOrderFromCollectionOrder(CollectionOrderModel CO, List<getPaymentMethodModel> PM, ref RefundOrderModel RFO)
        {
            //WO.header.baseEntry = SO.header.docId;
            //WO.header.productStoreId = LoginInfo.ProductStoreId;
            ////WO.header.docDate = DateTime.Now.ToString();
            ////WO.header.docStatus = ((int)DocStatus.VALID).ToString();
            ////WO.header.updateUserId = LoginInfo.PartyId;
            ////WO.header.updateDate = WO.header.docDate;
            //WO.header.userId = LoginInfo.PartyId;
            //WO.header.movementTypeId = MoveTypeKey.salesOrder;
            //WO.header.partyId = LoginInfo.OwnerPartyId;
            ////WO.header.status = ((int)BusinessStatus.CLEARED).ToString();

            //清空明细
            RFO.detail.Clear();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < CO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(CO.detail[i].docId))
                {
                    RefundOrderDtlModel RFOdtl = new RefundOrderDtlModel();
                    RFOdtl.type = CO.detail[i].type;
                    RFOdtl.typeName = CO.detail[i].type;
                    RFOdtl.lineNoBaseEntry = CO.detail[i].lineNo;
                    RFOdtl.collectionAmount = CO.detail[i].amount;
                    RFOdtl.style = CO.detail[i].style;

                    getPaymentMethodModel resultPM = PM.Find(delegate(getPaymentMethodModel result) { return result.paymentMethodTypeId.Equals(RFOdtl.type); });
                    if (resultPM != null)
                    {
                        RFOdtl.typeName = resultPM.description;
                    }

                    RFO.detail.Add(RFOdtl);
                }
            }
            return true;
        }
    }
}
