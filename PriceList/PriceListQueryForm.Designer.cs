namespace PriceList
{
    partial class PriceListQueryForm
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
            this.repositoryItemImageComboBox_Docstatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridControl_PriceList = new DevExpress.XtraGrid.GridControl();
            this.gridView_SalesOrder = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_Brand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ProductType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_PolicyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_StandardPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_MinPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox_BusinessStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit_PolicyName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_Policy = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_ProductName = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_ProductType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_ProductType = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_Brand = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_Brand = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_Query = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_ProductId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_ProductId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PriceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SalesOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_BusinessStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_PolicyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Brand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemImageComboBox_Docstatus
            // 
            this.repositoryItemImageComboBox_Docstatus.AutoHeight = false;
            this.repositoryItemImageComboBox_Docstatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox_Docstatus.Name = "repositoryItemImageComboBox_Docstatus";
            // 
            // gridControl_PriceList
            // 
            this.gridControl_PriceList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl_PriceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_PriceList.Location = new System.Drawing.Point(2, 2);
            this.gridControl_PriceList.MainView = this.gridView_SalesOrder;
            this.gridControl_PriceList.Name = "gridControl_PriceList";
            this.gridControl_PriceList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox_Docstatus,
            this.repositoryItemImageComboBox_BusinessStatus});
            this.gridControl_PriceList.Size = new System.Drawing.Size(1274, 644);
            this.gridControl_PriceList.TabIndex = 21;
            this.gridControl_PriceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_SalesOrder,
            this.gridView1});
            // 
            // gridView_SalesOrder
            // 
            this.gridView_SalesOrder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_Brand,
            this.gridColumn_ProductType,
            this.gridColumn_ItemCode,
            this.gridColumn_ItemName,
            this.gridColumn_PolicyName,
            this.gridColumn_StandardPrice,
            this.gridColumn_MinPrice,
            this.gridColumn_CostPrice});
            this.gridView_SalesOrder.GridControl = this.gridControl_PriceList;
            this.gridView_SalesOrder.Name = "gridView_SalesOrder";
            this.gridView_SalesOrder.OptionsBehavior.ReadOnly = true;
            this.gridView_SalesOrder.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn_Brand
            // 
            this.gridColumn_Brand.Caption = "品牌";
            this.gridColumn_Brand.FieldName = "brands";
            this.gridColumn_Brand.Name = "gridColumn_Brand";
            this.gridColumn_Brand.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_Brand.Visible = true;
            this.gridColumn_Brand.VisibleIndex = 0;
            this.gridColumn_Brand.Width = 120;
            // 
            // gridColumn_ProductType
            // 
            this.gridColumn_ProductType.Caption = "商品型号";
            this.gridColumn_ProductType.FieldName = "models";
            this.gridColumn_ProductType.Name = "gridColumn_ProductType";
            this.gridColumn_ProductType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_ProductType.Visible = true;
            this.gridColumn_ProductType.VisibleIndex = 1;
            this.gridColumn_ProductType.Width = 116;
            // 
            // gridColumn_ItemCode
            // 
            this.gridColumn_ItemCode.Caption = "商品编码";
            this.gridColumn_ItemCode.FieldName = "productId";
            this.gridColumn_ItemCode.Name = "gridColumn_ItemCode";
            this.gridColumn_ItemCode.Visible = true;
            this.gridColumn_ItemCode.VisibleIndex = 2;
            this.gridColumn_ItemCode.Width = 174;
            // 
            // gridColumn_ItemName
            // 
            this.gridColumn_ItemName.Caption = "商品名称";
            this.gridColumn_ItemName.FieldName = "productName";
            this.gridColumn_ItemName.Name = "gridColumn_ItemName";
            this.gridColumn_ItemName.Visible = true;
            this.gridColumn_ItemName.VisibleIndex = 3;
            this.gridColumn_ItemName.Width = 244;
            // 
            // gridColumn_PolicyName
            // 
            this.gridColumn_PolicyName.Caption = "销售政策";
            this.gridColumn_PolicyName.FieldName = "policyName";
            this.gridColumn_PolicyName.Name = "gridColumn_PolicyName";
            this.gridColumn_PolicyName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_PolicyName.Visible = true;
            this.gridColumn_PolicyName.VisibleIndex = 4;
            this.gridColumn_PolicyName.Width = 272;
            // 
            // gridColumn_StandardPrice
            // 
            this.gridColumn_StandardPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_StandardPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_StandardPrice.Caption = "指导价";
            this.gridColumn_StandardPrice.DisplayFormat.FormatString = "#";
            this.gridColumn_StandardPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn_StandardPrice.FieldName = "standardPrice";
            this.gridColumn_StandardPrice.Name = "gridColumn_StandardPrice";
            this.gridColumn_StandardPrice.Visible = true;
            this.gridColumn_StandardPrice.VisibleIndex = 5;
            this.gridColumn_StandardPrice.Width = 121;
            // 
            // gridColumn_MinPrice
            // 
            this.gridColumn_MinPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_MinPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_MinPrice.Caption = "销售底价";
            this.gridColumn_MinPrice.DisplayFormat.FormatString = "#";
            this.gridColumn_MinPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn_MinPrice.FieldName = "minPrice";
            this.gridColumn_MinPrice.Name = "gridColumn_MinPrice";
            this.gridColumn_MinPrice.Visible = true;
            this.gridColumn_MinPrice.VisibleIndex = 6;
            this.gridColumn_MinPrice.Width = 107;
            // 
            // gridColumn_CostPrice
            // 
            this.gridColumn_CostPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_CostPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_CostPrice.Caption = "考核成本价";
            this.gridColumn_CostPrice.DisplayFormat.FormatString = "#";
            this.gridColumn_CostPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn_CostPrice.FieldName = "costPrice";
            this.gridColumn_CostPrice.Name = "gridColumn_CostPrice";
            this.gridColumn_CostPrice.Visible = true;
            this.gridColumn_CostPrice.VisibleIndex = 7;
            this.gridColumn_CostPrice.Width = 102;
            // 
            // repositoryItemImageComboBox_BusinessStatus
            // 
            this.repositoryItemImageComboBox_BusinessStatus.AutoHeight = false;
            this.repositoryItemImageComboBox_BusinessStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox_BusinessStatus.Name = "repositoryItemImageComboBox_BusinessStatus";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl_PriceList;
            this.gridView1.Name = "gridView1";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.simpleButton_Cancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 728);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1278, 47);
            this.panelControl3.TabIndex = 2;
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.Location = new System.Drawing.Point(13, 11);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(87, 27);
            this.simpleButton_Cancel.TabIndex = 39;
            this.simpleButton_Cancel.Text = "取消(Esc)";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.gridControl_PriceList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 74);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1278, 648);
            this.panelControl2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 778);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.textEdit_PolicyName);
            this.panelControl1.Controls.Add(this.labelControl_Policy);
            this.panelControl1.Controls.Add(this.textEdit_ProductName);
            this.panelControl1.Controls.Add(this.textEdit_ProductType);
            this.panelControl1.Controls.Add(this.labelControl_ProductType);
            this.panelControl1.Controls.Add(this.textEdit_Brand);
            this.panelControl1.Controls.Add(this.labelControl_Brand);
            this.panelControl1.Controls.Add(this.simpleButton_Query);
            this.panelControl1.Controls.Add(this.textEdit_ProductId);
            this.panelControl1.Controls.Add(this.labelControl_ProductId);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1278, 65);
            this.panelControl1.TabIndex = 3;
            // 
            // textEdit_PolicyName
            // 
            this.textEdit_PolicyName.Location = new System.Drawing.Point(310, 11);
            this.textEdit_PolicyName.Name = "textEdit_PolicyName";
            this.textEdit_PolicyName.Size = new System.Drawing.Size(159, 20);
            this.textEdit_PolicyName.TabIndex = 7;
            // 
            // labelControl_Policy
            // 
            this.labelControl_Policy.Location = new System.Drawing.Point(256, 14);
            this.labelControl_Policy.Name = "labelControl_Policy";
            this.labelControl_Policy.Size = new System.Drawing.Size(48, 14);
            this.labelControl_Policy.TabIndex = 54;
            this.labelControl_Policy.Text = "销售政策";
            // 
            // textEdit_ProductName
            // 
            this.textEdit_ProductName.Location = new System.Drawing.Point(69, 38);
            this.textEdit_ProductName.Name = "textEdit_ProductName";
            this.textEdit_ProductName.Size = new System.Drawing.Size(177, 20);
            this.textEdit_ProductName.TabIndex = 6;
            // 
            // textEdit_ProductType
            // 
            this.textEdit_ProductType.Location = new System.Drawing.Point(477, 38);
            this.textEdit_ProductType.Name = "textEdit_ProductType";
            this.textEdit_ProductType.Size = new System.Drawing.Size(95, 20);
            this.textEdit_ProductType.TabIndex = 4;
            // 
            // labelControl_ProductType
            // 
            this.labelControl_ProductType.Location = new System.Drawing.Point(423, 40);
            this.labelControl_ProductType.Name = "labelControl_ProductType";
            this.labelControl_ProductType.Size = new System.Drawing.Size(48, 14);
            this.labelControl_ProductType.TabIndex = 51;
            this.labelControl_ProductType.Text = "商品型号";
            // 
            // textEdit_Brand
            // 
            this.textEdit_Brand.Location = new System.Drawing.Point(310, 37);
            this.textEdit_Brand.Name = "textEdit_Brand";
            this.textEdit_Brand.Size = new System.Drawing.Size(95, 20);
            this.textEdit_Brand.TabIndex = 3;
            // 
            // labelControl_Brand
            // 
            this.labelControl_Brand.Location = new System.Drawing.Point(280, 40);
            this.labelControl_Brand.Name = "labelControl_Brand";
            this.labelControl_Brand.Size = new System.Drawing.Size(24, 14);
            this.labelControl_Brand.TabIndex = 45;
            this.labelControl_Brand.Text = "品牌";
            // 
            // simpleButton_Query
            // 
            this.simpleButton_Query.Location = new System.Drawing.Point(772, 14);
            this.simpleButton_Query.Name = "simpleButton_Query";
            this.simpleButton_Query.Size = new System.Drawing.Size(75, 44);
            this.simpleButton_Query.TabIndex = 11;
            this.simpleButton_Query.Text = "查询";
            this.simpleButton_Query.Click += new System.EventHandler(this.simpleButton_Query_Click);
            // 
            // textEdit_ProductId
            // 
            this.textEdit_ProductId.Location = new System.Drawing.Point(69, 11);
            this.textEdit_ProductId.Name = "textEdit_ProductId";
            this.textEdit_ProductId.Size = new System.Drawing.Size(177, 20);
            this.textEdit_ProductId.TabIndex = 5;
            // 
            // labelControl_ProductId
            // 
            this.labelControl_ProductId.Location = new System.Drawing.Point(14, 12);
            this.labelControl_ProductId.Name = "labelControl_ProductId";
            this.labelControl_ProductId.Size = new System.Drawing.Size(48, 14);
            this.labelControl_ProductId.TabIndex = 26;
            this.labelControl_ProductId.Text = "商品编码";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 56;
            this.labelControl1.Text = "商品名称";
            // 
            // PriceListQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 778);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "PriceListQueryForm";
            this.Text = "商品价格查询";
            this.Load += new System.EventHandler(this.PriceListQueryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PriceListQueryForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PriceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SalesOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_BusinessStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_PolicyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_Brand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_ProductId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_Docstatus;
        private DevExpress.XtraGrid.GridControl gridControl_PriceList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_SalesOrder;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Brand;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ProductType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_PolicyName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_StandardPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_MinPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CostPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_BusinessStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit_PolicyName;
        private DevExpress.XtraEditors.LabelControl labelControl_Policy;
        private DevExpress.XtraEditors.TextEdit textEdit_ProductName;
        private DevExpress.XtraEditors.TextEdit textEdit_ProductType;
        private DevExpress.XtraEditors.LabelControl labelControl_ProductType;
        private DevExpress.XtraEditors.TextEdit textEdit_Brand;
        private DevExpress.XtraEditors.LabelControl labelControl_Brand;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Query;
        private DevExpress.XtraEditors.TextEdit textEdit_ProductId;
        private DevExpress.XtraEditors.LabelControl labelControl_ProductId;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}