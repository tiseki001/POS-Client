using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Commons.WinForm;
using Print.Models;
using Microsoft.Reporting.WinForms;

namespace Print
{
    public partial class Sales : Form
    {
        private string m_id;
        private bool m_isPrint;
        private List<SalesPrint> header;
        private List<SalesDtlPrint> item;
        private System.Drawing.Printing.PageSettings defaultSettings = null;

        public Sales(string salesOrderId, bool isPrint)
        {
            InitializeComponent();
            m_id = salesOrderId;
            m_isPrint = isPrint;
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            try
            {

                var searchConditions = new
                {
                    ViewName = "SalesOrderPrint",
                    Where = "SOH.DOC_ID = '" + m_id + "'"
                };
                var searchConditionsI = new
                {
                    ViewName = "SalesOrderDtlPrint",
                    Where = "SOD.DOC_ID = '" + m_id + "'"
                };
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditions, ref header);
                DevCommon.getDataByWebService("view", "QueryService", "selectByConditions", searchConditionsI, ref item);




                if (header != null && item != null)
                {
                    //加载二维码图片
                    reportViewer.LocalReport.EnableExternalImages = true;
                    var first = header.FirstOrDefault();
                    var ap = string.Format("{0}\\QRCodes\\{1}.jpg", Application.StartupPath, first.StoreId);
                    var url = string.Format("file:///{0}", ap.Replace("\\", "/"));
                    reportViewer.LocalReport.SetParameters(new ReportParameter("QRCodesPath", url));

                    reportViewer.LocalReport.DataSources.Clear();
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Header", header));
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Item", item));

                    defaultSettings = reportViewer.GetPageSettings();
                    System.Drawing.Printing.PageSettings PS = reportViewer.GetPageSettings();
                    PS.Landscape = false;
                    PS.PaperSize = new System.Drawing.Printing.PaperSize { Width = 950, Height = 450 };
                    reportViewer.SetPageSettings(PS);
                    
                    reportViewer.RefreshReport();                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sales_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (defaultSettings!=null)
            reportViewer.SetPageSettings(defaultSettings);
        }
    }
}
