using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //订单全部（销售订单，收款单，出库单）
    public class SalesOrderAllModel
    {
        //销售订单
        public SalesOrderModel salesOrder = new SalesOrderModel();

        //收款单
        public CollectionOrderModel collectionOrder = new CollectionOrderModel();

        //出库单
        public SalesOutWhsOrderModel salesOutWhsOrder = new SalesOutWhsOrderModel();
    }   
    
    
    //销售订单
    public class SalesOrderModel
    {
        //销售订单表头
        public SalesOrderHeaderModel header = new SalesOrderHeaderModel();

        //销售订单商品销售价格明细
        public List<SalesOrderDtlModel> detail = new List<SalesOrderDtlModel>();

        //销售订单明细
        public List<SalesOrderPriceDtlModel> priceDtl = new List<SalesOrderPriceDtlModel>();

        //销售订单明细-商品和促销对应关系
        public List<SalesOrderDtlPrmtnModel> detailPrmtn = new List<SalesOrderDtlPrmtnModel>();

        //销售订单-促销信息
        public List<SalesOrderPrmtnModel> prmtn = new List<SalesOrderPrmtnModel>();
    }
    
    //销售订单表头
    public class SalesOrderHeaderModel : OrderHeaderModel
    {
        //会员ID
        public string memberId { get; set; }

        //订单原价
        public decimal primeAmount { get; set; }

        //订单折扣价（订单销售总价）
        public decimal rebateAmount { get; set; }

        //订金额
        public decimal preCollectionAmount { get; set; }

        //收款额
        public decimal collectionAmount { get; set; }

        //收款状态（未清/部分已清/已清）
        public string fundStatus { get; set; }

        //出库状态（未清/部分已清/已清）
        public string warehouseStatus { get; set; }

        //退货状态（未清/部分已清/已清）
        public string returnStatus { get; set; }

        //会员总积分
        public int memberPointAmount { get; set; }

        //销售人员ID
        public string salesId { get; set; }

        //收款门店ID
        public string collectionStoreId { get; set; }

        //出库门店ID
        public string deliveryStoreId { get; set; }

        //物流地址
        public string logisticsAddress { get; set; }

        //移动类型
        public string movementTypeId { get; set; }
        

        //创建人员Name
        public string createUserNameF { get; set; }
        public string createUserNameM { get; set; }
        public string createUserNameL { get; set; }
        [JsonIgnore]
        public string createUserName
        {
            get { return createUserNameL + createUserNameM + createUserNameF; }
        }

        //会员Name
        public string memberNameF { get; set; }
        public string memberNameM { get; set; }
        public string memberNameL { get; set; }
        [JsonIgnore]
        public string memberName
        {
            get { return memberNameL + memberNameM + memberNameF; }
        }

        //会员手机号码
        public string memberPhoneMobile { get; set; }

        //销售人员Name
        public string salesPersonNameF { get; set; }
        public string salesPersonNameM { get; set; }
        public string salesPersonNameL { get; set; }
        [JsonIgnore]
        public string salesPersonName
        {
            get { return salesPersonNameL + salesPersonNameM + salesPersonNameF; }
        }
    }

    //销售订单明细
    public class SalesOrderDtlModel : ItemOrderDtlModel
    {  
        //上级订单号
        public string baseEntry { get; set; }

        //出库数量
        public int warehouseQuantity { get; set; }

        //占用数量
        public int lockedQuantity { get; set; }

        //退货数量
        public int returnQuantity { get; set; }

        //是否主商品												
        public string isMainProduct { get; set; }

        //销售政策ID
        public string productSalesPolicyId { get; set; }

        //销售政策NO
        public int productSalesPolicyNo { get; set; }

        //销售政策Name 
        public string productSalesPolicyName { get; set; }

        //套包Id
        public string bomId { get; set; }

        //套包Name
        public string bomName { get; set; }

        //套包金额
        public decimal bomAmount { get; set; }

        //发票号码
        public string invoiceNo { get; set; }

        //仓库ID
        public string facilityId { get; set; }

        //是否占用
        [JsonIgnore]
        public string isOccupied { get; set; }
        
        //库存数量
        [JsonIgnore]        
        public int stockQuantity { get; set; }
        
        //销售政策
        [JsonIgnore]
        public ProductSalesPolicyListModel productSalesPolicys = new ProductSalesPolicyListModel();

        //价格明细
        [JsonIgnore]
        public ProductPriceTypeListModel productSalesPrices = new ProductPriceTypeListModel(); 
        
        //套包
        [JsonIgnore]
        public BOMListModel boms = new BOMListModel(); 
        
        //副商品
        [JsonIgnore]
        public SubProductListModel subProducts = new SubProductListModel();

        //主商品折扣分摊
        [JsonIgnore]
        public decimal mainRatio { get; set; }
    }

    //销售订单明细-商品和促销对应关系
    public class SalesOrderDtlPrmtnModel
    {
        //销售订单ID：主键
        public string docId { get; set; }

        //明细行号：主键
        public int lineNo { get; set; }

        //促销ID：主键
        public string promotionId { get; set; }

        //订单促销ID
        public string orderPrmtnId { get; set; }

        //自定义字段1
        public string attrName1 { get; set; }

        //自定义字段2
        public string attrName2 { get; set; }

        //自定义字段3
        public string attrName3 { get; set; }

        //自定义字段4
        public string attrName4 { get; set; }

        //自定义字段5
        public string attrName5 { get; set; }

        //自定义字段6
        public string attrName6 { get; set; }

        //自定义字段7
        public string attrName7 { get; set; }

        //自定义字段8
        public string attrName8 { get; set; }

        //自定义字段9
        public string attrName9 { get; set; }
    }

    //销售订单-促销信息
    public class SalesOrderPrmtnModel
    {
        //订单促销ID：自增流水号
        public string orderPrmtnId { get; set; }

        //促销ID
        public string promotionId { get; set; }

        //自定义字段1
        public string attrName1 { get; set; }

        //自定义字段2
        public string attrName2 { get; set; }

        //自定义字段3
        public string attrName3 { get; set; }

        //自定义字段4
        public string attrName4 { get; set; }

        //自定义字段5
        public string attrName5 { get; set; }

        //自定义字段6
        public string attrName6 { get; set; }

        //自定义字段7
        public string attrName7 { get; set; }

        //自定义字段8
        public string attrName8 { get; set; }

        //自定义字段9
        public string attrName9 { get; set; }
    }

    //销售订单-销售订单商品销售价格明细
    public class SalesOrderPriceDtlModel
    {
        //销售订单ID：主键
        public string docId { get; set; }

        //明细行号：主键
        public int lineNo { get; set; }

        //销售政策ID
        public string productPriceTypeId { get; set; }

        //销售规则ID
        public string productPriceRuleId { get; set; }

        //价格
        public string price { get; set; }

        //自定义字段1
        public string attrName1 { get; set; }

        //自定义字段2
        public string attrName2 { get; set; }

        //自定义字段3
        public string attrName3 { get; set; }

        //自定义字段4
        public string attrName4 { get; set; }

        //自定义字段5
        public string attrName5 { get; set; }

        //自定义字段6
        public string attrName6 { get; set; }

        //自定义字段7
        public string attrName7 { get; set; }

        //自定义字段8
        public string attrName8 { get; set; }

        //自定义字段9
        public string attrName9 { get; set; }
    }
}
