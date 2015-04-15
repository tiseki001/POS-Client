namespace Print
{
    partial class Sales
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
            this.SalesPrintBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SalesDtlPrintBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPrintBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDtlPrintBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SalesPrintBindingSource
            // 
            this.SalesPrintBindingSource.DataSource = typeof(Print.Models.SalesPrint);
            // 
            // SalesDtlPrintBindingSource
            // 
            this.SalesDtlPrintBindingSource.DataSource = typeof(Print.Models.SalesDtlPrint);
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Header";
            reportDataSource1.Value = this.SalesPrintBindingSource;
            reportDataSource2.Name = "Item";
            reportDataSource2.Value = this.SalesDtlPrintBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "Print.SalesOrderPrint.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(846, 435);
            this.reportViewer.TabIndex = 0;
            // 
            // Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 435);
            this.Controls.Add(this.reportViewer);
            this.Name = "Sales";
            this.Text = "Sales";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Sales_FormClosed);
            this.Load += new System.EventHandler(this.Sales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SalesPrintBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDtlPrintBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource SalesPrintBindingSource;
        private System.Windows.Forms.BindingSource SalesDtlPrintBindingSource;
    }
}