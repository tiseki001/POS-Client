using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Print.Models
{
    /// <summary>
    /// 调拨出库头
    /// </summary>
    public class RequisitionPrint
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
        /// 调出仓库
        /// </summary>
        public string From { set; get; }
        /// <summary>
        /// 调入仓库
        /// </summary>
        public string To { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { set; get; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorFirstName { set; get; }
        public string CreatorLastName { set; get; }

    }

    /// <summary>
    /// 调拨出库行
    /// </summary>
    public class RequisitionDtlPrint
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
        /// 备注
        /// </summary>
        public string Memo { set; get; }
    }
}
