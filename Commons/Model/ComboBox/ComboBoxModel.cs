using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Commons.Model.ComboBox
{
    public class ComboBoxModel
    {

        private string name;
        private DataTable dt;

        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
        }
        private string where;

        public string Where
        {
            get { return where; }
            set { where = value; }
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

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string ValueKey { get; set; }
        public string TextKey { get; set; }

    }
}
