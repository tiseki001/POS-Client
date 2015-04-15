using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.SearchButton
{
    [Serializable]
    public class OperatorModel
    {
        /// <summary>
        /// 首操作符
        /// </summary>
        public string FirstOperator { get; set; }
        /// <summary>
        /// 操作符
        /// </summary>
        public List<string> OperatorList { get; set; } 
    }
}
