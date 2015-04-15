using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //退款单
    public class RefundOrderModel
    {
        //退款单表头
        public RefundOrderHeaderModel header = new RefundOrderHeaderModel();

        //退款单明细
        public List<RefundOrderDtlModel> detail = new List<RefundOrderDtlModel>();
    }

    //收款单表头
    public class RefundOrderHeaderModel : OrderHeaderModel
    {
        //退款金额
        public decimal amount { get; set; }

        //未退款项
        [JsonIgnore]
        public decimal unRefundAmount { get; set; }
    }

    //收款单明细
    public class RefundOrderDtlModel : FundOrderDtlModel
    {
        //支付类型名称
        [JsonIgnore]
        public string typeName { get; set; }

        //收款额
        [JsonIgnore]
        public decimal collectionAmount { get; set; }
    }

    public class RefundOrderInitModel
    {
        //上级订单号
        public string baseEntry { get; set; }

        //退款额
        public decimal refundAmount { get; set; }

        //商品串号
        public List<string> serialNoList = new List<string>();

    }
}
