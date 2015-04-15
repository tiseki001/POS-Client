using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //收款单
    public class PaymentInOrderModel
    {
        //收款单表头
        public PaymentInOrderHeaderModel header = new PaymentInOrderHeaderModel();

        //收款单明细
        public List<PaymentInOrderDtlModel> detail = new List<PaymentInOrderDtlModel>();
    }

    //收款单表头
    public class PaymentInOrderHeaderModel : OrderHeaderModel
    {
        //缴款总金额
        public decimal amount { get; set; }

        //单据类型（0：中途缴款，1：日结账）
        public string docType { get; set; }

        //创建人员Name
        public string createUserNameF { get; set; }
        public string createUserNameM { get; set; }
        public string createUserNameL { get; set; }
        [JsonIgnore]
        public string createUserName
        {
            get { return createUserNameL + createUserNameM + createUserNameF; }
        }
    }

    //款项类订单明细
    public class PaymentInOrderDtlModel
    {
        //订单ID：主键
        public string docId { get; set; }

        //明细行号：主键
        public int lineNo { get; set; }

        //上级订单明细行号
        public int lineNoBaseEntry { get; set; }

        //类型（订金/收款）
        public string style { get; set; }

        //支付类型
        public string type { get; set; }

        //支付类型名称
        public string typeName { get; set; }

        //外部编码
        public string extNo { get; set; }

        //缴款金额
        public decimal amount { get; set; }

        //应缴金额
        public decimal targetAmount { get; set; }

        //预缴金额
        public decimal preAmount { get; set; }

        //差异金额
        public decimal diffAmount { get; set; }

        //总部核对金额
        public decimal erpCheckAmount { get; set; }

        //总部处理原因
        public string erpMemo { get; set; }

        //备注
        public string memo { get; set; }

        //审批人ID
        public string approvalUserId { get; set; }

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

        //复制
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

