using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Stock;
using Newtonsoft.Json;

namespace Commons.Model.Order
{
    //退货入库单
    public class ReturnInWhsOrderModel
    {
        //销售订单表头
        public ReturnInWhsOrderHeaderModel header = new ReturnInWhsOrderHeaderModel();

        //销售订单明细
        public List<ReturnInWhsOrderDtlModel> item = new List<ReturnInWhsOrderDtlModel>();
    }

    //退货入库单表头
    public class ReturnInWhsOrderHeaderModel : WarehouseInHeader
    {
        //[JsonIgnore]
        //public string createUserName { get; set; }
    }

    //退货入库单明细
    public class ReturnInWhsOrderDtlModel : WarehouseInItem
    {
        //已出库数量
        [JsonIgnore]
        public int warehouseQuantity { get; set; }

        //商品名称
        public string productName { get; set; }

        //复制
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
