using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.Model;
using System.Windows.Forms;
using Commons.DLL;
using Commons.WinForm;

namespace BackOrder
{
    class BackOrderBLL
    {
        //设置退货单（根据销售订单得到退货单）
        static public bool getBackOrderFromPreOrder(PreOrderModel PO, ref BackOrderModel BO)
        {
            BO.header.baseEntry = PO.header.docId;

            //清空明细
            BO.detail.Clear();
            
            //明细
            for (int i = 0; i < PO.detail.Count; i++)
            {
                if (!string.IsNullOrEmpty(PO.detail[i].docId))
                {
                    //还没有退货完毕的才有效
                    if (PO.detail[i].quantity - PO.detail[i].backQuantity > 0)
                    {
                        BackOrderDtlModel BOdtl = new BackOrderDtlModel();
                        BOdtl.productId = PO.detail[i].productId;
                        BOdtl.productName = PO.detail[i].productName;
                        BOdtl.isSequence = PO.detail[i].isSequence;
                        BOdtl.serialNo = PO.detail[i].serialNo;
                        BOdtl.barCodes = PO.detail[i].barCodes;
                        BOdtl.baseEntry = PO.detail[i].docId;
                        BOdtl.lineNoBaseEntry = PO.detail[i].lineNo;
                        BOdtl.quantity = PO.detail[i].quantity - PO.detail[i].backQuantity;
                        BOdtl.unitPrice = PO.detail[i].unitPrice;
                        BOdtl.rebatePrice = PO.detail[i].rebatePrice;
                        BOdtl.rebateAmount = BOdtl.quantity * PO.detail[i].rebatePrice;
                        BOdtl.bomId = PO.detail[i].bomId;
                        BOdtl.productSalesPolicyId = PO.detail[i].productSalesPolicyId;
                        BOdtl.productSalesPolicyNo = PO.detail[i].productSalesPolicyNo;
                        BOdtl.bomAmount = PO.detail[i].bomAmount * BOdtl.quantity / PO.detail[i].quantity;
                        BOdtl.isMainProduct = PO.detail[i].isMainProduct;
                        //BOdtl.facilityId = PO.detail[i].facilityId;

                        //BOdtl.facilityId = PO.detail[i].facilityId;
                        //BOdtl.saleQuantity = PO.detail[i].quantity;
                        //BOdtl.quantity = PO.detail[i].quantity;
                        //BOdtl.baseEntry = PO.detail[i].docId;
                        //BOdtl.warehouseQuantity = PO.detail[i].warehouseQuantity;

                        BO.detail.Add(BOdtl);
                    }
                }
            }
            return true;
        }

        //设置退款单
        static public DialogResult setPreRefundOrder(ref BackOrderModel BO, ref PreRefundOrderModel PRFO)
        {
            object result;
            RefundOrderInitModel RFOI = new RefundOrderInitModel();
            List<PreCollectionOrderHeaderModel> PCOHeaderList = null;
            List<PreCollectionOrderDtlModel> PCODtlList = null;
            PreCollectionOrderModel PCO = new PreCollectionOrderModel();

            if (!string.IsNullOrEmpty(BO.header.baseEntry))
            {
                queryConditionModel QC = new queryConditionModel();
                QC.where = "BASE_ENTRY ='" + BO.header.baseEntry + "'";
                //查询收款单
                if (DevCommon.getDataByWebService("getPreCollectionOrderHeaderByCondition", "getPreCollectionOrderHeaderByCondition", QC, ref PCOHeaderList) == RetCode.OK && PCOHeaderList != null)
                {
                    PCO.header = PCOHeaderList[0];
                    QC.where = "DOC_ID ='" + PCO.header.docId + "'";
                    if (DevCommon.getDataByWebService("getPreCollectionOrderDtlByCondition", "getPreCollectionOrderDtlByCondition", QC, ref PCODtlList) == RetCode.OK && PCODtlList != null)
                    {
                        PCO.detail = PCODtlList;
                    }
                }
            }

            //参数
            RFOI.baseEntry = BO.header.docId;
            RFOI.refundAmount = BO.header.rebateAmount;
            foreach (BackOrderDtlModel item in BO.detail)
            {
                if (!string.IsNullOrEmpty(item.serialNo))
                {
                    RFOI.serialNoList.Add(item.serialNo);
                }
            }

            //窗口显示
            DllInvoke.Invoke("PreRefundOrder.dll", "PreRefundOrder.Run", "Show", new object[] { RFOI, PCO }, out result);
            PRFO = ((getPreRefundFormResultModel)result).PRFO;

            return ((getPreRefundFormResultModel)result).dialogResult;
        }

        //数据库写入订单
        static public bool saveBackOrderAll(ref BackOrderModel BO, ref PreRefundOrderModel PRFO)
        {
            try
            {
                BackOrderAllModel BOAll = new BackOrderAllModel();

                BOAll.backOrder = BO;                     //退货单
                BOAll.preRefundOrder = PRFO;              //退款单

                String outStr = null;

                //删除空白行
                //PO.detail.RemoveAt(PO.detail.Count - 1);

                if (DevCommon.getDataByWebService("createBackOrderAll", "createBackOrderAll", BOAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    return false;
                }


                //清空
                if (!BO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    BO = new BackOrderModel();
                    PRFO = null;
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
