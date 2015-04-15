using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Commons.WinForm;
using Microsoft.Reporting.WinForms;

namespace Print
{
    public partial class OtherInStorePrint : Form
    {
        private string m_id;
        private bool m_isPrint;
        private List<Models.OtherInStorePrint> header;
        private List<Models.OtherInStoreDtlPrint> item;
        public OtherInStorePrint(string key, bool isPrint)
        {
            InitializeComponent();
            m_id = key;
            m_isPrint = isPrint;
        }

        private void OtherInDeliveryPrint_Load(object sender, EventArgs e)
        {

            try
            {
                var searchConditions = new
                {
                    ViewName = "OtherInStorePrint",
                    Where = string.Format("Receive.DOC_ID='{0}'", m_id)
                };
                var searchConditionsI = new
                {
                    ViewName = "OtherInStoreDtlPrint",
                    Where = string.Format("ReceiveDtl.DOC_ID='{0}'", m_id)
                };
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditions, ref header);
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditionsI, ref item);


                if (header!=null &&item!=null)
                {
                    rv.LocalReport.DataSources.Clear();
                    rv.LocalReport.DataSources.Add(new ReportDataSource("Header", header));
                    rv.LocalReport.DataSources.Add(new ReportDataSource("Item", item));
                    rv.RefreshReport();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
