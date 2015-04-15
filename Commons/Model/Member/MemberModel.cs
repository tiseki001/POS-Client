using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.Member
{
    public class MemberModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string partyId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createdDate { get; set; }
        /// <summary>
        /// 店铺ID 
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phoneMobile { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string socialSecurityNumber { get; set; }
        /// <summary>
        /// 总积分
        /// </summary>
        public string totalIntegral { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public string integral { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string emailAddress { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardId { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public string partyClassificationGroupId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string stateProvinceGeoId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address1 { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string phoneHome { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string phoneWork { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string faxNumber { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string middleName { get; set; }
    }

    public class MemberDetailModel : MemberModel
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string storeName { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string memberName { get { return lastName + middleName + firstName; } }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public string description { get; set; }
    }
}
