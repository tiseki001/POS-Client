using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.WebService;
using Commons.Model.Order;
using Newtonsoft.Json;
using DevExpress.XtraTab;
using Commons.JSON;
using Commons.Model;

namespace PriceList
{
    public partial class ChangedPriceListForm : BaseForm
    {
        BaseMainForm m_FormBase;                          //框架窗口
        BaseForm m_FormParent;                            //上级窗口
        private String m_TimeofGetData;                   //读取数据的时间

        //窗体构造
        public ChangedPriceListForm(BaseMainForm formb, BaseForm formp)
        {
            m_FormBase = formb;
            m_FormParent = formp;
            InitializeComponent();
        }

        //窗体加载
        private void ChangedPriceListForm_Load(object sender, EventArgs e)
        {
            try
            {
                FillMatrixData();
                 simpleButton_Cancel.Focus();
            }
            catch (System.Exception ex)
            {
                m_FormBase.PromptInformation(ex.Message);
            }
        }

        //取消按钮按下
        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        //填充数据
        public void FillMatrixData()
        {
            String url = "";
            String func = "";
            String whereCondition = "";

            try
            {
                m_TimeofGetData = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
                //组织查询条件
                whereCondition += " C.PPRS_STORE_ID = '" + LoginInfo.ProductStoreId + "'";
               //  whereCondition += " AND C.PLC_LAST_UPDATED_STAMP < '" + m_TimeofGetData + "'";

                Commons.XML.GetData.GetUrl("QueryService", "selectByConditions", out url, out func);

                PriceListQueryModel Model = new PriceListQueryModel();
                Model.Name = "ChangedPriceListNotReadSearch";
                Model.ViewName = "ChangedPriceListNotRead";
                Model.Fields = new List<String>();
                Model.Fields.Add("brands");
                Model.Fields.Add("models");
                Model.Fields.Add("productId");
                Model.Fields.Add("productName");
                Model.Fields.Add("policyName");
                Model.Fields.Add("standardPrice");
                Model.Fields.Add("minPrice");
                Model.Fields.Add("costPrice");
                Model.Where = whereCondition;
                //Model.OrderBy = new List<String>();
                //Model.OrderBy.Add("productId");
                Model.Start = "0";
                Model.Number = "1000";

                Hashtable Pars = new Hashtable();
                Pars.Add("view", JsonConvert.SerializeObject(Model));
                String jsonOut = Commons.WebService.WebSvcCaller.QuerySoapWebService(Pars, url, func);
                BaseReturnResultModel<object> jsonData = JsonConvert.DeserializeObject<BaseReturnResultModel<object>>(jsonOut);
                
                if (!String.IsNullOrEmpty("jsonOut"))
                {
                    gridControl_PriceList.DataSource = jsonData.data;
                }
                else
                {
                    gridControl_PriceList.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }

        //关闭窗口
        private void CloseWindow()
        {
            try
            {
                //关闭画面
                if (m_FormParent == null)
                {
                    m_FormBase.closeTab();
                }
                else
                {
                    m_FormParent.Visible = true;
                    m_FormParent.BringToFront();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }

        //已处理
        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否已处理相关变价？", "确认", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                String url = "";
                String func = "";
                String condition = LoginInfo.ProductStoreId + "," + m_TimeofGetData;
                Commons.XML.GetData.GetUrl("updateChangedPriceList", "updateChangedPriceList", out url, out func);

                Hashtable Pars = new Hashtable();
                Pars.Add("whereCondition", JsonConvert.SerializeObject(condition));
                String jsonOut = WebSvcCaller.QuerySoapWebService(Pars, url,func);
                CloseWindow();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }

        //快捷键响应
        private void ChangedPriceListForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    //结算
                    this.simpleButton_OK.PerformClick();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    //取消
                    this.simpleButton_Cancel.PerformClick();
                }
            }
            catch (System.Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
    }
}