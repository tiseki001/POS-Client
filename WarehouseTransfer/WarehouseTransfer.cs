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
using Commons.Model;
using System.Linq;

namespace WarehouseTransfer
{
    public partial class WarehouseTransfer : BaseForm
    {
        #region 参数
        public TransferHeaderDetailCommand headerCommand = null;
        public WarehouseTransferOrder order = null;
        public WarehouseTransferSearch search = null;
        public TransferHeaderDetail headerItem = null;
        List<TransferItemDetail> transferItem = new List<TransferItemDetail>();
        List<TransferItemDetailCommand> detailCommand = new List<TransferItemDetailCommand>();
        int deliveryType = 0;
        List<TransferProduct> transferProduct = null;
        string facilityId = "";
        DataTable dtMoveType = null;
        int transferNum = 0;
        #endregion

        #region 构造
        public WarehouseTransfer()
        {
            InitializeComponent();
        }
        #endregion

        #region  取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "移库单查询";
                this.Close();
            }
            else if (order != null)
            {
                order.Visible = true;
                order.BringToFront();
                ((XtraTabPage)this.Parent).Text = "移库指令单";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 加载事件
        private void WarehouseTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                //获得移动类型
                GetMoveTypeDate();
                //获得状态数据
                GetStatusData();
                //单据状态禁用
                cboDocStatus.Enabled = false;
                //故障类型
                FaultType();
                if (search != null)
                {
                    pcHeader.Enabled = false;
                    teDocDate.Text = headerItem.updateDate;
                    teUserName.Text = headerItem.updateUserName;
                    cboDocStatus.EditValue = headerItem.docStatus;
                    cboMoveType.EditValue = headerItem.movementTypeId;
                    beOrder.Text = headerItem.baseEntry;
                    beOrder.Enabled = false;
                    teDocId.Text = headerItem.docId;
                    meMemo.Text = headerItem.memo;
                    deliveryType = string.IsNullOrEmpty(headerItem.baseEntry) ? 0 : 1;
                    if (Convert.ToInt32(headerItem.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        pcCondition.Enabled = false;
                        btnDraft.Enabled = false;
                        btnDeleteRow.Enabled = false;
                        gvTransfer.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvTransfer.Columns["planQuantity"].Visible = false;
                        gvTransfer.Columns["deliveryQuantity"].Visible = false;
                        gvTransfer.Columns["availableQuantity"].Visible = false;
                        gvTransfer.Columns["memo"].OptionsColumn.ReadOnly = true;
                        gvTransfer.Columns["faultTypeId"].OptionsColumn.AllowEdit = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(headerItem.baseEntry))
                        {
                            gvTransfer.Columns["deliveryQuantity"].Visible = false;
                            gvTransfer.Columns["planQuantity"].Visible = false;
                        }
                    }
                    beOrder.Enabled = false;
                    TransferItem();
                    gcTransfer.DataSource = transferItem;
                    gcTransfer.RefreshDataSource();
                }
                else if (order != null)
                {
                    deliveryType = 1;
                    //获得单号
                    GetDocId();
                    //移库类型禁用
                    cboMoveType.Enabled = false;
                    meCommandMemo.Text = headerCommand.memo;
                    teDocDate.Text = headerCommand.updateDate;
                    teUserName.Text = LoginInfo.UserName;
                    cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    beOrder.Text = headerCommand.docId;
                    beOrder.Enabled = false;
                    cboMoveType.EditValue = headerCommand.movementTypeId;
                    GetCommandItemData(headerCommand.docId);
                    if (transferItem.Count > 0)
                    {
                        gcTransfer.DataSource = transferItem;
                        gcTransfer.RefreshDataSource();
                    }
                    GetFacilityIdByMoveTypeId();
                }
                else
                {
                    //制单时间
                    teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //制单人
                    teUserName.Text = LoginInfo.UserName;
                    //单据状态
                    cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    //获得单号
                    GetDocId();
                    GetFacilityIdByMoveTypeId();
                    gvTransfer.Columns["deliveryQuantity"].Visible = false;
                    gvTransfer.Columns["planQuantity"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮删除行数据事件
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (gvTransfer.RowCount > 0)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvTransfer.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 右键菜单
        private void gvTransfer_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (gvTransfer.RowCount > 0)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvTransfer.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 获得移动类型数据
        public void GetMoveTypeDate()
        {
            try
            {
                var searchConditions = new
                {
                    orderType = MoveType.warehouseTransfer,
                    productStoreId = LoginInfo.ProductStoreId
                };
                dtMoveType = null;
                if (DevCommon.getDataByWebService("MoveType", "MoveSearch", searchConditions, ref dtMoveType) == RetCode.OK)
                {
                    if (cboMoveType != null)
                    {
                        DevCommon.initCmb(cboMoveType, dtMoveType, "movementTypeId", "movementTypeName", true);
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
                    DevCommon.initCmb(cboDocStatus, dtStatus, false);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }

        }
        #endregion

        #region 获得调拨发货单号
        public void GetDocId()
        {
            try
            {
                string docId = null;
                if (DevCommon.getDocId(DocType.WarehouseTransfer, ref docId) == RetCode.OK)
                {
                    teDocId.Text = docId;
                }
                else
                {
                    teDocId.Text = "";
                }
            }
            catch (Exception ex)
            {
                teDocId.Text = "";
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加列表故障类型数据
        public void FaultType()
        {
            try
            {
                DataTable faultType = DevCommon.FaultType();
                if (faultType != null)
                {
                    DevCommon.initCmb(repositoryItemImageComboBox1, faultType, "faultTypeId", "memo", false);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 回车事件选择移库单事件
        private void beOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(beOrder.Text.Trim()))
                    {
                        GetCommandHeaderData(beOrder.Text.Trim());
                        if (headerCommand != null)
                        {
                            if (Convert.ToInt32(string.IsNullOrEmpty(headerCommand.status) ? "0" : headerCommand.status) == Convert.ToInt32(BusinessStatus.CLEARED))
                            {
                                m_frm.PromptInformation("此出库单为已清单据");
                                headerCommand = null;
                                return;
                            }
                            GetCommandItemData(headerCommand.docId);
                            if (transferItem.Count > 0)
                            {
                                deliveryType = 1;
                                gcTransfer.DataSource = transferItem;
                                gcTransfer.RefreshDataSource();
                                cboMoveType.Enabled = false;
                                beOrder.Value = detailCommand[0].docId;
                                cboMoveType.EditValue = headerCommand.movementTypeId;
                                meCommandMemo.Text = headerCommand.memo;
                                gvTransfer.Columns["deliveryQuantity"].Visible = true;
                                gvTransfer.Columns["deliveryQuantity"].VisibleIndex = 6;
                                gvTransfer.Columns["planQuantity"].Visible = true;
                                gvTransfer.Columns["planQuantity"].VisibleIndex = 7;
                                return;
                            }
                        }
                    }
                    else
                    {
                        deliveryType = 0;
                        cboMoveType.Enabled = true;
                        cboMoveType.SelectedIndex = 0;
                        headerCommand = null;
                        transferItem.Clear();
                        gcTransfer.DataSource = null;
                        gcTransfer.RefreshDataSource();
                        meCommandMemo.Text = "";
                        meMemo.Text = "";
                        Clear();
                        cboMoveType.SelectedIndex = -1;
                        gvTransfer.Columns["deliveryQuantity"].Visible = false;
                        gvTransfer.Columns["planQuantity"].Visible = false;
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
        public void TransferItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teDocId.Text.Trim(),
                    orderType = MoveType.warehouseTransfer
                };
                transferItem = null;
                if (DevCommon.getDataByWebService("GoodsDeliveryItem", "GoodsDeliveryItem", searchConditions, ref transferItem) == RetCode.OK)
                {
                    gcTransfer.DataSource = transferItem;
                }
                else
                {
                    gcTransfer.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcTransfer.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据指令单号获得发货指令明细
        public void GetCommandItemData(string commandNo)
        {
            try
            {
                var searchItem = new
                {
                    docId = commandNo
                };
                detailCommand = null;
                if (DevCommon.getDataByWebService("TransferItemCommand", "TransferItemCommand", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        headerCommand = null;
                        return;
                    }
                    else
                    {
                        List<TransferItemDetailCommand> item = detailCommand.Where(p => (p.quantity - p.deliveryQuantity) > 0).ToList();
                        if (item.Count == 0)
                        {
                            headerCommand = null;
                            return;
                        }
                        else
                        {
                            transferItem.Clear();
                            gcTransfer.DataSource = null;
                            for (int i = 0; i < item.Count; i++)
                            {
                                if (item[i].isSequence == ProductSequenceManager.sequenceY && string.IsNullOrEmpty(item[i].sequenceId))
                                {
                                    for (int j = 0; j < item[i].quantity - item[i].deliveryQuantity; j++)
                                    {
                                        InsertTransferItem(item[i]);
                                    }
                                }
                                else
                                {
                                    InsertTransferItem(item[i]);
                                }
                            }
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

        #region 移库单赋值
        public void InsertTransferItem(TransferItemDetailCommand command)
        {
            try
            {
                TransferItemDetail detail = new TransferItemDetail();
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.config = command.config;
                detail.planQuantity = command.quantity;
                detail.deliveryQuantity = command.deliveryQuantity;
                detail.docId = teDocId.Text.Trim();
                detail.facilityId = command.facilityId;
                detail.facilityIdTo = command.facilityIdTo;
                detail.facilityName = command.facilityName;
                detail.facilityNameTo = command.facilityNameTo;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = transferItem.Count.ToString();
                detail.memo = "";
                detail.onHand = command.onHand;
                detail.promise = command.promise;
                detail.productId = command.productId;
                detail.productName = command.productName;
                detail.sequenceId = command.sequenceId;
                detail.isSequence = command.isSequence;
                detail.faultTypeId = command.faultTypeId;
                transferItem.Add(detail);
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
                docId = commandNo,
                orderType = MoveType.warehouseTransfer,
                productStoreId = LoginInfo.ProductStoreId
            };
            List<TransferHeaderDetailCommand> headerCommandItem = null;
            if (DevCommon.getDataByWebService("TransfertCommand", "TransfertCommand", searchCondition, ref headerCommandItem) == RetCode.OK)
            {
                if (headerCommandItem == null)
                {
                    headerCommand = null;
                    beOrder.ShowForm();
                    return;
                }
                else
                {
                    if (Convert.ToInt32(headerCommandItem[0].status) == Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        headerCommand = null;
                        return;
                    }
                    else
                    {
                        headerCommand = headerCommandItem[0];
                    }
                }
            }
        }
        #endregion

        #region 文本框清空
        public void Clear()
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

        #region 保存确定单据事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    InsertAndUpdateTransfer(DocStatus.VALID);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 保存草稿事件
        private void btnDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    InsertAndUpdateTransfer(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加或修改移库数据方法
        public void InsertAndUpdateTransfer(DocStatus type)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    #region 添加头数据
                    TransferHeader header = new TransferHeader();
                    //单号
                    header.docId = teDocId.Text.Trim();
                    if (headerCommand != null)
                    {
                        header.userId = LoginInfo.PartyId;
                        header.productStoreIdTo = headerCommand.productStoreIdTo;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.docDate = teDocDate.Text;
                    }
                    else if (headerItem != null)
                    {
                        header.userId = headerItem.userId;
                        header.productStoreIdTo = headerItem.productStoreIdTo;
                        header.status = headerItem.status;
                        header.docDate = headerItem.docDate;
                    }
                    else
                    {
                        header.productStoreIdTo = LoginInfo.ProductStoreId;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.userId = LoginInfo.PartyId;
                        header.docDate = teDocDate.Text;
                    }
                    if (Convert.ToInt32(DocStatus.DRAFT) == Convert.ToInt32(cboDocStatus.EditValue))
                    {
                        header.postingDate = DevCommon.PostingDate();
                    }
                    else
                    {
                        if (headerItem != null)
                        {
                            header.postingDate = headerItem.postingDate;
                        }
                        else
                        {
                            header.postingDate = DevCommon.PostingDate();
                        }
                    }
                    header.updateUserId = LoginInfo.PartyId;
                    header.updateDate = teDocDate.Text.Trim();
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.baseEntry = beOrder.Text.Trim();
                    header.partyId = LoginInfo.OwnerPartyId;
                    header.movementTypeId = cboMoveType.EditValue.ToString();
                    header.docStatus = Convert.ToInt32(type).ToString();
                    header.memo = meMemo.Text.Trim();
                    if (type == DocStatus.DRAFT)
                    {
                        header.isSync = "";
                    }
                    if (type == DocStatus.VALID)
                    {
                        header.isSync = "0";
                    }
                    #endregion

                    #region 添加发货行明细
                    List<TransferItemDetail> itemDetail = new List<TransferItemDetail>();
                    if (Convert.ToInt32(type) == Convert.ToInt32(DocStatus.VALID))
                    {
                        itemDetail = (List<TransferItemDetail>)transferItem.Where(p => p.quantity > 0).ToList();
                    }
                    else if (Convert.ToInt32(type) == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = transferItem;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要移库的数据！");
                        return;
                    }
                    List<TransferItem> item = (List<TransferItem>)itemDetail.Select(p => new TransferItem
                    {
                        docId = p.docId,
                        lineNo = p.lineNo,
                        productId = p.productId,
                        facilityId = p.facilityId,
                        facilityIdTo = p.facilityIdTo,
                        memo = p.memo,
                        quantity = p.quantity == null ? 0 : p.quantity,
                        baseEntry = p.baseEntry,
                        baseLineNo = p.baseLineNo,
                        sequenceId = p.sequenceId,
                        isSequence = p.isSequence,
                        idValue = p.idValue,
                        faultTypeId = p.faultTypeId
                    }).ToList();
                    #endregion

                    #region 添加或修改收货数据操作
                    if (search != null)
                    {
                        if (UpdateHeaderAndItem(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "移库单查询";
                            this.Close();
                            search.Visible = true;
                            search.BringToFront();
                            search.Data();
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (order != null)
                    {
                        if (Insert(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "移库指令单";
                            this.Close();
                            order.Visible = true;
                            order.BringToFront();
                            order.Reload();
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (Insert(header, item))
                        {
                            headerCommand = null;
                            beOrder.Text = "";
                            meMemo.Text = "";
                            meCommandMemo.Text = "";
                            GetDocId();
                            gcTransfer.DataSource = null;
                            transferItem.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                            cboMoveType.Enabled = true;
                            //cboMoveType.SelectedIndex = 0;
                        }
                        else
                        {
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

        #region 添加移库数据
        public bool Insert(TransferHeader header, List<TransferItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("GoodsTransferInsert", "GoodsTransferInsert", searchCondition, ref result) == RetCode.OK)
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

        #region 修改移库单头数据和明细数据
        public bool UpdateHeaderAndItem(TransferHeader header, List<TransferItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsTransfer", "UpdateGoodsTransfer", searchCondition, ref result) == RetCode.OK)
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
        private void KeyPeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && string.IsNullOrEmpty(((SearchButton)sender).Text.Trim()))
                {
                    SendKeys.Send("{Tab}");
                }
                if (e.KeyChar == 13 && !string.IsNullOrEmpty(((SearchButton)sender).Text.Trim()))
                {
                    //输入条件判断
                    if (((SearchButton)sender).Text.Trim().Length < 4)
                    {
                        m_frm.PromptInformation("输入的条件不能少于4位！");
                        return;
                    }
                    ProductSearch condition = new ProductSearch();
                    condition.productStoreId = LoginInfo.ProductStoreId;
                    condition.movementTypeId = cboMoveType.EditValue.ToString();

                    #region 根据商品代码、商品名称、条形码查询
                    if (sender.Equals(btnProductId) || sender.Equals(btnProductName) || sender.Equals(btnSerial))
                    {
                        if (sender.Equals(btnProductId))
                        {
                            condition.productId = btnProductId.Text.Trim();
                        }
                        else if (sender.Equals(btnProductName))
                        {
                            if (string.IsNullOrEmpty(btnProductName.Value))
                            {
                                condition.productName = btnProductName.Text.Trim();
                            }
                            else
                            {
                                condition.productId = btnProductName.Value.Trim();
                                condition.productName = btnProductName.Text.Trim();
                            }
                        }
                        else if (sender.Equals(btnSerial))
                        {
                            if (string.IsNullOrEmpty(btnSerial.Value))
                            {
                                condition.idValue = btnSerial.Text.Trim();
                            }
                            else
                            {
                                condition.productId = btnSerial.Value.Trim();
                                condition.idValue = btnSerial.Text.Trim();
                            }
                        }
                        #region 指令移库
                        if (deliveryType == 1)
                        {
                            var con = new
                            {
                                condition.productId,
                                condition.productName,
                                condition.idValue
                            };
                            transferProduct = null;
                            if (DevCommon.getDataByWebService("ProductInfo", "ProductInfo", con, ref transferProduct) == RetCode.OK)
                            {
                                AddTransferRowDataNotSeq(transferProduct, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion

                        #region 自主移库
                        if (deliveryType == 0)
                        {
                            if (string.IsNullOrEmpty(facilityId))
                            {
                                m_frm.PromptInformation("请选择正确的移动类型!");
                                return;
                            }
                            condition.facilityId = facilityId;
                            var con = new
                            {
                                productId = condition.productId,
                                idValue = condition.idValue,
                                productName = condition.productName,
                                facilityId = condition.facilityId,
                                productStoreId = condition.productStoreId
                            };
                            transferProduct = null;
                            if (DevCommon.getDataByWebService("ProductAndSequenceSearch", "ProductAndSequenceSearch", con, ref transferProduct) == RetCode.OK)
                            {
                                AddDeliveryRowData(transferProduct, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region 根据商品串号查询
                    if (sender.Equals(btnSequence))
                    {
                        if (sender.Equals(btnSequence))
                        {
                            condition.sequenceId = btnSequence.Text.Trim();
                        }

                        #region 指令移库
                        if (deliveryType == 1)
                        {
                            transferProduct = null;
                            var con = new
                            {
                                sequenceId = condition.sequenceId,
                                productStoreId = condition.productStoreId
                            };
                            if (DevCommon.getDataByWebService("ProductSequenceInfo", "ProductSequenceInfo", con, ref transferProduct) == RetCode.OK)
                            {
                                AddTransferRowDataSeq(transferProduct, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion

                        #region 自主移库
                        if (deliveryType == 0)
                        {
                            if (string.IsNullOrEmpty(facilityId))
                            {
                                m_frm.PromptInformation("请选择正确的移动类型!");
                                return;
                            }
                            condition.facilityId = facilityId;
                            var con = new
                            {
                                facilityId = condition.facilityId,
                                sequenceId = condition.sequenceId,
                                productStoreId = condition.productStoreId
                            };
                            transferProduct = null;
                            if (DevCommon.getDataByWebService("ProductAndSequenceSearch", "ProductAndSequenceSearch", con, ref transferProduct) == RetCode.OK)
                            {
                                AddDeliveryRowData(transferProduct, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion
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

        #region 根据指令移库时，添加移库行的移库数据
        public void AddTransferRowDataNotSeq(List<TransferProduct> list, SearchButton button)
        {
            if (list != null)
            {
                //根据条件没有查询到数据，进入到搜索帮助页面
                if (list.Count == 0)
                {
                    button.ShowForm();
                }
                //根据条件搜索到一条符合的数据，数据进行赋值
                if (list.Count == 1)
                {
                    if (list[0].isSequence == ProductSequenceManager.sequenceY)
                    {
                        m_frm.PromptInformation("商品：" + list[0].ProductId + "是串号管理商品，请输入串号！");
                        Clear();
                        return;
                    }
                    List<TransferItemDetail> listDetail = transferItem.Where(p =>
                        p.productId == list[0].ProductId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceN).ToList();
                    if (listDetail.Count > 0)
                    {
                        bool sign = false;
                        for (int i = 0; i < transferItem.Count; i++)
                        {
                            if (transferItem[i].productId == list[0].ProductId && transferItem[i].isSequence == ProductSequenceManager.sequenceN)
                            {
                                int planQuantity = transferItem[i].planQuantity == null ? 0 : (int)transferItem[i].planQuantity;
                                int deliveryQuantity = transferItem[i].deliveryQuantity == null ? 0 : (int)transferItem[i].deliveryQuantity;
                                int quantity = transferItem[i].quantity == null ? 0 : (int)transferItem[i].quantity;
                                if (quantity < planQuantity - deliveryQuantity)
                                {
                                    transferItem[i].quantity = quantity + 1;
                                    gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                    gcTransfer.RefreshDataSource();
                                    sign = true;
                                    Clear();
                                    return;
                                }
                            }
                        }
                        if (!sign)
                        {
                            m_frm.PromptInformation("商品：" + list[0].ProductId + "在此移库单移库数量已满足！");
                            Clear();
                            return;
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("商品：" + list[0].ProductId + "不在此移库单！");
                    }
                }
                //查询到的值是多条数据，进入搜索帮助页面
                if (list.Count > 1)
                {
                    button.ShowForm();
                    return;
                }
            }
            else
            {
                button.ShowForm();
                return;
            }
        }
        #endregion

        #region 根据指令移库时，添加移库行的移库数据
        public void AddTransferRowDataSeq(List<TransferProduct> list, SearchButton button)
        {
            if (list != null)
            {
                //根据条件没有查询到数据，进入到搜索帮助页面
                if (list.Count == 0)
                {
                    m_frm.PromptInformation("没有搜索到相关商品！");
                    return;
                }
                //根据条件搜索到一条符合的数据，数据进行赋值
                if (list.Count == 1)
                {
                    List<TransferItemDetail> listDetail = transferItem.Where(p =>
                        !string.IsNullOrEmpty(p.sequenceId)
                        && p.sequenceId == list[0].SequenceId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceY).ToList();
                    if (listDetail.Count > 0)
                    {
                        for (int i = 0; i < transferItem.Count; i++)
                        {
                            if (transferItem[i].productId == list[0].ProductId
                                && transferItem[i].isSequence == ProductSequenceManager.sequenceY
                                && transferItem[i].sequenceId == list[0].SequenceId)
                            {
                                int quantity = transferItem[i].quantity == null ? 0 : (int)transferItem[i].quantity;
                                if (quantity > 0)
                                {
                                    m_frm.PromptInformation("串号：" + list[0].SequenceId + "在此移库单移库数量已满足！");
                                    gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                    Clear();
                                    return;
                                }
                                else
                                {
                                    transferItem[i].quantity = quantity + 1;
                                    gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                    gcTransfer.RefreshDataSource();
                                    Clear();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("串号：" + list[0].SequenceId + "不在此移库单！");
                    }
                }
                //查询到的值是多条数据，进入搜索帮助页面
                if (list.Count > 1)
                {
                    button.ShowForm();
                    return;
                }
            }
            else
            {
                m_frm.PromptInformation("没有搜索到相关商品！");
                return;
            }
        }
        #endregion

        #region 自主移库，添加行数据
        public void AddDeliveryRowData(List<TransferProduct> list, SearchButton button)
        {
            try
            {
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        if (transferItem.Count > 0)
                        {
                            if (list[0].isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                #region 串号管理
                                //判断串号是否被占用
                                if (list[0].isOccupied == SequenceOccuepied.occupied)
                                {
                                    m_frm.PromptInformation("串号:" + list[0].SequenceId + "已被占用！");
                                    Clear();
                                    return;
                                }
                                List<TransferItemDetail> item = transferItem.Where(p =>
                                !string.IsNullOrEmpty(p.sequenceId)
                                && p.productId == list[0].ProductId
                                && p.sequenceId == list[0].SequenceId
                                ).ToList();
                                if (item.Count == 0)
                                {
                                    if (string.IsNullOrEmpty(list[0].SequenceId))
                                    {
                                        m_frm.PromptInformation("商品:" + list[0].ProductId + "是串号管理的商品，请输入串号!");
                                        Clear();
                                        return;
                                    }
                                    else
                                    {
                                        InsertTransferData(list[0]);
                                        return;
                                    }
                                }
                                else
                                {
                                    int sumDelivery = transferItem.Where(p => p.productId.Equals(list[0].ProductId)).Sum(p => p.quantity) == null ? 0 : (int)transferItem.Where(p => p.productId.Equals(list[0].ProductId)).Sum(p => p.quantity);
                                    for (int i = 0; i < transferItem.Count; i++)
                                    {
                                        int quantity = transferItem[i].quantity == null ? 0 : (int)transferItem[i].quantity;
                                        int availableQuantity = transferItem[i].availableQuantity == null ? 0 : (int)transferItem[i].availableQuantity;
                                        //可以库存不足
                                        if (availableQuantity <= 0)
                                        {
                                            m_frm.PromptInformation("商品:" + transferItem[i].productId + "可用库存不足！");
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            Clear();
                                            return;
                                        }
                                        //列表已存在此串号信息
                                        if (transferItem[i].productId == list[0].ProductId
                                              && transferItem[i].sequenceId == list[0].SequenceId
                                              && quantity > 0
                                            )
                                        {
                                            m_frm.PromptInformation("串号:" + list[0].SequenceId + "已在移库列表！");
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            Clear();
                                            return;
                                        }
                                        //串号信息存在，添加发货数量
                                        if (transferItem[i].productId == list[0].ProductId
                                              && transferItem[i].sequenceId == list[0].SequenceId
                                              && availableQuantity - sumDelivery > 0
                                              && quantity == 0
                                            )
                                        {
                                            transferItem[i].quantity = 1;
                                            gcTransfer.DataSource = transferItem;
                                            gcTransfer.RefreshDataSource();
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            gcTransfer.RefreshDataSource();
                                            Clear();
                                            return;
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (list[0].isSequence.Equals(ProductSequenceManager.sequenceN))
                            {
                                #region 非串号管理
                                //判断库存数量是否满足
                                int onhand = list[0].OnHand == null ? 0 : (int)list[0].OnHand;
                                int promime = list[0].Promise == null ? 0 : (int)list[0].Promise;
                                if (onhand <= promime)
                                {
                                    m_frm.PromptInformation("商品:" + list[0].ProductId + "库存不足！");
                                    Clear();
                                    return;
                                }
                                List<TransferItemDetail> item = transferItem.Where(p =>
                                    p.productId == list[0].ProductId
                                    && p.isSequence == ProductSequenceManager.sequenceN).ToList();
                                if (item.Count == 0)
                                {
                                    InsertTransferData(list[0]);
                                }
                                else
                                {
                                    for (int i = 0; i < transferItem.Count; i++)
                                    {
                                        int availableQuantity = transferItem[i].availableQuantity == null ? 0 : (int)transferItem[i].availableQuantity;
                                        int quantity = transferItem[i].quantity == null ? 0 : (int)transferItem[i].quantity;
                                        if (availableQuantity <= 0)
                                        {
                                            m_frm.PromptInformation("商品:" + transferItem[i].productId + "可用库存不足！");
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            Clear();
                                            return;
                                        }
                                        if (transferItem[i].productId == list[0].ProductId
                                              && availableQuantity - quantity > 0
                                              && transferItem[i].isSequence == ProductSequenceManager.sequenceN
                                            )
                                        {
                                            transferItem[i].quantity = transferItem[i].quantity + 1;
                                            gcTransfer.DataSource = transferItem;
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            gcTransfer.RefreshDataSource();
                                            Clear();
                                            return;
                                        }
                                        if (transferItem[i].productId == list[0].ProductId
                                            && availableQuantity == quantity
                                            && transferItem[i].isSequence == ProductSequenceManager.sequenceN)
                                        {
                                            m_frm.PromptInformation("商品:" + list[0].ProductId + "移库数量等于可以库存数！");
                                            gcTransfer.RefreshDataSource();
                                            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(i);
                                            Clear();
                                            return;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            if (list[0].isSequence == ProductSequenceManager.sequenceY)
                            {
                                //判断串号是否被占用
                                if (list[0].isOccupied == SequenceOccuepied.occupied)
                                {
                                    m_frm.PromptInformation("串号:" + list[0].SequenceId + "已被占用！");
                                    Clear();
                                    return;
                                }
                                if (string.IsNullOrEmpty(list[0].SequenceId))
                                {
                                    m_frm.PromptInformation("商品:" + list[0].ProductId + "是串号管理商品，请输入串号！");
                                    Clear();
                                    return;
                                }
                                else
                                {
                                    InsertTransferData(list[0]);
                                    return;
                                }
                            }
                            else if (list[0].isSequence == ProductSequenceManager.sequenceN)
                            {
                                //判断库存数量是否满足
                                int onhand = list[0].OnHand == null ? 0 : (int)list[0].OnHand;
                                int promime = list[0].Promise == null ? 0 : (int)list[0].Promise;
                                if (onhand <= promime)
                                {
                                    m_frm.PromptInformation("商品:" + list[0].ProductId + "库存不足！");
                                    Clear();
                                    return;
                                }
                                InsertTransferData(list[0]);
                                return;
                            }
                            return;
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
                    m_frm.PromptInformation("没有搜索到相关商品!");
                    Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加发货数据
        public void InsertTransferData(TransferProduct product)
        {
            TransferItemDetail itemDetail = new TransferItemDetail();
            itemDetail.lineNo = transferItem.Count.ToString();
            itemDetail.docId = teDocId.Text;
            itemDetail.productId = product.ProductId;
            itemDetail.productName = product.ProductName;
            itemDetail.sequenceId = product.SequenceId;
            itemDetail.idValue = product.IdValue;
            itemDetail.onHand = product.OnHand;
            itemDetail.promise = product.Promise;
            itemDetail.facilityName = product.FacilityName;
            itemDetail.facilityId = product.FacilityId;
            itemDetail.config = product.Config;
            itemDetail.isSequence = product.isSequence;
            itemDetail.sequenceId = product.SequenceId;
            itemDetail.quantity = 1;
            transferItem.Add(itemDetail);
            gcTransfer.DataSource = transferItem;
            gvTransfer.FocusedRowHandle = gvTransfer.GetRowHandle(transferItem.Count - 1);
            gcTransfer.RefreshDataSource();
            Clear();
        }
        #endregion

        #region 移动类型值改变事件
        private void cboMoveType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (headerCommand != null)
                {
                    return;
                }
                else if (headerItem != null)
                {
                    return;
                }
                else
                {
                    beOrder.Text = "";
                    gcTransfer.DataSource = null;
                    headerCommand = null;
                    transferItem.Clear();
                    GetFacilityIdByMoveTypeId();
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据移动类型获得仓库ID
        public void GetFacilityIdByMoveTypeId()
        {
            facilityId = DevCommon.GetFacilityId(LoginInfo.ProductStoreId, cboMoveType.EditValue.ToString());
        }
        #endregion

        #region 修改列表事件
        private void gvTransfer_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == Quantity)
                {
                    string isSequence = gvTransfer.GetFocusedRowCellValue("isSequence") == null ? "" : gvTransfer.GetFocusedRowCellValue("isSequence").ToString();
                    if (gvTransfer.GetFocusedRowCellValue(Quantity) == null
                        || Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(Quantity)) == 0)
                    {
                        return;
                    }
                    if (Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(Quantity)) < 0)
                    {
                        gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                        return;
                    }
                    if (!string.IsNullOrEmpty(isSequence))
                    {
                        #region 自主调拨
                        if (deliveryType == 0)
                        {
                            int deliveryNumAfter = gvTransfer.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(Quantity));
                            int availableQuantity = gvTransfer.GetFocusedRowCellValue(AvailableQuantity) == null ? 0 : Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(AvailableQuantity));
                            int sumDelivery = (int)transferItem.Where(p => p.productId.Equals(gvTransfer.GetFocusedRowCellValue(productId))).Sum(p => p.quantity);
                            string sequenceId = gvTransfer.GetFocusedRowCellValue("sequenceId") == null ? null : gvTransfer.GetFocusedRowCellValue("sequenceId").ToString();
                            if (isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                if (string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 1)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 1)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId) && (deliveryNumAfter > 1 || sumDelivery > availableQuantity))
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, 0);
                                    return;
                                }
                            }
                            else if (isSequence.Equals(ProductSequenceManager.sequenceN))
                            {
                                if (deliveryNumAfter > availableQuantity
                                    || availableQuantity <= 0)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                            }
                        }
                        #endregion

                        #region 指令调拨
                        if (deliveryType == 1)
                        {
                            int planQuantity = gvTransfer.GetFocusedRowCellValue(PlanDeliveryQuantity) == null ? 0 : (int)gvTransfer.GetFocusedRowCellValue(PlanDeliveryQuantity);
                            int deliveryQuantity = gvTransfer.GetFocusedRowCellValue(DeliveryQuantity) == null ? 0 : Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(DeliveryQuantity));
                            int deliveryNumAfter = gvTransfer.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(Quantity));
                            string sequenceId = gvTransfer.GetFocusedRowCellValue("sequenceId") == null ? null : gvTransfer.GetFocusedRowCellValue("sequenceId").ToString();
                            if (isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                if (string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 0)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId)
                                    && deliveryNumAfter > 1)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                            }
                            else if (isSequence.Equals(ProductSequenceManager.sequenceN))
                            {
                                if (deliveryNumAfter > planQuantity - deliveryQuantity)
                                {
                                    gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        gvTransfer.SetFocusedRowCellValue(Quantity, transferNum);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 修改列表事件
        private void gvTransfer_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvTransfer.RowCount > 0)
                {
                    if (e.Column == Quantity)
                    {
                        transferNum = gvTransfer.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvTransfer.GetFocusedRowCellValue(Quantity));
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion
    }
}