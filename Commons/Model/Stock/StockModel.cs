using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.JSON;

namespace Commons.Model.Stock
{
    public class StockModel
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public string StoreId { get; set; }
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
        /// 公司
        /// </summary>
        public string PartyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BaseEntryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BaseEntryType { get; set; }
        /// <summary>
        /// 串号是否被占用
        /// </summary>
        public string IsOccupied { get; set; }
    }

    public class StockDeTailModel : StockModel
    {
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
        /// 基础价格
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 仓库类型描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 库存Type
        /// </summary>
        public string InventoryItemType { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 是否串号管理
        /// </summary>
        public string isSequence { get; set; }
    }
}
