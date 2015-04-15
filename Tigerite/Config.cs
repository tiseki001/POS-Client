using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.XML;

namespace Tigerite
{
    public partial class Config : DevExpress.XtraEditors.XtraForm
    {
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            XMLBase xmlUrl = new XMLBase("WebUrl.xml");

            //获得Ip地址
            this.txtIn.Text = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/In"), "IpAddress");
            //获得Ip地址
            this.txtOut.Text = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/Out"), "IpAddress");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XMLBase xmlUrl = new XMLBase("WebUrl.xml");
            xmlUrl.SetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/In"), "IpAddress", this.txtIn.Text);
            xmlUrl.SetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/Out"), "IpAddress", this.txtOut.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}