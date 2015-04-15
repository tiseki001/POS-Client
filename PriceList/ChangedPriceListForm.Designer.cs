namespace PriceList
{
    partial class ChangedPriceListForm
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
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
            this.repositoryItemImageComboBox_Docstatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox_BusinessStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton_OK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PriceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SalesOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_BusinessStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.gridControl_PriceList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1278, 719);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl_PriceList
            // 
            this.gridControl_PriceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_PriceList.Location = new System.Drawing.Point(2, 2);
            this.gridControl_PriceList.MainView = this.gridView_SalesOrder;
            this.gridControl_PriceList.Name = "gridControl_PriceList";
            this.gridControl_PriceList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox_Docstatus,
            this.repositoryItemImageComboBox_BusinessStatus});
            this.gridControl_PriceList.Size = new System.Drawing.Size(1274, 715);
            this.gridControl_PriceList.TabIndex = 2;
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
            this.gridView_SalesOrder.OptionsView.ShowFooter = true;
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
            this.gridColumn_CostPrice.FieldName = "costPrice";
            this.gridColumn_CostPrice.Name = "gridColumn_CostPrice";
            this.gridColumn_CostPrice.Visible = true;
            this.gridColumn_CostPrice.VisibleIndex = 7;
            this.gridColumn_CostPrice.Width = 102;
            // 
            // repositoryItemImageComboBox_Docstatus
            // 
            this.repositoryItemImageComboBox_Docstatus.AutoHeight = false;
            this.repositoryItemImageComboBox_Docstatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox_Docstatus.Name = "repositoryItemImageComboBox_Docstatus";
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
            this.panelControl3.Controls.Add(this.simpleButton_OK);
            this.panelControl3.Controls.Add(this.simpleButton_Cancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 728);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1278, 47);
            this.panelControl3.TabIndex = 2;
            // 
            // simpleButton_OK
            // 
            this.simpleButton_OK.Location = new System.Drawing.Point(13, 11);
            this.simpleButton_OK.Name = "simpleButton_OK";
            this.simpleButton_OK.Size = new System.Drawing.Size(87, 27);
            this.simpleButton_OK.TabIndex = 41;
            this.simpleButton_OK.Text = "已处理(F4)";
            this.simpleButton_OK.Click += new System.EventHandler(this.simpleButton_OK_Click);
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.Location = new System.Drawing.Point(110, 11);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(87, 27);
            this.simpleButton_Cancel.TabIndex = 39;
            this.simpleButton_Cancel.Text = "取消(Esc)";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 778);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ChangedPriceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 778);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "ChangedPriceListForm";
            this.Text = "调价通知";
            this.Load += new System.EventHandler(this.ChangedPriceListForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangedPriceListForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PriceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SalesOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_Docstatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox_BusinessStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_Docstatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_BusinessStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton_OK;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;


    }
}