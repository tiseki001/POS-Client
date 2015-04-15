using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //订单全部（预订单，订金预收单）
    public class PreOrderAllModel
    {
        //销售订单
        public PreOrderModel preOrder = new PreOrderModel();

        //订金预收单
        public PreCollectionOrderModel preCollectionOrder = new PreCollectionOrderModel();
    } 


    //预订单
    public class PreOrderModel
    {
        //预订单表头
        public PreOrderHeaderModel header = new PreOrderHeaderModel();

        //预订单明细
        public List<PreOrderDtlModel> detail = new List<PreOrderDtlModel>();
    }

    //预订单表头
    public class PreOrderHeaderModel : OrderHeaderModel
    {
        //会员ID
        public string memberId { get; set; }

        //订单原价
        public decimal primeAmount { get; set; }

        //订单折扣价（订单销售总价）
        public decimal rebateAmount { get; set; }

        //预订收款额
        public decimal preCollectionAmount { get; set; }

        //收款状态（未清/部分已清/已清）
        public string fundStatus { get; set; }

        //退货状态（未清/部分已清/已清）
        public string backStatus { get; set; }

        //会员总积分
        public int memberPointAmount { get; set; }

        //销售人员ID
        public string salesId { get; set; }

        //收款门店ID
        public string collectionStoreId { get; set; }

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

    //预订单明细
    public class PreOrderDtlModel : ItemOrderDtlModel
    {
        //退订数量
        public int backQuantity { get; set; }

        //是否主商品												
        public string isMainProduct { get; set; }

        //销售政策ID
        public string productSalesPolicyId { get; set; }

        //销售政策NO
        public int productSalesPolicyNo { get; set; }

        //套包Id
        public string bomId { get; set; }

        //套包Name
        //public string bomName { get; set; }

        //套包金额
        public decimal bomAmount { get; set; }
    }


}
