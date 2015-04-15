namespace Commons.WinForm
{
    partial class SearchForm
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pcResult = new DevExpress.XtraEditors.PanelControl();
            this.gcResult = new DevExpress.XtraGrid.GridControl();
            this.gvResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pcSearchCondition = new DevExpress.XtraEditors.PanelControl();
            this.deDate = new DevExpress.XtraEditors.DateEdit();
            this.teText = new DevExpress.XtraEditors.TextEdit();
            this.baseCbo = new Commons.WinForm.BaseCombobox();
            this.sbtnSerach = new DevExpress.XtraEditors.SimpleButton();
            this.cboOperator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.imgcboSelectCondition = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pcWhere = new DevExpress.XtraEditors.PanelControl();
            this.gcWhere = new DevExpress.XtraGrid.GridControl();
            this.gvWhere = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcResult)).BeginInit();
            this.pcResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSearchCondition)).BeginInit();
            this.pcSearchCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseCbo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcboSelectCondition.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcWhere)).BeginInit();
            this.pcWhere.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWhere)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWhere)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // pcResult
            // 
            this.pcResult.Controls.Add(this.gcResult);
            this.pcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcResult.Location = new System.Drawing.Point(3, 201);
            this.pcResult.Name = "pcResult";
            this.pcResult.Size = new System.Drawing.Size(661, 250);
            this.pcResult.TabIndex = 2;
            // 
            // gcResult
            // 
            this.gcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcResult.Location = new System.Drawing.Point(2, 2);
            this.gcResult.MainView = this.gvResult;
            this.gcResult.Name = "gcResult";
            this.gcResult.Size = new System.Drawing.Size(657, 246);
            this.gcResult.TabIndex = 0;
            this.gcResult.TabStop = false;
            this.gcResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvResult});
            // 
            // gvResult
            // 
            this.gvResult.GridControl = this.gcResult;
            this.gvResult.Name = "gvResult";
            this.gvResult.OptionsView.ShowGroupPanel = false;
            this.gvResult.DoubleClick += new System.EventHandler(this.gvResult_DoubleClick);
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // pcSearchCondition
            // 
            this.pcSearchCondition.Controls.Add(this.deDate);
            this.pcSearchCondition.Controls.Add(this.teText);
            this.pcSearchCondition.Controls.Add(this.baseCbo);
            this.pcSearchCondition.Controls.Add(this.sbtnSerach);
            this.pcSearchCondition.Controls.Add(this.cboOperator);
            this.pcSearchCondition.Controls.Add(this.imgcboSelectCondition);
            this.pcSearchCondition.Controls.Add(this.lblCondition);
            this.pcSearchCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcSearchCondition.Location = new System.Drawing.Point(3, 3);
            this.pcSearchCondition.Name = "pcSearchCondition";
            this.pcSearchCondition.Size = new System.Drawing.Size(661, 54);
            this.pcSearchCondition.TabIndex = 0;
            // 
            // deDate
            // 
            this.deDate.EditValue = null;
            this.deDate.Location = new System.Drawing.Point(281, 18);
            this.deDate.Name = "deDate";
            this.deDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deDate.Size = new System.Drawing.Size(150, 20);
            this.deDate.TabIndex = 2;
            this.deDate.Visible = false;
            this.deDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPress);
            // 
            // teText
            // 
            this.teText.Location = new System.Drawing.Point(281, 18);
            this.teText.Name = "teText";
            this.teText.Size = new System.Drawing.Size(150, 20);
            this.teText.TabIndex = 2;
            this.teText.Visible = false;
            this.teText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPress);
            // 
            // baseCbo
            // 
            this.baseCbo.FilterExpression = null;
            this.baseCbo.Location = new System.Drawing.Point(281, 18);
            this.baseCbo.Model = null;
            this.baseCbo.ModelName = null;
            this.baseCbo.Name = "baseCbo";
            this.baseCbo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.baseCbo.Size = new System.Drawing.Size(147, 20);
            this.baseCbo.TabIndex = 2;
            this.baseCbo.Visible = false;
            this.baseCbo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPress);
            // 
            // sbtnSerach
            // 
            this.sbtnSerach.Location = new System.Drawing.Point(454, 16);
            this.sbtnSerach.Name = "sbtnSerach";
            this.sbtnSerach.Size = new System.Drawing.Size(75, 23);
            this.sbtnSerach.TabIndex = 3;
            this.sbtnSerach.Text = "查询";
            this.sbtnSerach.Click += new System.EventHandler(this.sbtnSerach_Click);
            // 
            // cboOperator
            // 
            this.cboOperator.Location = new System.Drawing.Point(199, 18);
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOperator.Size = new System.Drawing.Size(76, 20);
            this.cboOperator.TabIndex = 1;
            this.cboOperator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPress);
            // 
            // imgcboSelectCondition
            // 
            this.imgcboSelectCondition.Location = new System.Drawing.Point(43, 18);
            this.imgcboSelectCondition.Name = "imgcboSelectCondition";
            this.imgcboSelectCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imgcboSelectCondition.Size = new System.Drawing.Size(150, 20);
            this.imgcboSelectCondition.TabIndex = 0;
            this.imgcboSelectCondition.SelectedValueChanged += new System.EventHandler(this.imgcboSelectCondition_SelectedValueChanged);
            this.imgcboSelectCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPeyPress);
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(9, 21);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(28, 14);
            this.lblCondition.TabIndex = 1;
            this.lblCondition.Text = "条件:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pcSearchCondition, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pcWhere, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pcResult, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(667, 515);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pcWhere
            // 
            this.pcWhere.Controls.Add(this.gcWhere);
            this.pcWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcWhere.Location = new System.Drawing.Point(3, 63);
            this.pcWhere.Name = "pcWhere";
            this.pcWhere.Size = new System.Drawing.Size(661, 132);
            this.pcWhere.TabIndex = 1;
            // 
            // gcWhere
            // 
            this.gcWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWhere.Location = new System.Drawing.Point(2, 2);
            this.gcWhere.MainView = this.gvWhere;
            this.gcWhere.Name = "gcWhere";
            this.gcWhere.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit5});
            this.gcWhere.Size = new System.Drawing.Size(657, 128);
            this.gcWhere.TabIndex = 0;
            this.gcWhere.TabStop = false;
            this.gcWhere.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWhere});
            // 
            // gvWhere
            // 
            this.gvWhere.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gvWhere.GridControl = this.gcWhere;
            this.gvWhere.Name = "gvWhere";
            this.gvWhere.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "条件";
            this.gridColumn1.FieldName = "ConditionDesc";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "符号";
            this.gridColumn2.FieldName = "Operator";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "结果";
            this.gridColumn3.FieldName = "ValueDesc";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "删除";
            this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit5;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // repositoryItemButtonEdit5
            // 
            this.repositoryItemButtonEdit5.AutoHeight = false;
            this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
            this.repositoryItemButtonEdit5.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit5.Click += new System.EventHandler(this.repositoryItemButtonEdit1_Click);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "列名";
            this.gridColumn5.FieldName = "FieldColunm";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "结果Key值";
            this.gridColumn6.FieldName = "ValueId";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "模糊查询类型";
            this.gridColumn7.FieldName = "LikeType";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 457);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(661, 55);
            this.panelControl1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(118, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(22, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemButtonEdit3
            // 
            this.repositoryItemButtonEdit3.AutoHeight = false;
            this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
            this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemButtonEdit4
            // 
            this.repositoryItemButtonEdit4.AutoHeight = false;
            this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true)});
            this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
            this.repositoryItemButtonEdit4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 515);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SearchForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "搜索帮助";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcResult)).EndInit();
            this.pcResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSearchCondition)).EndInit();
            this.pcSearchCondition.ResumeLayout(false);
            this.pcSearchCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseCbo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcboSelectCondition.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcWhere)).EndInit();
            this.pcWhere.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWhere)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWhere)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcResult;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.PanelControl pcSearchCondition;
        private DevExpress.XtraEditors.SimpleButton sbtnSerach;
        private DevExpress.XtraEditors.ComboBoxEdit cboOperator;
        private DevExpress.XtraEditors.ImageComboBoxEdit imgcboSelectCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl pcWhere;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit4;
        private DevExpress.XtraGrid.GridControl gcResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gvResult;
        private DevExpress.XtraGrid.GridControl gcWhere;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWhere;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit5;
        private BaseCombobox baseCbo;
        private DevExpress.XtraEditors.TextEdit teText;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.DateEdit deDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;

    }
}