using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.Stock
{
    class InventotyModel
    {
    }
    #region 盘点指令头数据
    public class InventotyHeaderCommand
    {
        /// <summary>
        /// 盘点名称
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string docId { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 盘点店铺
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? docDate { get; set; }
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
        public DateTime? updateDate { get; set; }
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
        /// 条件
        /// </summary>
        public string where { get; set; }
        /// <summary>
        /// 盘点类型
        /// </summary>
        public string type { get; set; }
    }
    #endregion

    #region 盘点指令头明细数据
    public class InventoryHeaderDetailCommand : InventotyHeaderCommand
    {
        public string productStoreName { set; get; }
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

    #region 盘点行数据
    public class InventoryItemCommand
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
        /// 库存数量
        /// </summary>
        public int? onHand { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
        /// <summary>
        /// 商品串号
        /// </summary>
        public string sequenceId { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string idValue { get; set; }
    }
    #endregion

    #region 盘点行详细数据
    public class InventotyDetailItemCommand : InventoryItemCommand
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 仓库Id
        /// </summary>
        public string facilityName { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string config { get; set; }
    }
    #endregion

    #region 盘点头数据
    public class InventotyHeader
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
        /// 盘点店铺
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? docDate { get; set; }
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
        public DateTime? updateDate { get; set; }
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
        /// 审批时间
        /// </summary>
        public DateTime? approveDate { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string approveUserId { get; set; }
        /// <summary>
        /// 盘点类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string attrName1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string attrName2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        public string attrName3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        public string attrName4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        public string attrName5 { get; set; }
        /// <summary>
        /// 预留字段6
        /// </summary>
        public string attrName6 { get; set; }
        /// <summary>
        /// 预留字段7
        /// </summary>
        public string attrName7 { get; set; }
        /// <summary>
        /// 预留字段8
        /// </summary>
        public string attrName8 { get; set; }
        /// <summary>
        /// 预留字段9
        /// </summary>
        public string attrName9 { get; set; }
        /// <summary>
        /// 预留字段10
        /// </summary>
        public string attrName10 { get; set; }

    }
    #endregion

    #region 盘点头详细数据
    public class InventotyDetailHeader : InventotyHeader
    {
        public string productStoreName { set; get; }
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

    #region 盘点行数据
    public class InventoryItem
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
        /// 盘点数量
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int? onHand { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
        /// <summary>
        /// 商品串号
        /// </summary>
        public string sequenceId { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string idValue { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string attrName1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string attrName2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        public string attrName3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        public string attrName4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        public string attrName5 { get; set; }
        /// <summary>
        /// 预留字段6
        /// </summary>
        public string attrName6 { get; set; }
        /// <summary>
        /// 预留字段7
        /// </summary>
        public string attrName7 { get; set; }
        /// <summary>
        /// 预留字段8
        /// </summary>
        public string attrName8 { get; set; }
        /// <summary>
        /// 预留字段9
        /// </summary>
        public string attrName9 { get; set; }
        /// <summary>
        /// 预留字段10
        /// </summary>
        public string attrName10 { get; set; }
    }
    #endregion

    #region 盘点行详细数据
    public class InventotyDetailItem : InventoryItem
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 仓库Id
        /// </summary>
        public string facilityName { get; set; }
        /// <summary>
        /// 差异数
        /// </summary>
        public int? differenceNum { get { return onHand - quantity; } }
    }
    #endregion

    #region 商品信息
    public class Product
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int? OnHand { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 库存ID
        /// </summary>
        public string InventoryItemId { get; set; }
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string IdValue { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string FacilityName { get; set; }
        /// <summary>
        /// 发货仓库
        /// </summary>
        public string FacilityId { get; set; }
        /// <summary>
        /// 是否串号管理
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string Config { get; set; }
    }
    #endregion
}
