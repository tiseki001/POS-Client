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
using Commons.XML;
using Commons.WebService;
using Commons.Model;
using Newtonsoft.Json;
using System.Collections;
using Commons.Model.SearchButton;
using DevExpress.Utils.Menu;
using Commons.Model.Stock;
using System.Linq;
using Print;

namespace GoodsDelivery
{
    public partial class Delivery : BaseForm
    {
        #region 参数
        /// <summary>
        /// 命令行数据
        /// </summary>
        List<DeliveryItemDetailCommand> detailCommand = new List<DeliveryItemDetailCommand>();
        /// <summary>
        /// 发货单头数据
        /// </summary>
        public DeliveryHeaderDetail headerItem = null;
        public DeliveryHeaderDetailCommand headerCommand = null;
        /// <summary>
        /// 发货单明细数据
        /// </summary>
        List<DeliveryItemDetail> deliveryItemlist = new List<DeliveryItemDetail>();
        /// <summary>
        /// 发货单查询类
        /// </summary>
        public DeliverySearch search = null;
        /// <summary>
        /// 命令查询类
        /// </summary>
        public DeliveryOrder order = null;
        /// <summary>
        /// 商品库存信息
        /// </summary>
        List<DeliveryProduct> deliveryProductList = null;
        int? deliveryNum = null;
        /// <summary>
        /// 发货类型 0自主调拨 1根据指令调拨
        /// </summary>
        int deliveryType = 0;
        string facilityId = "";
        DataTable dtMoveType = null;
        #endregion

        #region 构造
        public Delivery()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void Delivery_Load(object sender, EventArgs e)
        {
            try
            {
                beOrder.Focus();
                //状态禁止选择
                cboStatus.Enabled = false;
                //获得移动类型
                GetMoveTypeDate();
                //获得状态数据
                GetStatusData();
                //发货门店
                teDeliveryStore.Text = LoginInfo.StoreName;
                //制单时间
                tePoerationTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //出库指令单
                if (search != null)
                {
                    meDesc.Text = headerItem.memo;
                    btnReceiveStore.Text = headerItem.storeNameTo;
                    btnReceiveStore.Value = headerItem.productStoreIdTo;
                    teDeliveryNo.Text = headerItem.docId;
                    tePoerationTime.Text = Convert.ToDateTime(headerItem.updateDate).ToString("yyyy-MM-dd HH:mm:ss");
                    cboStatus.EditValue = headerItem.docStatus;
                    cboMoveType.EditValue = headerItem.movementTypeId;
                    teUserName.Text = headerItem.updateUserName;
                    deliveryType = string.IsNullOrEmpty(headerItem.baseEntry) ? 0 : 1;
                    beOrder.Text = headerItem.baseEntry;
                    beOrder.Enabled = false;
                    btnReceiveStore.Enabled = false;
                    cboMoveType.Enabled = false;
                    if (Convert.ToInt32(cboStatus.EditValue) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        gvDelivery.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        pcHeaderData.Enabled = false;
                        pcSearchCondition.Enabled = false;
                        btnDelete.Enabled = false;
                        pcHeaderData.Enabled = false;
                        btnDraft.Enabled = false;
                        gvDelivery.Columns["availableStock"].Visible = false;
                        gvDelivery.Columns["planQuantity"].Visible = false;
                        gvDelivery.Columns["deliveryQuantity"].Visible = false;
                        gvDelivery.Columns["memo"].OptionsColumn.ReadOnly = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(headerItem.baseEntry))
                        {
                            gvDelivery.Columns["deliveryQuantity"].Visible = false;
                            gvDelivery.Columns["planQuantity"].Visible = false;
                        }
                    }
                    GetDeliveryItem();
                    gcDelivery.DataSource = deliveryItemlist;
                    gcDelivery.RefreshDataSource();
                }
                else if (order != null)
                {
                    deliveryType = 1;
                    meCommandMemo.Text = headerCommand.memo;
                    teDeliveryStore.Text = LoginInfo.StoreName;
                    cboMoveType.EditValue = headerCommand.movementTypeId;
                    beOrder.Text = headerCommand.docId;
                    beOrder.Enabled = false;
                    cboMoveType.Enabled = false;
                    btnReceiveStore.Text = headerCommand.storeNameTo;
                    btnReceiveStore.Value = headerCommand.productStoreIdTo;
                    btnReceiveStore.Enabled = false;
                    GetDocId();
                    teUserName.Text = LoginInfo.UserName;
                    GetCommandItemData(headerCommand.docId);
                    if (deliveryItemlist.Count > 0)
                    {
                        gcDelivery.DataSource = deliveryItemlist;
                        gcDelivery.RefreshDataSource();
                    }
                    GetFacilityIdByMoveTypeId();
                }
                else
                {
                    GetDocId();
                    teUserName.Text = LoginInfo.UserName;
                    //单据状态
                    cboStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    gvDelivery.Columns["deliveryQuantity"].Visible = false;
                    gvDelivery.Columns["planQuantity"].Visible = false;
                    GetFacilityIdByMoveTypeId();
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
                ((XtraTabPage)this.Parent).Text = "发货单查询";
                this.Close();
                search.Visible = true;
                search.BringToFront();
                m_frm.PromptInformation("");
            }
            else if (order != null)
            {
                ((XtraTabPage)this.Parent).Text = "调拨发货指令单";
                this.Close();
                order.Visible = true;
                order.BringToFront();
                m_frm.PromptInformation("");
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 点击按钮删除行数据事件
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvDelivery.RowCount > 0)
            {
                if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    gvDelivery.DeleteSelectedRows();
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
                    orderType = MoveType.goodsDelivery,
                    productStoreId = LoginInfo.ProductStoreId
                };
                if (DevCommon.getDataByWebService("MoveType", "MoveSearch", searchConditions, ref dtMoveType) == RetCode.OK)
                {
                    if (dtMoveType != null)
                    {
                        DevCommon.initCmb(cboMoveType, dtMoveType, "movementTypeId", "movementTypeName", false);
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

        #region 右键菜单
        private void gvDelivery_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DXMenuItem item = new DXMenuItem(" 删除");
            item.Click += new EventHandler(item_Click);
            if (e.Menu != null && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                {
                    e.Menu.Items.Insert(0, item);
                }
            }
        }
        #endregion

        #region 右键删除信息
        private void item_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
            {
                gvDelivery.DeleteSelectedRows();
            }
        }
        #endregion

        #region 获得调拨发货单号
        public void GetDocId()
        {
            try
            {
                string docId = null;
                if (DevCommon.getDocId(DocType.Delivery, ref docId) == RetCode.OK)
                {
                    teDeliveryNo.Text = docId;
                }
                else
                {
                    teDeliveryNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teDeliveryNo.Text = "";
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得发货行明细数据
        public void GetDeliveryItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teDeliveryNo.Text.Trim()
                };
                if (DevCommon.getDataByWebService("GoodsDeliveryItem", "GoodsDeliveryItem", searchConditions, ref deliveryItemlist) == RetCode.OK)
                {
                    if (deliveryItemlist != null)
                    {
                        if (Convert.ToInt32(cboStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                        {
                            for (int i = 0; i < deliveryItemlist.Count; i++)
                            {
                                if (deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceY)
                                {
                                    if (string.IsNullOrEmpty(deliveryItemlist[i].sequenceId)
                                    && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        deliveryItemlist[i].updateSign = UpdateCondition.update;
                                    }
                                    if (!string.IsNullOrEmpty(deliveryItemlist[i].sequenceId)
                                       && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceY)
                                    {
                                        deliveryItemlist[i].updateSign = UpdateCondition.enabled;
                                    }
                                    deliveryItemlist[i].planQuantity = 1;
                                    deliveryItemlist[i].deliveryQuantity = 0;
                                }
                                else
                                {
                                    deliveryItemlist[i].updateSign = UpdateCondition.enabled;
                                }
                            }
                        }
                        else
                        {
                            gcDelivery.DataSource = deliveryItemlist;
                        }
                    }
                    else
                    {
                        gcDelivery.DataSource = deliveryItemlist;
                    }
                }
                else
                {
                    gcDelivery.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcDelivery.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 发货单保存事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                InsertDelivery(DocStatus.VALID);
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
                if (gvDelivery.RowCount > 0)
                {
                    InsertDelivery(DocStatus.DRAFT);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }

        }
        #endregion

        #region 移动类型值改变数据删除
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
                    gcDelivery.DataSource = null;
                    headerCommand = null;
                    deliveryItemlist.Clear();
                    GetFacilityIdByMoveTypeId();
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
        public void InsertDelivery(DocStatus type)
        {
            try
            {
                if (gvDelivery.RowCount > 0)
                {
                    //判断单号是否有空
                    if (string.IsNullOrEmpty(teDeliveryNo.Text.Trim()))
                    {
                        m_frm.PromptInformation("发货单号为空！");
                        return;
                    }
                    //判断发货门店
                    if (string.IsNullOrEmpty(btnReceiveStore.Value) || btnReceiveStore.Value.Equals(LoginInfo.ProductStoreId))
                    {
                        m_frm.PromptInformation("请确认收货门店！");
                        return;
                    }
                    #region 添加头数据
                    DeliveryHeader header = new DeliveryHeader();
                    if (headerCommand != null)
                    {
                        header.baseEntry = headerCommand.docId;
                        header.userId = LoginInfo.PartyId;
                        header.updateUserId = LoginInfo.PartyId;
                        header.docDate = tePoerationTime.Text;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                    }
                    else if (headerItem != null)
                    {
                        header.baseEntry = headerItem.baseEntry;
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = headerItem.userId;
                        header.status = headerItem.status;
                        header.docDate = headerItem.docDate;
                    }
                    else
                    {
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = LoginInfo.PartyId;
                        header.docDate = tePoerationTime.Text;
                    }
                    header.updateDate = tePoerationTime.Text.Trim();
                    header.productStoreIdTo = btnReceiveStore.Value;
                    header.productStoreId = LoginInfo.ProductStoreId;
                    //公司ID
                    header.partyId = LoginInfo.OwnerPartyId;
                    //单号
                    header.docId = teDeliveryNo.Text.Trim();
                    header.docStatus = Convert.ToInt32(type).ToString();
                    if (Convert.ToInt32(DocStatus.DRAFT) == Convert.ToInt32(cboStatus.EditValue))
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
                    else
                    {
                        header.isSync = "0";
                    }
                    header.memo = meDesc.Text.Trim();
                    header.movementTypeId = cboMoveType.EditValue.ToString();
                    #endregion

                    #region 添加发货行明细
                    List<DeliveryItemDetail> itemDetail = new List<DeliveryItemDetail>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<DeliveryItemDetail>)gcDelivery.DataSource).Where(p => p.quantity > 0).ToList();
                    }
                    else
                    {
                        itemDetail = (List<DeliveryItemDetail>)gcDelivery.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要发货的数据！");
                        return;
                    }
                    List<DeliveryItem> item = itemDetail.Select(p => new DeliveryItem
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

                    #region 添加或修改数据
                    if (search != null)
                    {
                        if (UpdateHeaderItem(header, item))
                        {
                            if (Convert.ToInt32(DocStatus.DRAFT) == Convert.ToInt32(cboStatus.EditValue)
                                && header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID).ToString()))
                            {
                                //打印并修改打印次数
                                PrintAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "发货单查询";
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
                        if (InsertDelivery(header, item))
                        {
                            //打印并修改打印次数
                            if (header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID)))
                            {
                                PrintAndUpdatePrintNum(header.docId);
                            }
                            ((XtraTabPage)this.Parent).Text = "调拨发货指令单";
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
                        if (InsertDelivery(header, item))
                        {
                            headerCommand = null;
                            meDesc.Text = "";
                            meCommandMemo.Text = "";
                            GetDocId();
                            gcDelivery.DataSource = null;
                            deliveryItemlist.Clear();
                            tePoerationTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            btnReceiveStore.Text = "";
                            btnReceiveStore.Value = "";
                            beOrder.Text = "";
                            beOrder.Value = "";
                            beOrder.Enabled = true;
                            btnReceiveStore.Enabled = true;
                            cboMoveType.Enabled = true;
                            cboStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                            cboMoveType.SelectedIndex = 0;
                            meDesc.Text = "";
                            pcHeaderData.Enabled = true;
                            pcSearchCondition.Enabled = true;
                            //打印并修改打印次数
                            if (header.docStatus.Equals(Convert.ToInt32(DocStatus.VALID)))
                            {
                                PrintAndUpdatePrintNum(header.docId);
                            }
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

        #region 获取发货命令头和明细数据
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
                            if (deliveryItemlist.Count > 0)
                            {
                                deliveryType = 1;
                                gcDelivery.DataSource = deliveryItemlist;
                                gcDelivery.RefreshDataSource();
                                cboMoveType.Enabled = false;
                                btnReceiveStore.Text = string.IsNullOrEmpty(headerCommand.storeNameTo) ? "" : headerCommand.storeNameTo;
                                btnReceiveStore.Value = string.IsNullOrEmpty(headerCommand.productStoreIdTo) ? "" : headerCommand.productStoreIdTo;
                                btnReceiveStore.Enabled = false;
                                beOrder.Value = detailCommand[0].docId;
                                cboMoveType.EditValue = headerCommand.movementTypeId;
                                meCommandMemo.Text = headerCommand.memo;
                                gvDelivery.Columns["deliveryQuantity"].Visible = true;
                                gvDelivery.Columns["deliveryQuantity"].VisibleIndex = 8;
                                gvDelivery.Columns["planQuantity"].Visible = true;
                                gvDelivery.Columns["planQuantity"].VisibleIndex = 7;
                                return;
                            }
                        }
                    }
                    else
                    {
                        deliveryType = 0;
                        cboMoveType.Enabled = true;
                        cboMoveType.SelectedIndex = 0;
                        btnReceiveStore.Enabled = true;
                        btnReceiveStore.Text = "";
                        headerCommand = null;
                        deliveryItemlist.Clear();
                        gcDelivery.DataSource = null;
                        gcDelivery.RefreshDataSource();
                        gvDelivery.Columns["deliveryQuantity"].Visible = false;
                        gvDelivery.Columns["planQuantity"].Visible = false;
                        gvDelivery.Columns["availableStock"].Visible = true;
                        gvDelivery.Columns["availableStock"].VisibleIndex = 6;
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加发货数据
        public void InsertDeliveryData(DeliveryProduct product)
        {
            DeliveryItemDetail itemDetail = new DeliveryItemDetail();
            itemDetail.lineNo = deliveryItemlist.Count.ToString();
            itemDetail.docId = teDeliveryNo.Text;
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
            deliveryItemlist.Add(itemDetail);
            gcDelivery.DataSource = deliveryItemlist;
            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(deliveryItemlist.Count - 1);
            gcDelivery.RefreshDataSource();
            ClearConditionText();
        }
        #endregion

        #region 修改发货前数量
        private void gvDelivery_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == Quantity)
            {
                if (gvDelivery.GetFocusedRowCellValue(Quantity) == null)
                {
                    deliveryNum = 0;
                }
                else
                {
                    deliveryNum = Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(Quantity));
                }
            }
        }
        #endregion

        #region 修改发货后数量
        private void gvDelivery_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == Quantity)
                {
                    string isSequence = gvDelivery.GetFocusedRowCellValue("isSequence") == null ? "" : gvDelivery.GetFocusedRowCellValue("isSequence").ToString();
                    if (gvDelivery.GetFocusedRowCellValue(Quantity) == null
                        || Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(Quantity)) == 0)
                    {
                        return;
                    }
                    if (Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(Quantity)) < 0)
                    {
                        gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                        return;
                    }
                    if (!string.IsNullOrEmpty(isSequence))
                    {
                        #region 自主调拨
                        if (deliveryType == 0)
                        {
                            int deliveryNumAfter = gvDelivery.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(Quantity));
                            int availableQuantity = gvDelivery.GetFocusedRowCellValue(AvailableStockQuantity) == null ? 0 : Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(AvailableStockQuantity));
                            int sumDelivery = (int)deliveryItemlist.Where(p => p.productId.Equals(gvDelivery.GetFocusedRowCellValue(productId))).Sum(p => p.quantity);
                            string sequenceId = gvDelivery.GetFocusedRowCellValue("sequenceId") == null ? null : gvDelivery.GetFocusedRowCellValue("sequenceId").ToString();
                            if (isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                if (string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 1)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 1)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId) && (deliveryNumAfter > 1 || sumDelivery > availableQuantity))
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, 0);
                                    return;
                                }
                            }
                            else if (isSequence.Equals(ProductSequenceManager.sequenceN))
                            {
                                if (deliveryNumAfter > availableQuantity
                                    || availableQuantity <= 0)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                            }
                        }
                        #endregion

                        #region 指令调拨
                        if (deliveryType == 1)
                        {
                            int planQuantity = gvDelivery.GetFocusedRowCellValue(PlanDeliveryQuantity) == null ? 0 : (int)gvDelivery.GetFocusedRowCellValue(PlanDeliveryQuantity);
                            int deliveryQuantity = gvDelivery.GetFocusedRowCellValue(DeliveryQuantity) == null ? 0 : Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(DeliveryQuantity));
                            int deliveryNumAfter = gvDelivery.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvDelivery.GetFocusedRowCellValue(Quantity));
                            string sequenceId = gvDelivery.GetFocusedRowCellValue("sequenceId") == null ? null : gvDelivery.GetFocusedRowCellValue("sequenceId").ToString();
                            if (isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                if (string.IsNullOrEmpty(sequenceId) && deliveryNumAfter > 0)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(sequenceId)
                                    && deliveryNumAfter > 1)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                            }
                            else if (isSequence.Equals(ProductSequenceManager.sequenceN))
                            {
                                if (deliveryNumAfter > planQuantity - deliveryQuantity)
                                {
                                    gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        gvDelivery.SetFocusedRowCellValue(Quantity, deliveryNum);
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

        #region 发货单赋值
        public void InsertDeliveryItem(DeliveryItemDetailCommand command)
        {
            try
            {
                DeliveryItemDetail detail = new DeliveryItemDetail();
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
                detail.baseEntry = command.docId;
                detail.baseLineNo = command.lineNo;
                detail.config = command.config;
                detail.deliveryQuantity = command.deliveryQuantity;
                detail.docId = teDeliveryNo.Text.Trim();
                detail.facilityId = command.facilityId;
                detail.facilityIdTo = command.facilityIdTo;
                detail.facilityName = command.facilityName;
                detail.facilityNameTo = command.facilityNameTo;
                detail.idValue = command.idValue;
                detail.isSequence = command.isSequence;
                detail.lineNo = deliveryItemlist.Count.ToString();
                detail.memo = "";
                detail.onHand = command.onHand;
                detail.promise = command.promise;
                detail.productId = command.productId;
                detail.productName = command.productName;
                detail.sequenceId = command.sequenceId;
                detail.isSequence = command.isSequence;
                deliveryItemlist.Add(detail);
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 选择收货门店
        private void btnReceiveStore_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(btnReceiveStore.Text.Trim()))
                    {
                        btnReceiveStore.ShowForm();
                    }
                    else
                    {
                        btnReceiveStore.Text = "";
                        btnReceiveStore.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
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
                    docId = commandNo,
                    orderType = MoveType.goodsDelivery
                };
                detailCommand = null;
                if (DevCommon.getDataByWebService("GoodsDeliveryItemCommand", "DeliveryItemCommandSearch", searchItem, ref detailCommand) == RetCode.OK)
                {
                    if (detailCommand == null)
                    {
                        headerCommand = null;
                        return;
                    }
                    else
                    {
                        List<DeliveryItemDetailCommand> item = detailCommand.Where(p => p.quantity - p.deliveryQuantity > 0).ToList();
                        if (item.Count == 0)
                        {
                            headerCommand = null;
                            m_frm.PromptInformation("已完成的发货单！");
                            return;
                        }
                        else
                        {
                            deliveryItemlist.Clear();
                            gcDelivery.DataSource = null;
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

        #region 根据指令单号获得指令单头数据
        public void GetCommandHeaderData(string commandNo)
        {
            var searchCondition = new
            {
                docId = commandNo,
                orderType = MoveType.goodsDelivery,
                productStoreId = LoginInfo.ProductStoreId
            };
            List<DeliveryHeaderDetailCommand> headerCommandItem = null;
            if (DevCommon.getDataByWebService("GoodsDeliveryCommand", "DeliveryCommandSearch", searchCondition, ref headerCommandItem) == RetCode.OK)
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
                        m_frm.PromptInformation("已完成的发货单！");
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

        #region 添加发货数据
        public bool InsertDelivery(DeliveryHeader header, List<DeliveryItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
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

        #region 修改发货头数据
        public bool UpdateReceiveHeader(DeliveryHeader header)
        {
            try
            {
                var searchCondition = new
                {
                    header = header
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsDelivery", "UpdateGoodsDelivery", searchCondition, ref result) == RetCode.OK)
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

        #region 修改发货数据
        public bool UpdateHeaderItem(DeliveryHeader header, List<DeliveryItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("UpdateGoodsDelivery", "UpdateGoodsDelivery", searchCondition, ref result) == RetCode.OK)
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
                        #region 指令发货
                        if (deliveryType == 1)
                        {
                            var con = new
                            {
                                condition.productId,
                                condition.productName,
                                condition.idValue
                            };
                            deliveryProductList = null;
                            if (DevCommon.getDataByWebService("ProductInfo", "ProductInfo", con, ref deliveryProductList) == RetCode.OK)
                            {
                                AddDeliveryRowDataNotSeq(deliveryProductList, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion

                        #region 自主调拨
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
                            deliveryProductList = null;
                            if (DevCommon.getDataByWebService("ProductAndSequenceSearch", "ProductAndSequenceSearch", con, ref deliveryProductList) == RetCode.OK)
                            {
                                AddDeliveryRowData(deliveryProductList, ((SearchButton)sender));
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

                        #region 指令发货
                        if (deliveryType == 1)
                        {
                            deliveryProductList = null;
                            var con = new
                            {
                                sequenceId = condition.sequenceId,
                                productStoreId = condition.productStoreId
                            };
                            if (DevCommon.getDataByWebService("ProductSequenceInfo", "ProductSequenceInfo", con, ref deliveryProductList) == RetCode.OK)
                            {
                                AddDeliveryRowDataSeq(deliveryProductList, ((SearchButton)sender));
                            }
                            else
                            {
                                return;
                            }
                        }
                        #endregion

                        #region 自主调拨
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
                            deliveryProductList = null;
                            if (DevCommon.getDataByWebService("ProductAndSequenceSearch", "ProductAndSequenceSearch", con, ref deliveryProductList) == RetCode.OK)
                            {
                                AddDeliveryRowData(deliveryProductList, ((SearchButton)sender));
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

        #region 根据指令发货时，添加发货行的发货数
        public void AddDeliveryRowDataNotSeq(List<DeliveryProduct> list, SearchButton button)
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
                        ClearConditionText();
                        return;
                    }
                    List<DeliveryItemDetail> listDetail = deliveryItemlist.Where(p =>
                        p.productId == list[0].ProductId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceN).ToList();
                    if (listDetail.Count > 0)
                    {
                        bool sign = false;
                        for (int i = 0; i < deliveryItemlist.Count; i++)
                        {
                            if (deliveryItemlist[i].productId == list[0].ProductId && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceN)
                            {
                                int planQuantity = deliveryItemlist[i].planQuantity == null ? 0 : (int)deliveryItemlist[i].planQuantity;
                                int deliveryQuantity = deliveryItemlist[i].deliveryQuantity == null ? 0 : (int)deliveryItemlist[i].deliveryQuantity;
                                int quantity = deliveryItemlist[i].quantity == null ? 0 : (int)deliveryItemlist[i].quantity;
                                if (quantity < planQuantity - deliveryQuantity)
                                {
                                    deliveryItemlist[i].quantity = quantity + 1;
                                    gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                    gcDelivery.RefreshDataSource();
                                    sign = true;
                                    ClearConditionText();
                                    return;
                                }
                            }
                        }
                        if (!sign)
                        {
                            m_frm.PromptInformation("商品：" + list[0].ProductId + "在此发货单发货数量以满足！");
                            ClearConditionText();
                            return;
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("商品：" + list[0].ProductId + "不在此发货单！");
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

        #region 根据指令发货时，添加发货行的发货数
        public void AddDeliveryRowDataSeq(List<DeliveryProduct> list, SearchButton button)
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
                    List<DeliveryItemDetail> listDetail = deliveryItemlist.Where(p =>
                        !string.IsNullOrEmpty(p.sequenceId)
                        && p.sequenceId == list[0].SequenceId
                        && !string.IsNullOrEmpty(p.isSequence)
                        && p.isSequence == ProductSequenceManager.sequenceY).ToList();
                    if (listDetail.Count > 0)
                    {
                        for (int i = 0; i < deliveryItemlist.Count; i++)
                        {
                            if (deliveryItemlist[i].productId == list[0].ProductId
                                && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceY
                                && deliveryItemlist[i].sequenceId == list[0].SequenceId)
                            {
                                int quantity = deliveryItemlist[i].quantity == null ? 0 : (int)deliveryItemlist[i].quantity;
                                if (quantity > 0)
                                {
                                    m_frm.PromptInformation("串号：" + list[0].SequenceId + "在此发货单发货数量以满足！");
                                    gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                    ClearConditionText();
                                    return;
                                }
                                else
                                {
                                    deliveryItemlist[i].quantity = quantity + 1;
                                    gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                    gcDelivery.RefreshDataSource();
                                    ClearConditionText();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_frm.PromptInformation("串号：" + list[0].SequenceId + "不在此发货单！");
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

        #region 清空文本
        public void ClearConditionText()
        {
            btnProductId.Text = "";
            btnProductId.Value = null;
            btnProductName.Text = "";
            btnProductName.Value = null;
            btnSerial.Text = "";
            btnSerial.Value = null; ;
            btnSequence.Text = "";
            btnSequence.Value = null;
        }
        #endregion

        #region 自主调拨发货，添加行数据
        public void AddDeliveryRowData(List<DeliveryProduct> list, SearchButton button)
        {
            try
            {
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        if (deliveryItemlist.Count > 0)
                        {
                            if (list[0].isSequence.Equals(ProductSequenceManager.sequenceY))
                            {
                                #region 串号管理
                                //判断串号是否被占用
                                if (list[0].isOccupied == SequenceOccuepied.occupied)
                                {
                                    m_frm.PromptInformation("串号:" + list[0].SequenceId + "已被占用！");
                                    ClearConditionText();
                                    return;
                                }
                                List<DeliveryItemDetail> item = deliveryItemlist.Where(p =>
                                !string.IsNullOrEmpty(p.sequenceId)
                                && p.productId == list[0].ProductId
                                && p.sequenceId == list[0].SequenceId
                                ).ToList();
                                if (item.Count == 0)
                                {
                                    if (string.IsNullOrEmpty(list[0].SequenceId))
                                    {
                                        m_frm.PromptInformation("商品:" + list[0].ProductId + "是串号管理的商品，请输入串号!");
                                        ClearConditionText();
                                        return;
                                    }
                                    else
                                    {
                                        InsertDeliveryData(list[0]);
                                        return;
                                    }
                                }
                                else
                                {
                                    int sumDelivery = deliveryItemlist.Where(p => p.productId.Equals(list[0].ProductId)).Sum(p => p.quantity) == null ? 0 : (int)deliveryItemlist.Where(p => p.productId.Equals(list[0].ProductId)).Sum(p => p.quantity);
                                    for (int i = 0; i < deliveryItemlist.Count; i++)
                                    {
                                        int quantity = deliveryItemlist[i].quantity == null ? 0 : (int)deliveryItemlist[i].quantity;
                                        int availableQuantity = deliveryItemlist[i].availableStock == null ? 0 : (int)deliveryItemlist[i].availableStock;
                                        //可以库存不足
                                        if (availableQuantity <= 0)
                                        {
                                            m_frm.PromptInformation("商品:" + deliveryItemlist[i].productId + "可用库存不足！");
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            ClearConditionText();
                                            return;
                                        }
                                        //列表已存在此串号信息
                                        if (deliveryItemlist[i].productId == list[0].ProductId
                                              && deliveryItemlist[i].sequenceId == list[0].SequenceId
                                              && quantity > 0
                                            )
                                        {
                                            m_frm.PromptInformation("串号:" + list[0].SequenceId + "已在发货列表！");
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            ClearConditionText();
                                            return;
                                        }
                                        //串号信息存在，添加发货数量
                                        if (deliveryItemlist[i].productId == list[0].ProductId
                                              && deliveryItemlist[i].sequenceId == list[0].SequenceId
                                              && availableQuantity - sumDelivery > 0
                                              && quantity == 0
                                            )
                                        {
                                            deliveryItemlist[i].quantity = 1;
                                            gcDelivery.DataSource = deliveryItemlist;
                                            gcDelivery.RefreshDataSource();
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            gcDelivery.RefreshDataSource();
                                            ClearConditionText();
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
                                    ClearConditionText();
                                    return;
                                }
                                List<DeliveryItemDetail> item = deliveryItemlist.Where(p =>
                                    p.productId == list[0].ProductId
                                    && p.isSequence == ProductSequenceManager.sequenceN).ToList();
                                if (item.Count == 0)
                                {
                                    InsertDeliveryData(list[0]);
                                }
                                else
                                {
                                    for (int i = 0; i < deliveryItemlist.Count; i++)
                                    {
                                        int availableQuantity = deliveryItemlist[i].availableStock == null ? 0 : (int)deliveryItemlist[i].availableStock;
                                        int quantity = deliveryItemlist[i].quantity == null ? 0 : (int)deliveryItemlist[i].quantity;
                                        if (availableQuantity <= 0)
                                        {
                                            m_frm.PromptInformation("商品:" + deliveryItemlist[i].productId + "可用库存不足！");
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            ClearConditionText();
                                            return;
                                        }
                                        if (deliveryItemlist[i].productId == list[0].ProductId
                                              && availableQuantity - quantity > 0
                                              && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceN
                                            )
                                        {
                                            deliveryItemlist[i].quantity = deliveryItemlist[i].quantity + 1;
                                            gcDelivery.DataSource = deliveryItemlist;
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            gcDelivery.RefreshDataSource();
                                            ClearConditionText();
                                            return;
                                        }
                                        if (deliveryItemlist[i].productId == list[0].ProductId
                                            && availableQuantity == quantity
                                            && deliveryItemlist[i].isSequence == ProductSequenceManager.sequenceN)
                                        {
                                            m_frm.PromptInformation("商品:" + list[0].ProductId + "发货数量等于可以库存数！");
                                            gcDelivery.RefreshDataSource();
                                            gvDelivery.FocusedRowHandle = gvDelivery.GetRowHandle(i);
                                            ClearConditionText();
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
                                    ClearConditionText();
                                    return;
                                }
                                if (string.IsNullOrEmpty(list[0].SequenceId))
                                {
                                    m_frm.PromptInformation("商品:" + list[0].ProductId + "是串号管理商品，请输入串号！");
                                    ClearConditionText();
                                    return;
                                }
                                else
                                {
                                    InsertDeliveryData(list[0]);
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
                                    ClearConditionText();
                                    return;
                                }
                                InsertDeliveryData(list[0]);
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
                    ClearConditionText();
                    return;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 行编辑事件
        private void gvDelivery_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "sequenceId")
            {
                string isSequence = gvDelivery.GetFocusedRowCellValue("isSequence") == null ? "" : gvDelivery.GetFocusedRowCellValue("isSequence").ToString();
                string updateSign = gvDelivery.GetFocusedRowCellValue("updateSign") == null ? "" : gvDelivery.GetFocusedRowCellValue("updateSign").ToString();
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

        #region 根据移动类型获得仓库ID
        public void GetFacilityIdByMoveTypeId()
        {
            facilityId = DevCommon.GetFacilityId(LoginInfo.ProductStoreId, cboMoveType.EditValue.ToString());
        }
        #endregion

        #region 打印并修改打印次数
        private void PrintAndUpdatePrintNum(string docId)
        {
            try
            {
                RequisitionPrint print = new RequisitionPrint(docId, true);
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