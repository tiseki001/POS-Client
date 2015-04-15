using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model
{
    public class LoginInfo
    {
        /// <summary>
        /// 登录人ID
        /// </summary>
        public static string UserLoginId { get; set; }
        /// <summary>
        /// 登录人姓名
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public static string PartyId { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public static string ProductStoreId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public static string StoreName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public static string CompanyName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public static string OwnerPartyId { get; set; }
    }

    public class LoginUser
    {
        /// <summary>
        /// 登录人ID
        /// </summary>
        public string UserLoginId { get; set; }
        /// <summary>
        /// 登录人姓名
        /// </summary>
        public  string UserName
        { get { return LastName + MiddleName + FirstName; } }
        /// <summary>
        /// 公司ID
        /// </summary>
        public  string PartyId { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public  string ProductStoreId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public  string StoreName { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public  string FirstName { get; set; }
        /// <summary>
        /// 中间名
        /// </summary>
        public  string MiddleName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public  string LastName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public  string CompanyName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string OwnerPartyId { get; set; }
    }
}
