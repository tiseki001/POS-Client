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
    public partial class PriceListQueryForm : BaseForm
    {
        BaseMainForm m_FormBase;                          //框架窗口
        BaseForm m_FormParent;                            //上级窗口

        //窗体构造
        public PriceListQueryForm(BaseMainForm formb, BaseForm formp)
        {
            m_FormBase = formb;
            m_FormParent = formp;
            InitializeComponent();
        }

        //窗体加载
        private void PriceListQueryForm_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化显示
                //simpleButton_Query.PerformClick();
            }
            catch (System.Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }

        }

        //查询按钮按下
        private void simpleButton_Query_Click(object sender, EventArgs e)
        {
            SearchDataAndFillMatrix();
        }

        //填充数据
        public void SearchDataAndFillMatrix()
        {
            String url = "";
            String func = "";
            String whereCondition = "";

            try
            {
                 //组织查询条件
                whereCondition += " P0.PPRS_STORE_ID = '" + LoginInfo.ProductStoreId + "'";

                //品牌
                if (!String.IsNullOrEmpty(textEdit_Brand.Text.Trim()))
                {
                    whereCondition += " AND F1.DESCRIPTION LIKE '%" + textEdit_Brand.Text.Trim() + "%'";
                }
                //型号
                if (!String.IsNullOrEmpty(textEdit_ProductType.Text.Trim()))
                {
                    whereCondition += " AND F0.DESCRIPTION LIKE '%" + textEdit_ProductType.Text.Trim() + "%'";
                }
                //商品
                if (!String.IsNullOrEmpty(textEdit_ProductId.Text.Trim()))
                {
                    whereCondition += " AND P0.PPL_PRODUCT_ID LIKE '%" + textEdit_ProductId.Text.Trim() + "%'";
                }
                if (!String.IsNullOrEmpty(textEdit_ProductName.Text.Trim()))
                {
                    whereCondition += " AND D.PRODUCT_NAME LIKE '%" + textEdit_ProductName.Text.Trim() + "%'";
                }
                //销售政策
                if (!String.IsNullOrEmpty(textEdit_PolicyName.Text.Trim()))
                {
                    whereCondition += " AND S.POLICY_NAME LIKE '%" + textEdit_PolicyName.Text.Trim() + "%'";
                }

                if (!whereCondition.Contains("AND"))
                {
                    return;
                }

                //检索数据
                Commons.XML.GetData.GetUrl("QueryService", "selectByConditions", out url, out func);

                PriceListQueryModel Model = new PriceListQueryModel();
                Model.Name = "ProductPriceListQuerySearch";
                Model.ViewName = "ProductPriceListQuery";
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
                Model.OrderBy = new List<String>();
                Model.OrderBy.Add("productId");
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

        //取消按钮按下
        private void simpleButton_Cancel_Click(object sender, EventArgs e)
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

        //快捷键响应
        private void PriceListQueryForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (e.KeyCode == Keys.Escape)
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