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
using DevExpress.XtraGrid.Views.Grid;

namespace GoodsReceipt
{
    public partial class Receive : BaseForm
    {
        #region 参数
        /// <summary>
        /// 指令行数据
        /// </summary>
        List<ReceiptItemCommandDetail> detailCommand = new List<ReceiptItemCommandDetail>();
        /// <summary>
        /// 发货单明细数据
        /// </summary>
        List<ReceiptItemDetail> receiveItemlist = new List<ReceiptItemDetail>();
        //收货指令头数据
        public ReceiptHeaderCommandDetail commandHeader = null;
        //收货单头数据
        public ReceiptHeaderDetail headerItem = null;
        public ReveiveSearch search = null;
        public ReceiveOrder order = null;
        public ReceiptItemDetail product = new ReceiptItemDetail();
        int beforQuantity = 0;
        #endregion

        #region 构造
        public Receive()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void Receive_Load(object sender, EventArgs e)
        {
            try
            {
                btnCommandNo.Focus();
                //状态禁止选择
                cboDocStatus.Enabled = false;
                //获得移动类型
                GetMoveTypeDate();
                //获得状态数据
                GetStatusData();
                if (search != null)
                {
                    pcHeader.Enabled = false;
                    teDocDate.Text = headerItem.docDate;
                    teUserName.Text = headerItem.updateUserName;
                    teDeliveryStoreName.Text = headerItem.storeNameFrom;
                    cboDocStatus.EditValue = headerItem.docStatus;
                    cboMoveType.Enabled = false;
                    cboMoveType.EditValue = headerItem.movementTypeId;
                    btnCommandNo.Text = headerItem.baseEntry;
                    btnCommandNo.Enabled = false;
                    teReceiveNo.Text = headerItem.docId;
                    meMemo.Text = headerItem.memo;
                    if (Convert.ToInt32(headerItem.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        pcCondition.Enabled = false;
                        btnDraft.Enabled = false;
                        btnDeleteRow.Enabled = false;
                        gvReceiveRowData.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvReceiveRowData.Columns["planQuantity"].Visible = false;
                        gvReceiveRowData.Columns["receiveQuantity"].Visible = false;
                        gvReceiveRowData.Columns["memo"].OptionsColumn.AllowEdit = false;
                    }
                    ReceivtItem();
                }
                else if (order != null)
                {
                    //获得单号
                    GetDocId();
                    meCommandMemo.Text = commandHeader.memo;
                    teDeliveryStoreName.Text = commandHeader.storeNameFrom;
                    teDocDate.Text = commandHeader.updateDate;
                    teUserName.Text = LoginInfo.UserName;
                    cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                    btnCommandNo.Text = commandHeader.docId;
                    btnCommandNo.Enabled = false;
                    cboMoveType.EditValue = commandHeader.movementTypeId;
                    GetCommandItemData(commandHeader.docId);
                    if (receiveItemlist.Count > 0)
                    {
                        gcReceiveData.DataSource = receiveItemlist;
                        gcReceiveData.RefreshDataSource();
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

        #region 点击按钮，选择指令单号事件
        private void beOrder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            ReceiveOrder order = new ReceiveOrder();
            order.m_frm = m_frm;
            //order.receive = this;
            order.Location = new Point(0, 0);
            order.TopLevel = false;
            order.TopMost = false;
            order.ControlBox = false;
            order.FormBorderStyle = FormBorderStyle.None;
            order.Dock = DockStyle.Fill;
            this.Visible = false;
            ((XtraTabPage)this.Parent).Controls.Add(order);
            order.Show();
            order.BringToFront();
        }
        #endregion

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "收货单查询";
                this.Close();
            }
            else if (order != null)
            {
                order.Visible = true;
                order.BringToFront();
                ((XtraTabPage)this.Parent).Text = "调拨指令单查询";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 草稿状态点击按钮删除行数据事件
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (gvReceiveRowData.RowCount > 0)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvReceiveRowData.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 右键菜单事件
        private void gvReceiveRowDate_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
                gvReceiveRowData.DeleteSelectedRows();
            }
        }
        #endregion

        #region 保存草稿信息事件
        private void btnDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceiveRowData.RowCount > 0)
                {
                    InsertReceiveData(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 保存确定收货单事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvReceiveRowData.RowCount>0)
                {
                    InsertReceiveData(DocStatus.VALID);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
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
                    orderType = MoveType.goodsReceipt,
                    productStoreId = LoginInfo.ProductStoreId
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
                if (DevCommon.getDocId(DocType.Receive, ref docId) == RetCode.OK)
                {
                    teReceiveNo.Text = docId;
                }
                else
                {
                    teReceiveNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teReceiveNo.Text = "";
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 发货单赋值
        public void InsertReceiveItem(ReceiptItemCommandDetail command)
        {
            try
            {
                ReceiptItemDetail detail = new ReceiptItemDetail();
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.config = command.config;
                detail.receiveQuantity = command.receiveQuantity;
                detail.docId = teReceiveNo.Text.Trim();
                detail.facilityId = command.facilityId;
                detail.facilityIdFrom = command.facilityIdFrom;
                detail.facilityName = command.facilityName;
                detail.facilityNameFrom = command.facilityNameFrom;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = receiveItemlist.Count.ToString();
                detail.memo = "";
                detail.productId = command.productId;
                detail.productName = command.productName;
                detail.sequenceId = command.sequenceId;
                detail.isSequence = command.isSequence;
                detail.planQuantity = command.quantity;
                receiveItemlist.Add(detail);
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据指令单号获得收货指令明细
        public void GetCommandItemData(string commandNo)
        {
            try
            {
                var searchItem = new { docId = commandNo };
                detailCommand = null;
                if (DevCommon.getDataByWebService("GoodsReceiveItemCommand", "GoodsReceiveItemCommand", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        commandHeader = null;
                        return;
                    }
                    else
                    {
                        List<ReceiptItemCommandDetail> item = detailCommand.Where(p => p.quantity - (p.receiveQuantity == null ? 0 : (int)p.receiveQuantity) > 0).ToList();
                        if (item.Count == 0)
                        {
                            commandHeader = null;
                            return;
                        }
                        else
                        {
                            receiveItemlist.Clear();
                            gcReceiveData.DataSource = null;
                            for (int i = 0; i < item.Count; i++)
                            {
                                if (item[i].isSequence == ProductSequenceManager.sequenceY && string.IsNullOrEmpty(item[i].sequenceId))
                                {
                                    for (int j = 0; j < item[i].quantity - item[i].receiveQuantity; j++)
                                    {
                                        InsertReceiveItem(item[i]);
                                    }
                                }
                                else
                                {
                                    InsertReceiveItem(item[i]);
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

        #region 根据指令单号获得指令单头数据
        public void GetCommandHeaderData(string commandNo)
        {
            var searchCondition = new
            {
                docId = commandNo,
                orderType = MoveType.goodsReceipt,
                productStoreId = LoginInfo.ProductStoreId
            };
            List<ReceiptHeaderCommandDetail> headerCommandItem = null;
            if (DevCommon.getDataByWebService("GoodsReceiveCommand", "GoodsReceiveCommand", searchCondition, ref headerCommandItem) == RetCode.OK)
            {
                if (headerCommandItem == null)
                {
                    commandHeader = null;
                    btnCommandNo.ShowForm();
                    return;
                }
                else
                {
                    if (Convert.ToInt32(headerCommandItem[0].status) == Convert.ToInt32(BusinessStatus.CLEARED))
                    {
                        commandHeader = null;
                        m_frm.PromptInformation("此收货单是已清单据！");
                        return;
                    }
                    else
                    {
                        commandHeader = headerCommandItem[0];
                    }
                }
            }
        }
        #endregion

        #region 回车事件选择收货指令单
        private void btnCommandNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(btnCommandNo.Text.Trim()))
                    {
                        GetCommandHeaderData(btnCommandNo.Text.Trim());
                        if (commandHeader != null)
                        {
                            GetCommandItemData(commandHeader.docId);
                            if (receiveItemlist.Count > 0)
                            {
                                gcReceiveData.DataSource = receiveItemlist;
                                gcReceiveData.RefreshDataSource();
                                cboMoveType.Enabled = false;
                                teDeliveryStoreName.Text = string.IsNullOrEmpty(commandHeader.storeNameFrom) ? "" : commandHeader.storeNameFrom;
                                btnCommandNo.Value = detailCommand[0].docId;
                                cboMoveType.EditValue = commandHeader.movementTypeId;
                                meCommandMemo.Text = commandHeader.memo;
                                return;
                            }
                        }
                    }
                    else
                    {
                        cboMoveType.SelectedIndex = -1;
                        commandHeader = null;
                        receiveItemlist.Clear();
                        gcReceiveData.DataSource = null;
                        gcReceiveData.RefreshDataSource();
                        teDeliveryStoreName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 商品代码、条形码、商品串号键盘按下回车键事件
        private void KeyPeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && !string.IsNullOrEmpty(((TextEdit)sender).Text.Trim()) && receiveItemlist.Count > 0)
                {
                    #region 商品代码
                    if (sender.Equals(teProductId))
                    {
                        if (teProductId.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的商品代码最少是后4位！");
                            Clear();
                            return;
                        }
                        List<ReceiptItemDetail> itemlist = receiveItemlist.Where(
                            p => !string.IsNullOrEmpty(p.productId)
                            && p.productId.EndsWith(teProductId.Text.Trim())
                            ).ToList();
                        if (itemlist.Count == 0)
                        {
                            m_frm.PromptInformation("没有此商品信息");
                            Clear();
                            return;
                        }
                        else if (itemlist.Count == 1)
                        {
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity!=1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，请输入串号！");
                                Clear();
                                return;
                            }
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity == 1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，收货数量已满足！");
                                Clear();
                                return;
                            }
                            GetData(itemlist[0]);
                        }
                        else if (itemlist.Count > 1)
                        {
                            GetFromData(itemlist);
                        }
                    }
                    #endregion

                    #region 商品名称
                    if (sender.Equals(teProductName))
                    {
                        if (teProductName.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的商品名称最少是4位！");
                            Clear();
                            return;
                        }
                        List<ReceiptItemDetail> itemlist = receiveItemlist.Where(
                            p => !string.IsNullOrEmpty(p.productName)
                            && p.productName.Contains(teProductName.Text.Trim())
                            ).ToList();
                        if (itemlist.Count == 0)
                        {
                            m_frm.PromptInformation("没有此商品信息");
                            return;
                        }
                        else if (itemlist.Count == 1)
                        {
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity != 1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，请输入串号！");
                                Clear();
                                return;
                            }
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity == 1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，收货数量已满足！");
                                Clear();
                                return;
                            }
                            GetData(itemlist[0]);
                        }
                        else if (itemlist.Count > 1)
                        {
                            GetFromData(itemlist);
                        }
                    }
                    #endregion

                    #region 条形码
                    if (sender.Equals(teBarCode))
                    {
                        if (teBarCode.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的条形码最少是后4位！");
                            return;
                        }
                        List<ReceiptItemDetail> itemlist = receiveItemlist.Where(
                            p => !string.IsNullOrEmpty(p.idValue)
                            && p.idValue.EndsWith(teBarCode.Text.Trim())
                            ).ToList();
                        if (itemlist.Count == 0)
                        {
                            m_frm.PromptInformation("没有此商品信息");
                            return;
                        }
                        else if (itemlist.Count == 1)
                        {
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity != 1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，请输入串号！");
                                Clear();
                                return;
                            }
                            if (itemlist[0].isSequence == ProductSequenceManager.sequenceY && itemlist[0].quantity == 1)
                            {
                                m_frm.PromptInformation("商品：" + itemlist[0].productId + "是串号管理商品，收货数量已满足！");
                                Clear();
                                return;
                            }
                            GetData(itemlist[0]);
                        }
                        else if (itemlist.Count > 1)
                        {
                            GetFromData(itemlist);
                        }
                    }
                    #endregion

                    #region 串号
                    if (sender.Equals(teSequence))
                    {
                        if (teSequence.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的串号最少是后4位！");
                            return;
                        }
                        List<ReceiptItemDetail> itemlist = receiveItemlist.Where(p => !string.IsNullOrEmpty(p.sequenceId) && p.sequenceId.ToUpper().EndsWith(teSequence.Text.Trim().ToUpper())).ToList();
                        if (itemlist.Count == 0)
                        {
                            m_frm.PromptInformation("没有此商品信息");
                            Clear();
                            return;
                        }
                        else if (itemlist.Count == 1)
                        {
                            for (int i = 0; i < receiveItemlist.Count; i++)
                            {
                                int quantity = (int)(receiveItemlist[i].quantity == null ? 0 : receiveItemlist[i].quantity);
                                if (receiveItemlist[i].sequenceId == itemlist[0].sequenceId)
                                {
                                    if (quantity > 0)
                                    {
                                        m_frm.PromptInformation("串号：" + receiveItemlist[i].sequenceId + "收货数量已满足！");
                                        Clear();
                                        gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                                        return;
                                    }
                                    else
                                    {
                                        receiveItemlist[i].quantity = 1;
                                        gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                                        gcReceiveData.RefreshDataSource();
                                        Clear();
                                        return;
                                    }
                                }
                            }
                        }
                        else if (itemlist.Count > 1)
                        {
                            Prompt prompt = new Prompt();
                            prompt.receive = this;
                            prompt.item = itemlist;
                            prompt.ShowDialog();
                            for (int i = 0; i < receiveItemlist.Count; i++)
                            {
                                int quantity = (int)(receiveItemlist[i].quantity == null ? 0 : receiveItemlist[i].quantity);
                                if (receiveItemlist[i].sequenceId == product.sequenceId)
                                {
                                    if (quantity > 0)
                                    {
                                        m_frm.PromptInformation("串号：" + receiveItemlist[i].sequenceId + "收货数量已满足！");
                                        gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                                        return;
                                    }
                                    else
                                    {
                                        receiveItemlist[i].quantity = 1;
                                        gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                                        gcReceiveData.RefreshDataSource();
                                        Clear();
                                        return;
                                    }
                                }
                            }
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

        #region 清空文本框数据
        private void Clear()
        {
            teProductId.Text = "";
            teBarCode.Text = "";
            teSequence.Text = "";
            teProductName.Text = "";
        }
        #endregion

        #region 添加或修改收货数据方法
        public void InsertReceiveData(DocStatus type)
        {
            try
            {
                if (gvReceiveRowData.RowCount > 0)
                {
                    #region 添加头数据
                    ReceiptHeader header = new ReceiptHeader();
                    //单号
                    header.docId = teReceiveNo.Text.Trim();
                    if (commandHeader != null)
                    {
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = LoginInfo.PartyId;
                        header.productStoreIdFrom = commandHeader.productStoreIdFrom;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.docDate = teDocDate.Text;
                    }
                    else if (headerItem != null)
                    {
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = headerItem.userId;
                        header.productStoreIdFrom = headerItem.productStoreIdFrom;
                        header.status = headerItem.status;
                        header.docDate = headerItem.docDate;
                    }
                    header.updateDate = teDocDate.Text.Trim();
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.baseEntry = btnCommandNo.Text.Trim();
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
                    #endregion

                    #region 添加发货行明细
                    List<ReceiptItemDetail> itemDetail = new List<ReceiptItemDetail>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<ReceiptItemDetail>)gcReceiveData.DataSource).Where(p => p.quantity > 0&&!string.IsNullOrEmpty(p.facilityId)).ToList();
                    }
                    else
                    {
                        itemDetail = (List<ReceiptItemDetail>)gcReceiveData.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要收货的数据！");
                        return;
                    }
                    List<ReceiptItem> item = (List<ReceiptItem>)itemDetail.Select(p => new ReceiptItem
                    {
                        docId = p.docId,
                        lineNo = p.lineNo,
                        productId = p.productId,
                        facilityId = p.facilityId,
                        facilityIdFrom = p.facilityIdFrom,
                        memo = p.memo,
                        quantity = p.quantity == null ? 0 : p.quantity,
                        baseEntry = p.baseEntry,
                        baseLineNo = p.baseLineNo,
                        sequenceId = p.sequenceId,
                        isSequence = p.isSequence,
                        idValue = p.idValue
                    }).ToList();
                    #endregion

                    #region 添加或修改收货数据操作
                    if (search != null)
                    {
                        if (UpdateHeaderAndItem(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "发货单查询";
                            this.Close();
                            search.Visible = true;
                            search.BringToFront();
                            search.Data();
                            m_frm.PromptInformation("保存成功！");
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (order != null)
                    {
                        if (InsertReceive(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "调拨收货指令单";
                            this.Close();
                            order.Visible = true;
                            order.BringToFront();
                            order.Reload();
                            m_frm.PromptInformation("保存成功！");
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (InsertReceive(header, item))
                        {
                            commandHeader = null;
                            ClearData();
                            GetDocId();
                            gcReceiveData.DataSource = null;
                            receiveItemlist.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            teDeliveryStoreName.Text = "";
                            cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                            cboMoveType.SelectedIndex = 0;
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

        #region 添加收货数据
        public bool InsertReceive(ReceiptHeader header, List<ReceiptItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("GoodsReceiveInsert", "GoodsReceiveInsert", searchCondition, ref result) == RetCode.OK)
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

        #region 修改收货头数据
        public bool UpdateReceiveHeader(ReceiptHeader header)
        {
            try
            {
                var searchCondition = new
                {
                    header = header
                };
                string result = null;
                if (DevCommon.getDataByWebService("GoodsDeliveryInsert", "GoodsDeliveryInsert", searchCondition, ref result) == RetCode.OK)
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

        #region 修改收货头数据和明细数据
        public bool UpdateHeaderAndItem(ReceiptHeader header, List<ReceiptItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsReceive", "UpdateGoodsReceive", searchCondition, ref result) == RetCode.OK)
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

        #region 调用窗体确认收货商品
        public void GetFromData(List<ReceiptItemDetail> item)
        {
            try
            {
                Prompt prompt = new Prompt();
                prompt.receive = this;
                prompt.item = item;
                prompt.ShowDialog();
                int quantity = 0;
                int planQuantity = 0;
                int receiveQuantity = 0;
                if (product.isSequence == ProductSequenceManager.sequenceY && product.quantity!=1)
                {
                    m_frm.PromptInformation("商品：" + product.productId + "是串号管理商品，请输入串号！");
                    return;
                }
                if (product.isSequence == ProductSequenceManager.sequenceY && product.quantity == 1)
                {
                    m_frm.PromptInformation("商品：" + product.productId + "是串号管理商品，收货数量已满足！");
                    return;
                }
                for (int i = 0; i < receiveItemlist.Count; i++)
                {
                    if (receiveItemlist[i].lineNo == product.lineNo)
                    {
                        quantity = (int)(receiveItemlist[i].quantity == null ? 0 : receiveItemlist[i].quantity);
                        planQuantity = (int)(receiveItemlist[i].planQuantity == null ? 0 : receiveItemlist[i].planQuantity);
                        receiveQuantity = (int)(receiveItemlist[i].receiveQuantity == null ? 0 : receiveItemlist[i].receiveQuantity);
                        if (receiveItemlist[i].productId == product.productId
                                && quantity < planQuantity - receiveQuantity
                            && receiveItemlist[i].isSequence == ProductSequenceManager.sequenceN)
                        {
                            receiveItemlist[i].quantity = (receiveItemlist[i].quantity == null ? 0 : receiveItemlist[i].quantity) + 1;
                            gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                            gcReceiveData.RefreshDataSource();
                            Clear();
                            return;
                        }
                        if (receiveItemlist[i].productId == product.productId
                            && quantity >= planQuantity - receiveQuantity
                            && receiveItemlist[i].isSequence == ProductSequenceManager.sequenceN)
                        {
                            m_frm.PromptInformation("商品：" + product.productId + "收货数量已满足！");
                            gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                            Clear();
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

        #region 查询列表确认收货商品信息
        public void GetData(ReceiptItemDetail item)
        {
            try
            {
                for (int i = 0; i < receiveItemlist.Count; i++)
                {
                    if (receiveItemlist[i].productId == item.productId && receiveItemlist[i].isSequence == ProductSequenceManager.sequenceN)
                    {
                        int quantity = (int)(receiveItemlist[i].quantity == null ? 0 : receiveItemlist[i].quantity);
                        int planQuantity = (int)(receiveItemlist[i].planQuantity == null ? 0 : receiveItemlist[i].planQuantity);
                        int receiveQuantity = (int)(receiveItemlist[i].receiveQuantity == null ? 0 : receiveItemlist[i].receiveQuantity);
                        if (quantity >= planQuantity - receiveQuantity)
                        {
                            m_frm.PromptInformation("商品：" + receiveItemlist[i].productId + "收货数量已满足！");
                            gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                            Clear();
                            return;
                        }
                        else
                        {
                            receiveItemlist[i].quantity = quantity + 1;
                            gvReceiveRowData.FocusedRowHandle = gvReceiveRowData.GetRowHandle(i);
                            gcReceiveData.RefreshDataSource();
                            Clear();
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

        #region 单元格修改事件
        private void gvReceiveRowData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == columnQuantity)
                {
                    beforQuantity = gvReceiveRowData.GetFocusedRowCellValue(columnQuantity) == null ? 0 : Convert.ToInt32(gvReceiveRowData.GetFocusedRowCellValue(columnQuantity));
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 单元格修改事件
        private void gvReceiveRowData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == columnQuantity)
                {
                    int quantity = gvReceiveRowData.GetFocusedRowCellValue(columnQuantity) == null ? 0 : Convert.ToInt32(gvReceiveRowData.GetFocusedRowCellValue(columnQuantity));
                    int planQuantity = gvReceiveRowData.GetFocusedRowCellValue(columnPlanQuantity) == null ? 0 : Convert.ToInt32(gvReceiveRowData.GetFocusedRowCellValue(columnPlanQuantity));
                    int receiveQuantity = gvReceiveRowData.GetFocusedRowCellValue(columnReceiveQuantity) == null ? 0 : Convert.ToInt32(gvReceiveRowData.GetFocusedRowCellValue(columnReceiveQuantity));
                    if (string.IsNullOrEmpty(gvReceiveRowData.GetFocusedRowCellValue(columnQuantity).ToString())
                        || quantity == 0)
                    {
                        return;
                    }
                    if (quantity < 0)
                    {
                        gvReceiveRowData.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                        return;
                    }
                    string isSeq = gvReceiveRowData.GetFocusedRowCellValue(isSequence) == null ? null : gvReceiveRowData.GetFocusedRowCellValue(isSequence).ToString();
                    if (string.IsNullOrEmpty(isSeq))
                    {
                        gvReceiveRowData.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                        return;
                    }
                    if (gvReceiveRowData.GetFocusedRowCellValue(isSequence).ToString().Equals(ProductSequenceManager.sequenceY))
                    {
                        if (string.IsNullOrEmpty(gvReceiveRowData.GetFocusedRowCellValue(sequenceId).ToString()) || quantity > 1)
                        {
                            gvReceiveRowData.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                            return;
                        }
                    }
                    if (gvReceiveRowData.GetFocusedRowCellValue(isSequence).ToString().Equals(ProductSequenceManager.sequenceN))
                    {
                        if (quantity > planQuantity - receiveQuantity)
                        {
                            gvReceiveRowData.SetFocusedRowCellValue(columnQuantity, beforQuantity);
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

        #region 获得发货行明细数据
        public void ReceivtItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teReceiveNo.Text.Trim()
                };
                if (DevCommon.getDataByWebService("GoodsReceiveItem", "GoodsReceiveItem", searchConditions, ref receiveItemlist) == RetCode.OK)
                {
                    gcReceiveData.DataSource = receiveItemlist;
                }
                else
                {
                    gcReceiveData.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcReceiveData.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加完成清空数据
        public void ClearData()
        {
            btnCommandNo.Text = "";
            meMemo.Text = "";
            meCommandMemo.Text = "";
        }
        #endregion

        #region 快捷键响应
        private void Receive_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    if (receiveItemlist.Count <= 0) return;
                    foreach (ReceiptItemDetail wiid in receiveItemlist)
                    {
                        wiid.quantity = wiid.planQuantity - (wiid.receiveQuantity == null ? 0 : (int)wiid.receiveQuantity);
                    }
                    gcReceiveData.RefreshDataSource();
                }
            }
            catch (System.Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion
    }
}