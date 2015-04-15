using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.Stock
{
    public class GoodsReceiptModel
    {
    }

    #region 收货头命令
    public class ReceiptHeaderCommand
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
        /// 收货门店
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 发货门店
        /// </summary>
        public string productStoreIdFrom { get; set; }
        /// <summary>
        /// 收货店铺
        /// </summary>
        public string storeIdFrom { get; set; }
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
        /// 状态
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
        /// 移动类型
        /// </summary>
        public string movementTypeId { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string status { get; set; }
        public string PartyId { get; set; }
        public string PartyIdTo { get; set; }
        public string isSync { get; set; }
        public string printNum { get; set; }
    }
    #endregion

    #region 命令头详细对象数据
    public class ReceiptHeaderCommandDetail : ReceiptHeaderCommand
    {
        /// <summary>
        /// 收货门店
        /// </summary>
        public string storeNameFrom { get; set; }
        /// <summary>
        /// 发货门店
        /// </summary>
        public string storeName { get; set; }
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

    #region 收货行指令
    public class ReceiptItemCommand
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
        /// 商品串号
        /// </summary>
        public string sequenceId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string facilityId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string facilityIdFrom { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// 已收货数量
        /// </summary>
        public int? receiveQuantity { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string idValue { get; set; }
        /// <summary>
        /// 串号管理
        /// </summary>
        public string isSequence { get; set; }
    }
    #endregion

    #region 收货行指令明细
    public class ReceiptItemCommandDetail : ReceiptItemCommand
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 出库仓库名称
        /// </summary>
        public string facilityName { get; set; }
        /// <summary>
        /// 收货仓库名称
        /// </summary>
        public string facilityNameFrom { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string brandName { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string config { get; set; }
    }
    #endregion

    #region 收货头数据
    public class ReceiptHeader
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
        /// 发货店铺
        /// </summary>
        public string productStoreId { get; set; }
        /// <summary>
        /// 收货店铺
        /// </summary>
        public string productStoreIdFrom { get; set; }
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
        /// 状态
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
        /// 移动类型
        /// </summary>
        public string movementTypeId { get; set; }
        public string partyId { get; set; }
        public string partyIdTo { get; set; }
        public string status { get; set; }
        public string isSync { get; set; }
        public string printNum { get; set; }
    }
    #endregion

    #region 收货头数据
    public class ReceiptHeaderDetail : ReceiptHeader
    {
        /// <summary>
        /// 收货门店
        /// </summary>
        public string storeNameFrom { get; set; }
        /// <summary>
        /// 发货门店
        /// </summary>
        public string storeName { get; set; }
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

    #region 收货明细
    public class ReceiptItem
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
        /// 商品串号
        /// </summary>
        public string sequenceId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string facilityId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string facilityIdFrom { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEntry { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string idValue { get; set; }
        /// <summary>
        /// 是否串号管理
        /// </summary>
        public string isSequence { get; set; }
    }
    #endregion

    #region 收货明细
    public class ReceiptItemDetail : ReceiptItem
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 出库仓库名称
        /// </summary>
        public string facilityName { get; set; }
        /// <summary>
        /// 收货仓库名称
        /// </summary>
        public string facilityNameFrom { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string brandName { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string config { get; set; }
        /// <summary>
        /// 已收货数量
        /// </summary>
        public int? receiveQuantity { get; set; }
        /// <summary>
        /// 计划收货数
        /// </summary>
        public int? planQuantity { get; set; }
    }
    #endregion
}
