using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.Model;
using Commons.WinForm;

namespace SalesOutWhsOrder
{
    class SalesOutWhsOrderBLL
    {
        //设置出库单（销售直接出库用）
        static public SalesOutWhsOrderModel setSalesOutWhsOrderFromSalesOrder(SalesOrderModel SO, bool isUnlocked)
        {
            SalesOutWhsOrderModel WO = new SalesOutWhsOrderModel();
            
            //调用服务，获得单号
            string str = null;
            if (RetCode.NG == DevCommon.getDocId(DocType.SALESOUTWHS, ref str))
            {
                return null;
            }
            //表头
            WO.header.docId = str;
            WO.header.baseEntry = SO.header.docId;
            WO.header.productStoreId = LoginInfo.ProductStoreId;
            WO.header.docDate = DateTime.Now.ToString();
            WO.header.docStatus = ((int)DocStatus.VALID).ToString();
            WO.header.updateUserId = LoginInfo.PartyId;
            WO.header.updateDate = WO.header.docDate;
            WO.header.userId = LoginInfo.PartyId;
            WO.header.movementTypeId = MoveTypeKey.salesOrder;
            WO.header.partyId = LoginInfo.OwnerPartyId;
            WO.header.status = ((int)BusinessStatus.CLEARED).ToString();
            //过账日期设置
            WO.header.postingDate = DevCommon.PostingDate();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < SO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(SO.detail[i].docId))
                {
                    SalesOutWhsOrderDtlModel WOdtl = new SalesOutWhsOrderDtlModel();
                    WOdtl.docId = WO.header.docId;
                    WOdtl.baseEntry = WO.header.baseEntry;
                    WOdtl.lineNo = SO.detail[i].lineNo.ToString();
                    WOdtl.productId = SO.detail[i].productId;
                    WOdtl.isSequence = SO.detail[i].isSequence;
                    WOdtl.sequenceId = SO.detail[i].serialNo;
                    WOdtl.idValue = SO.detail[i].barCodes;
                    WOdtl.facilityId = SO.detail[i].facilityId;
                    WOdtl.quantity = SO.detail[i].quantity;
                    WOdtl.baseEntry = SO.detail[i].docId;
                    WOdtl.baseLineNo = SO.detail[i].lineNo.ToString();
                    if (isUnlocked)
                    {
                        WOdtl.unLockedQuantity = SO.detail[i].quantity;
                    }
                    else
                    {
                        WOdtl.unLockedQuantity = 0;
                    }
                    WO.item.Add(WOdtl);
                }
            }
            return WO;
        }

        //设置出库单（根据销售订单得到出库单）
        static public bool getSalesOutWhsOrderFromSalesOrder(SalesOrderModel SO, ref SalesOutWhsOrderModel WO)
        {
            WO.header.baseEntry = SO.header.docId;
            WO.header.productStoreId = LoginInfo.ProductStoreId;
            //WO.header.docDate = DateTime.Now.ToString();
            //WO.header.docStatus = ((int)DocStatus.VALID).ToString();
            //WO.header.updateUserId = LoginInfo.PartyId;
            //WO.header.updateDate = WO.header.docDate;
            WO.header.userId = LoginInfo.PartyId;
            WO.header.movementTypeId = MoveTypeKey.salesOrder;
            WO.header.partyId = LoginInfo.OwnerPartyId;
            //WO.header.status = ((int)BusinessStatus.CLEARED).ToString();

            //清空明细
            WO.item.Clear();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < SO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(SO.detail[i].docId))
                {
                    SalesOutWhsOrderDtlModel WOdtl = new SalesOutWhsOrderDtlModel();
                    WOdtl.docId = WO.header.docId;
                    WOdtl.baseEntry = WO.header.baseEntry;
                    WOdtl.lineNo = SO.detail[i].lineNo.ToString();
                    WOdtl.productId = SO.detail[i].productId;
                    WOdtl.isSequence = SO.detail[i].isSequence;
                    WOdtl.sequenceId = SO.detail[i].serialNo;
                    WOdtl.idValue = SO.detail[i].barCodes;
                    WOdtl.facilityId = SO.detail[i].facilityId;
                    WOdtl.saleQuantity = SO.detail[i].quantity;
                    WOdtl.quantity = SO.detail[i].quantity;
                    WOdtl.baseEntry = SO.detail[i].docId;
                    WOdtl.baseLineNo = SO.detail[i].lineNo.ToString();

                    WOdtl.warehouseQuantity = SO.detail[i].warehouseQuantity;
                    WOdtl.productName = SO.detail[i].productName;
                    WOdtl.unLockedQuantity = SO.detail[i].quantity;

                    WO.item.Add(WOdtl);
                }
            }
            return true;
        }

        //数据库写入订单
        static public bool createSalesOutWhsOrder(ref SalesOutWhsOrderModel WO)
        {
            try
            {
                String outStr = null;
                if (DevCommon.getDataByWebService("createSalesOutWhsOrder", "createSalesOutWhsOrder", WO, ref outStr) == RetCode.NG)
                {
                    return false;
                }

                //清空
                if (!WO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    WO = new SalesOutWhsOrderModel();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                throw ex;
            }
        }


        //数据库写入订单
        static public bool updateSalesOutWhsOrder(ref SalesOutWhsOrderModel WO)
        {
            try
            {
                String outStr = null;
                if (DevCommon.getDataByWebService("updateSalesOutWhsOrder", "updateSalesOutWhsOrder", WO, ref outStr) == RetCode.NG)
                {
                    return false;
                }


                //清空
                if (!WO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    WO = new SalesOutWhsOrderModel();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                throw ex;
            }

        }
    }
}
