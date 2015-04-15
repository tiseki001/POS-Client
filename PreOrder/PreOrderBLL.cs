using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Commons.Model.Order;
using Commons.DLL;
using Commons.WinForm;
using System.Data;

namespace PreOrder
{
    class PreOrderBLL
    {
        //设置收款单
        static public DialogResult setPreCollectionOrder(ref PreOrderModel PO, ref PreCollectionOrderModel PCO)
        {
            object result;
            CollectionOrderInitModel COI = new CollectionOrderInitModel();


            //参数
            COI.baseEntry = PO.header.docId;
            COI.collectionAmount = PO.header.rebateAmount;
            foreach (PreOrderDtlModel item in PO.detail)
            {
                if (!string.IsNullOrEmpty(item.serialNo))
                {
                    COI.serialNoList.Add(item.serialNo);
                }
            }

            //窗口显示
            DllInvoke.Invoke("PreCollectionOrder.dll", "PreCollectionOrder.Run", "Show", new object[] { COI }, out result);
            PCO = ((getPreCollectionFormResultModel)result).PCO;

            return ((getPreCollectionFormResultModel)result).dialogResult;
        }

        //数据库写入订单
        static public bool savePreOrderAll(ref PreOrderModel PO, ref PreCollectionOrderModel PCO)
        {
            try
            {
                PreOrderAllModel POAll = new PreOrderAllModel();

                POAll.preOrder = PO;                  //预订单
                POAll.preCollectionOrder = PCO;       //订金预收单

                String outStr = null;

                //删除空白行
                PO.detail.RemoveAt(PO.detail.Count - 1);

                if (DevCommon.getDataByWebService("createPreOrderAll", "createPreOrderAll", POAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    PreOrderDtlModel PODtl = new PreOrderDtlModel();
                    PO.detail.Add(PODtl);
                    return false;
                }


                //清空
                if (PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    PreOrderDtlModel PODtl = new PreOrderDtlModel();
                    PO.detail.Add(PODtl);
                }
                else
                {
                    PO = new PreOrderModel();
                    PCO = null;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                PreOrderDtlModel PODtl = new PreOrderDtlModel();
                PO.detail.Add(PODtl);
                throw ex;
            }
        }


        //数据库写入订单
        static public bool updatePreOrderAll(bool bUpdatePreOrder, ref PreOrderModel PO, ref PreCollectionOrderModel PCO)
        {
            try
            {
                PreOrderAllModel POAll = new PreOrderAllModel();

                if (bUpdatePreOrder)
                {
                    POAll.preOrder = PO;                  //预订单
                }
                else
                {
                    POAll.preOrder = null;                //已经生成非草稿销售订单情况下不更新预订单
                }

                POAll.preCollectionOrder = PCO;       //订金预收单

                //删除空白行
                PO.detail.RemoveAt(PO.detail.Count - 1);

                String outStr = null;
                if (DevCommon.getDataByWebService("updatePreOrderAll", "updatePreOrderAll", POAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    PreOrderDtlModel PODtl = new PreOrderDtlModel();
                    PO.detail.Add(PODtl);
                    return false;
                }

                //清空
                if (PO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    PreOrderDtlModel PODtl = new PreOrderDtlModel();
                    PO.detail.Add(PODtl);
                }
                else
                {
                    PO = new PreOrderModel();
                    PCO = null;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                PreOrderDtlModel PODtl = new PreOrderDtlModel();
                PO.detail.Add(PODtl);
                throw ex;
            }
        }

        //将产品ID作为初始条件查询
        public static DataTable getSerialNoWhere(string productId)
        {
            DataTable dtWhere = new DataTable();
            dtWhere.Columns.Add("FieldColunm", typeof(string));
            dtWhere.Columns.Add("ConditionDesc", typeof(string));
            dtWhere.Columns.Add("ValueDesc", typeof(string));
            dtWhere.Columns.Add("ValueId", typeof(string));
            dtWhere.Columns.Add("Operator", typeof(string));
            dtWhere.Columns.Add("LikeType", typeof(string));

            DataRow dr = dtWhere.NewRow();
            dr["LikeType"] = "1";
            dr["Operator"] = "like";
            dr["FieldColunm"] = "PR.PRODUCT_ID";
            dr["ConditionDesc"] = "产品ID";
            dr["ValueDesc"] = productId;
            dr["ValueId"] = productId;
            dtWhere.Rows.Add(dr);

            return dtWhere;
        }
    }
}
