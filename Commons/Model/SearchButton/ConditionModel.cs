using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.SearchButton
{
    public class ConditionModel
    {
        /// <summary>
        /// value
        /// </summary>
        public string ConditionValue { get; set; }
        /// <summary>
        /// text
        /// </summary>
        public string ConditionText { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string FieldColumnName { get; set; }
        /// <summary>
        /// XML文件名
        /// </summary>
        public string XmlFielName { get; set; }
        /// <summary>
        /// FilterExpression
        /// </summary>
        public string FilterExpression { get; set; }
        /// <summary>
        /// 查询是like条件
        /// </summary>
        public string LikeType { set; get; }
    }

    public class Condition
    {
        public List<ConditionModel> ListConditionModel { get; set; }
    }

    public class SearchConditionModel
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public List<string> OrderBy { get; set; }
        /// <summary>
        /// 显示的列
        /// </summary>
        public List<ShowColumnCondition> ShowColumn { get; set; }
        /// <summary>
        /// 要查询的列
        /// </summary>
        public List<string> Fields { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentNote { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public string ChildNote { get; set; }
        /// <summary>
        /// XML名称
        /// </summary>
        public string Name { get; set; }
    }

    public class ShowColumnCondition
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string FieldColumnName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string FieldColumnCaption { get; set; }
    }
}
