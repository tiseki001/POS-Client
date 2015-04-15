namespace RefundOrder
{
    partial class RefundOrder
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit_UnRefundAmount = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_RefundAmount = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_OK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textEdit_Amount = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_CardCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_Code = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_TypeNo = new DevExpress.XtraEditors.TextEdit();
            this.gridControl_Refund = new DevExpress.XtraGrid.GridControl();
            this.gridView_Refund = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_Type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_SerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CardCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CollectionAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton_Add = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_TypeName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_SerialNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl_RefundType = new DevExpress.XtraGrid.GridControl();
            this.gridView_RefundType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_TypeNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_TypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_UnRefundAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_RefundAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Amount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CardCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Code.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TypeNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Refund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Refund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_SerialNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_RefundType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_RefundType)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.textEdit_UnRefundAmount);
            this.panelControl1.Controls.Add(this.textEdit_RefundAmount);
            this.panelControl1.Controls.Add(this.simpleButton_Cancel);
            this.panelControl1.Controls.Add(this.simpleButton_OK);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(705, 464);
            this.panelControl1.TabIndex = 0;
            // 
            // textEdit_UnRefundAmount
            // 
            this.textEdit_UnRefundAmount.Location = new System.Drawing.Point(234, 11);
            this.textEdit_UnRefundAmount.Name = "textEdit_UnRefundAmount";
            this.textEdit_UnRefundAmount.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit_UnRefundAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit_UnRefundAmount.Properties.Mask.EditMask = "############.##";
            this.textEdit_UnRefundAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_UnRefundAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit_UnRefundAmount.Properties.ReadOnly = true;
            this.textEdit_UnRefundAmount.Size = new System.Drawing.Size(100, 20);
            this.textEdit_UnRefundAmount.TabIndex = 102;
            // 
            // textEdit_RefundAmount
            // 
            this.textEdit_RefundAmount.Location = new System.Drawing.Point(70, 11);
            this.textEdit_RefundAmount.Name = "textEdit_RefundAmount";
            this.textEdit_RefundAmount.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit_RefundAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit_RefundAmount.Properties.Mask.EditMask = "############.##";
            this.textEdit_RefundAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_RefundAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit_RefundAmount.Properties.ReadOnly = true;
            this.textEdit_RefundAmount.Size = new System.Drawing.Size(100, 20);
            this.textEdit_RefundAmount.TabIndex = 36;
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(153, 419);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(75, 37);
            this.simpleButton_Cancel.TabIndex = 25;
            this.simpleButton_Cancel.Text = "取消(Esc)";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // simpleButton_OK
            // 
            this.simpleButton_OK.Location = new System.Drawing.Point(35, 419);
            this.simpleButton_OK.Name = "simpleButton_OK";
            this.simpleButton_OK.Size = new System.Drawing.Size(75, 37);
            this.simpleButton_OK.TabIndex = 26;
            this.simpleButton_OK.Text = "确定(F4)";
            this.simpleButton_OK.Click += new System.EventHandler(this.simpleButton_OK_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEdit_Amount);
            this.groupControl1.Controls.Add(this.simpleButton_Delete);
            this.groupControl1.Controls.Add(this.textEdit_CardCode);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.textEdit_Code);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.textEdit_TypeNo);
            this.groupControl1.Controls.Add(this.gridControl_Refund);
            this.groupControl1.Controls.Add(this.simpleButton_Add);
            this.groupControl1.Controls.Add(this.textEdit_TypeName);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.textEdit_SerialNo);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Location = new System.Drawing.Point(12, 37);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(392, 376);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "退款方式";
            // 
            // textEdit_Amount
            // 
            this.textEdit_Amount.Location = new System.Drawing.Point(58, 50);
            this.textEdit_Amount.Name = "textEdit_Amount";
            this.textEdit_Amount.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit_Amount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit_Amount.Properties.Mask.EditMask = "############.##";
            this.textEdit_Amount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_Amount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit_Amount.Size = new System.Drawing.Size(100, 20);
            this.textEdit_Amount.TabIndex = 103;
            this.textEdit_Amount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditAmount_KeyDown);
            // 
            // simpleButton_Delete
            // 
            this.simpleButton_Delete.Location = new System.Drawing.Point(261, 104);
            this.simpleButton_Delete.Name = "simpleButton_Delete";
            this.simpleButton_Delete.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Delete.TabIndex = 29;
            this.simpleButton_Delete.Text = "删除";
            this.simpleButton_Delete.Click += new System.EventHandler(this.simpleButton_Delete_Click);
            // 
            // textEdit_CardCode
            // 
            this.textEdit_CardCode.Location = new System.Drawing.Point(222, 75);
            this.textEdit_CardCode.Name = "textEdit_CardCode";
            this.textEdit_CardCode.Size = new System.Drawing.Size(114, 20);
            this.textEdit_CardCode.TabIndex = 4;
            this.textEdit_CardCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCardCode_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(164, 78);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 28;
            this.labelControl4.Text = "开户号码:";
            // 
            // textEdit_Code
            // 
            this.textEdit_Code.Location = new System.Drawing.Point(222, 50);
            this.textEdit_Code.Name = "textEdit_Code";
            this.textEdit_Code.Size = new System.Drawing.Size(114, 20);
            this.textEdit_Code.TabIndex = 2;
            this.textEdit_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.texteditCode_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(164, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 26;
            this.labelControl3.Text = "编码:";
            // 
            // textEdit_TypeNo
            // 
            this.textEdit_TypeNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.textEdit_TypeNo.Location = new System.Drawing.Point(58, 26);
            this.textEdit_TypeNo.Name = "textEdit_TypeNo";
            this.textEdit_TypeNo.Size = new System.Drawing.Size(100, 20);
            this.textEdit_TypeNo.TabIndex = 0;
            this.textEdit_TypeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditTypeNo_KeyDown);
            // 
            // gridControl_Refund
            // 
            gridLevelNode2.RelationName = "Level1";
            this.gridControl_Refund.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl_Refund.Location = new System.Drawing.Point(5, 133);
            this.gridControl_Refund.MainView = this.gridView_Refund;
            this.gridControl_Refund.Name = "gridControl_Refund";
            this.gridControl_Refund.Size = new System.Drawing.Size(382, 238);
            this.gridControl_Refund.TabIndex = 24;
            this.gridControl_Refund.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Refund});
            // 
            // gridView_Refund
            // 
            this.gridView_Refund.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_Type,
            this.gridColumn_Code,
            this.gridColumn_Amount,
            this.gridColumn_SerialNo,
            this.gridColumn_CardCode,
            this.gridColumn_CollectionAmount});
            this.gridView_Refund.GridControl = this.gridControl_Refund;
            this.gridView_Refund.Name = "gridView_Refund";
            this.gridView_Refund.OptionsBehavior.Editable = false;
            this.gridView_Refund.OptionsBehavior.ReadOnly = true;
            this.gridView_Refund.OptionsView.ShowFooter = true;
            this.gridView_Refund.OptionsView.ShowGroupPanel = false;
            this.gridView_Refund.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewRefund_FocusedRowChanged);
            // 
            // gridColumn_Type
            // 
            this.gridColumn_Type.Caption = "退款方式";
            this.gridColumn_Type.FieldName = "type";
            this.gridColumn_Type.Name = "gridColumn_Type";
            this.gridColumn_Type.Visible = true;
            this.gridColumn_Type.VisibleIndex = 0;
            // 
            // gridColumn_Code
            // 
            this.gridColumn_Code.Caption = "编码";
            this.gridColumn_Code.FieldName = "code";
            this.gridColumn_Code.Name = "gridColumn_Code";
            this.gridColumn_Code.Visible = true;
            this.gridColumn_Code.VisibleIndex = 3;
            // 
            // gridColumn_Amount
            // 
            this.gridColumn_Amount.Caption = "退款金额";
            this.gridColumn_Amount.DisplayFormat.FormatString = "#";
            this.gridColumn_Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn_Amount.FieldName = "amount";
            this.gridColumn_Amount.Name = "gridColumn_Amount";
            this.gridColumn_Amount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_Amount.Visible = true;
            this.gridColumn_Amount.VisibleIndex = 2;
            // 
            // gridColumn_SerialNo
            // 
            this.gridColumn_SerialNo.Caption = "串号";
            this.gridColumn_SerialNo.FieldName = "serialNo";
            this.gridColumn_SerialNo.Name = "gridColumn_SerialNo";
            this.gridColumn_SerialNo.Visible = true;
            this.gridColumn_SerialNo.VisibleIndex = 4;
            // 
            // gridColumn_CardCode
            // 
            this.gridColumn_CardCode.Caption = "开户号码";
            this.gridColumn_CardCode.FieldName = "cardCode";
            this.gridColumn_CardCode.Name = "gridColumn_CardCode";
            this.gridColumn_CardCode.Visible = true;
            this.gridColumn_CardCode.VisibleIndex = 5;
            // 
            // gridColumn_CollectionAmount
            // 
            this.gridColumn_CollectionAmount.Caption = "收款金额";
            this.gridColumn_CollectionAmount.DisplayFormat.FormatString = "#";
            this.gridColumn_CollectionAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn_CollectionAmount.FieldName = "collectionAmount";
            this.gridColumn_CollectionAmount.Name = "gridColumn_CollectionAmount";
            this.gridColumn_CollectionAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn_CollectionAmount.Visible = true;
            this.gridColumn_CollectionAmount.VisibleIndex = 1;
            // 
            // simpleButton_Add
            // 
            this.simpleButton_Add.Location = new System.Drawing.Point(180, 104);
            this.simpleButton_Add.Name = "simpleButton_Add";
            this.simpleButton_Add.Size = new System.Drawing.Size(75, 23);
            this.simpleButton_Add.TabIndex = 5;
            this.simpleButton_Add.Text = "添加";
            this.simpleButton_Add.Click += new System.EventHandler(this.simpleButton_Add_Click);
            // 
            // textEdit_TypeName
            // 
            this.textEdit_TypeName.Location = new System.Drawing.Point(222, 26);
            this.textEdit_TypeName.Name = "textEdit_TypeName";
            this.textEdit_TypeName.Properties.ReadOnly = true;
            this.textEdit_TypeName.Size = new System.Drawing.Size(114, 20);
            this.textEdit_TypeName.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(164, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(28, 14);
            this.labelControl9.TabIndex = 21;
            this.labelControl9.Text = "名称:";
            // 
            // textEdit_SerialNo
            // 
            this.textEdit_SerialNo.Location = new System.Drawing.Point(58, 75);
            this.textEdit_SerialNo.Name = "textEdit_SerialNo";
            this.textEdit_SerialNo.Size = new System.Drawing.Size(100, 20);
            this.textEdit_SerialNo.TabIndex = 3;
            this.textEdit_SerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditSerialNo_KeyDown);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(4, 75);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "串号:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(4, 49);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(28, 14);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "金额:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(4, 27);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 14);
            this.labelControl8.TabIndex = 1;
            this.labelControl8.Text = "收银方式:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl_RefundType);
            this.panelControl2.Location = new System.Drawing.Point(410, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(281, 444);
            this.panelControl2.TabIndex = 101;
            // 
            // gridControl_RefundType
            // 
            this.gridControl_RefundType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_RefundType.Location = new System.Drawing.Point(2, 2);
            this.gridControl_RefundType.MainView = this.gridView_RefundType;
            this.gridControl_RefundType.Name = "gridControl_RefundType";
            this.gridControl_RefundType.Size = new System.Drawing.Size(277, 440);
            this.gridControl_RefundType.TabIndex = 25;
            this.gridControl_RefundType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_RefundType});
            // 
            // gridView_RefundType
            // 
            this.gridView_RefundType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_TypeNo,
            this.gridColumn_TypeName});
            this.gridView_RefundType.GridControl = this.gridControl_RefundType;
            this.gridView_RefundType.GroupPanelText = "退款方式";
            this.gridView_RefundType.Name = "gridView_RefundType";
            this.gridView_RefundType.OptionsBehavior.ReadOnly = true;
            this.gridView_RefundType.DoubleClick += new System.EventHandler(this.gridViewRefundType_DoubleClick);
            // 
            // gridColumn_TypeNo
            // 
            this.gridColumn_TypeNo.Caption = "编号";
            this.gridColumn_TypeNo.FieldName = "paymentMethodTypeId";
            this.gridColumn_TypeNo.Name = "gridColumn_TypeNo";
            this.gridColumn_TypeNo.Visible = true;
            this.gridColumn_TypeNo.VisibleIndex = 0;
            // 
            // gridColumn_TypeName
            // 
            this.gridColumn_TypeName.Caption = "名称";
            this.gridColumn_TypeName.FieldName = "description";
            this.gridColumn_TypeName.Name = "gridColumn_TypeName";
            this.gridColumn_TypeName.Visible = true;
            this.gridColumn_TypeName.VisibleIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(176, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "未退金额:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "应退金额:";
            // 
            // RefundOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 464);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RefundOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "退款";
            this.Load += new System.EventHandler(this.RefundOrder_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPreview_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_UnRefundAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_RefundAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Amount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CardCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Code.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TypeNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Refund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Refund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_SerialNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_RefundType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_RefundType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton_OK;
        private DevExpress.XtraGrid.GridControl gridControl_RefundType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_RefundType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TypeNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TypeName;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit textEdit_CardCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textEdit_Code;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit_TypeNo;
        private DevExpress.XtraGrid.GridControl gridControl_Refund;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Refund;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Type;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Code;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Amount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_SerialNo;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Add;
        private DevExpress.XtraEditors.TextEdit textEdit_TypeName;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit textEdit_SerialNo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CardCode;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Delete;
        private DevExpress.XtraEditors.TextEdit textEdit_RefundAmount;
        private DevExpress.XtraEditors.TextEdit textEdit_UnRefundAmount;
        private DevExpress.XtraEditors.TextEdit textEdit_Amount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CollectionAmount;
    }
}