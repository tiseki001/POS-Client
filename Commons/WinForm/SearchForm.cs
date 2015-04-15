using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Commons.Model.SearchButton;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using Commons.Cache;
using System.Runtime.Caching;
using Commons.XML;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using Commons.WebService;
using Commons.Model;


namespace Commons.WinForm
{
    public partial class SearchForm : DevExpress.XtraEditors.XtraForm
    {
        #region 参数
        /// <summary>
        /// 是否返回
        /// </summary>
        bool rtnEvent = false;
        //XML条件名称
        public string xmlConditionModel = null;
        //XMLWhere条件和显示列名称
        public string xmlWhereAndColumnModel = null;
        //XML条件路径
        public string xmlConditionPath = null;
        //XMLwhere条件和列表列路径
        public string xmlWhereAndColumnPath = null;
        //返回列的Value
        public string valueFieldColumn = null;
        //要查询的列
        public string fidleColumn = null;
        //查询的条件值
        public string fidleColumnText = null;
        /// <summary>
        /// 缓存主键
        /// </summary>
        public string primaryKey = null;
        /// <summary>
        /// 返回值列名
        /// </summary>
        public string fieldColumnOtherName = null;
        /// <summary>
        /// 查询列名列名
        /// </summary>
        string FieldColunm = null;
        /// <summary>
        /// 选择条件列表
        /// </summary>
        DataTable dtSelectCondition = null;
        /// <summary>
        /// where条件
        /// </summary>
        public DataTable dtWhere = null;
        /// <summary>
        /// 点击显示状态
        /// </summary>
        bool status = false;
        List<ShowColumnCondition> show = null;
        List<ConditionModel> listCondition = null;
        SearchConditionModel searchModel = null;
        string defaultwhere = "";
        object obj = null;
        bool initWhere = false;
        #endregion

        #region 返回值
        /// <summary>
        /// 返回KEY值
        /// </summary>
        public string RtnValue { get; set; }
        /// <summary>
        /// 返回文本值
        /// </summary>
        public string RtnText { get; set; }
        #endregion

        #region 构造
        public SearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="searchBrnName">按钮名称</param>
        /// <param name="searchText">查询输入的文本</param>
        /// <param name="FieldColunm">要查询的列名</param>
        public SearchForm(
            string xmlConditionPath,
            string xmlConditionModel,
            string xmlWhereAndColumnPath,
            string xmlWhereAndColumnModel,
            string fidleColumn,
            string fidleColumnText,
            string valueFieldColumn,
            string fieldColumnOtherName,
            string primaryKey,
            bool status,
            bool rtnEvent
            )
        {
            this.xmlConditionPath = xmlConditionPath;
            this.xmlConditionModel = xmlConditionModel;
            this.xmlWhereAndColumnModel = xmlWhereAndColumnModel;
            this.xmlWhereAndColumnPath = xmlWhereAndColumnPath;
            this.fidleColumn = fidleColumn;
            this.fidleColumnText = fidleColumnText;
            this.valueFieldColumn = valueFieldColumn;
            this.fieldColumnOtherName = fieldColumnOtherName;
            this.primaryKey = primaryKey;
            this.status = status;
            this.rtnEvent = rtnEvent;
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void SearchForm_Load(object sender, EventArgs e)
        {
            try
            {
                //隐藏操作符
                cboOperator.Visible = false;
                //隐藏查询按钮
                sbtnSerach.Visible = false;
                ////获得操作符
                OperatorData();
                ////创建选择条例列表
                CreateSelectCondition();
                //创建Where条件列
                if (dtWhere == null)
                {
                    CreateWhere();
                }
                //获得条件数据和下拉列表赋值
                ConditionInitData();
                //获得列表列和查询条件
                ShowAndSearchData();
                if (!string.IsNullOrEmpty(fidleColumnText.Trim()))
                {
                    if (listCondition != null)
                    {
                        foreach (var item in listCondition)
                        {
                            if (item.FieldColumnName.Equals(fidleColumn))
                            {
                                //显示操作符
                                cboOperator.Visible = true;
                                //显示查询按钮
                                sbtnSerach.Visible = true;
                                if (item.Type.Equals("1"))
                                {
                                    teText.Visible = true;
                                    imgcboSelectCondition.EditValue = item.ConditionValue;
                                    teText.Text = fidleColumnText.Trim();
                                }
                                if (item.Type.Equals("2"))
                                {
                                    baseCbo.Visible = true;
                                    imgcboSelectCondition.EditValue = item.ConditionValue;
                                    baseCbo.EditValue = fidleColumnText.Trim();
                                }
                                if (item.Type.Equals("3"))
                                {
                                    deDate.Visible = true;
                                    imgcboSelectCondition.EditValue = item.ConditionValue;
                                    teText.Text = fidleColumnText.Trim();
                                }
                                initWhere = true;
                                gcWhere.DataSource = dtWhere;
                                sbtnSerach_Click(null, null);
                            }
                        }
                    }
                }
                
                FormClose();
                if (dtWhere != null)
                {
                    if (dtWhere.Rows.Count > 0 && initWhere==false)
                    {
                        gcWhere.DataSource = dtWhere;
                        if (gvWhere.RowCount > 0)
                        {
                            GetWhereCondition();
                            InitData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Opacity = 1;
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 查询
        private void sbtnSerach_Click(object sender, EventArgs e)
        {
            string searchConditionId = imgcboSelectCondition.EditValue.ToString();
            string likeType = listCondition.FirstOrDefault(p => p.ConditionValue == searchConditionId).LikeType;
            DataRow dr = dtWhere.NewRow();
            int rowCount = dtWhere.Rows.Count;
            dr["ConditionDesc"] = imgcboSelectCondition.Text;
            dr["Operator"] = cboOperator.Text;
            dr["LikeType"] = likeType;
            if (teText.Visible)
            {
                if (!string.IsNullOrEmpty(teText.Text.Trim()))
                {
                    dr["ValueDesc"] = teText.Text.Trim();
                    dr["ValueId"] = teText.Text.Trim();
                }
                else
                {
                    return;
                }
            }
            if (baseCbo.Visible)
            {
                if (!string.IsNullOrEmpty(baseCbo.Text))
                {
                    dr["ValueDesc"] = baseCbo.Text.Trim();
                    dr["ValueId"] = baseCbo.EditValue.ToString();
                }
                else
                {
                    return;
                }
            }
            if (deDate.Visible)
            {
                if (!string.IsNullOrEmpty(deDate.Text))
                {
                    dr["ValueDesc"] = Convert.ToDateTime(deDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    dr["ValueId"] = Convert.ToDateTime(deDate.EditValue).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return;
                }
            }
            var fieldColumn = listCondition.Where(p => p.ConditionValue.Equals(searchConditionId)).Select(p => p.FieldColumnName).FirstOrDefault();
            dr["FieldColunm"] = fieldColumn;
            dtWhere.Rows.Add(dr);
            gcWhere.DataSource = dtWhere;
            if (gvWhere.RowCount > 0)
            {
                GetWhereCondition();
                InitData();
            }
        }
        #endregion

        #region 获得where条件
        private void GetWhereCondition()
        {
            try
            {
                StringBuilder sqlWhere = new StringBuilder();
                for (int i = 0; i < gvWhere.RowCount; i++)
                {
                    sqlWhere.Append(@" " + gvWhere.GetRowCellValue(i, "FieldColunm").ToString() + " ");
                    if (gvWhere.GetRowCellValue(i, "Operator").ToString().Equals("like") && gvWhere.GetRowCellValue(i, "LikeType").ToString().Equals("1"))
                    {
                        sqlWhere.Append(@" " + gvWhere.GetRowCellValue(i, "Operator").ToString() + " ");
                        sqlWhere.Append(@" '%" + gvWhere.GetRowCellValue(i, "ValueId").ToString() + "%' and");
                    }
                    else if (gvWhere.GetRowCellValue(i, "Operator").ToString().Equals("like") && gvWhere.GetRowCellValue(i, "LikeType").ToString().Equals("2"))
                    {
                        sqlWhere.Append(@" " + gvWhere.GetRowCellValue(i, "Operator").ToString() + " ");
                        sqlWhere.Append(@" '%" + gvWhere.GetRowCellValue(i, "ValueId").ToString() + "' and");
                    }
                    else
                    {
                        sqlWhere.Append(@" " + gvWhere.GetRowCellValue(i, "Operator").ToString() + " ");
                        sqlWhere.Append(@" '" + gvWhere.GetRowCellValue(i, "ValueId").ToString() + "' and");
                    }
                }

                if (sqlWhere.Length > 0)
                {
                    string conShere = sqlWhere.ToString();
                    searchModel.Where = defaultwhere+" "+conShere.Substring(0, conShere.LastIndexOf("and"));
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 条件列表删除事件
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            if (gvWhere.RowCount > 0)
            {
                gvWhere.DeleteSelectedRows();
            }
        }
        #endregion

        #region 选择条件改变事件
        private void imgcboSelectCondition_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(imgcboSelectCondition.Text.Trim()))
                {
                    cboOperator.Visible = false;
                    sbtnSerach.Visible = false;
                    teText.Visible = false;
                    baseCbo.Visible = false;
                    deDate.Visible=false;
                    pcSearchCondition.Refresh();
                }
                else
                {
                    cboOperator.Visible = true;
                    sbtnSerach.Visible = true;
                    string conditionKey = imgcboSelectCondition.EditValue.ToString().Trim();
                    if (listCondition != null)
                    {
                        var type = listCondition.Where(p => p.ConditionValue.Equals(conditionKey)).Select(p => p.Type).FirstOrDefault();
                        if (type != null)
                        {
                            if (type.Equals("1"))
                            {
                                teText.Text = "";
                                teText.Visible = true;
                                baseCbo.Visible = false;
                                deDate.Visible = false;
                            }
                            if (type.Equals("2"))
                            {
                                teText.Visible = false;
                                deDate.Visible = false;
                                baseCbo.Visible = true;
                                var xmlFile = listCondition.Where(p => p.ConditionValue.Equals(conditionKey)).Select(p => p.XmlFielName).FirstOrDefault();
                                var filterExpression = listCondition.Where(p => p.ConditionValue.Equals(conditionKey)).Select(p => p.FilterExpression).FirstOrDefault();
         
                                baseCbo.ModelName = xmlFile;
                                baseCbo.Properties.Items.Clear();
                                baseCbo.FilterExpression = filterExpression;
                                var fieldColumnName = listCondition.Where(p => p.ConditionValue.Equals(conditionKey)).Select(p => p.FieldColumnName).FirstOrDefault();
                                if (!string.IsNullOrEmpty(primaryKey) && !string.IsNullOrEmpty(fieldColumnName))
                                {
                                    baseCbo.Init(null, primaryKey + fieldColumnName);
                                    baseCbo.Bind(false);
                                }
                            }
                            if (type.Equals("3"))
                            {
                                deDate.Text = "";
                                teText.Visible = false;
                                baseCbo.Visible = false;
                                deDate.Visible = true; ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 获得操作符
        private void GetOperator()
        {
            //if (OperatorModel.OperatorList == null)
            //{
            //    List<OperatorModel> list = new List<OperatorModel>();
            //    OperatorModel item_1 = new OperatorModel();
            //    item_1.OperatorId = "1";
            //    item_1.Operator = "like";
            //    item_1.OperatorDesc = "等于号";
            //    list.Add(item_1);
            //    OperatorModel item_2 = new OperatorModel();
            //    item_2.OperatorId = "1";
            //    item_2.Operator = "=";
            //    item_2.OperatorDesc = "模糊查询";
            //    list.Add(item_2);
            //    OperatorModel.OperatorList = list;
            //    foreach (OperatorModel item in list)
            //    {
            //        cboOperator.Properties.Items.Add(item.Operator);
            //    }
            //    cboOperator.SelectedIndex = 0;
            //}
            //else
            //{
            //    foreach (OperatorModel operatorItem in OperatorModel.OperatorList)
            //    {
            //        cboOperator.Properties.Items.Add(operatorItem.Operator);
            //    }
            //    cboOperator.SelectedIndex = 0;
            //}
        }
        #endregion

        #region 创建选择条件下拉列表
        public void CreateSelectCondition()
        {
            try
            {
                dtSelectCondition = new DataTable();
                dtSelectCondition.Columns.Add("Id", typeof(string));
                dtSelectCondition.Columns.Add("Desc", typeof(string));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 创建Where条件列表
        public void CreateWhere()
        {
            try
            {
                dtWhere = new DataTable();
                dtWhere.Columns.Add("FieldColunm", typeof(string));
                dtWhere.Columns.Add("ConditionDesc", typeof(string));
                dtWhere.Columns.Add("ValueDesc", typeof(string));
                dtWhere.Columns.Add("ValueId", typeof(string));
                dtWhere.Columns.Add("Operator", typeof(string));
                dtWhere.Columns.Add("LikeType", typeof(string));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 查询条件集合
        public void ConditionInitData()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string path = AppDomain.CurrentDomain.BaseDirectory + @"" + xmlConditionPath + "" + "" + xmlConditionModel + ".xml";
                doc.Load(path);
                string condition = doc.InnerXml.ToString();
                Condition con = (Condition)XmlUtil.Deserialize(typeof(Condition), condition);
                listCondition = con.ListConditionModel;
                if (listCondition != null)
                {
                    foreach (var item in listCondition)
                    {
                        DataRow dr = dtSelectCondition.NewRow();
                        dr["Id"] = item.ConditionValue;
                        dr["Desc"] = item.ConditionText;
                        dtSelectCondition.Rows.Add(dr);
                    }
                    if (dtSelectCondition != null)
                    {
                        DevCommon.initCmb(imgcboSelectCondition, dtSelectCondition, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询搜索条件和列表显示数据
        public void ShowAndSearchData()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string path = AppDomain.CurrentDomain.BaseDirectory + @"" + xmlWhereAndColumnPath + "" + "" + xmlWhereAndColumnModel + ".xml";
                doc.Load(path);
                string condition = doc.InnerXml.ToString();
                searchModel = (SearchConditionModel)XmlUtil.Deserialize(typeof(SearchConditionModel), condition);
                if (searchModel != null)
                {
                    defaultwhere = formatWhere(searchModel.Where);
                    foreach (var item in searchModel.ShowColumn)
                    {
                        GridColumn gridColumn = new GridColumn();
                        gridColumn.Caption = item.FieldColumnCaption;
                        gridColumn.FieldName = item.FieldColumnName;
                        gridColumn.Visible = true;
                        gridColumn.OptionsColumn.ReadOnly = true;
                        gvResult.Columns.Add(gridColumn);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询列表数据
        public void InitData()
        {
            try
            {
                if (DevCommon.getDataByWebService("view", searchModel.ParentNote, searchModel.ChildNote, searchModel, ref obj) == RetCode.OK)
                {
                    gcResult.DataSource = obj;
                }
                else
                {
                    gcResult.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 确定
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvResult.RowCount > 0)
            {
                foreach (GridColumn item in gvResult.Columns)
                {
                    if (item.FieldName.Equals(fieldColumnOtherName))
                    {
                        RtnText = gvResult.GetFocusedRowCellValue(fieldColumnOtherName).ToString();
                    }
                    if (item.FieldName.Equals(valueFieldColumn))
                    {
                        RtnValue = gvResult.GetFocusedRowCellValue(valueFieldColumn).ToString();
                    }
                }
                this.Close();
            }
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 获取操作符数据
        public void OperatorData()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string path = AppDomain.CurrentDomain.BaseDirectory + @"XML\OperatorXML\Operator.xml";
                doc.Load(path);
                string condition = doc.InnerXml.ToString();
                OperatorModel operatorData = (OperatorModel)XmlUtil.Deserialize(typeof(OperatorModel), condition);
                if (operatorData != null)
                {
                    foreach (var item in operatorData.OperatorList)
                    {
                        cboOperator.Properties.Items.Add(item);
                    }
                    cboOperator.Text = operatorData.FirstOperator;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region 返回结果，关闭窗体
        private void FormClose()
        {
            if (status)
            {
                if (obj != null)
                {
                    DataTable dtResult = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                    if (rtnEvent == true)
                    {
                        if (dtResult.Rows.Count == 1)
                        {
                            foreach (GridColumn item in gvResult.Columns)
                            {
                                if (item.FieldName.Equals(fieldColumnOtherName))
                                {
                                    RtnText = gvResult.GetFocusedRowCellValue(fieldColumnOtherName).ToString();
                                }
                                if (item.FieldName.Equals(valueFieldColumn))
                                {
                                    RtnValue = gvResult.GetFocusedRowCellValue(valueFieldColumn).ToString();
                                }
                            }
                            this.Close();
                            return;
                        }
                        else
                        {
                            this.Opacity = 1;
                            return;
                        }
                    }
                    else 
                    {
                        this.Opacity = 1;
                        return;
                    }
                }
                else
                {
                    this.Opacity = 1;
                    return;
                }
            }
            else
            {
                this.Opacity = 1;
                return;
            }
        }
        #endregion

        #region 双击选择行数据
        private void gvResult_DoubleClick(object sender, EventArgs e)
        {
            if (gvResult.RowCount > 0)
            {
                foreach (GridColumn item in gvResult.Columns)
                {
                    if (item.FieldName.Equals(fieldColumnOtherName)&&!string.IsNullOrEmpty(fieldColumnOtherName))
                    {
                        RtnText = gvResult.GetFocusedRowCellValue(fieldColumnOtherName).ToString();
                    }
                    if (item.FieldName.Equals(valueFieldColumn) && !string.IsNullOrEmpty(valueFieldColumn))
                    {
                        RtnValue = gvResult.GetFocusedRowCellValue(valueFieldColumn).ToString();
                    }
                }
                this.Close();
            }
        }
        #endregion

        #region 条件转换
        /// <summary>
        /// 条件转换
        /// </summary>
        /// <param name="where">sql语句</param>
        /// <returns></returns>
        private string formatWhere(string where)
        {
            string result = "";
            if (where.IndexOf("@PRODUCT_STORE_ID") >= 0)
            {
                result = where.Replace("@PRODUCT_STORE_ID", LoginInfo.ProductStoreId);
            }
            else
            {
                result = where;
            }
            return result;
        }
        #endregion
    }
}