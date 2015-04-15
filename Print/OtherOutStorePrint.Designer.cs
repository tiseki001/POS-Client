namespace Print
{
    partial class OtherOutStorePrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.OtherOutStorePrintBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OtherOutStoreDtlPrintBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rv = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.OtherOutStorePrintBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherOutStoreDtlPrintBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // OtherOutStorePrintBindingSource
            // 
            this.OtherOutStorePrintBindingSource.DataSource = typeof(Print.Models.OtherOutStorePrint);
            // 
            // OtherOutStoreDtlPrintBindingSource
            // 
            this.OtherOutStoreDtlPrintBindingSource.DataSource = typeof(Print.Models.OtherOutStoreDtlPrint);
            // 
            // rv
            // 
            this.rv.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Header";
            reportDataSource1.Value = this.OtherOutStorePrintBindingSource;
            reportDataSource2.Name = "Item";
            reportDataSource2.Value = this.OtherOutStoreDtlPrintBindingSource;
            this.rv.LocalReport.DataSources.Add(reportDataSource1);
            this.rv.LocalReport.DataSources.Add(reportDataSource2);
            this.rv.LocalReport.ReportEmbeddedResource = "Print.OtherOutStorePrint.rdlc";
            this.rv.Location = new System.Drawing.Point(0, 0);
            this.rv.Name = "rv";
            this.rv.Size = new System.Drawing.Size(906, 436);
            this.rv.TabIndex = 0;
            // 
            // OtherOutStorePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 436);
            this.Controls.Add(this.rv);
            this.Name = "OtherOutStorePrint";
            this.Text = "OtherOutDeliveryPrint";
            this.Load += new System.EventHandler(this.OtherOutDeliveryPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OtherOutStorePrintBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherOutStoreDtlPrintBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rv;
        private System.Windows.Forms.BindingSource OtherOutStorePrintBindingSource;
        private System.Windows.Forms.BindingSource OtherOutStoreDtlPrintBindingSource;
    }
}