using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Commons.Model;
using Commons.Model.Order;
using Newtonsoft.Json;
using System.Collections;
using Commons.XML;
using Commons.WebService;

namespace Commons.WinForm
{
    #region 单据状态
    public enum DocStatus   //（草稿/确定/批准/已清/作废）
    {
        DRAFT = 0,                  //草稿
        VALID = 1,                  //确定
        APPROVED = 2,               //批准
        CLEARED = 3,                //已清
        INVALID = 4                 //作废
    }
    #endregion

    #region 单据业务状态
    public enum BusinessStatus   //（未清/部分已清/已清）
    {
        NOTCLEARED = 0,             //未清
        PARTCLEARED = 1,            //部分已清
        CLEARED = 2,                //已清
    }
    #endregion

    #region 单据单号首字母
    public enum DocType         //（单据类型）
    {
        PRE = 'P',              //P-预订单
        PRECOLLECTION = 'L',    //L-预收单
        BACK = 'B',             //B-退订单
        PREREFUND = 'U',        //U-订金返还单        
        SALES = 'S',            //S-销售订单
        COLLECTION = 'C',       //C-收款单
        SALESOUTWHS = 'O',      //O-销售出库单
        RETURN = 'R',           //R-退货单
        REFUND = 'F',           //F-退款单
        RETURNINWHS = 'I',      //I-退货入库单
        PAYMENTIN = 'N',        //N-缴款单

        Delivery = 'D',                             //调拨发货单
        Receive = 'J',                              //调拨收货单
        Inventory = 'H',                            //盘点
        WarehouseOut = 'G',                         //发货
        WarehouseIn = 'E',                          //收货
        WarehouseTransfer = 'Y',                    //移库单
        WarehouseReplenishment = 'A',                //补货单 
        transfer = 'M',                                //中转库
        member='V'                                    //会员
    }
    #endregion

    #region 返回单据状态
    public enum RetCode         //返回值
    {
        OK = 'S',                 //成功
        NG = 'F',                //失败
        UNKOWN = 'U',             //未知
    }
    #endregion

    #region 移动类型
    public static class MoveType
    {
        //调拨发货单
        public const string goodsDelivery = "GoodsDelivery";
        //调拨收货单
        public const string goodsReceipt = "GoodsReceipt";
        //出库
        public const string warehouseOut = "WarehouseOut";
        //入库
        public const string warehouseIn = "WarehouseIn";
        //移库单
        public const string warehouseReplenishment = "WarehouseReplenishment";
        //销售出库单
        public const string saleOut = "SaleOut";
        //退货入库
        public const string returnIn = "ReturnIn";
        /// <summary>
        /// 库存移库
        /// </summary>
        public const string warehouseTransfer = "WarehouseTransfer";
    }
    #endregion

    #region 移动类型key
    public static class MoveTypeKey
    {
        /// <summary>
        /// 销售正品出库移动类型
        /// </summary>
        public const string salesOrder = "SZ";
        public const string returnOrderRCW = "TC";
        public const string returnOrderNormal = "TG";
    }
    #endregion

    #region 商品是否串号管理
    public static class MainProductType
    {
        /// <summary>
        /// 商品是否串号管理
        /// </summary>
        public const string yes = "Y";
        public const string no = "N";
    }
    #endregion

    #region 商品是否串号管理
    public class ProductSequenceManager
    {
        public const string sequenceY = "Y";
        public const string sequenceN = "N";
    }
    #endregion

    #region 商品是否被占用
    public class ProductOccupiedManager
    {
        public const string occupiedY = "Y";
        public const string occupiedN = "N";
    }
    #endregion

    #region 错误数据
    public class ErrorMessage
    {
        public const string errorMessage = "error";
    }
    #endregion

    #region 默认指导价
    public static class ConstantValue
    {
        /// <summary>
        /// 销售正品出库移动类型
        /// </summary>
        public const decimal defaultPrice = 99999999;
        public const string guidePriceId = "GUIDE_PRICE";
    }
    #endregion

    #region 修改列表串号条件
    public class UpdateCondition
    {
        public const string update = "Y";
        public const string enabled = "N";
    }
    #endregion

    #region 串号是否被占用
    public class SequenceOccuepied
    {
        public const string occupied = "Y";
    }
    #endregion

    #region 库存是否存在
    public class IsInventory
    {
        public const string isInventory = "Y";
        public const string notInventory = "N";
    }
    #endregion

    #region 仓库类型
    public class FacilityType
    {
        public const string WarehouseTypeZP = "R-WAREHOUSE";
    }
    #endregion

    #region 收款类型（中途缴款/日结账）
    public static class CollectionStyle
    {
        /// <summary>
        ///  收款类型（订金/收款）
        /// </summary>
        public const string PreCollection = "0";
        public const string Collection = "1";
    }
    #endregion

    #region 日结账类型（中途缴款/日结账）
    public static class PaymentInType
    {
        /// <summary>
        ///  收款类型（0：中途缴款，1：日结账）
        /// </summary>
        public const string PrePaymentIn = "0";
        public const string PaymentIn = "1";
    }
    #endregion

#region 手机号码是否存在
    public static class MobilePhoneManager
    {
        public const string isExist = "Y";
        public const string notExist = "N";
    }
#endregion
}