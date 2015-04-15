using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Model.Order;

namespace ReturnInWhsOrder
{
    public class Run
    {
        //设置入库单
        public ReturnInWhsOrderModel setReturnInWhsOrderFromReturnOrder(ReturnOrderModel RO)
        {
            return ReturnInWhsBLL.setReturnInWhsOrderFromReturnOrder(RO);
        }
    }
}
