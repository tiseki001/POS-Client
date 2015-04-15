using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //销售订单表头
    public class OrderHeaderModel
    {
        //订单ID：主键
        public string docId { get; set; }

        //订单号码（纯数字）
        public int docNo { get; set; }

        //公司ID
        public string partyId { get; set; }

        //门店ID
        public string storeId { get; set; }

        //上级订单号
        public string baseEntry { get; set; }

        //外部系统编码
        public string extSystemNo { get; set; }

        //外部单据编码
        public string extDocNo { get; set; }

        //过账日期
        public string postingDate { get; set; }

        //单据状态（草稿/确定/批准/已清/作废）	
        public string docStatus { get; set; }

        //备注信息
        public string memo { get; set; }

        //创建用户ID
        public string createUserId { get; set; }

        //创建日期
        public string createDocDate { get; set; }

        //最后更新用户ID
        public string lastUpdateUserId { get; set; }

        //最后更新日期
        public string lastUpdateDocDate { get; set; }

        //审批人ID
        public string approvalUserId { get; set; }

        //审批时间
        public string approvalDate { get; set; }

        //打印次数
        public int printNum { get; set; }

        //单据同步状态
        public string isSync { get; set; }

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

    //物品类订单明细
    public class ItemOrderDtlModel
    {
        //订单ID：主键
        public string docId { get; set; }

        //明细行号：主键
        public int lineNo { get; set; }

        //上级订单明细行号
        public int lineNoBaseEntry { get; set; }

        //商品ID
        public string productId { get; set; }

        //商品名称
        public string productName { get; set; }

        //商品条形码
        public string barCodes { get; set; }

        //外部编码
        public string extNo { get; set; }

        //序列号
        public string isSequence { get; set; }

        //序列号
        public string serialNo { get; set; }

        //原价
        public decimal unitPrice { get; set; }

        //折扣价（单价）
        public decimal rebatePrice { get; set; }

        //数量
        public int quantity { get; set; }

        //折扣价（小计单价）
        public decimal rebateAmount { get; set; }

        //备注
        public string memo { get; set; }

        //销售人员ID
        public string salesId { get; set; }

        //会员积分
        public int memberPoint { get; set; }

        //是否赠品
        public string isGift { get; set; }

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
    
    //款项类订单明细
    public class FundOrderDtlModel
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

        //外部编码
        public string extNo { get; set; }

        //收款金额
        public decimal amount { get; set; }

        //支付卡号
        public string cardCode { get; set; }

        //支付卡名称
        public string cardName { get; set; }

        //编码
        public string code { get; set; }

        //串号
        public string serialNo { get; set; }

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
