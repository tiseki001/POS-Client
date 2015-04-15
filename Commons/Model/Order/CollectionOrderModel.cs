using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //收款单
    public class CollectionOrderModel
    {
        //收款单表头
        public CollectionOrderHeaderModel header = new CollectionOrderHeaderModel();

        //收款单明细
        public List<CollectionOrderDtlModel> detail = new List<CollectionOrderDtlModel>();
    }

    //收款单表头
    public class CollectionOrderHeaderModel : OrderHeaderModel
    {
        //收款总金额（订金额+收款额）
        public decimal amount { get; set; }

        //订金额
        public decimal preCollectionAmount { get; set; }

        //收款额
        public decimal collectionAmount { get; set; }

        //未收款项
        [JsonIgnore]
        public decimal uncollectionAmount { get; set; }
    }

    //收款单明细
    public class CollectionOrderDtlModel : FundOrderDtlModel
    {
        //退款金额
        public decimal refundAmount { get; set; }

        //支付类型名称
        [JsonIgnore]
        public string typeName { get; set; }
    }

    public class CollectionOrderInitModel
    {
        //上级订单号
        public string baseEntry { get; set; }

        //收款额
        public decimal collectionAmount { get; set; }

        //商品串号
        public List<string> serialNoList = new List<string>();

    }

}
