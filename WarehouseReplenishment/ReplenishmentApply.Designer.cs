namespace WarehouseReplenishment
{
    partial class ReplenishmentApply
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
            this.teOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcReplenishment = new DevExpress.XtraGrid.GridControl();
            this.gvReplenishment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.meMemo = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteRow = new DevExpress.XtraEditors.SimpleButton();
            this.pcHeader = new DevExpress.XtraEditors.PanelControl();
            this.deReceiveDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboDocStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cboSaleDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teDocDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnBarCode = new Commons.WinForm.SearchButton();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnProductName = new Commons.WinForm.SearchButton();
            this.btnProductId = new Commons.WinForm.SearchButton();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.teOrderNo.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReplenishment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReplenishment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcHeader)).BeginInit();
            this.pcHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSaleDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDocDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBarCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // teOrderNo
            // 
            this.teOrderNo.Location = new System.Drawing.Point(77, 12);
            this.teOrderNo.Name = "teOrderNo";
            this.teOrderNo.Properties.ReadOnly = true;
            this.teOrderNo.Size = new System.Drawing.Size(170, 20);
            this.teOrderNo.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 14);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "补货单号:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(491, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "状态:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pcHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(945, 476);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcReplenishment);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 143);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(939, 245);
            this.panelControl2.TabIndex = 3;
            // 
            // gcReplenishment
            // 
            this.gcReplenishment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcReplenishment.Location = new System.Drawing.Point(2, 2);
            this.gcReplenishment.MainView = this.gvReplenishment;
            this.gcReplenishment.Name = "gcReplenishment";
            this.gcReplenishment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcReplenishment.Size = new System.Drawing.Size(935, 241);
            this.gcReplenishment.TabIndex = 0;
            this.gcReplenishment.TabStop = false;
            this.gcReplenishment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReplenishment});
            // 
            // gvReplenishment
            // 
            this.gvReplenishment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn7,
            this.Quantity,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn5,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn8});
            this.gvReplenishment.CustomizationFormBounds = new System.Drawing.Rectangle(777, 374, 216, 187);
            this.gvReplenishment.GridControl = this.gcReplenishment;
            this.gvReplenishment.Name = "gvReplenishment";
            this.gvReplenishment.OptionsFind.AlwaysVisible = true;
            this.gvReplenishment.OptionsFind.ClearFindOnClose = false;
            this.gvReplenishment.OptionsFind.ShowCloseButton = false;
            this.gvReplenishment.OptionsView.ShowGroupPanel = false;
            this.gvReplenishment.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvReplenishment_PopupMenuShowing);
            this.gvReplenishment.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvReplenishment_CellValueChanged);
            this.gvReplenishment.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvReplenishment_CellValueChanging);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "商品代码";
            this.gridColumn1.FieldName = "productId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 73;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "商品名称";
            this.gridColumn2.FieldName = "productName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 73;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "待入库数";
            this.gridColumn3.FieldName = "receiveQuantity";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 73;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "安全库存";
            this.gridColumn6.FieldName = "saftyQuantity";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 73;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "可用库存";
            this.gridColumn7.FieldName = "availableQuantity";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 73;
            // 
            // Quantity
            // 
            this.Quantity.Caption = "配货数";
            this.Quantity.ColumnEdit = this.repositoryItemTextEdit1;
            this.Quantity.FieldName = "quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 5;
            this.Quantity.Width = 79;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "d";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.MaxLength = 8;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "预定数";
            this.gridColumn9.FieldName = "preQuantity";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 73;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "原因";
            this.gridColumn10.FieldName = "memo";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 81;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "库存数";
            this.gridColumn5.FieldName = "onhand";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            this.gridColumn5.Width = 73;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "占用数";
            this.gridColumn12.FieldName = "promise";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 9;
            this.gridColumn12.Width = 73;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "行号";
            this.gridColumn13.FieldName = "lineNo";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 33;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "erp备注";
            this.gridColumn8.FieldName = "erpMemo";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 11;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panel1);
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Controls.Add(this.btnDeleteRow);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 394);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(939, 79);
            this.panelControl3.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.meMemo);
            this.panel1.Controls.Add(this.labelControl8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(644, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 75);
            this.panel1.TabIndex = 0;
            // 
            // meMemo
            // 
            this.meMemo.Location = new System.Drawing.Point(53, 5);
            this.meMemo.Name = "meMemo";
            this.meMemo.Size = new System.Drawing.Size(233, 64);
            this.meMemo.TabIndex = 0;
            this.meMemo.TabStop = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(7, 32);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(28, 14);
            this.labelControl8.TabIndex = 17;
            this.labelControl8.Text = "备注:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(94, 45);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 45);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(11, 9);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 0;
            this.btnDeleteRow.TabStop = false;
            this.btnDeleteRow.Text = "删除行数据";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // pcHeader
            // 
            this.pcHeader.Controls.Add(this.deReceiveDate);
            this.pcHeader.Controls.Add(this.labelControl1);
            this.pcHeader.Controls.Add(this.cboDocStatus);
            this.pcHeader.Controls.Add(this.teOrderNo);
            this.pcHeader.Controls.Add(this.labelControl6);
            this.pcHeader.Controls.Add(this.labelControl5);
            this.pcHeader.Controls.Add(this.cboSaleDay);
            this.pcHeader.Controls.Add(this.labelControl4);
            this.pcHeader.Controls.Add(this.teDocDate);
            this.pcHeader.Controls.Add(this.labelControl3);
            this.pcHeader.Controls.Add(this.teUserName);
            this.pcHeader.Controls.Add(this.labelControl2);
            this.pcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcHeader.Location = new System.Drawing.Point(3, 3);
            this.pcHeader.Name = "pcHeader";
            this.pcHeader.Size = new System.Drawing.Size(939, 74);
            this.pcHeader.TabIndex = 0;
            // 
            // deReceiveDate
            // 
            this.deReceiveDate.EditValue = null;
            this.deReceiveDate.Location = new System.Drawing.Point(317, 43);
            this.deReceiveDate.Name = "deReceiveDate";
            this.deReceiveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deReceiveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deReceiveDate.Size = new System.Drawing.Size(170, 20);
            this.deReceiveDate.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(253, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "到货日期:";
            // 
            // cboDocStatus
            // 
            this.cboDocStatus.Enabled = false;
            this.cboDocStatus.Location = new System.Drawing.Point(547, 43);
            this.cboDocStatus.Name = "cboDocStatus";
            this.cboDocStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDocStatus.Size = new System.Drawing.Size(170, 20);
            this.cboDocStatus.TabIndex = 12;
            this.cboDocStatus.TabStop = false;
            // 
            // cboSaleDay
            // 
            this.cboSaleDay.Location = new System.Drawing.Point(77, 43);
            this.cboSaleDay.Name = "cboSaleDay";
            this.cboSaleDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSaleDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboSaleDay.Size = new System.Drawing.Size(170, 20);
            this.cboSaleDay.TabIndex = 0;
            this.cboSaleDay.SelectedValueChanged += new System.EventHandler(this.cboSaleDay_SelectedValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 46);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "销售天数:";
            // 
            // teDocDate
            // 
            this.teDocDate.Enabled = false;
            this.teDocDate.Location = new System.Drawing.Point(317, 11);
            this.teDocDate.Name = "teDocDate";
            this.teDocDate.Size = new System.Drawing.Size(170, 20);
            this.teDocDate.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(253, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "制单时间:";
            // 
            // teUserName
            // 
            this.teUserName.Enabled = false;
            this.teUserName.Location = new System.Drawing.Point(547, 12);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(170, 20);
            this.teUserName.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(491, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "制单人:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnBarCode);
            this.panelControl1.Controls.Add(this.labelControl11);
            this.panelControl1.Controls.Add(this.btnProductName);
            this.panelControl1.Controls.Add(this.btnProductId);
            this.panelControl1.Controls.Add(this.labelControl13);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 83);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(939, 54);
            this.panelControl1.TabIndex = 4;
            // 
            // btnBarCode
            // 
            this.btnBarCode.EnterEnent = true;
            this.btnBarCode.FieldColumn = "GI.Id_Value";
            this.btnBarCode.FieldColumnOtherName = "idValue";
            this.btnBarCode.Location = new System.Drawing.Point(547, 17);
            this.btnBarCode.Name = "btnBarCode";
            this.btnBarCode.PrimaryKey = "barCode";
            this.btnBarCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnBarCode.RtnEvent = true;
            this.btnBarCode.Size = new System.Drawing.Size(170, 20);
            this.btnBarCode.TabIndex = 4;
            this.btnBarCode.Value = null;
            this.btnBarCode.ValueFieldColumn = "idValue";
            this.btnBarCode.where = null;
            this.btnBarCode.XMLConditionModel = "Condition";
            this.btnBarCode.XMLConditionPath = "XML\\BarCodeXML\\";
            this.btnBarCode.XMLWhereAndColumnModel = "SearchAndResult";
            this.btnBarCode.XMLWhereAndColumnPath = "XML\\BarCodeXML\\";
            this.btnBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPressEvent);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(491, 20);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(40, 14);
            this.labelControl11.TabIndex = 1015;
            this.labelControl11.Text = "条形码:";
            // 
            // btnProductName
            // 
            this.btnProductName.EnterEnent = true;
            this.btnProductName.FieldColumn = "P.PRODUCT_NAME";
            this.btnProductName.FieldColumnOtherName = "productName";
            this.btnProductName.Location = new System.Drawing.Point(317, 17);
            this.btnProductName.Name = "btnProductName";
            this.btnProductName.PrimaryKey = "productName";
            this.btnProductName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnProductName.RtnEvent = true;
            this.btnProductName.Size = new System.Drawing.Size(170, 20);
            this.btnProductName.TabIndex = 3;
            this.btnProductName.Value = null;
            this.btnProductName.ValueFieldColumn = "productId";
            this.btnProductName.where = null;
            this.btnProductName.XMLConditionModel = "Condition";
            this.btnProductName.XMLConditionPath = "XML\\ProductNameXML\\";
            this.btnProductName.XMLWhereAndColumnModel = "SearchAndResult";
            this.btnProductName.XMLWhereAndColumnPath = "XML\\ProductNameXML\\";
            this.btnProductName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPressEvent);
            // 
            // btnProductId
            // 
            this.btnProductId.EditValue = "";
            this.btnProductId.EnterEnent = true;
            this.btnProductId.FieldColumn = "P.PRODUCT_ID";
            this.btnProductId.FieldColumnOtherName = "productId";
            this.btnProductId.Location = new System.Drawing.Point(77, 17);
            this.btnProductId.Name = "btnProductId";
            this.btnProductId.PrimaryKey = "productId";
            this.btnProductId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnProductId.RtnEvent = true;
            this.btnProductId.Size = new System.Drawing.Size(170, 20);
            this.btnProductId.TabIndex = 2;
            this.btnProductId.Value = null;
            this.btnProductId.ValueFieldColumn = "productId";
            this.btnProductId.where = null;
            this.btnProductId.XMLConditionModel = "Condition";
            this.btnProductId.XMLConditionPath = "XML\\ProductIdXML\\";
            this.btnProductId.XMLWhereAndColumnModel = "SearchAndResult";
            this.btnProductId.XMLWhereAndColumnPath = "XML\\ProductIdXML\\";
            this.btnProductId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPressEvent);
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(253, 20);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(52, 14);
            this.labelControl13.TabIndex = 1013;
            this.labelControl13.Text = "商品名称:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 20);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 14);
            this.labelControl7.TabIndex = 1012;
            this.labelControl7.Text = "商品代码:";
            // 
            // ReplenishmentApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 476);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReplenishmentApply";
            this.Text = "补货申请";
            this.Load += new System.EventHandler(this.ReplenishmentApply_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teOrderNo.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReplenishment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReplenishment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcHeader)).EndInit();
            this.pcHeader.ResumeLayout(false);
            this.pcHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSaleDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDocDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBarCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProductId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit teOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.MemoEdit meMemo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDeleteRow;
        private DevExpress.XtraEditors.PanelControl pcHeader;
        private DevExpress.XtraEditors.ComboBoxEdit cboSaleDay;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teDocDate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit teUserName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gcReplenishment;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReplenishment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboDocStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deReceiveDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Commons.WinForm.SearchButton btnProductName;
        private Commons.WinForm.SearchButton btnProductId;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Commons.WinForm.SearchButton btnBarCode;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}