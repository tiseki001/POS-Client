using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.WinForm;
using Commons.Model;

namespace ReturnInWhsOrder
{
    class ReturnInWhsBLL
    {
        //设置出库单（销售直接出库用）
        static public ReturnInWhsOrderModel setReturnInWhsOrderFromReturnOrder(ReturnOrderModel RO)
        {
            ReturnInWhsOrderModel IO = new ReturnInWhsOrderModel();

            //调用服务，获得单号
            string str = null;
            if (RetCode.NG == DevCommon.getDocId(DocType.RETURNINWHS, ref str))
            {
                return null;
            }
            //表头
            IO.header.docId = str;
            IO.header.baseEntry = RO.header.docId;
            IO.header.productStoreId = LoginInfo.ProductStoreId;
            IO.header.docDate = DateTime.Now.ToString();
            IO.header.docStatus = ((int)DocStatus.VALID).ToString();
            IO.header.updateUserId = LoginInfo.PartyId;
            IO.header.updateDate = IO.header.docDate;
            IO.header.userId = LoginInfo.PartyId;
            IO.header.movementTypeId = RO.header.movementTypeId;
            IO.header.partyId = LoginInfo.OwnerPartyId;
            IO.header.status = ((int)BusinessStatus.CLEARED).ToString();
            //过账日期设置
            IO.header.postingDate = DevCommon.PostingDate();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < RO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(RO.detail[i].docId))
                {
                    ReturnInWhsOrderDtlModel IOdtl = new ReturnInWhsOrderDtlModel();
                    IOdtl.docId = IO.header.docId;
                    IOdtl.baseEntry = IO.header.baseEntry;
                    IOdtl.lineNo = RO.detail[i].lineNo.ToString();
                    IOdtl.productId = RO.detail[i].productId;
                    IOdtl.isSequence = RO.detail[i].isSequence;
                    IOdtl.sequenceId = RO.detail[i].serialNo;
                    IOdtl.idValue = RO.detail[i].barCodes;
                    IOdtl.facilityId = RO.detail[i].facilityId;
                    IOdtl.quantity = RO.detail[i].quantity;
                    IOdtl.baseEntry = RO.detail[i].docId;
                    IOdtl.baseLineNo = RO.detail[i].lineNo.ToString();
                    IO.item.Add(IOdtl);
                }
            }
            return IO;
        }
    }
}
