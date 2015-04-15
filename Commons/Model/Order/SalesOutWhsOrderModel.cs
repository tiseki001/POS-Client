using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Stock;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //销售出库单
    public class SalesOutWhsOrderModel
    {
        //销售订单表头
        public SalesOutWhsOrderHeaderModel header = new SalesOutWhsOrderHeaderModel();

        //销售订单明细
        public List<SalesOutWhsOrderDtlModel> item = new List<SalesOutWhsOrderDtlModel>();
    }

    //销售出库单表头
    public class SalesOutWhsOrderHeaderModel : WarehouseOutHeader
    {
        //[JsonIgnore]
        //public string createUserName { get; set; }
    }

    //销售出库单明细
    public class SalesOutWhsOrderDtlModel : WarehouseOutItem
    {
        //销售数量
        public int saleQuantity { get; set; }

        //已出库数量
        [JsonIgnore]
        public int warehouseQuantity { get; set; }

        //解锁数量
        public int unLockedQuantity { get; set; }

        //商品名称
        public string productName { get; set; }

        //复制
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
