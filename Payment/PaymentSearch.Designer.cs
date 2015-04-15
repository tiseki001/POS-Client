namespace Payment
{
    partial class PaymentSearch
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
            this.baseCombobox_DocStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_Query = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_OrderId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_EndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_StartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridColumn_DocStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox_Docstatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn_Memo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_PostingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton_Detail = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn_DocType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox_DocType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl_PaymentInOrder = new DevExpress.XtraGrid.GridControl();
            this.gridView_PaymentInOrder = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_DocId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CreateDocDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CreateUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseCombobox_DocStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_OrderId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_EndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_EndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_StartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_StartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_DocType)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PaymentInOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PaymentInOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.baseCombobox_DocStatus);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.simpleButton_Query);
            this.panelControl1.Controls.Add(this.textEdit_OrderId);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.dateEdit_EndDate);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dateEdit_StartDate);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1045, 54);
            this.panelControl1.TabIndex = 0;
            // 
            // baseCombobox_DocStatus
            // 
            this.baseCombobox_DocStatus.Location = new System.Drawing.Point(670, 18);
            this.baseCombobox_DocStatus.Name = "baseCombobox_DocStatus";
            this.baseCombobox_DocStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.baseCombobox_DocStatus.Size = new System.Drawing.Size(120, 20);
            this.baseCombobox_DocStatus.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(636, 21);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 14);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "状态:";
            // 
            // simpleButton_Query
            // 
            this.simpleButton_Query.Location = new System.Drawing.Point(834, 15);
            this.simpleButton_Query.Name = "simpleButton_Query";
            this.simpleButton_Query.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Query.TabIndex = 6;
            this.simpleButton_Query.Text = "查询";
            this.simpleButton_Query.Click += new System.EventHandler(this.simpleButton_Query_Click);
            // 
            // textEdit_OrderId
            // 
            this.textEdit_OrderId.Location = new System.Drawing.Point(451, 18);
            this.textEdit_OrderId.Name = "textEdit_OrderId";
            this.textEdit_OrderId.Size = new System.Drawing.Size(171, 20);
            this.textEdit_OrderId.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(393, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "缴款单号:";
            // 
            // dateEdit_EndDate
            // 
            this.dateEdit_EndDate.EditValue = null;
            this.dateEdit_EndDate.Location = new System.Drawing.Point(259, 18);
            this.dateEdit_EndDate.Name = "dateEdit_EndDate";
            this.dateEdit_EndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_EndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_EndDate.Size = new System.Drawing.Size(120, 20);
            this.dateEdit_EndDate.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(201, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "结束时间:";
            // 
            // dateEdit_StartDate
            // 
            this.dateEdit_StartDate.EditValue = null;
            this.dateEdit_StartDate.Location = new System.Drawing.Point(68, 18);
            this.dateEdit_StartDate.Name = "dateEdit_StartDate";
            this.dateEdit_StartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_StartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_StartDate.Size = new System.Drawing.Size(120, 20);
            this.dateEdit_StartDate.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "开始时间:";
            // 
            // gridColumn_DocStatus
            // 
            this.gridColumn_DocStatus.Caption = "状态";
            this.gridColumn_DocStatus.ColumnEdit = this.repositoryItemImageComboBox_Docstatus;
            this.gridColumn_DocStatus.FieldName = "docStatus";
            this.gridColumn_DocStatus.Name = "gridColumn_DocStatus";
            this.gridColumn_DocStatus.OptionsColumn.AllowEdit = false;
            this.gridColumn_DocStatus.Visible = true;
            this.gridColumn_DocStatus.VisibleIndex = 5;
            // 
            // repositoryItemImageComboBox_Docstatus
            // 
            this.repositoryItemImageComboBox_Docstatus.AutoHeight = false;
            this.repositoryItemImageComboBox_Docstatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox_Docstatus.Name = "repositoryItemImageComboBox_Docstatus";
            // 
            // gridColumn_Memo
            // 
            this.gridColumn_Memo.Caption = "备注";
            this.gridColumn_Memo.FieldName = "memo";
            this.gridColumn_Memo.Name = "gridColumn_Memo";
            this.gridColumn_Memo.OptionsColumn.AllowEdit = false;
            this.gridColumn_Memo.Visible = true;
            this.gridColumn_Memo.VisibleIndex = 7;
            // 
            // gridColumn_PostingDate
            // 
            this.gridColumn_PostingDate.Caption = "过账日期";
            this.gridColumn_PostingDate.FieldName = "postingDate";
            this.gridColumn_PostingDate.Name = "gridColumn_PostingDate";
            this.gridColumn_PostingDate.OptionsColumn.AllowEdit = false;
            this.gridColumn_PostingDate.Visible = true;
            this.gridColumn_PostingDate.VisibleIndex = 4;
            // 
            // gridColumn_Amount
            // 
            this.gridColumn_Amount.Caption = "缴款金额";
            this.gridColumn_Amount.FieldName = "amount";
            this.gridColumn_Amount.Name = "gridColumn_Amount";
            this.gridColumn_Amount.OptionsColumn.AllowEdit = false;
            this.gridColumn_Amount.Visible = true;
            this.gridColumn_Amount.VisibleIndex = 3;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.simpleButton_Detail);
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 464);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1045, 54);
            this.panelControl3.TabIndex = 2;
            // 
            // simpleButton_Detail
            // 
            this.simpleButton_Detail.Location = new System.Drawing.Point(10, 17);
            this.simpleButton_Detail.Name = "simpleButton_Detail";
            this.simpleButton_Detail.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Detail.TabIndex = 9;
            this.simpleButton_Detail.Text = "明细";
            this.simpleButton_Detail.Click += new System.EventHandler(this.simpleButton_Detail_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(91, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gridColumn_DocType
            // 
            this.gridColumn_DocType.Caption = "单据类型";
            this.gridColumn_DocType.ColumnEdit = this.repositoryItemImageComboBox_DocType;
            this.gridColumn_DocType.FieldName = "docType";
            this.gridColumn_DocType.Name = "gridColumn_DocType";
            this.gridColumn_DocType.OptionsColumn.ReadOnly = true;
            this.gridColumn_DocType.Visible = true;
            this.gridColumn_DocType.VisibleIndex = 6;
            // 
            // repositoryItemImageComboBox_DocType
            // 
            this.repositoryItemImageComboBox_DocType.AutoHeight = false;
            this.repositoryItemImageComboBox_DocType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox_DocType.Name = "repositoryItemImageComboBox_DocType";
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1051, 521);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl_PaymentInOrder);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 63);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1045, 395);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl_PaymentInOrder
            // 
            this.gridControl_PaymentInOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_PaymentInOrder.Location = new System.Drawing.Point(2, 2);
            this.gridControl_PaymentInOrder.MainView = this.gridView_PaymentInOrder;
            this.gridControl_PaymentInOrder.Name = "gridControl_PaymentInOrder";
            this.gridControl_PaymentInOrder.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox_Docstatus,
            this.repositoryItemImageComboBox_DocType});
            this.gridControl_PaymentInOrder.Size = new System.Drawing.Size(1041, 391);
            this.gridControl_PaymentInOrder.TabIndex = 0;
            this.gridControl_PaymentInOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_PaymentInOrder});
            // 
            // gridView_PaymentInOrder
            // 
            this.gridView_PaymentInOrder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_DocId,
            this.gridColumn_CreateDocDate,
            this.gridColumn_CreateUserName,
            this.gridColumn_DocType,
            this.gridColumn_Amount,
            this.gridColumn_PostingDate,
            this.gridColumn_Memo,
            this.gridColumn_DocStatus});
            this.gridView_PaymentInOrder.GridControl = this.gridControl_PaymentInOrder;
            this.gridView_PaymentInOrder.Name = "gridView_PaymentInOrder";
            this.gridView_PaymentInOrder.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn_DocId
            // 
            this.gridColumn_DocId.Caption = "缴款单号";
            this.gridColumn_DocId.FieldName = "docId";
            this.gridColumn_DocId.Name = "gridColumn_DocId";
            this.gridColumn_DocId.OptionsColumn.AllowEdit = false;
            this.gridColumn_DocId.Visible = true;
            this.gridColumn_DocId.VisibleIndex = 0;
            // 
            // gridColumn_CreateDocDate
            // 
            this.gridColumn_CreateDocDate.Caption = "缴款时间";
            this.gridColumn_CreateDocDate.FieldName = "createDocDate";
            this.gridColumn_CreateDocDate.Name = "gridColumn_CreateDocDate";
            this.gridColumn_CreateDocDate.OptionsColumn.AllowEdit = false;
            this.gridColumn_CreateDocDate.Visible = true;
            this.gridColumn_CreateDocDate.VisibleIndex = 1;
            // 
            // gridColumn_CreateUserName
            // 
            this.gridColumn_CreateUserName.Caption = "缴款人员";
            this.gridColumn_CreateUserName.FieldName = "createUserName";
            this.gridColumn_CreateUserName.Name = "gridColumn_CreateUserName";
            this.gridColumn_CreateUserName.OptionsColumn.AllowEdit = false;
            this.gridColumn_CreateUserName.Visible = true;
            this.gridColumn_CreateUserName.VisibleIndex = 2;
            // 
            // PaymentSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 521);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PaymentSearch";
            this.Text = "缴款查询";
            this.Load += new System.EventHandler(this.PaymentSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseCombobox_DocStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_OrderId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_EndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_EndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_StartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_StartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_DocType)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PaymentInOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PaymentInOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit baseCombobox_DocStatus;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Query;
        private DevExpress.XtraEditors.TextEdit textEdit_OrderId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateEdit_EndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEdit_StartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_DocStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Memo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_PostingDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Amount;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_DocType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl_PaymentInOrder;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_PaymentInOrder;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_DocId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CreateDocDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CreateUserName;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Detail;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_Docstatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_DocType;
    }
}