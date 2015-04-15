using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.Model;
using Commons.WinForm;

namespace Payment
{
    class PaymentBLL
    {
        //得到缴款金额
        static public bool getPaymentAmount(string postingDate, ref List<getPaymentAmountModel> PAList)
        {
            queryPaymentAmountModel QPA = new queryPaymentAmountModel();
            QPA.storeId = LoginInfo.ProductStoreId;
            QPA.postingDate = postingDate;
            if (DevCommon.getDataByWebService("getPaymentAmount", "getPaymentAmount", QPA, ref PAList) == RetCode.NG)
            {
                return false;
            }

            return true;
        }

        //设置缴款单（根据缴款金额得到缴款单）
        static public bool getPaymentInOrderFromPaymentAmount(List<getPaymentAmountModel> PAList, ref PaymentInOrderModel PIO)
        {
            //清空明细
            PIO.detail.Clear();

            //明细(销售订单最后一条是空白行)
            for (int i = 0; i < PAList.Count; i++)
            {
                PaymentInOrderDtlModel PIOdtl = new PaymentInOrderDtlModel();
                PIOdtl.docId = PIO.header.docId;
                PIOdtl.type = PAList[i].type;
                PIOdtl.typeName = PAList[i].typeName;
                PIOdtl.targetAmount = PAList[i].targetAmount;
                PIOdtl.preAmount = PAList[i].preAmount;
                PIOdtl.diffAmount = PIOdtl.targetAmount - PIOdtl.preAmount;

                PIO.detail.Add(PIOdtl);
            }
            return true;
        }

        //数据库写入订单
        static public bool savePaymentInOrder(ref PaymentInOrderModel PIO)
        {
            try
            {
                String outStr = null;

                if (DevCommon.getDataByWebService("createPaymentInOrder", "createPaymentInOrder", PIO, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    return false;
                }


                //清空
                if (PIO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {

                }
                else
                {
                    PIO = new PaymentInOrderModel();
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
