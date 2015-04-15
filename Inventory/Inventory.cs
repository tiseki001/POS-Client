using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using DevExpress.XtraTab;
using Commons.Model.Stock;
using DevExpress.Utils.Menu;
using Commons.XML;
using Commons.Model;
using System.Linq;
using Newtonsoft.Json;

namespace Inventory
{
    public partial class Inventory : BaseForm
    {
        #region 参数
        public InventorySearch search = null;
        public InventotyDetailHeader searchHeader = null;
        public InventoryOrder order = null;
        public InventoryHeaderDetailCommand orderHeader = null;
        List<InventotyDetailItemCommand> detailCommand = null;
        List<Product> productList = null;
        List<InventotyDetailItem> inventoryItem = new List<InventotyDetailItem>();
        string productId = null;
        string productName = null;
        string idValue = null;
        string sequenceId = null;
        string facilityId = null;
        int inventoryNum = 0;
        #endregion

        #region 构造
        public Inventory()
        {
            InitializeComponent();
        }
        #endregion

        #region 取消按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                ((XtraTabPage)this.Parent).Text = "盘点查询";
                search.Visible = true;
                search.BringToFront();
                this.Close();
            }
            else if (order != null)
            {
                ((XtraTabPage)this.Parent).Text = "盘点指令";
                order.Visible = true;
                order.BringToFront();
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 加载事件
        private void Inventory_Load(object sender, EventArgs e)
        {
            try
            {
                //禁用状态
                cboStatus.Enabled = false;
                //获得状态数据
                GetStatusData();
                //制单时间
                teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (search != null)
                {
                    //获得仓库数据
                    GetFacilityData();
                    //单号
                    teInventoryNo.Text = searchHeader.docId;
                    //指令
                    beOrder.Text = searchHeader.baseEntry;
                    //禁用命令
                    beOrder.Enabled = false;
                    //状态
                    cboStatus.EditValue = searchHeader.docStatus;
                    //备注
                    meMemo.Text = searchHeader.memo;
                    if (Convert.ToInt32(searchHeader.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        radioGroupType.Enabled = false;
                        gvInventory.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvInventory.Columns["memo"].OptionsColumn.ReadOnly = true;
                        pcCondition.Enabled = false;
                        cboFacility.Enabled = false;
                        btnDeleteRow.Enabled = false;
                        btnSave.Enabled = false;
                        btnDraft.Enabled = false;
                        meMemo.ReadOnly = true;
                    }
                    GetInventoryItem();
                    gcInventory.DataSource = inventoryItem;
                    gcInventory.RefreshDataSource();
                }
                else if (order != null)
                {
                    //禁用删除行
                    btnDeleteRow.Enabled = false;
                    //禁用盘点类型
                    radioGroupType.Enabled = false;
                    //单据状态
                    cboStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    //获得单号
                    getDocId();
                    //制单人
                    teUserName.Text = LoginInfo.UserName;
                    beOrder.Text = orderHeader.docId;
                    meCommandMemo.Text = orderHeader.memo;
                    commandFacility(orderHeader.docId);
                    clear();
                    CommandItem(orderHeader.docId);
                }
                else
                {
                    //获得仓库数据
                    GetFacilityData();
                    //单据状态
                    cboStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    //获得单号
                    getDocId();
                    //制单人
                    teUserName.Text = LoginInfo.UserName;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得仓库数据
        public void GetFacilityData()
        {
            try
            {
                string mag = null;
                GetData.GetUrl("Stock", "FacilitySearch", out mag);
                var searchCondition = new { productStoreId = LoginInfo.ProductStoreId, mag = mag };
                object obj = null;
                if (DevCommon.getDataByWebService("Stock", "FacilitySearch", searchCondition, ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        DataTable dtFacility = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                        DevCommon.initCmb(cboFacility, dtFacility, "facilityId", "facilityName", true);
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得状态数据
        public void GetStatusData()
        {
            try
            {
                DataTable dtStatus = null;
                dtStatus = DevCommon.GetAllOrderStatus();
                if (dtStatus != null)
                {
                    DevCommon.initCmb(cboStatus, dtStatus, false);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }

        }
        #endregion

        #region 删除行数据
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (gvInventory.RowCount > 0)
            {
                if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvInventory.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 右键菜单
        private void gvInventory_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            //DXMenuItem item = new DXMenuItem(" 删除");
            //item.Click += new EventHandler(item_Click);
            //if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            //{
            //    if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
            //    {
            //        e.Menu.Items.Insert(0, item);
            //    }
            //}
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
            //{
            //    gvInventory.DeleteSelectedRows();
            //}
        }
        #endregion

        #region 保存确定单据事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    insertInventory(DocStatus.VALID);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 保存草稿单据事件
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    insertInventory(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加发货单数据方法
        /// <summary>
        /// 状态数据
        /// </summary>
        /// <param name="type"></param>
        private void insertInventory(DocStatus type)
        {
            try
            {
                if (gvInventory.RowCount > 0)
                {
                    //判断单号是否有空
                    if (string.IsNullOrEmpty(teInventoryNo.Text.Trim()))
                    {
                        m_frm.PromptInformation("盘点单号为空！");
                        return;
                    }
                    #region 添加头数据
                    InventotyHeader header = new InventotyHeader();
                    if (search != null)
                    {
                        header.docDate = searchHeader.docDate;
                        header.userId = searchHeader.userId;
                        header.status = searchHeader.status;
                    }
                    else
                    {
                        header.docDate = string.IsNullOrEmpty(teDocDate.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(teDocDate.Text.Trim());
                        header.userId = LoginInfo.PartyId;
                        header.status = "0";
                    }
                    header.docId = teInventoryNo.Text.Trim();
                    header.baseEntry = beOrder.Text.Trim();
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.updateDate = string.IsNullOrEmpty(teDocDate.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(teDocDate.Text.Trim());
                    header.updateUserId = LoginInfo.PartyId;
                    header.docStatus = Convert.ToInt32(type).ToString();
                    header.memo = meMemo.Text.Trim();
                    header.partyId = LoginInfo.OwnerPartyId;
                    #endregion

                    #region 添加发货行明细
                    List<InventotyDetailItem> itemDetail = new List<InventotyDetailItem>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<InventotyDetailItem>)gcInventory.DataSource).Where(p => p.quantity > 0).ToList();
                    }
                    else
                    {
                        itemDetail = (List<InventotyDetailItem>)gcInventory.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要盘点的数据！");
                        return;
                    }
                    List<InventoryItem> item = itemDetail.Select(p => new InventoryItem
                    {
                        docId = p.docId,
                        lineNo = p.lineNo,
                        productId = p.productId,
                        facilityId = p.facilityId,
                        memo = p.memo,
                        quantity = p.quantity == null ? 0 : p.quantity,
                        baseEntry = p.baseEntry,
                        baseLineNo = p.baseLineNo,
                        sequenceId = p.sequenceId,
                        isSequence = p.isSequence,
                        idValue = p.idValue,
                        onHand = p.onHand
                    }).ToList();
                    #endregion

                    #region 添加或修改数据
                    if (search != null)
                    {
                        if (update(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "盘点查询";
                            this.Close();
                            search.Visible = true;
                            search.BringToFront();
                            search.Data();
                            return;
                        }
                        else
                        {
                            m_frm.PromptInformation("操作失败！");
                            return;
                        }
                    }
                    else if (order != null)
                    {
                        if (insert(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "盘点指令单";
                            this.Close();
                            order.Visible = true;
                            order.BringToFront();
                            order.Reload();
                            return;
                        }
                        else
                        {
                            m_frm.PromptInformation("操作失败！");
                            return;
                        }
                    }
                    else
                    {
                        if (insert(header, item))
                        {
                            meMemo.Text = "";
                            meCommandMemo.Text = "";
                            getDocId();
                            gcInventory.DataSource = null;
                            inventoryItem.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            beOrder.Text = "";
                            beOrder.Value = "";
                            beOrder.Enabled = true;
                            cboStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                            clear();
                            m_frm.PromptInformation("操作成功！");
                            return;
                        }
                        else
                        {
                            m_frm.PromptInformation("操作失败！");
                            return;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加盘点数据
        private bool insert(InventotyHeader header, List<InventoryItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("InsertInventoty", "InsertInventoty", searchCondition, ref result) == RetCode.OK)
                {
                    m_frm.PromptInformation("保存成功！");
                    return true;
                }
                else
                {
                    m_frm.PromptInformation("保存失败！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改盘点数据
        private bool update(InventotyHeader header, List<InventoryItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateInventoty", "UpdateInventoty", searchCondition, ref result) == RetCode.OK)
                {
                    m_frm.PromptInformation("保存成功！");
                    return true;
                }
                else
                {
                    m_frm.PromptInformation("保存失败！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 键盘按下事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && string.IsNullOrEmpty(((SearchButton)sender).Text.Trim()))
                {
                    SendKeys.Send("{Tab}");
                }
                if (e.KeyChar == 13 && !string.IsNullOrEmpty(((SearchButton)sender).Text.Trim()))
                {
                    #region 条件清空
                    productId = null;
                    productName = null;
                    idValue = null;
                    sequenceId = null;
                    #endregion
                    //仓库判断
                    if (string.IsNullOrEmpty(cboFacility.Text.Trim()))
                    {
                        m_frm.PromptInformation("请填写仓库！");
                        clear();
                        return;
                    }
                    //输入条件判断
                    if (((SearchButton)sender).Text.Trim().Length < 4)
                    {
                        m_frm.PromptInformation("输入的条件不能少于4位！");
                        return;
                    }
                    #region 根据商品代码、商品名称、条形码、串号查询
                    if (sender.Equals(btnProductId))
                    {
                        productId = btnProductId.Text.Trim();
                    }
                    else if (sender.Equals(btnProductName))
                    {
                        if (string.IsNullOrEmpty(btnProductName.Value))
                        {
                            productName = btnProductName.Text.Trim();
                        }
                        else
                        {
                            productId = btnProductName.Value.Trim();
                            productName = btnProductName.Text.Trim();
                        }
                    }
                    else if (sender.Equals(btnSerial))
                    {
                        if (string.IsNullOrEmpty(btnSerial.Value))
                        {
                            idValue = btnSerial.Text.Trim();
                        }
                        else
                        {
                            productId = btnSerial.Value.Trim();
                            idValue = btnSerial.Text.Trim();
                        }
                    }
                    else if (sender.Equals(btnSequence))
                    {
                        sequenceId = btnSequence.Text.Trim();
                    }
                    var con = new
                    {
                        productId = productId,
                        productName = productName,
                        idValue = idValue,
                        sequenceId = sequenceId,
                        facilityId = facilityId,
                        productStoreId = LoginInfo.ProductStoreId
                    };
                    productList = null;
                    if (DevCommon.getDataByWebService("ProductAndSequenceSearch", "ProductAndSequenceSearch", con, ref productList) == RetCode.OK)
                    {
                        AddInventoryRowData(productList, ((SearchButton)sender));
                    }
                    else
                    {
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加行数据
        public void AddInventoryRowData(List<Product> list, SearchButton button)
        {
            try
            {
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        if (inventoryItem.Count > 0)
                        {
                            List<InventotyDetailItem> item = inventoryItem.Where(p =>
                                p.productId == list[0].ProductId
                                && p.facilityId == list[0].FacilityId
                                ).ToList();
                            if (item.Count > 0)
                            {
                                for (int i = 0; i < inventoryItem.Count; i++)
                                {
                                    if (inventoryItem[i].productId == list[0].ProductId
                                        && inventoryItem[i].facilityId == list[0].FacilityId)
                                    {
                                        inventoryItem[i].quantity = (inventoryItem[i].quantity == null ? 0 : inventoryItem[i].quantity) + 1;
                                        inventoryItem[i].onHand = list[0].OnHand;
                                        gcInventory.RefreshDataSource();
                                        clear();
                                        gvInventory.FocusedRowHandle = gvInventory.GetRowHandle(i);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (orderHeader == null)
                                {
                                    InsertInventoryRowData(list[0]);
                                    clear();
                                    return;
                                }
                                else
                                {
                                    m_frm.PromptInformation("商品：" + list[0].ProductId + "不在此盘点之内!");
                                    clear();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (orderHeader == null)
                            {
                                InsertInventoryRowData(list[0]);
                                clear();
                                return;
                            }
                            else
                            {
                                m_frm.PromptInformation("商品：" + list[0].ProductId + "不在此盘点之内!");
                                clear();
                                return;
                            }
                        }
                    }
                    else
                    {
                        button.ShowForm();
                        return;
                    }
                }
                else
                {
                    m_frm.PromptInformation("没有搜索到相关商品：" + button.Text.Trim() + "!");
                    clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加盘点行数据
        public void InsertInventoryRowData(Product product)
        {
            InventotyDetailItem item = new InventotyDetailItem();
            item.lineNo = inventoryItem.Count.ToString();
            item.docId = teInventoryNo.Text;
            item.productId = product.ProductId;
            item.productName = product.ProductName;
            item.onHand = product.OnHand;
            item.facilityName = product.FacilityName;
            item.facilityId = product.FacilityId;
            item.isSequence = product.isSequence;
            //item.sequenceId = product.SequenceId;
            item.quantity = 1;
            inventoryItem.Add(item);
            gcInventory.DataSource = inventoryItem;
            gvInventory.FocusedRowHandle = gvInventory.GetRowHandle(inventoryItem.Count - 1);
            gcInventory.RefreshDataSource();
        }
        #endregion

        #region 清空文本
        private void clear()
        {
            btnProductId.Text = "";
            btnProductId.Value = null;
            btnProductName.Text = "";
            btnProductName.Value = null;
            btnSerial.Text = "";
            btnSerial.Value = null;
            btnSequence.Text = "";
            btnSequence.Value = null;
        }
        #endregion

        #region 下拉列表值改变事件
        private void cboFacility_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboFacility.Text.Trim()))
            {
                facilityId = null;
            }
            else
            {
                facilityId = cboFacility.EditValue.ToString();
            }
        }
        #endregion

        #region 获得盘点单号
        private void getDocId()
        {
            try
            {
                string docId = null;
                if (DevCommon.getDocId(DocType.Inventory, ref docId) == RetCode.OK)
                {
                    teInventoryNo.Text = docId;
                }
                else
                {
                    teInventoryNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teInventoryNo.Text = "";
                throw ex;
            }
        }
        #endregion

        #region 列表行修改之后事件
        private void gvInventory_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == Quantity)
                {
                    int quantity = gvInventory.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvInventory.GetFocusedRowCellValue(Quantity));
                    if (quantity < 0)
                    {
                        gvInventory.SetFocusedRowCellValue(Quantity, inventoryNum);
                        return;
                    }
                    if (Convert.ToInt32(radioGroupType.Tag) == 2)
                    {
                        if (quantity > 1)
                        {
                            gvInventory.SetFocusedRowCellValue(Quantity, inventoryNum);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 列表行修改之前事件
        private void gvInventory_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == Quantity)
                {
                    if (gvInventory.GetFocusedRowCellValue(Quantity) == null)
                    {
                        inventoryNum = 0;
                    }
                    else
                    {
                        inventoryNum = Convert.ToInt32(gvInventory.GetFocusedRowCellValue(Quantity));
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得发货行明细数据
        public void GetInventoryItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teInventoryNo.Text.Trim()
                };
                inventoryItem.Clear();
                if (DevCommon.getDataByWebService("InventotyItemSearch", "InventotyItemSearch", searchConditions, ref inventoryItem) == RetCode.OK)
                {
                    gcInventory.DataSource = inventoryItem;
                }
                else
                {
                    gcInventory.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcInventory.DataSource = null;
                throw ex;
            }
        }
        #endregion

        #region 根据指令单号获得仓库
        private void commandFacility(string docId)
        {
            try
            {
                var searchCondition = new
                {
                    docId = docId
                };
                object obj = null;
                if (DevCommon.getDataByWebService("SearchCommandFacility", "SearchCommandFacility", searchCondition, ref obj) == RetCode.OK)
                {
                    if (obj != null)
                    {
                        DataTable dtFacility = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                        DevCommon.initCmb(cboFacility, dtFacility, "facilityId", "facilityName", false);
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据指令单号获得要盘点的明细数据
        private void CommandItem(string docId)
        {
            try
            {
                var searchItem = new
                {
                    docId = docId
                };
                detailCommand = null;
                if (DevCommon.getDataByWebService("SearchInventiryItemCommand", "SearchInventiryItemCommand", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        orderHeader = null;
                        return;
                    }
                    else
                    {
                        if (detailCommand.Count > 0)
                        {
                            inventoryItem.Clear();
                            gcInventory.DataSource = null;
                            for (int i = 0; i < detailCommand.Count; i++)
                            {
                                InsertInventoryItem(detailCommand[i]);
                            }
                            gcInventory.DataSource = inventoryItem;
                            gcInventory.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 盘点明细赋值
        private void InsertInventoryItem(InventotyDetailItemCommand command)
        {
            try
            {
                InventotyDetailItem detail = new InventotyDetailItem();
                detail.docId = teInventoryNo.Text.Trim();
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.facilityId = command.facilityId;
                detail.facilityName = command.facilityName;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = inventoryItem.Count.ToString();
                detail.memo = "";
                detail.onHand = command.onHand;
                detail.productId = command.productId;
                detail.productName = command.productName;
                //detail.sequenceId = command.sequenceId;
                inventoryItem.Add(detail);
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据盘点单号获得盘点指令头数据
        private void beOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(beOrder.Text.Trim()))
                    {
                        GetCommandHeaderData(beOrder.Text.Trim());
                        if (orderHeader != null)
                        {
                            CommandItem(orderHeader.docId);
                            if (inventoryItem.Count > 0)
                            {
                                commandFacility(orderHeader.docId);
                                gcInventory.DataSource = inventoryItem;
                                gcInventory.RefreshDataSource();
                                beOrder.Value = detailCommand[0].docId;
                                meCommandMemo.Text = orderHeader.memo;
                                return;
                            }
                        }
                    }
                    else
                    {
                        orderHeader = null;
                        inventoryItem.Clear();
                        detailCommand = null;
                        gcInventory.DataSource = null;
                        gcInventory.RefreshDataSource();
                        meCommandMemo.Text = "";
                        //获得仓库数据
                        GetFacilityData();
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据指令单号获得指令单头数据
        public void GetCommandHeaderData(string commandNo)
        {
            var searchCondition = new
            {
                docId = commandNo
            };
            List<InventoryHeaderDetailCommand> headerCommandItem = null;
            if (DevCommon.getDataByWebService("SearchInventiryHeaderCommandByDocId", "SearchInventiryHeaderCommandByDocId", searchCondition, ref headerCommandItem) == RetCode.OK)
            {
                if (headerCommandItem == null)
                {
                    orderHeader = null;
                    beOrder.ShowForm();
                    return;
                }
                else
                {
                    if (Convert.ToInt32(headerCommandItem[0].status) == Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        orderHeader = null;
                        m_frm.PromptInformation("已完成的盘点单！");
                        return;
                    }
                    else
                    {
                        orderHeader = headerCommandItem[0];
                    }
                }
            }
        }
        #endregion

        #region 类型改变事件
        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(radioGroupType.Tag) == 1)
            {
                btnSequence.Enabled = false;
                btnProductId.Enabled = true;
                btnProductName.Enabled = true;
                inventoryItem.Clear();
                gcInventory.DataSource = null;
            }
            if (Convert.ToInt32(radioGroupType.Tag) == 2)
            {
                btnProductId.Enabled = false;
                btnProductName.Enabled = false;
                btnSequence.Enabled = true;
                inventoryItem.Clear();
                gcInventory.DataSource = null;
            }
        }
        #endregion
    }
}