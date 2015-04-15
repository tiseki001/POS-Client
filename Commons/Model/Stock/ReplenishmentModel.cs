using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.Stock
{
    class ReplenishmentModel
    {
    }

    #region 补货单头数据
    public class ReplenishmentHeader
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string docId { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 外推系统
        /// </summary>
        public string extSystemNo { get; set; }
        /// <summary>
        /// 外推单号
        /// </summary>
        public string extDocNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string docDate { get; set; }
        /// <summary>
        /// 承诺时间
        /// </summary>
        public string postingDate { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string docStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateUserId { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string updateDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string partyId { get; set; }
        /// <summary>
        /// 同步标示
        /// </summary>
        public string isSync { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public string printNum { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int saleDay { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName1 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName2 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName3 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName4 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName5 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName6 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName7 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName8 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName9 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName10 { get; set; }
        /// <summary>
        /// 到货时间
        /// </summary>
        public string receiveDate { get; set; }
    }
    #endregion

    #region 补货单头详细数据
    public class ReplenishmentHeaderDetail : ReplenishmentHeader
    {
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
        /// <summary>
        /// 制单人
        /// </summary>
        public string userName
        {
            get { return lastName + middleName + firstName; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateFirstName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateLastName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateMiddleName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateUserName
        {
            get { return updateLastName + updateMiddleName + updateFirstName; }
        }
    }
    #endregion

    #region 补货单行数据
    public class ReplenishmentItem
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string docId { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string lineNo { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string facilityId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string idValue { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName1 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName2 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName3 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName4 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName5 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName6 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName7 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName8 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName9 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string attrName10 { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int? onhand { get; set; }
        /// <summary>
        /// 安全库存
        /// </summary>
        public int? saftyQuantity { get; set; }
        /// <summary>
        /// 占用数
        /// </summary>
        public int? promise { get; set; }
        /// <summary>
        /// 待收货数
        /// </summary>
        public int? receiveQuantity { get; set; }
        /// <summary>
        /// 预定数
        /// </summary>
        public int? preQuantity { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public int? availableQuantity { get { return onhand - promise; } set { value = availableQuantity; } }
        /// <summary>
        /// erp回写的备注
        /// </summary>
        public string erpMemo { get; set; }
    }
    #endregion

    #region 补货单明细详细
    public class ReplenishmentItemDetail : ReplenishmentItem
    {
        public string productName { get; set; }
    }
    #endregion

    #region 商品信息
    public class ReplenishmentProduct
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
    }
    #endregion
}
