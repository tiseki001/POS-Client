namespace SalesOrder
{
    partial class LogisticsAddress
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
            this.textEdit_Address = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_OK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Address.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit_Address
            // 
            this.textEdit_Address.Location = new System.Drawing.Point(29, 42);
            this.textEdit_Address.Name = "textEdit_Address";
            this.textEdit_Address.Size = new System.Drawing.Size(621, 20);
            this.textEdit_Address.TabIndex = 60;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(29, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "物流出库地址:";
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(126, 87);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(87, 27);
            this.simpleButton_Cancel.TabIndex = 62;
            this.simpleButton_Cancel.Text = "取消(Esc)";
            // 
            // simpleButton_OK
            // 
            this.simpleButton_OK.Location = new System.Drawing.Point(29, 87);
            this.simpleButton_OK.Name = "simpleButton_OK";
            this.simpleButton_OK.Size = new System.Drawing.Size(87, 27);
            this.simpleButton_OK.TabIndex = 61;
            this.simpleButton_OK.Text = "确定(F4)";
            this.simpleButton_OK.Click += new System.EventHandler(this.simpleButton_OK_Click);
            // 
            // LogisticsAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 126);
            this.ControlBox = false;
            this.Controls.Add(this.simpleButton_Cancel);
            this.Controls.Add(this.simpleButton_OK);
            this.Controls.Add(this.textEdit_Address);
            this.Controls.Add(this.labelControl1);
            this.KeyPreview = true;
            this.Name = "LogisticsAddress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "信息填写";
            this.Load += new System.EventHandler(this.LogisticsAddress_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPreview_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Address.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit_Address;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton_OK;
    }
}