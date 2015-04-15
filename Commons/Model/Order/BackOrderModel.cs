using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //订单全部（退订单，订金返还单）
    public class BackOrderAllModel
    {
        //退订单
        public BackOrderModel backOrder = new BackOrderModel();

        //订金返还单
        public PreRefundOrderModel preRefundOrder = new PreRefundOrderModel();
    }

    //退订单
    public class BackOrderModel
    {
        //退订单表头
        public BackOrderHeaderModel header = new BackOrderHeaderModel();

        //退订单明细
        public List<BackOrderDtlModel> detail = new List<BackOrderDtlModel>();
    }

    //退订单表头
    public class BackOrderHeaderModel : OrderHeaderModel
    {
        //会员ID
        public string memberId { get; set; }

        //订单原价
        public decimal primeAmount { get; set; }

        //订单折扣价（订单退货总价）
        public decimal rebateAmount { get; set; }

        //退款金额
        public decimal preRefundAmount { get; set; }

        //退款状态（未清/部分已清/已清）
        public string refundStatus { get; set; }

        //会员总积分
        public int memberPointAmount { get; set; }

        //销售人员ID
        public string salesId { get; set; }

        //退款门店ID
        public string preRefundStoreId { get; set; }

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

    //退订单明细
    public class BackOrderDtlModel : ItemOrderDtlModel
    {
        //上级订单号
        public string baseEntry { get; set; }

        //退货数量
        public int returnQuantity { get; set; }

        //是否主商品												
        public string isMainProduct { get; set; }

        //销售政策ID
        public string productSalesPolicyId { get; set; }

        //销售政策NO
        public int productSalesPolicyNo { get; set; }

        //销售政策Name 
        //public string productSalesPolicyName { get; set; }

        //套包Id
        public string bomId { get; set; }

        //套包Name
        //public string bomName { get; set; }

        //套包金额
        public decimal bomAmount { get; set; }
    }
}
