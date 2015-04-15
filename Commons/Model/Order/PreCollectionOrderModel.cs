using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    public class PreCollectionOrderModel
    {
        //收款单表头
        public PreCollectionOrderHeaderModel header = new PreCollectionOrderHeaderModel();

        //收款单明细
        public List<PreCollectionOrderDtlModel> detail = new List<PreCollectionOrderDtlModel>();
    }

    //订金预收单表头
    public class PreCollectionOrderHeaderModel : OrderHeaderModel
    {
        //订金总金额
        public decimal amount { get; set; }

        //未收款项
        [JsonIgnore]
        public decimal uncollectionAmount { get; set; }
    }

    //收款单明细
    public class PreCollectionOrderDtlModel : FundOrderDtlModel
    {
        //退款金额
        public decimal refundAmount { get; set; }

        //支付类型名称
        [JsonIgnore]
        public string typeName { get; set; }
    }

    public class PreCollectionOrderInitModel
    {
        //上级订单号
        public string baseEntry { get; set; }

        //收款额
        public decimal collectionAmount { get; set; }

        //商品串号
        public List<string> serialNoList = new List<string>();

    }
}
