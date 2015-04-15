namespace GoodsReceipt
{
    partial class ReveiveSearch
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.gcReceive = new DevExpress.XtraGrid.GridControl();
            this.gvReceiveSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.columnDocId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnCommand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnDocStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnReceiveStore = new Commons.WinForm.SearchButton();
            this.cboStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.teReceiveNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teOrderNO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.deStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearchCommand = new DevExpress.XtraEditors.SimpleButton();
            this.btnReceiveDetail = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteDraft = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiveSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnReceiveStore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teReceiveNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teOrderNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(449, 15);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "状态:";
            // 
            // gcReceive
            // 
            this.gcReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcReceive.Location = new System.Drawing.Point(2, 2);
            this.gcReceive.MainView = this.gvReceiveSearch;
            this.gcReceive.Name = "gcReceive";
            this.gcReceive.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2,
            this.repositoryItemImageComboBox1});
            this.gcReceive.Size = new System.Drawing.Size(874, 326);
            this.gcReceive.TabIndex = 0;
            this.gcReceive.TabStop = false;
            this.gcReceive.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReceiveSearch});
            // 
            // gvReceiveSearch
            // 
            this.gvReceiveSearch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.columnDocId,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6,
            this.columnCommand,
            this.gridColumn8,
            this.columnDocStatus});
            this.gvReceiveSearch.GridControl = this.gcReceive;
            this.gvReceiveSearch.Name = "gvReceiveSearch";
            this.gvReceiveSearch.OptionsView.ShowFooter = true;
            this.gvReceiveSearch.OptionsView.ShowGroupPanel = false;
            this.gvReceiveSearch.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gvReceiveSearch_PopupMenuShowing);
            // 
            // columnDocId
            // 
            this.columnDocId.Caption = "收货单号";
            this.columnDocId.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.columnDocId.FieldName = "docId";
            this.columnDocId.Name = "columnDocId";
            this.columnDocId.OptionsColumn.ReadOnly = true;
            this.columnDocId.Visible = true;
            this.columnDocId.VisibleIndex = 0;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "制单时间";
            this.gridColumn2.FieldName = "docDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "发货门店";
            this.gridColumn5.FieldName = "storeNameFrom";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "制单人";
            this.gridColumn6.FieldName = "userName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // columnCommand
            // 
            this.columnCommand.Caption = "指令单号";
            this.columnCommand.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.columnCommand.FieldName = "baseEntry";
            this.columnCommand.Name = "columnCommand";
            this.columnCommand.OptionsColumn.ReadOnly = true;
            this.columnCommand.Visible = true;
            this.columnCommand.VisibleIndex = 4;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            this.repositoryItemHyperLinkEdit2.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit2_Click);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "备注";
            this.gridColumn8.FieldName = "memo";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            // 
            // columnDocStatus
            // 
            this.columnDocStatus.Caption = "状态";
            this.columnDocStatus.ColumnEdit = this.repositoryItemImageComboBox1;
            this.columnDocStatus.FieldName = "docStatus";
            this.columnDocStatus.Name = "columnDocStatus";
            this.columnDocStatus.OptionsColumn.AllowEdit = false;
            this.columnDocStatus.Visible = true;
            this.columnDocStatus.VisibleIndex = 5;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(711, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnReceiveStore);
            this.panelControl1.Controls.Add(this.cboStatus);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.teReceiveNo);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.teOrderNO);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.deEndDate);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.deStartDate);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(878, 74);
            this.panelControl1.TabIndex = 0;
            // 
            // btnReceiveStore
            // 
            this.btnReceiveStore.EnterEnent = false;
            this.btnReceiveStore.FieldColumn = "PS.PRODUCT_STORE_ID";
            this.btnReceiveStore.FieldColumnOtherName = "storeName";
            this.btnReceiveStore.Location = new System.Drawing.Point(295, 42);
            this.btnReceiveStore.Name = "btnReceiveStore";
            this.btnReceiveStore.PrimaryKey = "ReceiveStore";
            this.btnReceiveStore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnReceiveStore.Size = new System.Drawing.Size(150, 20);
            this.btnReceiveStore.TabIndex = 1015;
            this.btnReceiveStore.Value = null;
            this.btnReceiveStore.ValueFieldColumn = "productStoreId";
            this.btnReceiveStore.XMLConditionModel = "Condition";
            this.btnReceiveStore.XMLConditionPath = "XML\\ReceiveStoreXML\\";
            this.btnReceiveStore.XMLWhereAndColumnModel = "SearchAndResult";
            this.btnReceiveStore.XMLWhereAndColumnPath = "XML\\ReceiveStoreXML\\";
            this.btnReceiveStore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnReceiveStore_KeyPress);
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(514, 12);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Size = new System.Drawing.Size(150, 20);
            this.cboStatus.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(229, 45);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "发货门店:";
            // 
            // teReceiveNo
            // 
            this.teReceiveNo.Location = new System.Drawing.Point(74, 42);
            this.teReceiveNo.Name = "teReceiveNo";
            this.teReceiveNo.Size = new System.Drawing.Size(150, 20);
            this.teReceiveNo.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "收货单号:";
            // 
            // teOrderNO
            // 
            this.teOrderNO.Location = new System.Drawing.Point(514, 42);
            this.teOrderNO.Name = "teOrderNO";
            this.teOrderNO.Size = new System.Drawing.Size(150, 20);
            this.teOrderNO.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(449, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "指令单号:";
            // 
            // deEndDate
            // 
            this.deEndDate.EditValue = null;
            this.deEndDate.Location = new System.Drawing.Point(295, 12);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndDate.Size = new System.Drawing.Size(150, 20);
            this.deEndDate.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(229, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "结束时间:";
            // 
            // deStartDate
            // 
            this.deStartDate.EditValue = null;
            this.deStartDate.Location = new System.Drawing.Point(74, 12);
            this.deStartDate.Name = "deStartDate";
            this.deStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deStartDate.Size = new System.Drawing.Size(150, 20);
            this.deStartDate.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "开始时间:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcReceive);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 83);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(878, 330);
            this.panelControl2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(253, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 476);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnSearchCommand);
            this.panelControl3.Controls.Add(this.btnReceiveDetail);
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Controls.Add(this.btnDeleteDraft);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 419);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(878, 54);
            this.panelControl3.TabIndex = 2;
            // 
            // btnSearchCommand
            // 
            this.btnSearchCommand.Location = new System.Drawing.Point(91, 16);
            this.btnSearchCommand.Name = "btnSearchCommand";
            this.btnSearchCommand.Size = new System.Drawing.Size(75, 23);
            this.btnSearchCommand.TabIndex = 8;
            this.btnSearchCommand.Text = "指令查询";
            this.btnSearchCommand.Click += new System.EventHandler(this.btnSearchCommand_Click);
            // 
            // btnReceiveDetail
            // 
            this.btnReceiveDetail.Location = new System.Drawing.Point(10, 16);
            this.btnReceiveDetail.Name = "btnReceiveDetail";
            this.btnReceiveDetail.Size = new System.Drawing.Size(75, 23);
            this.btnReceiveDetail.TabIndex = 7;
            this.btnReceiveDetail.Text = "收货明细";
            this.btnReceiveDetail.Click += new System.EventHandler(this.btnReceiveDetail_Click);
            // 
            // btnDeleteDraft
            // 
            this.btnDeleteDraft.Location = new System.Drawing.Point(172, 16);
            this.btnDeleteDraft.Name = "btnDeleteDraft";
            this.btnDeleteDraft.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDraft.TabIndex = 9;
            this.btnDeleteDraft.Text = "删除草稿";
            this.btnDeleteDraft.Click += new System.EventHandler(this.btnDeleteDraft_Click);
            // 
            // ReveiveSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 476);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReveiveSearch";
            this.Text = "收货单查询";
            this.Load += new System.EventHandler(this.ReveiveSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiveSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnReceiveStore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teReceiveNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teOrderNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.GridControl gcReceive;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReceiveSearch;
        private DevExpress.XtraGrid.Columns.GridColumn columnDocId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn columnCommand;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn columnDocStatus;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit teReceiveNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teOrderNO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit deStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnDeleteDraft;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboStatus;
        private DevExpress.XtraEditors.SimpleButton btnReceiveDetail;
        private DevExpress.XtraEditors.SimpleButton btnSearchCommand;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private Commons.WinForm.SearchButton btnReceiveStore;
    }
}