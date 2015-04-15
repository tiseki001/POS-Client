using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PriceList
{
    class PriceListQueryModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string where;
        public string Where
        {
            get { return where; }
            set { where = value; }
        }

        private string start;
        public string Start
        {
            get { return start; }
            set { start = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private string viewName;
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        private List<string> orderBy;
        public List<string> OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        private List<string> fields;
        public List<string> Fields
        {
            get { return fields; }
            set { fields = value; }
        }
    }
}
