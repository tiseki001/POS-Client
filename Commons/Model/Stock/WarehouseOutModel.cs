using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Model.Stock
{
    public class WarehouseOutModel
    {
    }

    #region 发货头命令
    public class WarehouseOutHeaderCommand
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
        public string productStoreIdTo { get; set; }
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
        /// 公司ID
        /// </summary>
        public string partyId { get; set; }
        public string partyIdTo { get; set; }
        /// <summary>
        /// 同步标示
        /// </summary>
        public string isSync { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string status { get; set; }
        public string printNum { get; set; }
    }
    #endregion

    #region 命令头详细对象数据
    public class WarehouseOutHeaderDetailCommand : WarehouseOutHeaderCommand
    {
        /// <summary>
        /// 收货门店
        /// </summary>
        public string storeNameTo { get; set; }
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

    #region 发货行命令
    public class WarehouseOutItemCommand
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
        public string facilityIdTo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// 已发货数量
        /// </summary>
        public int? deliveryQuantity { get; set; }
        /// <summary>
        /// 父单号
        /// </summary>
        public string baseEnrtyNo { get; set; }
        /// <summary>
        /// 父行号
        /// </summary>
        public string baseLineNo { get; set; }
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

    #region 命令行详细对象数据
    public class WarehouseOutItemCommandDetail : WarehouseOutItemCommand
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
        public string facilityNameTo { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string brandName { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string config { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int? onHand { get; set; }
        /// <summary>
        /// 承诺数
        /// </summary>
        public int? promise { get; set; }
        /// <summary>
        /// 是否串号管理
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public int? availableQuantity { get { return onHand - promise; } }
    }
    #endregion

    #region 发货头数据
    public class WarehouseOutHeader
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
        public string productStoreIdTo { get; set; }
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
        /// 公司ID
        /// </summary>
        public string partyId { get; set; }
        public string partyIdTo { get; set; }
        /// <summary>
        /// 同步标示
        /// </summary>
        public string isSync { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string status { get; set; }
        public string printNum { get; set; }
    }
    #endregion

    #region 发货头明细数据
    public class WarehouseOutHeaderDetail : WarehouseOutHeader
    {
        /// <summary>
        /// 收货店铺名称
        /// </summary>
        public string storeNameTo { get; set; }
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

    #region 发货行
    public class WarehouseOutItem
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
        /// 收货仓库编号
        /// </summary>
        public string facilityIdTo { get; set; }
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

    #region 发货行明细
    public class WarehouseOutItemDetail : WarehouseOutItem
    {
        /// <summary>
        /// 出库仓库名称
        /// </summary>
        public string facilityName { get; set; }
        /// <summary>
        /// 收货仓库名称
        /// </summary>
        public string facilityNameTo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string idValue { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string config { get; set; }
        /// <summary>
        /// 发货数
        /// </summary>
        public int? planQuantity { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int? onHand { get; set; }
        /// <summary>
        /// 承诺数
        /// </summary>
        public int? promise { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public int? availableQuantity { get { return onHand - promise; } }
        /// <summary>
        /// 是否串号管理
        /// </summary>
        public string isSequence { get; set; }
        /// <summary>
        /// 已发货数量
        /// </summary>
        public int? deliveryQuantity { get; set; }
        public string updateSign { get; set; }
    }
    #endregion

    #region 商品库存
    public class WhsOutProduct
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
        /// 占用库存数
        /// </summary>
        public int? Promise { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string UnitCost { get; set; }
        /// <summary>
        /// 串号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 库存ID
        /// </summary>
        public string InventoryItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BaseEntryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BaseEntryType { get; set; }
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
        /// 可用库存
        /// </summary>
        public int? availableQuantity { get { return OnHand - Promise; } }
        /// <summary>
        /// 配置
        /// </summary>
        public string Config { get; set; }
    }
    #endregion
}
