namespace Payment
{
    partial class Payment
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateEdit_PostingDate = new DevExpress.XtraEditors.DateEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_DocId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_PostingDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl_DocId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_Save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit_MemoHeader = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_PaymentIn = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_PrePaymentIn = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl_PaymentIn = new DevExpress.XtraGrid.GridControl();
            this.gridView__PaymentIn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_TypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_TargetAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_PreAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_DiffAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6_Memo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ERPCheckAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ERPMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PostingDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PostingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_DocId.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_MemoHeader.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PaymentIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView__PaymentIn)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateEdit_PostingDate);
            this.panelControl1.Controls.Add(this.comboBoxEdit1);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.textEdit_DocId);
            this.panelControl1.Controls.Add(this.labelControl_PostingDate);
            this.panelControl1.Controls.Add(this.labelControl_DocId);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1044, 54);
            this.panelControl1.TabIndex = 0;
            // 
            // dateEdit_PostingDate
            // 
            this.dateEdit_PostingDate.EditValue = null;
            this.dateEdit_PostingDate.Location = new System.Drawing.Point(379, 18);
            this.dateEdit_PostingDate.Name = "dateEdit_PostingDate";
            this.dateEdit_PostingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_PostingDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_PostingDate.Properties.ReadOnly = true;
            this.dateEdit_PostingDate.Size = new System.Drawing.Size(120, 20);
            this.dateEdit_PostingDate.TabIndex = 32;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(585, 18);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(120, 20);
            this.comboBoxEdit1.TabIndex = 8;
            this.comboBoxEdit1.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(551, 20);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "状态:";
            this.labelControl6.Visible = false;
            // 
            // textEdit_DocId
            // 
            this.textEdit_DocId.Location = new System.Drawing.Point(64, 18);
            this.textEdit_DocId.Name = "textEdit_DocId";
            this.textEdit_DocId.Properties.ReadOnly = true;
            this.textEdit_DocId.Size = new System.Drawing.Size(201, 20);
            this.textEdit_DocId.TabIndex = 5;
            // 
            // labelControl_PostingDate
            // 
            this.labelControl_PostingDate.Location = new System.Drawing.Point(321, 20);
            this.labelControl_PostingDate.Name = "labelControl_PostingDate";
            this.labelControl_PostingDate.Size = new System.Drawing.Size(52, 14);
            this.labelControl_PostingDate.TabIndex = 4;
            this.labelControl_PostingDate.Text = "过账日期:";
            // 
            // labelControl_DocId
            // 
            this.labelControl_DocId.Location = new System.Drawing.Point(6, 20);
            this.labelControl_DocId.Name = "labelControl_DocId";
            this.labelControl_DocId.Size = new System.Drawing.Size(52, 14);
            this.labelControl_DocId.TabIndex = 2;
            this.labelControl_DocId.Text = "交款单号:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 14);
            this.labelControl1.TabIndex = 0;
            // 
            // simpleButton_Save
            // 
            this.simpleButton_Save.Location = new System.Drawing.Point(15, 38);
            this.simpleButton_Save.Name = "simpleButton_Save";
            this.simpleButton_Save.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Save.TabIndex = 4;
            this.simpleButton_Save.Text = "保存";
            this.simpleButton_Save.Visible = false;
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.Location = new System.Drawing.Point(172, 31);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Cancel.TabIndex = 4;
            this.simpleButton_Cancel.Text = "取消(Esc)";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1050, 520);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.textEdit_MemoHeader);
            this.panelControl3.Controls.Add(this.simpleButton_Cancel);
            this.panelControl3.Controls.Add(this.panel1);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.simpleButton_PaymentIn);
            this.panelControl3.Controls.Add(this.simpleButton_PrePaymentIn);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 438);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1044, 79);
            this.panelControl3.TabIndex = 2;
            // 
            // textEdit_MemoHeader
            // 
            this.textEdit_MemoHeader.Location = new System.Drawing.Point(333, 31);
            this.textEdit_MemoHeader.Name = "textEdit_MemoHeader";
            this.textEdit_MemoHeader.Size = new System.Drawing.Size(404, 20);
            this.textEdit_MemoHeader.TabIndex = 59;
            this.textEdit_MemoHeader.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditMemoHeader_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton_Save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(940, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(102, 75);
            this.panel1.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(288, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "备注:";
            // 
            // simpleButton_PaymentIn
            // 
            this.simpleButton_PaymentIn.Location = new System.Drawing.Point(91, 31);
            this.simpleButton_PaymentIn.Name = "simpleButton_PaymentIn";
            this.simpleButton_PaymentIn.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_PaymentIn.TabIndex = 1;
            this.simpleButton_PaymentIn.Text = "日结";
            this.simpleButton_PaymentIn.Click += new System.EventHandler(this.simpleButton_PaymentIn_Click);
            // 
            // simpleButton_PrePaymentIn
            // 
            this.simpleButton_PrePaymentIn.Location = new System.Drawing.Point(10, 31);
            this.simpleButton_PrePaymentIn.Name = "simpleButton_PrePaymentIn";
            this.simpleButton_PrePaymentIn.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_PrePaymentIn.TabIndex = 0;
            this.simpleButton_PrePaymentIn.Text = "缴款";
            this.simpleButton_PrePaymentIn.Click += new System.EventHandler(this.simpleButton_PrePaymentIn_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl_PaymentIn);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 63);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1044, 369);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl_PaymentIn
            // 
            this.gridControl_PaymentIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_PaymentIn.Location = new System.Drawing.Point(2, 2);
            this.gridControl_PaymentIn.MainView = this.gridView__PaymentIn;
            this.gridControl_PaymentIn.Name = "gridControl_PaymentIn";
            this.gridControl_PaymentIn.Size = new System.Drawing.Size(1040, 365);
            this.gridControl_PaymentIn.TabIndex = 0;
            this.gridControl_PaymentIn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView__PaymentIn});
            // 
            // gridView__PaymentIn
            // 
            this.gridView__PaymentIn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_TypeName,
            this.gridColumn_TargetAmount,
            this.gridColumn_PreAmount,
            this.gridColumn_Amount,
            this.gridColumn_DiffAmount,
            this.gridColumn6_Memo,
            this.gridColumn_ERPCheckAmount,
            this.gridColumn_ERPMemo});
            this.gridView__PaymentIn.GridControl = this.gridControl_PaymentIn;
            this.gridView__PaymentIn.Name = "gridView__PaymentIn";
            this.gridView__PaymentIn.OptionsView.ShowGroupPanel = false;
            this.gridView__PaymentIn.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewPaymentIn_CellValueChanged);
            // 
            // gridColumn_TypeName
            // 
            this.gridColumn_TypeName.Caption = "收银类型";
            this.gridColumn_TypeName.FieldName = "typeName";
            this.gridColumn_TypeName.Name = "gridColumn_TypeName";
            this.gridColumn_TypeName.OptionsColumn.ReadOnly = true;
            this.gridColumn_TypeName.Visible = true;
            this.gridColumn_TypeName.VisibleIndex = 0;
            // 
            // gridColumn_TargetAmount
            // 
            this.gridColumn_TargetAmount.Caption = "当日应缴款金额";
            this.gridColumn_TargetAmount.FieldName = "targetAmount";
            this.gridColumn_TargetAmount.Name = "gridColumn_TargetAmount";
            this.gridColumn_TargetAmount.OptionsColumn.ReadOnly = true;
            this.gridColumn_TargetAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_TargetAmount.Visible = true;
            this.gridColumn_TargetAmount.VisibleIndex = 1;
            // 
            // gridColumn_PreAmount
            // 
            this.gridColumn_PreAmount.Caption = "预交款金额";
            this.gridColumn_PreAmount.FieldName = "preAmount";
            this.gridColumn_PreAmount.Name = "gridColumn_PreAmount";
            this.gridColumn_PreAmount.OptionsColumn.ReadOnly = true;
            this.gridColumn_PreAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_PreAmount.Visible = true;
            this.gridColumn_PreAmount.VisibleIndex = 2;
            // 
            // gridColumn_Amount
            // 
            this.gridColumn_Amount.Caption = "当日实缴款金额";
            this.gridColumn_Amount.FieldName = "amount";
            this.gridColumn_Amount.Name = "gridColumn_Amount";
            this.gridColumn_Amount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_Amount.Visible = true;
            this.gridColumn_Amount.VisibleIndex = 3;
            // 
            // gridColumn_DiffAmount
            // 
            this.gridColumn_DiffAmount.Caption = "差异金额";
            this.gridColumn_DiffAmount.FieldName = "diffAmount";
            this.gridColumn_DiffAmount.Name = "gridColumn_DiffAmount";
            this.gridColumn_DiffAmount.OptionsColumn.ReadOnly = true;
            this.gridColumn_DiffAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_DiffAmount.Visible = true;
            this.gridColumn_DiffAmount.VisibleIndex = 4;
            // 
            // gridColumn6_Memo
            // 
            this.gridColumn6_Memo.Caption = "备注";
            this.gridColumn6_Memo.FieldName = "memo";
            this.gridColumn6_Memo.Name = "gridColumn6_Memo";
            this.gridColumn6_Memo.Visible = true;
            this.gridColumn6_Memo.VisibleIndex = 5;
            // 
            // gridColumn_ERPCheckAmount
            // 
            this.gridColumn_ERPCheckAmount.Caption = "核对金额";
            this.gridColumn_ERPCheckAmount.FieldName = "erpCheckAmount";
            this.gridColumn_ERPCheckAmount.Name = "gridColumn_ERPCheckAmount";
            this.gridColumn_ERPCheckAmount.OptionsColumn.ReadOnly = true;
            this.gridColumn_ERPCheckAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_ERPCheckAmount.Visible = true;
            this.gridColumn_ERPCheckAmount.VisibleIndex = 6;
            // 
            // gridColumn_ERPMemo
            // 
            this.gridColumn_ERPMemo.Caption = "总部处理原因";
            this.gridColumn_ERPMemo.FieldName = "erpMemo";
            this.gridColumn_ERPMemo.Name = "gridColumn_ERPMemo";
            this.gridColumn_ERPMemo.OptionsColumn.ReadOnly = true;
            this.gridColumn_ERPMemo.Visible = true;
            this.gridColumn_ERPMemo.VisibleIndex = 7;
            // 
            // Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Payment";
            this.Text = "缴款";
            this.Load += new System.EventHandler(this.Payment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPreview_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PostingDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PostingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_DocId.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_MemoHeader.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PaymentIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView__PaymentIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit textEdit_DocId;
        private DevExpress.XtraEditors.LabelControl labelControl_PostingDate;
        private DevExpress.XtraEditors.LabelControl labelControl_DocId;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Save;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton_PaymentIn;
        private DevExpress.XtraEditors.SimpleButton simpleButton_PrePaymentIn;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl_PaymentIn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView__PaymentIn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TypeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TargetAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_PreAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Amount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_DiffAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6_Memo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ERPCheckAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ERPMemo;
        private DevExpress.XtraEditors.DateEdit dateEdit_PostingDate;
        private DevExpress.XtraEditors.TextEdit textEdit_MemoHeader;
    }
}