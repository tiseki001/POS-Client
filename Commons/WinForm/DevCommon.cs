using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Data;
using System.Windows.Forms;
using Commons.Model;
using Commons.Cache;
using Commons.XML;
using System.Collections;
using Commons.WebService;
using Newtonsoft.Json;
using DevExpress.XtraEditors.Repository;
using Commons.Model.Order;
using Newtonsoft.Json.Converters;
using Commons.JSON;

namespace Commons.WinForm
{
    #region 通用方法
    public class DevCommon
    {
        #region 下拉列表数据赋值
        /// <summary>
        /// 下拉列表条件
        /// </summary>
        /// <param name="cmb">下拉列表控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="key">key值</param>
        /// <param name="value">value值</param>
        /// <param name="isNeedNull">isNeedNull=false,下拉列表默认显示第一行。isNeedNull=true,下拉列表默认为空。</param>
        public static void initCmb(ImageComboBoxEdit cmb, DataTable dt, string key, string value, bool isNeedNull)
        {
            ImageComboBoxItem[] items = new ImageComboBoxItem[dt.Rows.Count + 1];
            int i = 0;
            if (isNeedNull)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = "";
                items[i].Value = "";
                items[i].ImageIndex = -1;
                i++;
            }
            foreach (DataRow dr1 in dt.Rows)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = dr1[value].ToString();
                items[i].Value = dr1[key];
                items[i].ImageIndex = -1;
                i++;
            }
            cmb.Properties.Items.Clear();
            cmb.Properties.Items.AddRange(items);
            cmb.EditValue = items[0].Value;
        }

        #region 下拉列表数据赋值
        /// <summary>
        /// 下拉列表条件
        /// </summary>
        /// <param name="cmb">下拉列表控件</param>
        /// <param name="dt">数据源,第一列为key，第二列为value</param>
        /// <param name="isNeedNull">isNeedNull=false,下拉列表默认显示第一行。isNeedNull=true,下拉列表默认为空。</param>
        public static void initCmb(ImageComboBoxEdit cmb, DataTable dt, bool isNeedNull)
        {
            ImageComboBoxItem[] items = new ImageComboBoxItem[dt.Rows.Count + 1];
            int i = 0;
            if (isNeedNull)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = "";
                items[i].Value = "";
                items[i].ImageIndex = -1;
                i++;
            }
            foreach (DataRow dr1 in dt.Rows)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = dr1[1].ToString();
                items[i].Value = dr1[0];
                items[i].ImageIndex = -1;
                i++;
            }
            cmb.Properties.Items.Clear();
            cmb.Properties.Items.AddRange(items);
            if (cmb.Properties.Items.Count > 0)
            {
                cmb.EditValue = items[0].Value;
            }
        }
        #endregion

        #region 下拉列表数据赋值
        /// <summary>
        /// 下拉列表条件
        /// </summary>
        /// <param name="cmb">下拉列表控件</param>
        /// <param name="dt">数据源,第一列为key，第二列为value</param>
        /// <param name="isNeedNull">isNeedNull=false,下拉列表默认显示第一行。isNeedNull=true,下拉列表默认为空。</param>
        public static void initCmb(RepositoryItemImageComboBox cmb, DataTable dt, bool isNeedNull)
        {
            ImageComboBoxItem[] items = new ImageComboBoxItem[dt.Rows.Count + 1];
            int i = 0;
            if (isNeedNull)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = "";
                items[i].Value = "";
                items[i].ImageIndex = -1;
                i++;
            }
            foreach (DataRow dr1 in dt.Rows)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = dr1[1].ToString();
                items[i].Value = dr1[0];
                items[i].ImageIndex = -1;
                i++;
            }
            cmb.Properties.Items.AddRange(items);
        }
        #endregion

        #region 下拉列表数据赋值
        /// <summary>
        /// 下拉列表数据赋值
        /// </summary>
        /// <param name="cmb">下拉列表控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="value">key</param>
        /// <param name="text">text</param>
        /// <param name="isNeedNull"></param>
        public static void initCmb(RepositoryItemImageComboBox cmb, DataTable dt,string value,string text, bool isNeedNull)
        {
            ImageComboBoxItem[] items = new ImageComboBoxItem[dt.Rows.Count + 1];
            int i = 0;
            if (isNeedNull)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = "";
                items[i].Value = "";
                items[i].ImageIndex = -1;
                i++;
            }
            foreach (DataRow dr1 in dt.Rows)
            {
                items[i] = new ImageComboBoxItem();
                items[i].Description = dr1[text].ToString();
                items[i].Value = dr1[value];
                items[i].ImageIndex = -1;
                i++;
            }
            cmb.Properties.Items.AddRange(items);

        }
        #endregion
        #endregion

        #region 时间验证
        /// <summary>
        /// 时间验证
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回值：true or false</returns>
        public static bool ValidateDate(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
            {
                MessageBox.Show("开始时间和结束时间不能为空！");
                return false;
            }
            else if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                MessageBox.Show("开始时间或结束时间不能为空！");
                return false;
            }
            else if (startDate.Year <= 1753 || endDate.Year <= 1753)
            {
                MessageBox.Show("开始时间或结束时间不能小于1753年！");
                return false;
            }
            else if (startDate > endDate)
            {
                MessageBox.Show("开始时间不能小于结束时间！");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 查询常量信息
        public static List<ConstantModel> GetConstant()
        {
            return MemoryCacheHelper.GetCacheItem<List<ConstantModel>>("Constant",
                () => LoadConstant(),
                new TimeSpan(12, 0, 0));
        }

        private static List<ConstantModel>LoadConstant()
        {
            try
            {
                List<ConstantModel> list = null;
                var storeId = new { storeId = LoginInfo.ProductStoreId };
                if (getDataByWebService("Constant", "ConstantSearch", storeId, ref list) == RetCode.OK)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得查询条件天数
        public static int GetDay()
        {
            try
            {
                int day = 0;
                List<ConstantModel> constant = GetConstant();
                var dayItem = constant.Where(p => p.ConstantName.ToUpper() == "searchDate".ToUpper()).Select(p => new { day = p.ConstantValue });
                foreach (var item in dayItem)
                {
                    day = Convert.ToInt32(item.day);
                }
                return day;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得所有单据状态
        /// <summary>
        /// 获得草稿状态
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllOrderStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                List<ConstantModel> constant = GetConstant();
                var draft = constant.Where(p => p.ConstantName.ToUpper().StartsWith("docStatus".ToUpper())
                                    )
                                    .Select(p => new { Value = p.ConstantValue, Text = p.ConstantText });
                foreach (var item in draft)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.Text;
                    dr["Value"] = item.Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得所有业务状态
        /// <summary>
        /// 获得草稿状态
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllBusinessStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                List<ConstantModel> constant = GetConstant();
                var draft = constant.Where(p => p.ConstantName.ToUpper().StartsWith("businessStatus".ToUpper())
                                           )
                                    .Select(p => new { Value = p.ConstantValue, Text = p.ConstantText });
                foreach (var item in draft)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.Text;
                    dr["Value"] = item.Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 故障类型
        public static DataTable FaultType()
        {
            try
            {
                object obj = null;
                DataTable dtFaultType = null;
                if (getDataByWebService("FaultType", "FaultType", "", ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        return dtFaultType = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 会员等级
        public static DataTable MemberGrade()
        {
            try
            {
                object obj = null;
                if (getDataByWebService("MemberGrade", "MemberGrade", "", ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        return JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得订单单号
        public static RetCode getDocId(DocType docType, ref string docId)
        {
            try
            {
                queryOrderId QOI = new queryOrderId();
                QOI.storeId = LoginInfo.ProductStoreId;
                QOI.type = Convert.ToChar(docType);         
                if (getDataByWebService("getOrderId", "getOrderId", QOI, ref docId) == RetCode.NG)
                {
                    return RetCode.NG;
                }
                return RetCode.OK;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取数据
        public static RetCode getDataByWebService<TI, TO>(string nodeIpAddress, string attributeIpAddress, TI inObj, ref TO outObj)
        {
            try
            {
                string url = null;
                string functionName = null;
                string defaultValue = null;
                //时间转换
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
                String strJason = JsonConvert.SerializeObject(inObj, convert);

                Hashtable Pars = new Hashtable();
                Pars.Add("jsonStr", strJason);

                GetData.GetUrl(nodeIpAddress, attributeIpAddress, out url, out functionName, out defaultValue);
                String str = WebSvcCaller.QuerySoapWebService(Pars, url, functionName);
                BaseReturnResultModel<TO> jsonData = JsonConvert.DeserializeObject<BaseReturnResultModel<TO>>(str);
                if (jsonData.flag == (Convert.ToChar(RetCode.NG)).ToString())
                {
                    throw new Exception(jsonData.msg);
                }
                else if (jsonData.flag == (Convert.ToChar(RetCode.OK)).ToString())
                {
                    outObj = jsonData.data;
                }

                return RetCode.OK;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static BasePosModel getDataByWebServiceN<TI, TO>(string nodeIpAddress, string attributeIpAddress, TI inObj, ref TO outObj)
        {
            try
            {
                BasePosModel msg = new BasePosModel();
                string url = null;
                string functionName = null;
                string defaultValue = null;
                //时间转换
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
                String strJason = JsonConvert.SerializeObject(inObj, convert);

                Hashtable Pars = new Hashtable();
                Pars.Add("jsonStr", strJason);

                GetData.GetUrl(nodeIpAddress, attributeIpAddress, out url, out functionName, out defaultValue);
                String str = WebSvcCaller.QuerySoapWebService(Pars, url, functionName);
                BaseReturnResultModel<TO> jsonData = JsonConvert.DeserializeObject<BaseReturnResultModel<TO>>(str);
                if (jsonData.flag != (Convert.ToChar(RetCode.OK)).ToString())
                {
                    msg.flag = jsonData.flag;
                    msg.msg = jsonData.msg;
                }
                else
                {
                    outObj = jsonData.data;
                    msg.flag = jsonData.flag;
                    msg.msg = jsonData.msg;
                }

                return msg;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static string getDataByWebServiceN<TI>(string nodeIpAddress, string attributeIpAddress, TI inObj)
        {
            try
            {
                BasePosModel msg = new BasePosModel();
                string url = null;
                string functionName = null;
                string defaultValue = null;
                //时间转换
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
                String strJason = JsonConvert.SerializeObject(inObj, convert);

                Hashtable Pars = new Hashtable();
                Pars.Add("jsonStr", strJason);

                GetData.GetUrl(nodeIpAddress, attributeIpAddress, out url, out functionName, out defaultValue);
                String str = WebSvcCaller.QuerySoapWebService(Pars, url, functionName);

                return str;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #region 获得所有故障状态
        /// <summary>
        /// 获得故障状态
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllFaultType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                List<ConstantModel> constant = GetConstant();
                var draft = constant.Where(p => p.ConstantName.StartsWith("faultType")
                                           )
                                    .Select(p => new { Value = p.ConstantValue, Text = p.ConstantText });
                foreach (var item in draft)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.Text;
                    dr["Value"] = item.Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static RetCode getDataByWebService<TI, TO>(string jsonName,string nodeIpAddress, string attributeIpAddress, TI inObj, ref TO outObj)
        {
            try
            {
                string url = null;
                string functionName = null;
                string defaultValue = null;
                //时间转换
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
                String strJason = JsonConvert.SerializeObject(inObj, convert);

                Hashtable Pars = new Hashtable();
                Pars.Add(jsonName, strJason);

                GetData.GetUrl(nodeIpAddress, attributeIpAddress, out url, out functionName, out defaultValue);
                String str = WebSvcCaller.QuerySoapWebService(Pars, url, functionName);
                BaseReturnResultModel<TO> jsonData = JsonConvert.DeserializeObject<BaseReturnResultModel<TO>>(str);
                if (jsonData.flag == "F")
                {
                    throw new Exception(jsonData.msg);
                }
                else if (jsonData.flag == "S")
                {
                    outObj = jsonData.data;
                }
                else if(string.IsNullOrEmpty(jsonData.flag))
                {
                    outObj = jsonData.data;
                }
                return RetCode.OK;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region 获得总部库ID
        /// <summary>
        /// 获得webservices地址和方法名
        /// </summary>
        /// <param name="routeNote">services地址节点名</param>
        /// <param name="functionNote">方法节点名</param>
        /// <param name="url">webservices地址</param>
        /// <param name="functionName">方法名</param>
        /// <param name="defaultValue">默认值</param>
        public static string getStoreSetting(string note, string type)
        {
            try
            {
                XMLBase xmlBase = new XMLBase("StoreSetting.xml");
                //获得总部仓库ID
                return xmlBase.GetNodeValue(xmlBase.GetNodeObject(note), type);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #endregion

        #region 过账日期
        public static string PostingDate()
        {
            try
            {
                object dtPostingDate = null;
                var condition = new { productStoreId=LoginInfo.ProductStoreId};
                if (getDataByWebService("PostingDate", "PostingDate", condition, ref dtPostingDate) == RetCode.OK)
                {
                    if (dtPostingDate != null)
                    {
                        Hashtable dt = JsonConvert.DeserializeObject<Hashtable >(dtPostingDate.ToString());
                        if (dt!=null)
                        {
                            return Convert.ToDateTime(dt["postingDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据店铺ID和移动类型获得仓库Id
        public static string GetFacilityId(string productStoreId, string moveType)
        {
            try
            {
                object dtMoveType = null;
                var condition = new { productStoreId = LoginInfo.ProductStoreId, movementTypeId = moveType };
                if (getDataByWebService("FacilityId", "FacilityId", condition, ref dtMoveType) == RetCode.OK)
                {
                    if (dtMoveType != null)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(dtMoveType.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            return dt.Rows[0]["facilityId"].ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region searchButton获得显示本门店员工的条件
        public static DataTable getSalesPersonWhere()
        {
            DataTable dtWhere = new DataTable();
            dtWhere.Columns.Add("FieldColunm", typeof(string));
            dtWhere.Columns.Add("ConditionDesc", typeof(string));
            dtWhere.Columns.Add("ValueDesc", typeof(string));
            dtWhere.Columns.Add("ValueId", typeof(string));
            dtWhere.Columns.Add("Operator", typeof(string));
            dtWhere.Columns.Add("LikeType", typeof(string));

            DataRow dr = dtWhere.NewRow();
            dr["LikeType"] = "1";
            dr["Operator"] = "like";
            dr["FieldColunm"] = "PST.PRODUCT_STORE_ID";
            dr["ConditionDesc"] = "门店编号";
            dr["ValueDesc"] = LoginInfo.ProductStoreId;
            dr["ValueId"] = LoginInfo.ProductStoreId;
            dtWhere.Rows.Add(dr);

            return dtWhere;
        }
        #endregion 

        #region 获得日结账类型
        /// <summary>
        /// 获得日结账类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPostingType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                List<ConstantModel> constant = GetConstant();
                var draft = constant.Where(p => p.ConstantName.ToUpper().StartsWith("postingType".ToUpper())
                                    )
                                    .Select(p => new { Value = p.ConstantValue, Text = p.ConstantText });
                foreach (var item in draft)
                {
                    DataRow dr = dt.NewRow();
                    dr["Text"] = item.Text;
                    dr["Value"] = item.Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获得仓库
        public static DataTable Facility()
        {
            try
            {
                //标示
                string mag = null;
                GetData.GetUrl("Stock", "FacilitySearch", out mag);
                var searchCondition = new { productStoreId = LoginInfo.ProductStoreId, mag = mag };
                object obj = null;
                if (DevCommon.getDataByWebService("Stock", "FacilitySearch", searchCondition, ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        return JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
    #endregion

    #region 查询订单号码
    public class queryOrderId
    {
        public string storeId { get; set; }
        public char type { get; set; }
    }
    #endregion
}
