using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //订金返还单
    public class PreRefundOrderModel
    {
        //订金返还单表头
        public PreRefundOrderHeaderModel header = new PreRefundOrderHeaderModel();

        //订金返还单明细
        public List<PreRefundOrderDtlModel> detail = new List<PreRefundOrderDtlModel>();
    }

    //订金返还单表头
    public class PreRefundOrderHeaderModel : OrderHeaderModel
    {
        //退款金额
        public decimal amount { get; set; }

        //未退款项
        [JsonIgnore]
        public decimal unRefundAmount { get; set; }
    }

    //订金返还单明细
    public class PreRefundOrderDtlModel : FundOrderDtlModel
    {
        //支付类型名称
        [JsonIgnore]
        public string typeName { get; set; }

        //收款额
        [JsonIgnore]
        public decimal preCollectionAmount { get; set; }
    }
}
