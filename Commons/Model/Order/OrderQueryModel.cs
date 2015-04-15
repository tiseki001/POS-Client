using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Commons.Model.Order
{
    //查询条件
    public class queryConditionModel
    {
        public string where { get; set; }
    }

    //订单号码
    public class queryDocIdModel
    {
        //订单ID：主键
        public string docId { get; set; }
    }

    //店铺ID
    public class queryStoreIdModel
    {
        public string storeId { get; set; }
    }

    //付款方式
    public class getPaymentMethodModel
    {
        public string paymentMethodTypeId { get; set; }
        public string description { get; set; }
    }

    //销售人员
    public class querySalesPersonModel
    {
        public string partyId { get; set; }
    }
    public class getSalesPersonModel
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        [JsonIgnore]
        public string name
        {
            get { return lastName + middleName + firstName; }
        }
    }

    //会员
    public class queryMemberModel
    {
        public string phoneMobile { get; set; }
    }
    public class getMemberModel
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string phoneMobile { get; set; }
        public string partyId { get; set; }
        [JsonIgnore]
        public string name
        {
            get { return lastName + middleName + firstName; }
        }
    }

    //商品
    public class queryProductModel
    {
        public string productStoreId { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
        public string idValue { get; set; }
        public string sequenceId { get; set; }
        public string movementTypeId { get; set; }
    }
    public class getProductModel
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productStoreId { get; set; }//门店Id
        public string isSequence { get; set; }
        public string isOccupied { get; set; }
        public string idValue { get; set; }
        public decimal suggestedPrice { get; set; }
        public int onhand { get; set; }//库存数量
        public int promise { get; set; }//承诺数量
        public string sequenceId { get; set; }
        public string facilityId { get; set; }
        public List<ProductSalesPolicyModel> productSalesPolicys { get; set; }   

        [JsonIgnore]
        public decimal unitPrice { get; set; }        //单价
        [JsonIgnore]
        public decimal rebatePrice { get; set; }      //折扣价
        [JsonIgnore]
        public int quantity { get; set; }             //数量
        [JsonIgnore]
        public decimal rebateAmount { get; set; }     //金额
        [JsonIgnore]
        public decimal bomAmount { get; set; }        //套包金额
        [JsonIgnore]
        public List<ProductPriceTypeModel> priceList; //价格清单
    }
    public class ProductSalesPolicyModel
    {
        public string productSalesPolicyId { get; set; }
        public string policyName { get; set; }
        public List<ProductPriceTypeModel> productPriceTypes { get; set; }
        public int mainRatio { get; set; }
    }
    public class ProductPriceTypeModel
    {
        public String productPriceTypeId { get; set; }
        public string productPriceRuleId { get; set; }
        public String description { get; set; }
        public String price{ get; set; }
    }
    public class ProductSalesPolicyListModel
    {
        public List<ProductSalesPolicyModel> items;
    }
    public class ProductPriceTypeListModel
    {
        public List<ProductPriceTypeModel> items;
    }

    //套包
    public class queryBOMModel
    {
        public string productStoreId { get; set; }
        public string productSalesPolicyId { get; set; }
    }
    public class getBOMModel
    {
        public string bomId { get; set; }
        public string bomName { get; set; }
    }
    public class BOMListModel
    {
        public List<getBOMModel> items;
    }
    //副商品List
    public class querySubProductsModel
    {
        public string bomId { get; set; }
        public string productStoreId { get; set; }
        public string movementTypeId { get; set; }
    }
    public class SubProductListModel
    {
        public List<getProductModel> items;
    }

    //更新状态
    public class updateStatusModel
    {
        public string docId { get; set; }
        public string docStatus { get; set; }
        public string fundStatus { get; set; }
        public string warehouseStatus { get; set; }
        public string returnStatus { get; set; }
        public string lastUpdateDocDate { get; set; }
        public string lastUpdateUserId { get; set; }
    }

    //发货单查询
    public class querySalesOutWhsOrderModel
    {
        public string orderType { get; set; }        
        public string docId { get; set; }
        public string baseEntry { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string docStatus { get; set; }
    }

    //收款单画面返回结果
    public class getCollectionFormResultModel
    {
        public DialogResult dialogResult { get; set; }
        public CollectionOrderModel CO { get; set; }
    }

    //退款单画面返回结果
    public class getRefundFormResultModel
    {
        public DialogResult dialogResult { get; set; }
        public RefundOrderModel RFO { get; set; }
    }

    //订金预收单画面返回结果
    public class getPreCollectionFormResultModel
    {
        public DialogResult dialogResult { get; set; }
        public PreCollectionOrderModel PCO { get; set; }
    }

    //订金返还单画面返回结果
    public class getPreRefundFormResultModel
    {
        public DialogResult dialogResult { get; set; }
        public PreRefundOrderModel PRFO { get; set; }
    }

    //缴款金额条件
    public class queryPaymentAmountModel
    {
        public string storeId { get; set; }
        public string postingDate { get; set; }
    }

    //缴款金额
    public class getPaymentAmountModel
    {
        public string type { get; set; }
        public string typeName { get; set; }
        public decimal targetAmount { get; set; }
        public decimal preAmount { get; set; }
    }
}
