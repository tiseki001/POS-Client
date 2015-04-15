using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.Model.ComboBox;
using System.IO;
using System.Xml.Serialization;
using Commons.XML;
using Commons.Cache;
using System.Collections;
using Newtonsoft.Json;
using Commons.WebService;
using Commons.JSON;
using Commons.Model;

namespace Commons.WinForm
{
    public partial class BaseCombobox : DevExpress.XtraEditors.ImageComboBoxEdit
    {
        private ComboBoxModel model;

        public ComboBoxModel Model
        {
            get { return model; }
            set { model = value; }
        }

        [
        Category("Alignment"),
        Description("filterExpression.")
        ]
        private string filterExpression;

        public string FilterExpression
        {
            get { return filterExpression; }
            set { filterExpression = value; }
        }

        [
        Category("Alignment"),
        Description("name.")
        ]
        private string modelName;

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }
        public BaseCombobox()
        {
            InitializeComponent();
        }
        public void Init(DataTable dt)
        {
            if (dt != null && dt.Rows.Count>0)
            {
                
                Model = new ComboBoxModel();

                Model.Dt = dt;
            }
            else
            {
                Model = MemoryCacheHelper.GetCacheItem<ComboBoxModel>("ComboBox-" + ModelName,
                   delegate() { return init(ModelName); },
                   new TimeSpan(0, 600, 0));//600分钟过期
            }
        }

        public void Init(DataTable dt,string primaryKey)
        {
            if (dt != null && dt.Rows.Count > 0)
            {

                Model = new ComboBoxModel();
                Model.Dt = dt;
            }
            else
            {
                Model = MemoryCacheHelper.GetCacheItem<ComboBoxModel>(primaryKey,
                   delegate() { return init(ModelName); },
                   new TimeSpan(0, 600, 0));//600分钟过期
            }
        }

        public void Bind(bool isNeedNull)
        {
            if (Model.Dt != null)
            {
                
                DataTable dt = Model.Dt.Select(FilterExpression).CopyToDataTable();
                DevCommon.initCmb(this, dt, Model.ValueKey, Model.TextKey, isNeedNull);
            }
        }
        private ComboBoxModel init(string name)
        {
            XMLBase xmlbase = new XMLBase(ModelName + ".xml");
            string str = xmlbase.ToString();
            ComboBoxModel tempModel = (ComboBoxModel)XmlUtil.Deserialize(typeof(ComboBoxModel), str);
            tempModel.Where = formatWhere(tempModel.Where);
           // tempModel.Where = "ssdd";
            string url = "";
            string func = "";
            GetData.GetUrl("QueryService", "selectByConditions", out url, out func);
            
            Hashtable Pars = new Hashtable();
            Pars.Add("view", JsonConvert.SerializeObject(tempModel));
            String jsonOut = WebSvcCaller.QuerySoapWebService(Pars, url,
            func);
            StorePosModel o = (StorePosModel)JsonConvert.DeserializeObject(jsonOut, typeof(StorePosModel));
            if (!string.IsNullOrEmpty("jsonOut"))
            {
               // tempModel.Dt = JsonDt.ListToDataTable(o.List);
                tempModel.Dt = o.Data;
            }
            
            return tempModel;
        }
        private string formatWhere(string where)
        {
            string result = "";
            
            if (where.IndexOf("@PARTY_ID")>=0)
            {
                result = where.Replace("@PARTY_ID", LoginInfo.PartyId);
            }
            if (where.IndexOf("@USER_ID") >= 0)
            {
                result = where.Replace("@USER_ID", LoginInfo.UserLoginId);
            }
            if (where.IndexOf("@STORE_ID") >= 0)
            {
                result = where.Replace("@STORE_ID", LoginInfo.ProductStoreId);
            }

            return result;
        }
    }
}
