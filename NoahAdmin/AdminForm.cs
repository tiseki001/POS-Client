using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Commons.Data;
using Commons.XML;
using Commons.JSON;
using System.Collections;
using Commons.WebService;

namespace NoahAdmin
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        DataTable m_dt;
        DataSet m_ds = new DataSet();
        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelDataProcess pro = new ExcelDataProcess();
            DataTable dt = pro.getExcelDataFromFile(2, 1);
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = dt;
            String entityName = this.textBox1.Text;
            dt.TableName = entityName;
            m_ds.Tables.Clear();
            m_ds.Tables.Add(dt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String entityName = this.textBox1.Text;

            String jsonTable = JsonDt.DataSetToJson(m_ds);
            string url = "";
            string func = "";
            GetData.GetUrl("importEntity", "importEntity", out url, out func);

            Hashtable Pars = new Hashtable();
            //  String jsonTable = "{123>456}";
            Pars.Add("default", jsonTable);
            // Pars.Add("entity-"+entityName, jsonTable);

            String jsonOut = WebSvcCaller.QuerySoapWebService(Pars, url,
            func);
            if (jsonOut.Equals("S"))
                MessageBox.Show("导入成功");
            else
            {
                MessageBox.Show(jsonOut);
            }
        }
    }
}
