using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;
using Commons.DLL;
using Commons.WinForm;
using Commons.Model;
using Commons.Model.Stock;
using System.Windows.Forms;
using System.Data;

namespace SalesOrder
{
    class SalesOrderBLL
    {
        //设置收款单
        static public DialogResult setCollectionOrder(ref SalesOrderModel SO, ref CollectionOrderModel CO)
        {
            object result;
            CollectionOrderInitModel COI = new CollectionOrderInitModel();
            

            //参数
            COI.baseEntry = SO.header.docId;
            COI.collectionAmount = SO.header.rebateAmount;
            foreach (SalesOrderDtlModel item in SO.detail)
            {
                if (!string.IsNullOrEmpty(item.serialNo))
                {
                    COI.serialNoList.Add(item.serialNo);
                }
            }
            
            //窗口显示
            DllInvoke.Invoke("CollectionOrder.dll", "CollectionOrder.Run", "Show", new object[] { COI }, out result);
            CO = ((getCollectionFormResultModel)result).CO;

            return ((getCollectionFormResultModel)result).dialogResult;
        }

        //设置出库单
        static public bool setSalesOutWhsOrder(bool isUnlocked, ref SalesOrderModel SO, ref SalesOutWhsOrderModel WO)
        {
            object result;

            //接口调用
            DllInvoke.Invoke("SalesOutWhsOrder.dll", "SalesOutWhsOrder.Run", "setSalesOutWhsOrderFromSalesOrder", new object[] { SO, isUnlocked }, out result);
            WO = (SalesOutWhsOrderModel)result;

            return true;
        }

        //数据库写入订单
        static public bool saveSalesOrderAll(ref SalesOrderModel SO, ref CollectionOrderModel CO, ref SalesOutWhsOrderModel WO)
        {
            try
            {
                SalesOrderAllModel SOAll = new SalesOrderAllModel();

                SOAll.salesOrder = SO;                  //销售订单
                SOAll.collectionOrder = CO;             //收款单
                SOAll.salesOutWhsOrder = WO;            //销售出库单

                String outStr = null;

                //删除空白行
                SO.detail.RemoveAt(SO.detail.Count - 1);

                if (DevCommon.getDataByWebService("createSalesOrderAll", "createSalesOrderAll", SOAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                    SO.detail.Add(SODtl);
                    return false;
                }


                //清空
                if (SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                    SO.detail.Add(SODtl);
                }
                else
                {
                    SO = new SalesOrderModel();
                    CO = null;
                    WO = null;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                SO.detail.Add(SODtl);
                throw ex;
            }
        }


        //数据库写入订单
        static public bool updateSalesOrderAll(bool bUpdateSalesOrder, ref SalesOrderModel SO, ref CollectionOrderModel CO, ref SalesOutWhsOrderModel WO)
        {
            try
            {
                SalesOrderAllModel SOAll = new SalesOrderAllModel();

                if (bUpdateSalesOrder)
                {
                    SOAll.salesOrder = SO;                  //销售订单
                }
                else
                {
                    SOAll.salesOrder = null;                //已经生成非草稿销售订单情况下不更新销售订单
                }

                SOAll.collectionOrder = CO;             //收款单
                SOAll.salesOutWhsOrder = WO;            //销售出库单

                //删除空白行
                SO.detail.RemoveAt(SO.detail.Count - 1);

                String outStr = null;
                if (DevCommon.getDataByWebService("updateSalesOrderAll", "updateSalesOrderAll", SOAll, ref outStr) == RetCode.NG)
                {
                    //插入空白行可以后续操作
                    SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                    SO.detail.Add(SODtl);
                    return false;
                }


                //清空
                if (SO.header.docStatus.Equals(((int)DocStatus.DRAFT).ToString()))
                {
                    SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                    SO.detail.Add(SODtl);
                }
                else
                {
                    SO = new SalesOrderModel();
                    CO = null;
                    WO = null;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                //插入空白行可以后续操作
                SalesOrderDtlModel SODtl = new SalesOrderDtlModel();
                SO.detail.Add(SODtl);
                throw ex;
            }

        }

        //明细中添加副商品
        static public bool addSubProducts(int nIndex, SalesOrderDtlModel SODtl, ref List<SalesOrderDtlModel> SODetail)
        {
            if (SODtl.subProducts.items == null)
            {
                return true;
            }

            foreach (getProductModel item in SODtl.subProducts.items)
            {
                SalesOrderDtlModel SODtlTmp = new SalesOrderDtlModel();
                SODtlTmp.isMainProduct = MainProductType.no;
                SODtlTmp.serialNo = item.sequenceId;
                SODtlTmp.productName = item.productName;
                SODtlTmp.productId = item.productId;
                SODtlTmp.barCodes = item.idValue;
                SODtlTmp.isSequence = item.isSequence;
                SODtlTmp.quantity = item.quantity;
                if (!string.IsNullOrEmpty(item.isSequence) && item.isSequence.Equals(ProductSequenceManager.sequenceY))
                {
                    if (SODtl.isOccupied.Equals(ProductOccupiedManager.occupiedY))
                    {
                        SODtlTmp.stockQuantity = 0;
                    }
                    else
                    {
                        SODtlTmp.stockQuantity = 1;
                    }
                }
                else
                {
                    SODtlTmp.stockQuantity = item.onhand - item.promise;
                }

                SODtlTmp.unitPrice = item.unitPrice;
                SODtlTmp.rebatePrice = item.rebatePrice;
                SODtlTmp.rebateAmount = item.rebateAmount;
                SODtlTmp.productSalesPolicyId = SODtl.productSalesPolicyId;
                SODtlTmp.productSalesPolicyName = SODtl.productSalesPolicyName;
                SODtlTmp.productSalesPolicyNo = SODtl.productSalesPolicyNo;
                SODtlTmp.bomId = SODtl.bomId;
                SODtlTmp.bomName = SODtl.bomName;
                SODtlTmp.productSalesPolicys = SODtl.productSalesPolicys;
                SODtlTmp.productSalesPrices.items = item.priceList;
                SODtlTmp.boms = SODtl.boms;
                SODtlTmp.bomAmount = SODtl.bomAmount;
                SODetail.Insert(nIndex++, SODtlTmp);
            }

            return true;
        }

        //明细中添加副商品
        static public decimal getProductUnitPrice(string prdocutSalesPolicyId, List<ProductSalesPolicyModel> ListProductSalesPolicy, ref List<ProductPriceTypeModel> ListProducPrice)
        {
            decimal unitPrice = ConstantValue.defaultPrice;

            //查找销售政策相同的
            ProductSalesPolicyModel productSalesPolicy = ListProductSalesPolicy.Find(delegate(ProductSalesPolicyModel item) { return item.productSalesPolicyId.Equals(prdocutSalesPolicyId);});
            if (productSalesPolicy != null)
            {
                //查找指导价格
                ListProducPrice = productSalesPolicy.productPriceTypes;
                ProductPriceTypeModel productPrice = productSalesPolicy.productPriceTypes.Find(delegate(ProductPriceTypeModel item) { return item.productPriceTypeId.Equals(ConstantValue.guidePriceId);});
                if (productPrice != null)
                {
                    unitPrice = Convert.ToDecimal(productPrice.price);
                }
            }

            return unitPrice;
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
