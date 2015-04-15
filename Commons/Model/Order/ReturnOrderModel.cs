using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //订单全部（退货单，退款单，入库单）
    public class ReturnOrderAllModel
    {
        //退货单
        public ReturnOrderModel returnOrder = new ReturnOrderModel();

        //退款单
        public RefundOrderModel refundOrder = new RefundOrderModel();

        //入库单
        public ReturnInWhsOrderModel returnInWhsOrder = new ReturnInWhsOrderModel();
    }

    //退货单
    public class ReturnOrderModel
    {
        //退货单表头
        public ReturnOrderHeaderModel header = new ReturnOrderHeaderModel();

        //退货单明细
        public List<ReturnOrderDtlModel> detail = new List<ReturnOrderDtlModel>();
    }

    //退货单表头
    public class ReturnOrderHeaderModel : OrderHeaderModel
    {
        //会员ID
        public string memberId { get; set; }

        //订单原价
        public decimal primeAmount { get; set; }

        //订单折扣价（订单销售总价）
        public decimal rebateAmount { get; set; }

        //退款金额
        public decimal refundAmount { get; set; }

        //退款状态（未清/部分已清/已清）
        public string refundStatus { get; set; }

        //入库状态（未清/部分已清/已清）
        public string warehouseStatus { get; set; }

        //换货状态（未清/部分已清/已清）
        public string exchangeStatus { get; set; }

        //会员总积分
        public int memberPointAmount { get; set; }

        //销售人员ID
        public string salesId { get; set; }

        //退款门店ID
        public string refundStoreId { get; set; }

        //入库门店ID
        public string receiveStoreId { get; set; }
       
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

    //退货单明细
    public class ReturnOrderDtlModel : ItemOrderDtlModel
    {
        //上级订单号
        public string baseEntry { get; set; }

        //入库数量
        public int warehouseQuantity { get; set; }

        //解锁数量
        public int unLockedQuantity { get; set; }

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

        //仓库ID
        public string facilityId { get; set; }

        //故障类型ID
        public string faultTypeId { get; set; }

        //故障类型描述
        public string faultDescription { get; set; }

        //故障类型名称
        public string faultType { get; set; }
    }
}
