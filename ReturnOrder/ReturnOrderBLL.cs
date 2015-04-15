using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.Model;
using System.Windows.Forms;
using Commons.DLL;
using Commons.WinForm;

namespace ReturnOrder
{
    class ReturnOrderBLL
    {
        //设置退货单（根据销售订单得到退货单）
        static public bool getReturnOrderFromSalesOrder(SalesOrderModel SO, ref ReturnOrderModel RO)
        {
            RO.header.baseEntry = SO.header.docId;

            //清空明细
            RO.detail.Clear();
            
            //明细
            for (int i = 0; i < SO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(SO.detail[i].docId))
                {
                    //还没有退货完毕的才有效
                    if (SO.detail[i].quantity - SO.detail[i].returnQuantity > 0)
                    {
                        ReturnOrderDtlModel ROdtl = new ReturnOrderDtlModel();
                        ROdtl.productId = SO.detail[i].productId;
                        ROdtl.productName = SO.detail[i].productName;
                        ROdtl.isSequence = SO.detail[i].isSequence;
                        ROdtl.serialNo = SO.detail[i].serialNo;
                        ROdtl.barCodes = SO.detail[i].barCodes;
                        ROdtl.baseEntry = SO.detail[i].docId;
                        ROdtl.lineNoBaseEntry = SO.detail[i].lineNo;
                        ROdtl.quantity = SO.detail[i].quantity - SO.detail[i].returnQuantity;
                        ROdtl.unitPrice = SO.detail[i].unitPrice;
                        ROdtl.rebatePrice = SO.detail[i].rebatePrice;
                        ROdtl.rebateAmount = ROdtl.quantity * SO.detail[i].rebatePrice;
                        ROdtl.bomId = SO.detail[i].bomId;
                        ROdtl.bomName = SO.detail[i].bomName;
                        ROdtl.productSalesPolicyId = SO.detail[i].productSalesPolicyId;
                        ROdtl.productSalesPolicyName = SO.detail[i].productSalesPolicyName;
                        ROdtl.productSalesPolicyNo = SO.detail[i].productSalesPolicyNo;
                        ROdtl.bomAmount = SO.detail[i].bomAmount * ROdtl.quantity / SO.detail[i].quantity;
                        ROdtl.isMainProduct = SO.detail[i].isMainProduct;
                        //ROdtl.facilityId = SO.detail[i].facilityId;

                        //ROdtl.facilityId = SO.detail[i].facilityId;
                        //ROdtl.saleQuantity = SO.detail[i].quantity;
                        //ROdtl.quantity = SO.detail[i].quantity;
                        //ROdtl.baseEntry = SO.detail[i].docId;
                        //ROdtl.warehouseQuantity = SO.detail[i].warehouseQuantity;

                        RO.detail.Add(ROdtl);
                    }
                }
            }
            return true;
        }

        //设置退款单
        static public DialogResult setRefundOrder(ref ReturnOrderModel RO, ref RefundOrderModel RFO)
        {
            object result;
            RefundOrderInitModel RFOI = new RefundOrderInitModel();
            List<CollectionOrderHeaderModel> COHeaderList = null;
            List<CollectionOrderDtlModel> CODtlList = null;
            CollectionOrderModel CO = new CollectionOrderModel();

            if (!string.IsNullOrEmpty(RO.header.baseEntry))
            {
                queryConditionModel QC = new queryConditionModel();
                QC.where = "BASE_ENTRY ='" + RO.header.baseEntry + "'";;
                //查询收款单
                if (DevCommon.getDataByWebService("getCollectionOrderHeaderByCondition", "getCollectionOrderHeaderByCondition", QC, ref COHeaderList) == RetCode.OK && COHeaderList != null)
                {
                    CO.header = COHeaderList[0];
                    QC.where = "DOC_ID ='" + CO.header.docId + "'";
                    if (DevCommon.getDataByWebService("getCollectionOrderDtlByCondition", "getCollectionOrderDtlByCondition", QC, ref CODtlList) == RetCode.OK && CODtlList != null)
                    {
                        CO.detail = CODtlList;
                    }
                }
            }


            //参数
            RFOI.baseEntry = RO.header.docId;
            RFOI.refundAmount = RO.header.rebateAmount;
            foreach (ReturnOrderDtlModel item in RO.detail)
            {
                if (!string.IsNullOrEmpty(item.serialNo))
                {
                    RFOI.serialNoList.Add(item.serialNo);
                }
            }

            //窗口显示
            DllInvoke.Invoke("RefundOrder.dll", "RefundOrder.Run", "Show", new object[] { RFOI, CO }, out result);
            RFO = ((getRefundFormResultModel)result).RFO;

            return ((getRefundFormResultModel)result).dialogResult;
        }

        //设置出库单
        static public bool setReturnInWhsOrder(ref ReturnOrderModel RO, ref ReturnInWhsOrderModel IO)
        {
            object result;

            //接口调用
            DllInvoke.Invoke("ReturnInWhsOrder.dll", "ReturnInWhsOrder.Run", "setReturnInWhsOrderFromReturnOrder", new object[] { RO }, out result);
            IO = (ReturnInWhsOrderModel)result;

            return true;
        }

        //数据库写入订单
        static public bool saveReturnOrderAll(ref ReturnOrderModel RO, ref RefundOrderModel RFO, ref ReturnInWhsOrderModel IO)
        {
            try
            {
                ReturnOrderAllModel ROAll = new ReturnOrderAllModel();

                ROAll.returnOrder = RO;                     //退货单
                ROAll.refundOrder = RFO;                    //退款单
                ROAll.returnInWhsOrder = IO;                //退货入库单

                String outStr = null;

                //删除空白行
                //SO.detail.RemoveAt(SO.detail.Count - 1);

                if (DevCommon.getDataByWebService("createReturnOrderAll", "createReturnOrderAll", ROAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    return false;
                }


                //清空
                if (!RO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    RO = new ReturnOrderModel();
                    RFO = null;
                    IO = null;
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
