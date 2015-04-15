using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Print.Models
{
    public class SalesPrint
    {
        public string Title { get; set; }
        public string Num { get; set; }
        public string PrintTime { get; set; }
        public string Tel { get; set; }
        public string StoreName { get; set; }
        public string StoreId { get; set; }
        public string DocId { get; set; }
        public string PersonLName { get; set; }
        public string PersonFName { get; set; }
        public string PersonTel { get; set; }
        public string DocDate { get; set; }
        public string AD { get; set; }
        public string SalesLName { get; set; }
        public string SalesFName { get; set; }
        public string CreateUserLName { get; set; }
        public string CreateUserFName { get; set; }
        public string CollectionAmount { set; get; }



    }
    public class SalesDtlPrint
    {
        public string DocId { get; set; }
        public string ProductName { get; set; }
        public string Config { get; set; }
        public string SeqId { get; set; }
        public string Quantity { get; set; }
        public string RebateAmount { get; set; }
        public string Memo { get; set; }
    }

}
