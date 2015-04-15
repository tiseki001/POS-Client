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
using System.Linq;
using Commons.Model;
using Print;

namespace WarehouseIn
{
    public partial class WarehouseIn : BaseForm
    {
        #region 参数
        List<WarehouseInItemCommandDetail> detailCommand = new List<WarehouseInItemCommandDetail>();
        public WarehouseInHeaderDetail headerItem = null;
        public WarehouseInSearch search = null;
        public WarehouseInHeaderDetailCommand headerCommand = null;
        public WarehouseInOrder order = null;
        List<WarehouseInItemDetail> whsIn = new List<WarehouseInItemDetail>();
        public WarehouseInItemDetail product = new WarehouseInItemDetail();
        int beforQuantity = 0;
        #endregion

        #region 构造
        public WarehouseIn()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void WarehouseIn_Load(object sender, EventArgs e)
        {
            try
            {
                //获得移动类型
                GetMoveTypeDate();
                //获得状态数据
                GetStatusData();
                if (search != null)
                {
                    pcHeader.Enabled = false;
                    teDocDate.Text = headerItem.docDate;
                    teUserName.Text = headerItem.updateUserName;
                    cboDocStatus.EditValue = headerItem.docStatus;
                    cboMoveType.Enabled = false;
                    cboMoveType.EditValue = headerItem.movementTypeId;
                    beOrder.Text = headerItem.baseEntry;
                    beOrder.Enabled = false;
                    teWhsInNo.Text = headerItem.docId;
                    meMemo.Text = headerItem.memo;
                    if (Convert.ToInt32(headerItem.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        pcCondition.Enabled = false;
                        btnDraft.Enabled = false;
                        btnDeleteRow.Enabled = false;
                        gvWhsIn.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvWhsIn.Columns["planQuantity"].Visible = false;
                        gvWhsIn.Columns["receiveQuantity"].Visible = false;
                        gvWhsIn.Columns["memo"].OptionsColumn.ReadOnly = true;
                    }
                    WhsInItem();
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
                    if (whsIn.Count > 0)
                    {
                        gcWhsIn.DataSource = whsIn;
                        gcWhsIn.RefreshDataSource();
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

        #region 取消事件
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "入库单查询";
                this.Close();
            }
            else if (order != null)
            {
                order.Visible = true;
                order.BringToFront();
                ((XtraTabPage)this.Parent).Text = "入库指令单";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 删除单据行数据
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (gvWhsIn.RowCount > 0)
            {
                if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvWhsIn.DeleteSelectedRows();
                }
            }
        }
        #endregion

        #region 右键菜单
        private void gvWhsIn_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
                gvWhsIn.DeleteSelectedRows();
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
                    orderType = MoveType.warehouseIn,
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

        #region 保存确定单据事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWhsIn.RowCount > 0)
                {
                    InsertWhsInData(DocStatus.VALID);
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
                if (gvWhsIn.RowCount > 0)
                {
                    InsertWhsInData(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 选择指令单号
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
                            GetCommandItemData(headerCommand.docId);
                            if (whsIn.Count > 0)
                            {
                                gcWhsIn.DataSource = whsIn;
                                gcWhsIn.RefreshDataSource();
                                cboMoveType.Enabled = false;
                                beOrder.Text = headerCommand.docId;
                                cboMoveType.EditValue = headerCommand.movementTypeId;
                                meCommandMemo.Text = headerCommand.memo;
                                return;
                            }
                        }
                    }
                    else
                    {
                        cboMoveType.SelectedIndex = -1;
                        headerCommand = null;
                        whsIn.Clear();
                        gcWhsIn.DataSource = null;
                        gcWhsIn.RefreshDataSource();
                        meCommandMemo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 根据入库指令单号获得指令单头数据
        public void GetCommandHeaderData(string commandNo)
        {
            var searchCondition = new
            {
                docId = commandNo,
                orderType = MoveType.warehouseIn,
                productStoreId = LoginInfo.ProductStoreId
            };
            List<WarehouseInHeaderDetailCommand> headerCommandItem = null;
            if (DevCommon.getDataByWebService("WhsInCommand", "WhsInCommand", searchCondition, ref headerCommandItem) == RetCode.OK)
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
                        m_frm.PromptInformation("此入库单是已清单据");
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

        #region 根据入库指令单号获得入库指令明细
        public void GetCommandItemData(string commandNo)
        {
            try
            {
                var searchItem = new { docId = commandNo };
                detailCommand = null;
                if (DevCommon.getDataByWebService("WhsInItemCommand", "WhsInItemCommand", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        headerCommand = null;
                        return;
                    }
                    else
                    {
                        List<WarehouseInItemCommandDetail> item = detailCommand.Where(p => p.quantity - (p.receiveQuantity == null ? 0 : (int)p.receiveQuantity) > 0).ToList();
                        if (item.Count == 0)
                        {
                            headerCommand = null;
                            return;
                        }
                        else
                        {
                            whsIn.Clear();
                            gcWhsIn.DataSource = null;
                            for (int i = 0; i < item.Count; i++)
                            {
                                int inQuantity = item[i].receiveQuantity == null ? 0 : (int)item[i].receiveQuantity;
                                if (item[i].isSequence == ProductSequenceManager.sequenceY && string.IsNullOrEmpty(item[i].sequenceId))
                                {
                                    for (int j = 0; j < item[i].quantity - inQuantity; j++)
                                    {
                                        InsertWhsInItem(item[i]);
                                    }
                                }
                                else
                                {
                                    InsertWhsInItem(item[i]);
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

        #region 入库单赋值
        public void InsertWhsInItem(WarehouseInItemCommandDetail command)
        {
            try
            {
                WarehouseInItemDetail detail = new WarehouseInItemDetail();
                //串号管理
                if (command.isSequence == ProductSequenceManager.sequenceY)
                {
                    detail.planQuantity = 1;
                    detail.receiveQuantity = 0;
                }
                else
                {
                    detail.planQuantity = command.quantity;
                    detail.receiveQuantity = command.receiveQuantity;
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
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.config = command.config;
                detail.docId = teWhsInNo.Text.Trim();
                detail.facilityId = command.facilityId;
                detail.facilityIdFrom = command.facilityIdFrom;
                detail.facilityName = command.facilityName;
                detail.facilityNameFrom = command.facilityNameFrom;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = whsIn.Count.ToString();
                detail.memo = "";
                detail.productId = command.productId;
                detail.productName = command.productName;
                detail.sequenceId = command.sequenceId;
                detail.isSequence = command.isSequence;
                whsIn.Add(detail);
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
                if (e.KeyChar == 13 && !string.IsNullOrEmpty(((TextEdit)sender).Text.Trim()) && gvWhsIn.RowCount>0)
                {
                    #region 输入商品代码
                    if (sender.Equals(teProductId))
                    {
                        if (teProductId.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的商品代码最少是后4位！");
                            return;
                        }
                        List<WarehouseInItemDetail> itemlist = whsIn.Where(p =>
                            !string.IsNullOrEmpty(p.productId)
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

                    #region 商品名称
                    if (sender.Equals(teProductName))
                    {
                        if (teProductName.Text.Trim().Length < 4)
                        {
                            m_frm.PromptInformation("输入的商品名称最少是4位！");
                            Clear();
                            return;
                        }
                        List<WarehouseInItemDetail> itemlist = whsIn.Where(p =>
                           !string.IsNullOrEmpty(p.productName)
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
                        List<WarehouseInItemDetail> itemlist = whsIn.Where(p =>
                            !string.IsNullOrEmpty(p.idValue)
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
                        List<WarehouseInItemDetail> itemlist = whsIn.Where(p =>
                            !string.IsNullOrEmpty(p.sequenceId)
                            && p.sequenceId.ToUpper().EndsWith(teSequence.Text.Trim().ToUpper())
                            && p.isSequence == ProductSequenceManager.sequenceY).ToList();
                        if (itemlist.Count == 0)
                        {
                            m_frm.PromptInformation("没有此商品信息");
                            return;
                        }
                        else if (itemlist.Count == 1)
                        {
                            for (int i = 0; i < whsIn.Count; i++)
                            {
                                int quantity = (int)(whsIn[i].quantity == null ? 0 : whsIn[i].quantity);
                                if (whsIn[i].sequenceId == itemlist[0].sequenceId)
                                {
                                    if (quantity > 0)
                                    {
                                        m_frm.PromptInformation("串号：" + whsIn[i].sequenceId + "收货数量已满足！");
                                        gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                                        Clear();
                                        return;
                                    }
                                    else
                                    {
                                        whsIn[i].quantity = 1;
                                        gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                                        gcWhsIn.RefreshDataSource();
                                        Clear();
                                        return;
                                    }
                                }
                            }
                        }
                        else if (itemlist.Count > 1)
                        {
                            Prompt prompt = new Prompt();
                            prompt.whsIn = this;
                            prompt.item = itemlist;
                            prompt.ShowDialog();
                            for (int i = 0; i < whsIn.Count; i++)
                            {
                                int quantity = (int)(whsIn[i].quantity == null ? 0 : whsIn[i].quantity);
                                if (whsIn[i].sequenceId == product.sequenceId)
                                {
                                    if (quantity > 0)
                                    {
                                        m_frm.PromptInformation("串号：" + whsIn[i].sequenceId + "收货数量已满足！");
                                        gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                                        Clear();
                                        return;
                                    }
                                    else
                                    {
                                        whsIn[i].quantity = 1;
                                        gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                                        gcWhsIn.RefreshDataSource();
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

        #region 查询列表确认收货商品信息
        public void GetData(WarehouseInItemDetail item)
        {
            try
            {
                for (int i = 0; i < whsIn.Count; i++)
                {
                    if (whsIn[i].productId == item.productId)
                    {
                        int quantity = (int)(whsIn[i].quantity == null ? 0 : whsIn[i].quantity);
                        int planQuantity = (int)(whsIn[i].planQuantity == null ? 0 : whsIn[i].planQuantity);
                        int receiveQuantity = (int)(whsIn[i].receiveQuantity == null ? 0 : whsIn[i].receiveQuantity);
                        if (quantity >= planQuantity - receiveQuantity)
                        {
                            m_frm.PromptInformation("商品：" + whsIn[i].productId + "收货数量已满足！");
                            gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                            Clear();
                            return;
                        }
                        else
                        {
                            whsIn[i].quantity = quantity + 1;
                            gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                            gcWhsIn.RefreshDataSource();
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

        #region 调用窗体确认收货商品
        public void GetFromData(List<WarehouseInItemDetail> item)
        {
            try
            {
                Prompt prompt = new Prompt();
                prompt.whsIn = this;
                prompt.item = item;
                prompt.ShowDialog();
                int quantity = 0;
                int planQuantity = 0;
                int receiveQuantity = 0;
                for (int i = 0; i < whsIn.Count; i++)
                {
                    if (whsIn[i].lineNo == product.lineNo)
                    {
                        quantity = (int)(whsIn[i].quantity == null ? 0 : whsIn[i].quantity);
                        planQuantity = (int)(whsIn[i].planQuantity == null ? 0 : whsIn[i].planQuantity);
                        receiveQuantity = (int)(whsIn[i].receiveQuantity == null ? 0 : whsIn[i].receiveQuantity);
                        if (quantity >= planQuantity - receiveQuantity)
                        {
                            m_frm.PromptInformation("商品：" + whsIn[i].productId + "收货数量已满足！");
                            gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                            Clear();
                            return;
                        }
                        else
                        {
                            whsIn[i].quantity = quantity + 1;
                            gvWhsIn.FocusedRowHandle = gvWhsIn.GetRowHandle(i);
                            gcWhsIn.RefreshDataSource();
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
        public void InsertWhsInData(DocStatus type)
        {
            try
            {
                if (gvWhsIn.RowCount > 0)
                {
                    #region 添加头数据
                    WarehouseInHeader header = new WarehouseInHeader();
                    //单号
                    header.docId = teWhsInNo.Text.Trim();
                    if (headerCommand != null)
                    {
                        header.productStoreIdFrom = headerCommand.productStoreIdFrom;
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = LoginInfo.PartyId;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.docDate = teDocDate.Text;
                    }
                    else if (headerItem != null)
                    {
                        header.productStoreIdFrom = headerItem.productStoreIdFrom;
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = headerItem.userId;
                        header.status = headerItem.status;
                        header.docDate = headerItem.docDate;
                    }
                    header.updateDate = teDocDate.Text.Trim();
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.baseEntry = beOrder.Text.Trim();
                    header.partyId = LoginInfo.OwnerPartyId;
                    header.movementTypeId = cboMoveType.EditValue.ToString();
                    header.docStatus = Convert.ToInt32(type).ToString();
                    header.memo = meMemo.Text.Trim();
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

                    #region 添加发货行明细
                    List<WarehouseInItemDetail> itemDetail = new List<WarehouseInItemDetail>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<WarehouseInItemDetail>)gcWhsIn.DataSource).Where(p => p.quantity > 0&&!string.IsNullOrEmpty(p.facilityId)).ToList();
                    }
                    else
                    {
                        itemDetail = (List<WarehouseInItemDetail>)gcWhsIn.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要入库的数据！");
                        return;
                    }
                    List<WarehouseInItem> item = (List<WarehouseInItem>)itemDetail.Select(p => new WarehouseInItem
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
                            if (Convert.ToInt32(DocStatus.DRAFT) == Convert.ToInt32(cboDocStatus.EditValue)
                               && header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID).ToString()))
                            {
                                //打印并修改打印次数
                                printAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "入库单查询";
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
                        if (InsertWhsIn(header, item))
                        {
                            //打印并修改打印次数
                            if (header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID)))
                            {
                                printAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "入库指令单";
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
                        if (InsertWhsIn(header, item))
                        {
                            headerCommand = null;
                            ClearData();
                            GetDocId();
                            gcWhsIn.DataSource = null;
                            whsIn.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                            cboMoveType.SelectedIndex = -1;
                            meCommandMemo.Text = "";
                            meMemo.Text = "";
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

        #region 添加收货数据
        public bool InsertWhsIn(WarehouseInHeader header, List<WarehouseInItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("GoodsWhsInInsert", "GoodsWhsInInsert", searchCondition, ref result) == RetCode.OK)
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

        #region 修改入库头数据和明细数据
        public bool UpdateHeaderAndItem(WarehouseInHeader header, List<WarehouseInItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsWhsIn", "UpdateGoodsWhsIn", searchCondition, ref result) == RetCode.OK)
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

        #region 获得调拨发货单号
        public void GetDocId()
        {
            try
            {
                string docId = null;
                if (DevCommon.getDocId(DocType.WarehouseIn, ref docId) == RetCode.OK)
                {
                    teWhsInNo.Text = docId;
                }
                else
                {
                    teWhsInNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teWhsInNo.Text = "";
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加完成清空数据
        public void ClearData()
        {
            beOrder.Text = "";
            meMemo.Text = "";
            meCommandMemo.Text = "";
        }
        #endregion

        #region 获得发货行明细数据
        private void WhsInItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teWhsInNo.Text.Trim()
                };
                if (DevCommon.getDataByWebService("GoodsWhsInItem", "GoodsWhsInItem", searchConditions, ref whsIn) == RetCode.OK)
                {
                    if (whsIn != null)
                    {
                        if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                        {
                            for (int i = 0; i < whsIn.Count; i++)
                            {
                                if (whsIn[i].isSequence == ProductSequenceManager.sequenceY)
                                {
                                    if (string.IsNullOrEmpty(whsIn[i].sequenceId)
                                        && whsIn[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        whsIn[i].updateSign = UpdateCondition.update;
                                    }
                                    if (!string.IsNullOrEmpty(whsIn[i].sequenceId)
                                        && whsIn[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        whsIn[i].updateSign = UpdateCondition.enabled;
                                    }
                                    whsIn[i].planQuantity = 1;
                                    whsIn[i].receiveQuantity = 0;
                                }
                                else
                                {
                                    whsIn[i].updateSign = UpdateCondition.enabled;
                                }
                            }
                        }
                        gcWhsIn.DataSource = whsIn;
                    }
                    else
                    {
                        gcWhsIn.DataSource = whsIn;
                    }
                }
                else
                {
                    gcWhsIn.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcWhsIn.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 单元格改变之前事件
        private void gvWhsIn_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == columnQuantity)
                {
                    beforQuantity = gvWhsIn.GetFocusedRowCellValue(columnQuantity) == null ? 0 : Convert.ToInt32(gvWhsIn.GetFocusedRowCellValue(columnQuantity));
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 单元格改变之后事件
        private void gvWhsIn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == columnQuantity)
                {
                    int quantity = gvWhsIn.GetFocusedRowCellValue(columnQuantity) == null ? 0 : Convert.ToInt32(gvWhsIn.GetFocusedRowCellValue(columnQuantity));
                    int planQuantity = gvWhsIn.GetFocusedRowCellValue(columnPlanQuantity) == null ? 0 : Convert.ToInt32(gvWhsIn.GetFocusedRowCellValue(columnPlanQuantity));
                    int receiveQuantity = gvWhsIn.GetFocusedRowCellValue(columnreceiveQuantity) == null ? 0 : Convert.ToInt32(gvWhsIn.GetFocusedRowCellValue(columnreceiveQuantity));
                    string seq= gvWhsIn.GetFocusedRowCellValue(isSequence)==null?"":gvWhsIn.GetFocusedRowCellValue(isSequence).ToString();
                    string seqId=gvWhsIn.GetFocusedRowCellValue(sequenceId)==null?"":gvWhsIn.GetFocusedRowCellValue(sequenceId).ToString();
                    if (quantity < 0)
                    {
                        gvWhsIn.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                        return;
                    }
                    if (quantity == 0)
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(seq))
                    {
                        gvWhsIn.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                        return;
                    }
                    if (seq.Equals(ProductSequenceManager.sequenceY))
                    {
                        if (string.IsNullOrEmpty(seqId) || quantity > 1)
                        {
                            gvWhsIn.SetFocusedRowCellValue(columnQuantity, beforQuantity);
                            return;
                        }
                    }
                    if (seq.Equals(ProductSequenceManager.sequenceN))
                    {
                        if (quantity > planQuantity - receiveQuantity)
                        {
                            gvWhsIn.SetFocusedRowCellValue(columnQuantity, beforQuantity);
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

        #region 行编辑事件
        private void gvWhsIn_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "sequenceId")
            {
                string isSequence = gvWhsIn.GetFocusedRowCellValue("isSequence") == null ? "" : gvWhsIn.GetFocusedRowCellValue("isSequence").ToString();
                string updateSign = gvWhsIn.GetFocusedRowCellValue("updateSign") == null ? "" : gvWhsIn.GetFocusedRowCellValue("updateSign").ToString();
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

        #region 快捷键响应
        private void WarehouseIn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    if (whsIn.Count <= 0) return;
                    foreach(WarehouseInItemDetail wiid in whsIn)
                    {
                        wiid.quantity = wiid.planQuantity - (wiid.receiveQuantity == null ? 0 : (int)wiid.receiveQuantity);
                    }
                    gcWhsIn.RefreshDataSource();
                }
            }
            catch (System.Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 打印并修改打印次数
        private void printAndUpdatePrintNum(string docId)
        {
            try
            {
                OtherInStorePrint print = new OtherInStorePrint(docId, true);
                print.ShowDialog();
                var searchCondition = new
                {
                    docId = docId
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateReceivePrintNum", "UpdateReceivePrintNum", searchCondition, ref result) != RetCode.OK)
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