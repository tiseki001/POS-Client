using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print.Models
{
    /// <summary>
    /// 其它入库
    /// </summary>
    public class OtherInStorePrint
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string DocId { set; get; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public string Num { set; get; }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string DocDate { set; get; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string Type { set; get; }
        /// <summary>
        /// 门店
        /// </summary>
        public string Store { set; get; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string CreatorFirstName { set; get; }
        public string CreatorLastName { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { set; get; }
        
    }

    public class OtherInStoreDtlPrint
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string DocId { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { set; get; }
        /// <summary>
        /// 配置
        /// </summary>
        public string Config { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public double Price { set; get; }
        /// <summary>
        /// 小计
        /// </summary>
        public double Subtotal { get { return Quantity * Price; } }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { set; get; }
    }
}
