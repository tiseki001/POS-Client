using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Commons.WinForm;
using Microsoft.Reporting.WinForms;

namespace Print
{
    public partial class OtherOutStorePrint : Form
    {
        private string m_id;
        private bool m_isPrint;
        private List<Models.OtherOutStorePrint> header;
        private List<Models.OtherOutStoreDtlPrint> item;
        public OtherOutStorePrint(string key, bool isPrint)
        {
            InitializeComponent();
            m_id = key;
            m_isPrint = isPrint;
        }

        private void OtherOutDeliveryPrint_Load(object sender, EventArgs e)
        {

            try
            {
                var searchConditions = new
                {
                    ViewName = "OtherOutStorePrint",
                    Where = string.Format("Delivery.DOC_ID='{0}'", m_id)
                };
                var searchConditionsI = new
                {
                    ViewName = "OtherOutStoreDtlPrint",
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
