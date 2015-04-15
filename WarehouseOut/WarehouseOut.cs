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
using Commons.Model;
using System.Linq;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using Print;

namespace WarehouseOut
{
    public partial class WarehouseOut : BaseForm
    {
        #region 参数
        public WarehouseOutOrder order = null;
        public WarehouseOutHeaderDetailCommand headerCommand = null;
        public WarehouseOutSearch search = null;
        public WarehouseOutHeaderDetail headerItem = null;
        List<WarehouseOutItemDetail> whsOut = new List<WarehouseOutItemDetail>();
        List<WarehouseOutItemCommandDetail> detailCommand = new List<WarehouseOutItemCommandDetail>();
        List<WhsOutProduct> productList = null;
        int whsOutNum = 0;
        #endregion

        #region 构造
        public WarehouseOut()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void WarehouseOut_Load(object sender, EventArgs e)
        {
            try
            {
                //获得移动类型
                GetMoveTypeDate();
                //获得状态数据
                GetStatusData();
                //禁用移动类型
                cboMoveType.Enabled = false;
                //单据状态禁用
                cboDocStatus.Enabled = false;
                if (search != null)
                {
                    pcHeader.Enabled = false;
                    teDocDate.Text = headerItem.updateDate;
                    teUserName.Text = headerItem.updateUserName;
                    cboDocStatus.EditValue = headerItem.docStatus;
                    cboMoveType.EditValue = headerItem.movementTypeId;
                    beOrder.Text = headerItem.baseEntry;
                    beOrder.Enabled = false;
                    teWhsOutNo.Text = headerItem.docId;
                    meWhsOutMemo.Text = headerItem.memo;
                    if (Convert.ToInt32(headerItem.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        pcCondition.Enabled = false;
                        btnDraft.Enabled = false;
                        btnDeleteRow.Enabled = false;
                        gvWhsOut.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvWhsOut.Columns["planQuantity"].Visible = false;
                        gvWhsOut.Columns["deliveryQuantity"].Visible = false;
                        gvWhsOut.Columns["memo"].OptionsColumn.AllowEdit = false;
                    }
                    WhsOutItem();
                }
                else if (order != null)
                {
                    //获得单号
                    GetDocId();
                    meCommandMemo.Text = headerCommand.memo;
                    teDocDate.Text = headerCommand.updateDate;
                    teUserName.Text = LoginInfo.UserName;
                    cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    beOrder.Text = headerCommand.docId;
                    beOrder.Enabled = false;
                    cboMoveType.EditValue = headerCommand.movementTypeId;
                    GetCommandItemData(headerCommand.docId);
                    if (whsOut.Count > 0)
                    {
                        gcWhsOut.DataSource = whsOut;
                        gcWhsOut.RefreshDataSource();
                    }
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
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询指令单数据
        private void beOrder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "出库单查询";
                this.Close();
            }
            else if (order != null)
            {
                order.Visible = true;
                order.BringToFront();
                ((XtraTabPage)this.Parent).Text = "出库指令单";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
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
                    orderType = MoveType.warehouseOut,
                    productStoreId=LoginInfo.ProductStoreId
                };
                DataTable dtMoveType = null;
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
                if (DevCommon.getDocId(DocType.WarehouseOut, ref docId) == RetCode.OK)
                {
                    teWhsOutNo.Text = docId;
                }
                else
                {
                    teWhsOutNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teWhsOutNo.Text = "";
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
                if (DevCommon.getDataByWebService("GoodsWhsOutItemCommand", "GoodsWhsOutItemCommand", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        headerCommand = null;
                        return;
                    }
                    else
                    {
                        List<WarehouseOutItemCommandDetail> item = detailCommand.Where(p => (p.quantity - p.deliveryQuantity) > 0).ToList();
                        if (item.Count == 0)
                        {
                            headerCommand = null;
                            return;
                        }
                        else
                        {
                            whsOut.Clear();
                            gcWhsOut.DataSource = null;
                            for (int i = 0; i < item.Count; i++)
                            {
                                if (item[i].isSequence == ProductSequenceManager.sequenceY && string.IsNullOrEmpty(item[i].sequenceId))
                                {
                                    for (int j = 0; j < item[i].quantity - item[i].deliveryQuantity; j++)
                                    {
                                        InsertDeliveryItem(item[i]);
                                    }
                                }
                                else
                                {
                                    InsertDeliveryItem(item[i]);
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

        #region 发货单赋值
        public void InsertDeliveryItem(WarehouseOutItemCommandDetail command)
        {
            try
            {
                WarehouseOutItemDetail detail = new WarehouseOutItemDetail();
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.config = command.config;
                //串号管理
                if (command.isSequence == ProductSequenceManager.sequenceY)
                {
                    detail.planQuantity = 1;
                    detail.deliveryQuantity = 0;
                }
                else
                {
                    detail.planQuantity = command.quantity;
                    detail.deliveryQuantity = command.deliveryQuantity;
                }
                //修改标示
                if (command.isSequence == ProductSequenceManager.sequenceY && string.IsNullOrEmpty(command.sequenceId))
                {
                    detail.updateSign = UpdateCondition.update;
                }
                else
                {
                    detail.updateSign = UpdateCondition.enabled;
                }
                detail.docId = teWhsOutNo.Text.Trim();
                detail.facilityId = command.facilityId;
                detail.facilityIdTo = command.facilityIdTo;
                detail.facilityName = command.facilityName;
                detail.facilityNameTo = command.facilityNameTo;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = whsOut.Count.ToString();
                detail.memo = "";
                detail.onHand = command.onHand;
                detail.promise = command.promise;
                detail.productId = command.productId;
                detail.productName = command.productName;
                detail.sequenceId = command.sequenceId;
                detail.isSequence = command.isSequence;
                whsOut.Add(detail);
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得发货行明细数据
        public void WhsOutItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teWhsOutNo.Text.Trim(),
                    orderType = MoveType.warehouseOut
                };
                if (DevCommon.getDataByWebService("GoodsDeliveryItem", "GoodsDeliveryItem", searchConditions, ref whsOut) == RetCode.OK)
                {
                    if (whsOut != null)
                    {
                        if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                        {
                            for (int i = 0; i < whsOut.Count; i++)
                            {
                                if (whsOut[i].isSequence == ProductSequenceManager.sequenceY)
                                {
                                    if (string.IsNullOrEmpty(whsOut[i].sequenceId)
                                    && whsOut[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        whsOut[i].updateSign = UpdateCondition.update;
                                    }
                                    if (!string.IsNullOrEmpty(whsOut[i].sequenceId)
                                       && whsOut[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        whsOut[i].updateSign = UpdateCondition.enabled;
                                    }
                                    whsOut[i].planQuantity = 1;
                                    whsOut[i].deliveryQuantity = 0;
                                }
                                else
                                {
                                    whsOut[i].updateSign = UpdateCondition.enabled;
                                }
                            }
                        }
                        gcWhsOut.DataSource = whsOut;
                    }
                    else
                    {
                        gcWhsOut.DataSource = whsOut;
                    }
                }
                else
                {
                    gcWhsOut.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcWhsOut.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 回车选择出库指令单事件
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
                            if (whsOut.Count > 0)
                            {
                                gcWhsOut.DataSource = whsOut;
                                gcWhsOut.RefreshDataSource();
                                cboMoveType.Enabled = false;
                                beOrder.Value = detailCommand[0].docId;
                                cboMoveType.EditValue = headerCommand.movementTypeId;
                                meCommandMemo.Text = headerCommand.memo;
                                return;
                            }
                        }
                    }
                    else
                    {
                        cboMoveType.Enabled = true;
                        cboMoveType.SelectedIndex = 0;
                        headerCommand = null;
                        whsOut.Clear();
                        gcWhsOut.DataSource = null;
                        gcWhsOut.RefreshDataSource();
                        meCommandMemo.Text = "";
                        meWhsOutMemo.Text = "";
                        Clear();
                        cboMoveType.SelectedIndex = -1;
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
                docId = commandNo,
                orderType = MoveType.warehouseOut,
                productStoreId = LoginInfo.ProductStoreId
            };
            List<WarehouseOutHeaderDetailCommand> headerCommandItem = null;
            if (DevCommon.getDataByWebService("GoodsWhsOutCommand", "GoodsWhsOutCommand", searchCondition, ref headerCommandItem) == RetCode.OK)
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

        #region 保存草稿事件
        private void btnDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    InsertAndUpdateWhsOut(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 保存确定事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    InsertAndUpdateWhsOut(DocStatus.VALID);
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
            if (gvWhsOut.RowCount > 0)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvWhsOut.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 右键菜单
        private void gvWhsOut_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
            if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
            {
                gvWhsOut.DeleteSelectedRows();
            }
        }
        #endregion

        #region 添加或修改收货数据方法
        public void InsertAndUpdateWhsOut(DocStatus type)
        {
            try
            {
                if (gvWhsOut.RowCount > 0)
                {
                    #region 添加头数据
                    WarehouseOutHeader header = new WarehouseOutHeader();
                    //单号
                    header.docId = teWhsOutNo.Text.Trim();
                    if (headerCommand != null)
                    {
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = LoginInfo.PartyId;
                        header.productStoreIdTo = headerCommand.productStoreIdTo;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.docDate = teDocDate.Text;
                    }
                    else if (headerItem != null)
                    {
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = headerItem.userId;
                        header.productStoreIdTo = headerItem.productStoreIdTo;
                        header.status = headerItem.status;
                        header.docDate = headerItem.docDate;
                    }
                    header.updateDate = teDocDate.Text.Trim();
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.baseEntry = beOrder.Text.Trim();
                    header.partyId = LoginInfo.OwnerPartyId;
                    header.movementTypeId = cboMoveType.EditValue.ToString();
                    header.docStatus = Convert.ToInt32(type).ToString();
                    header.memo = meWhsOutMemo.Text.Trim();
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
                    if (type == DocStatus.DRAFT)
                    {
                        header.isSync = "";
                    }
                    if (type == DocStatus.VALID)
                    {
                        header.isSync = "0";
                    }
                    #endregion

                    #region 添加出库行明细
                    List<WarehouseOutItemDetail> itemDetail = new List<WarehouseOutItemDetail>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<WarehouseOutItemDetail>)gcWhsOut.DataSource).Where(p => p.quantity > 0).ToList();
                    }
                    else
                    {
                        itemDetail = (List<WarehouseOutItemDetail>)gcWhsOut.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要出库的数据！");
                        return;
                    }
                    List<WarehouseOutItem> item = (List<WarehouseOutItem>)itemDetail.Select(p => new WarehouseOutItem
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
                        idValue = p.idValue
                    }).ToList();
                    #endregion

                    #region 添加或修改出库数据操作
                    if (search != null)
                    {
                        if (UpdateHeaderAndItem(header, item))
                        {
                            if (Convert.ToInt32(DocStatus.DRAFT) == Convert.ToInt32(cboDocStatus.EditValue)
                               && header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID).ToString()))
                            {
                                //打印并修改打印次数
                                printAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "出库单查询";
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
                            //打印并修改打印次数
                            if (header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID)))
                            {
                                printAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "出库指令单";
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
                            meWhsOutMemo.Text = "";
                            meCommandMemo.Text = "";
                            GetDocId();
                            gcWhsOut.DataSource = null;
                            whsOut.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                            cboMoveType.SelectedIndex = 0;
                            //打印并修改打印次数
                            if (header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID)))
                            {
                                printAndUpdatePrintNum(header.docId);
                            }
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

        #region 修改出库单头数据和明细数据
        public bool UpdateHeaderAndItem(WarehouseOutHeader header, List<WarehouseOutItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsWhsOut", "UpdateGoodsWhsOut", searchCondition, ref result) == RetCode.OK)
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

        #region 添加出库数据
        public bool Insert(WarehouseOutHeader header, List<WarehouseOutItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("GoodsWhsOutInsert", "GoodsWhsOutInsert", searchCondition, ref result) == RetCode.OK)
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
                if (e.KeyChar == 13 && !string.IsNullOrEmpty(((SearchButton)sender).Text.Trim()) && headerCommand!=null)
                {
                    if (((SearchButton)sender).Text.Trim().Length < 4)
                    {
                        m_frm.PromptInformation("输入的条件最少是4位！");
                        return;
                    }
                    ProductSearch condition = new ProductSearch();

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
                        productList = null;
                        var con = new
                        {
                            condition.productId,
                            condition.productName,
                            condition.idValue
                        };
                        if (DevCommon.getDataByWebService("ProductInfo", "ProductInfo", con, ref productList) == RetCode.OK)
                        {
                            AddDeliveryRowDataNotSeq(productList, ((SearchButton)sender));
                        }
                        else
                        {
                            return;
                        }
                    }
                    #endregion

                    #region 根据商品串号查询
                    if (sender.Equals(btnSequence))
                    {
                        if (sender.Equals(btnSequence))
                        {
                            condition.sequenceId = btnSequence.Text.Trim();
                        }
                        productList = null;
                        var con = new
                        {
                            condition.sequenceId,
                            productStoreId = LoginInfo.ProductStoreId
                        };
                        if (DevCommon.getDataByWebService("ProductSequenceInfo", "ProductSequenceInfo", con, ref productList) == RetCode.OK)
                        {
                            AddDeliveryRowDataSeq(productList, ((SearchButton)sender));
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

        #region 根据指令出库时，添加出库行的出库数
        public void AddDeliveryRowDataNotSeq(List<WhsOutProduct> list, SearchButton button)
        {
            if (list != null)
            {
                //根据条件没有查询到数据，进入到搜索帮助页面
                if (list.Count == 0)
                {
                    m_frm.PromptInformation("没有搜索到相关商品");
                    Clear();
                    return;
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
                    List<WarehouseOutItemDetail> listDetail = whsOut.Where(p =>
                        p.productId == list[0].ProductId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceN).ToList();
                    if (listDetail.Count > 0)
                    {
                        bool sign = false;
                        for (int i = 0; i < whsOut.Count; i++)
                        {
                            if (whsOut[i].productId == list[0].ProductId && whsOut[i].isSequence == ProductSequenceManager.sequenceN)
                            {
                                int planQuantity = whsOut[i].planQuantity == null ? 0 : (int)whsOut[i].planQuantity;
                                int deliveryQuantity = whsOut[i].deliveryQuantity == null ? 0 : (int)whsOut[i].deliveryQuantity;
                                int quantity = whsOut[i].quantity == null ? 0 : (int)whsOut[i].quantity;
                                if (quantity < planQuantity - deliveryQuantity)
                                {
                                    whsOut[i].quantity = quantity + 1;
                                    gvWhsOut.FocusedRowHandle = gvWhsOut.GetRowHandle(i);
                                    gcWhsOut.RefreshDataSource();
                                    sign = true;
                                    Clear();
                                    return;
                                }
                            }
                        }
                        if (!sign)
                        {
                            m_frm.PromptInformation("商品：" + list[0].ProductId + "在此出库单出库数量以满足！");
                            Clear();
                            return;
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("商品：" + list[0].ProductId + "不在此出库单！");
                        Clear();
                        return;
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
                m_frm.PromptInformation("没有搜索到相关商品");
                Clear();
                return;
            }
        }
        #endregion

        #region 根据指令出库时，添加出库单的出库数
        public void AddDeliveryRowDataSeq(List<WhsOutProduct> list, SearchButton button)
        {
            if (list != null)
            {
                //根据条件没有查询到数据，进入到搜索帮助页面
                if (list.Count == 0)
                {
                    m_frm.PromptInformation("没有搜索到相关商品");
                    Clear();
                    return;
                }
                //根据条件搜索到一条符合的数据，数据进行赋值
                if (list.Count == 1)
                {
                    List<WarehouseOutItemDetail> listDetail = whsOut.Where(p =>
                        !string.IsNullOrEmpty(p.sequenceId)
                        && p.sequenceId == list[0].SequenceId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceY).ToList();
                    if (listDetail.Count > 0)
                    {
                        for (int i = 0; i < whsOut.Count; i++)
                        {
                            if (whsOut[i].productId == list[0].ProductId
                                && whsOut[i].isSequence == ProductSequenceManager.sequenceY
                                && whsOut[i].sequenceId == list[0].SequenceId)
                            {
                                int quantity = whsOut[i].quantity == null ? 0 : (int)whsOut[i].quantity;
                                if (quantity > 0)
                                {
                                    m_frm.PromptInformation("串号：" + list[0].SequenceId + "在此出库单的出库数量以满足！");
                                    gvWhsOut.FocusedRowHandle = gvWhsOut.GetRowHandle(i);
                                    Clear();
                                    return;
                                }
                                else
                                {
                                    whsOut[i].quantity = quantity + 1;
                                    gvWhsOut.FocusedRowHandle = gvWhsOut.GetRowHandle(i);
                                    gcWhsOut.RefreshDataSource();
                                    Clear();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("串号：" + list[0].SequenceId + "不在此出库单！");
                        Clear();
                        return;
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
                m_frm.PromptInformation("没有搜索到相关商品");
                Clear();
                return;
            }
        }
        #endregion

        #region 修改列表事件
        private void gvWhsOut_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == Quantity)
            {
                if (gvWhsOut.GetFocusedRowCellValue(Quantity) == null)
                {
                    whsOutNum = 0;
                }
                else
                {
                    whsOutNum = Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue(Quantity));
                }
            }
        }
        #endregion

        #region 修改列表事件
        private void gvWhsOut_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == Quantity)
                {
                    string isSequence = gvWhsOut.GetFocusedRowCellValue("isSequence") == null ? "" : gvWhsOut.GetFocusedRowCellValue("isSequence").ToString();
                    if (gvWhsOut.GetFocusedRowCellValue(Quantity) == null
                        || Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue(Quantity)) == 0)
                    {
                        return;
                    }
                    if (Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue(Quantity)) < 0)
                    {
                        gvWhsOut.SetFocusedRowCellValue(Quantity, whsOutNum);
                        return;
                    }
                    if (!string.IsNullOrEmpty(isSequence))
                    {
                        int planQuantity = gvWhsOut.GetFocusedRowCellValue(PlanWhsQuantity) == null ? 0 : (int)gvWhsOut.GetFocusedRowCellValue(PlanWhsQuantity);
                        int deliveryQuantity = gvWhsOut.GetFocusedRowCellValue(DeliveryQuantity) == null ? 0 : Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue(DeliveryQuantity));
                        int deliveryNumAfter = gvWhsOut.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvWhsOut.GetFocusedRowCellValue(Quantity));
                        string sequenceId = gvWhsOut.GetFocusedRowCellValue("sequenceId") == null ? null : gvWhsOut.GetFocusedRowCellValue("sequenceId").ToString();
                        if (isSequence.Equals(ProductSequenceManager.sequenceY))
                        {
                            if (string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 0)
                            {
                                gvWhsOut.SetFocusedRowCellValue(Quantity, whsOutNum);
                                return;
                            }
                            if (!string.IsNullOrEmpty(sequenceId)
                                && deliveryNumAfter > 1)
                            {
                                gvWhsOut.SetFocusedRowCellValue(Quantity, whsOutNum);
                                return;
                            }
                        }
                        else if (isSequence.Equals(ProductSequenceManager.sequenceN))
                        {
                            if (deliveryNumAfter > planQuantity - deliveryQuantity)
                            {
                                gvWhsOut.SetFocusedRowCellValue(Quantity, whsOutNum);
                                return;
                            }
                        }
                    }
                    else
                    {
                        gvWhsOut.SetFocusedRowCellValue(Quantity, whsOutNum);
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

        #region 行编辑事件
        private void gvWhsOut_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "sequenceId")
            {
                string isSequence = gvWhsOut.GetFocusedRowCellValue("isSequence") == null ? "" : gvWhsOut.GetFocusedRowCellValue("isSequence").ToString();
                string updateSign = gvWhsOut.GetFocusedRowCellValue("updateSign") == null ? "" : gvWhsOut.GetFocusedRowCellValue("updateSign").ToString();
                if (isSequence == ProductSequenceManager.sequenceY && updateSign.Equals(UpdateCondition.update))
                {
                    e.RepositoryItem.ReadOnly = false;
                }
                else
                {
                    e.RepositoryItem.ReadOnly = true;
                }
            }
        }
        #endregion

        #region 打印并修改打印次数
        private void printAndUpdatePrintNum(string docId)
        {
            try
            {
                OtherOutStorePrint print = new OtherOutStorePrint(docId, true);
                print.ShowDialog();
                var searchCondition = new
                {
                    docId = docId
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateDeliveryPrintNum", "UpdateDeliveryPrintNum", searchCondition, ref result) != RetCode.OK)
                {
                    m_frm.PromptInformation("打印失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}