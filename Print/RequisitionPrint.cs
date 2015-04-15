using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Commons.WinForm;
using Microsoft.Reporting.WinForms;
using Print.Models;

namespace Print
{
    public partial class RequisitionPrint : Form
    {
        private string m_id;
        private bool m_isPrint;
        private List<Models.RequisitionPrint> header;
        private List<RequisitionDtlPrint> item;
        public RequisitionPrint(string key,bool isPrint)
        {
            InitializeComponent();
            this.m_id = key;
            this.m_isPrint = isPrint;
        }

        private void DeliveryPrint_Load(object sender, EventArgs e)
        {
            try
            {
                var searchConditions = new
                {
                    ViewName = "RequisitionPrint",
                    Where = string.Format("Delivery.DOC_ID='{0}'",m_id)
                };
                var searchConditionsI = new
                {
                    ViewName = "RequisitionDtlPrint",
                    Where = string.Format("DeliveryDtl.DOC_ID='{0}'", m_id)
                };
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditions, ref header);
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditionsI, ref item);


                if (header != null && item != null)
                {
                    rv.LocalReport.DataSources.Clear();
                    rv.LocalReport.DataSources.Add(new ReportDataSource("Header", header));
                    rv.LocalReport.DataSources.Add(new ReportDataSource("Item", item));
                    rv.RefreshReport();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
