namespace WarehouseOut
{
    partial class WarehouseOutOrder
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
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWhsOut = new DevExpress.XtraGrid.GridControl();
            this.gvWhsOut = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.columnDocId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnDocStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetail = new DevExpress.XtraEditors.SimpleButton();
            this.btnwhsOut = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pcCondition = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.b = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teCommandNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.deStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcWhsOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWhsOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcCondition)).BeginInit();
            this.pcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.b.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCommandNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "制单人";
            this.gridColumn4.FieldName = "updateUserName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gcWhsOut
            // 
            this.gcWhsOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWhsOut.Location = new System.Drawing.Point(2, 2);
            this.gcWhsOut.MainView = this.gvWhsOut;
            this.gcWhsOut.Name = "gcWhsOut";
            this.gcWhsOut.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemImageComboBox1});
            this.gcWhsOut.Size = new System.Drawing.Size(1011, 349);
            this.gcWhsOut.TabIndex = 0;
            this.gcWhsOut.TabStop = false;
            this.gcWhsOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWhsOut});
            // 
            // gvWhsOut
            // 
            this.gvWhsOut.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.columnDocId,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.columnDocStatus});
            this.gvWhsOut.CustomizationFormBounds = new System.Drawing.Rectangle(853, 398, 216, 187);
            this.gvWhsOut.GridControl = this.gcWhsOut;
            this.gvWhsOut.Name = "gvWhsOut";
            this.gvWhsOut.OptionsView.ShowGroupPanel = false;
            // 
            // columnDocId
            // 
            this.columnDocId.Caption = "指令单号";
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
            // gridColumn3
            // 
            this.gridColumn3.Caption = "制单时间";
            this.gridColumn3.FieldName = "updateDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "备注";
            this.gridColumn5.FieldName = "memo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // columnDocStatus
            // 
            this.columnDocStatus.Caption = "状态";
            this.columnDocStatus.ColumnEdit = this.repositoryItemImageComboBox1;
            this.columnDocStatus.FieldName = "status";
            this.columnDocStatus.Name = "columnDocStatus";
            this.columnDocStatus.Visible = true;
            this.columnDocStatus.VisibleIndex = 3;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pcCondition, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1021, 479);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.btnDetail);
            this.panelControl3.Controls.Add(this.btnwhsOut);
            this.panelControl3.Controls.Add(this.sbtnCancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 422);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1015, 54);
            this.panelControl3.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(182, 16);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "强制已清";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(101, 16);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(75, 23);
            this.btnDetail.TabIndex = 6;
            this.btnDetail.Text = "明细";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnwhsOut
            // 
            this.btnwhsOut.Location = new System.Drawing.Point(20, 16);
            this.btnwhsOut.Name = "btnwhsOut";
            this.btnwhsOut.Size = new System.Drawing.Size(75, 23);
            this.btnwhsOut.TabIndex = 5;
            this.btnwhsOut.Text = "出库";
            this.btnwhsOut.Click += new System.EventHandler(this.btnwhsOut_Click);
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(263, 16);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.sbtnCancel.TabIndex = 8;
            this.sbtnCancel.Text = "取消";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcWhsOut);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 63);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1015, 353);
            this.panelControl2.TabIndex = 1;
            // 
            // pcCondition
            // 
            this.pcCondition.Controls.Add(this.btnSearch);
            this.pcCondition.Controls.Add(this.b);
            this.pcCondition.Controls.Add(this.labelControl4);
            this.pcCondition.Controls.Add(this.teCommandNo);
            this.pcCondition.Controls.Add(this.labelControl3);
            this.pcCondition.Controls.Add(this.deEndDate);
            this.pcCondition.Controls.Add(this.labelControl2);
            this.pcCondition.Controls.Add(this.deStartDate);
            this.pcCondition.Controls.Add(this.labelControl1);
            this.pcCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcCondition.Location = new System.Drawing.Point(3, 3);
            this.pcCondition.Name = "pcCondition";
            this.pcCondition.Size = new System.Drawing.Size(1015, 54);
            this.pcCondition.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(912, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // b
            // 
            this.b.Location = new System.Drawing.Point(710, 19);
            this.b.Name = "b";
            this.b.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.b.Size = new System.Drawing.Size(150, 20);
            this.b.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(668, 22);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 14);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "状态:";
            // 
            // teCommandNo
            // 
            this.teCommandNo.Location = new System.Drawing.Point(512, 19);
            this.teCommandNo.Name = "teCommandNo";
            this.teCommandNo.Size = new System.Drawing.Size(150, 20);
            this.teCommandNo.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(446, 22);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "指令单号:";
            // 
            // deEndDate
            // 
            this.deEndDate.EditValue = null;
            this.deEndDate.Location = new System.Drawing.Point(292, 19);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndDate.Size = new System.Drawing.Size(150, 20);
            this.deEndDate.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(234, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "结束时间:";
            // 
            // deStartDate
            // 
            this.deStartDate.EditValue = null;
            this.deStartDate.Location = new System.Drawing.Point(80, 19);
            this.deStartDate.Name = "deStartDate";
            this.deStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deStartDate.Size = new System.Drawing.Size(150, 20);
            this.deStartDate.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "开始时间:";
            // 
            // WarehouseOutOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 479);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WarehouseOutOrder";
            this.Text = "出库指令单";
            this.Load += new System.EventHandler(this.WarehouseOutOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcWhsOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWhsOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcCondition)).EndInit();
            this.pcCondition.ResumeLayout(false);
            this.pcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.b.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCommandNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gcWhsOut;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWhsOut;
        private DevExpress.XtraGrid.Columns.GridColumn columnDocId;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton sbtnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl pcCondition;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit deStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.ImageComboBoxEdit b;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teCommandNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn columnDocStatus;
        private DevExpress.XtraEditors.SimpleButton btnDetail;
        private DevExpress.XtraEditors.SimpleButton btnwhsOut;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}