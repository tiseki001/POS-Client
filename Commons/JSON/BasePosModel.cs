using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Commons.JSON
{
    public class BasePosModel
    {
        public string flag { get; set; }
        public string msg { get; set; }        
    }
    public class StoreModel
    {
        public string ProductStoreId { get; set; }
        public string StoreName { get; set; }
    }
    public class StorePosModel : BasePosModel
    {
        public DataTable Data { get; set; }
    }
}
