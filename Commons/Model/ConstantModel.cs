using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model
{
    public class ConstantModel
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string StoreId { get; set; }
        /// <summary>
        /// DLL名称
        /// </summary>
        public string DllName { get; set; }
        /// <summary>
        /// 常量名称
        /// </summary>
        public string ConstantName { get; set; }
        /// <summary>
        /// 常量值
        /// </summary>
        public string ConstantValue { get; set; }
        /// <summary>
        /// 常量文本
        /// </summary>
        public string ConstantText { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class DocumentNumber
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string OrderId { get; set; }
    }
}
