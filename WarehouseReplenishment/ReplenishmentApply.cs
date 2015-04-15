using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model.Stock;
using Commons.Model;
using DevExpress.XtraTab;
using DevExpress.Utils.Menu;
using System.Linq;

namespace WarehouseReplenishment
{
    public partial class ReplenishmentApply : BaseForm
    {
        #region 参数
        public ReplenishmentSearch search = null;
        public ReplenishmentHeaderDetail headerItem = null;
        List<ReplenishmentItemDetail> itemList = new List<ReplenishmentItemDetail>();
        int num = 0;
        #endregion

        #region 构造
        public ReplenishmentApply()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void ReplenishmentApply_Load(object sender, EventArgs e)
        {
            try
            {
                deReceiveDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //获得状态数据
                GetStatusData();
                //单据状态禁用
                cboDocStatus.Enabled = false;
                if (search != null)
                {
                    pcHeader.Enabled = false;
                    teDocDate.Text = headerItem.docDate;
                    teUserName.Text = headerItem.updateUserName;
                    cboDocStatus.EditValue = headerItem.docStatus;
                    cboSaleDay.Text = headerItem.saleDay.ToString();
                    teOrderNo.Text = headerItem.docId;
                    meMemo.Text = headerItem.memo;
                    if (Convert.ToInt32(headerItem.docStatus) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        btnDeleteRow.Enabled = false;
                        gvReplenishment.Columns["quantity"].OptionsColumn.AllowEdit = false;
                        gvReplenishment.Columns["memo"].OptionsColumn.ReadOnly = true;
                        meMemo.Properties.ReadOnly = true;
                        btnDeleteRow.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    ReplenishmentItem();
                }
                else
                {
                    gvReplenishment.Columns["erpMemo"].Visible = false;
                    //制单时间
                    teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //制单人
                    teUserName.Text = LoginInfo.UserName;
                    //单据状态
                    cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT).ToString();
                    //获得单号
                    GetDocId();
                    //销售天数
                    SalaDay();
                    //数据
                    Data();
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
                if (DevCommon.getDocId(DocType.WarehouseReplenishment, ref docId) == RetCode.OK)
                {
                    teOrderNo.Text = docId;
                }
                else
                {
                    teOrderNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                teOrderNo.Text = "";
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 获得补货行明细数据
        public void ReplenishmentItem()
        {
            try
            {
                var searchConditions = new
                {
                    docId = teOrderNo.Text.Trim()
                };
                itemList = null;
                if (DevCommon.getDataByWebService("ReplenishmentItem", "ReplenishmentItem", searchConditions, ref itemList) == RetCode.OK)
                {
                    itemList = itemList.Where(p => p.quantity > 0 || p.promise > 0 || p.saftyQuantity > 0 || p.onhand > 0 || p.preQuantity > 0 || p.receiveQuantity > 0).OrderByDescending(p => p.quantity).ToList();
                    gcReplenishment.DataSource = itemList;
                }
                else
                {
                    gcReplenishment.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcReplenishment.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加销售天数
        public void SalaDay()
        {
            try
            {
                for (int i = 1; i <= 100; i++)
                {
                    cboSaleDay.Properties.Items.Add(i);
                }
                cboSaleDay.Text = "7";
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
            try
            {
                DeleteRow();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 删除行数据
        private void DeleteRow()
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    if (Convert.ToInt32(cboDocStatus.EditValue) == Convert.ToInt32(DocStatus.DRAFT))
                    {
                        gvReplenishment.DeleteSelectedRows();
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (search != null)
            {
                search.Visible = true;
                search.BringToFront();
                ((XtraTabPage)this.Parent).Text = "补货单查询";
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 销售天改变事件
        private void cboSaleDay_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                itemList = null;
                gcReplenishment.DataSource = null;
                Data();
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 查询补货数据
        public void Data()
        {
            try
            {
                var searchConditions = new
                {
                    saleDay = cboSaleDay.Text,
                    productStoreId = LoginInfo.ProductStoreId,
                    facilityTypeId = FacilityType.WarehouseTypeZP
                };
                if (DevCommon.getDataByWebService("ReplenishmentBeforItem", "ReplenishmentBeforItem", searchConditions, ref itemList) == RetCode.OK)
                {
                    if (itemList != null)
                    {
                        itemList = itemList.Where(p => p.quantity > 0 || p.promise > 0 || p.saftyQuantity > 0 || p.onhand > 0 || p.preQuantity > 0 || p.receiveQuantity > 0).OrderByDescending(p => p.quantity).ToList();
                        for (int i = 0; i < itemList.Count; i++)
                        {
                            itemList[i].lineNo = i.ToString();
                            itemList[i].docId = teOrderNo.Text.Trim();
                        }
                    }
                    gcReplenishment.DataSource = itemList;
                }
                else
                {
                    gcReplenishment.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                gcReplenishment.DataSource = null;
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 点击按钮保存草稿事件
        private void btnDraft_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 点击按钮保存确定单据事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    Insert(DocStatus.VALID);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 添加补货单方法
        public void InsertData(DocStatus type)
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    #region 添加头数据
                    ReplenishmentHeader header = new ReplenishmentHeader();
                    //单号
                    header.docId = teOrderNo.Text.Trim();
                    if (headerItem != null)
                    {
                        header.userId = LoginInfo.PartyId;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.postingDate = headerItem.postingDate;
                        header.docDate = teDocDate.Text;
                    }
                    else
                    {
                        header.userId = LoginInfo.PartyId;
                        header.updateUserId = LoginInfo.PartyId;
                        header.docDate = Convert.ToDateTime(teDocDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        header.updateDate = Convert.ToDateTime(teDocDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    header.productStoreId = LoginInfo.ProductStoreId;
                    header.partyId = LoginInfo.OwnerPartyId;
                    header.docStatus = Convert.ToInt32(type).ToString();
                    header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
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
                    List<ReplenishmentItemDetail> itemDetail = new List<ReplenishmentItemDetail>();
                    if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                    {
                        itemDetail = ((List<ReplenishmentItemDetail>)gcReplenishment.DataSource).Where(p => p.quantity > 0).ToList();
                    }
                    else
                    {
                        itemDetail = (List<ReplenishmentItemDetail>)gcReplenishment.DataSource;
                    }
                    if (itemDetail.Count == 0)
                    {
                        m_frm.PromptInformation("请添加要补货的数量！");
                        return;
                    }
                    List<ReplenishmentItem> item = (List<ReplenishmentItem>)itemDetail.Select(p => new ReplenishmentItem
                    {
                        docId = p.docId,
                        lineNo = p.lineNo,
                        productId = p.productId,
                        facilityId = p.facilityId,
                        memo = p.memo,
                        quantity = p.quantity == null ? 0 : p.quantity,
                        baseEntry = p.baseEntry,
                        baseLineNo = p.baseLineNo,
                        isSequence = p.isSequence,
                        idValue = p.idValue,
                        attrName1 = p.attrName1,
                        attrName2 = p.attrName2,
                        attrName3 = p.attrName3,
                        attrName4 = p.attrName4,
                        attrName5 = p.attrName5,
                        attrName6 = p.attrName6,
                        attrName7 = p.attrName7,
                        attrName8 = p.attrName8,
                        attrName9 = p.attrName9,
                        attrName10 = p.attrName10
                    }).ToList();
                    #endregion

                    #region 添加或修改补货单
                    if (search != null)
                    {
                        if (UpdateHeaderAndItem(header, item))
                        {
                            ((XtraTabPage)this.Parent).Text = "补货单查询";
                            this.Close();
                            search.Visible = true;
                            search.BringToFront();
                            search.Reload();
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
                            GetDocId();
                            gcReplenishment.DataSource = null;
                            itemList.Clear();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            cboDocStatus.EditValue = Convert.ToInt32(DocStatus.DRAFT);
                            meMemo.Text = "";
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

        #region 右键菜单
        private void gvReplenishment_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
            if (gvReplenishment.RowCount > 0)
            {
                DeleteRow();
            }
        }
        #endregion

        #region 修改补货单头数据和明细数据
        public bool UpdateHeaderAndItem(ReplenishmentHeader header, List<ReplenishmentItem> item)
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

        #region 添加收货数据
        public bool Insert(ReplenishmentHeader header, List<ReplenishmentItem> item)
        {
            try
            {
                var searchCondition = new
                {
                    header = header,
                    item = item
                };
                string result = null;
                if (DevCommon.getDataByWebService("InsertReplenishment", "InsertReplenishment", searchCondition, ref result) == RetCode.OK)
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

        #region 添加补货单
        public void Insert(DocStatus type)
        {
            {
                try
                {
                    if (gvReplenishment.RowCount > 0)
                    {
                        #region 添加头数据
                        ReplenishmentHeader header = new ReplenishmentHeader();
                        //单号
                        header.docId = teOrderNo.Text.Trim();
                        header.updateUserId = LoginInfo.PartyId;
                        header.userId = LoginInfo.PartyId;
                        header.productStoreId = LoginInfo.ProductStoreId;
                        header.status = Convert.ToInt32(BusinessStatus.NOTCLEARED).ToString();
                        header.docDate = teDocDate.Text;
                        header.updateDate = teDocDate.Text.Trim();
                        header.partyId = LoginInfo.OwnerPartyId;
                        header.docStatus = Convert.ToInt32(type).ToString();
                        header.memo = meMemo.Text.Trim();
                        header.saleDay = Convert.ToInt32(cboSaleDay.Text);
                        header.receiveDate = string.IsNullOrEmpty(deReceiveDate.Text.Trim()) ? null : Convert.ToDateTime(deReceiveDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        if (type == DocStatus.VALID)
                        {
                            header.isSync = "0";
                        }
                        #endregion

                        #region 添加发货行明细
                        //List<ReplenishmentItemDetail> itemDetail = new List<ReplenishmentItemDetail>();
                        //if (Convert.ToInt32(type) != Convert.ToInt32(DocStatus.DRAFT))
                        //{
                        //    itemDetail = itemList.Where(p => p.quantity > 0).ToList();
                        //}
                        //else
                        //{
                        //    itemDetail = (List<ReplenishmentItemDetail>)gcReplenishment.DataSource;
                        //}
                        //if (itemDetail.Count == 0)
                        //{
                        //    m_frm.PromptInformation("请添加要补货的数据！");
                        //    return;
                        //}
                        List<ReplenishmentItem> item = (List<ReplenishmentItem>)itemList.Select(p => new ReplenishmentItem
                        {
                            docId = p.docId,
                            lineNo = p.lineNo,
                            productId = p.productId,
                            facilityId = p.facilityId,
                            memo = p.memo,
                            quantity = p.quantity,
                            baseEntry = p.baseEntry,
                            baseLineNo = p.baseLineNo,
                            isSequence = p.isSequence,
                            promise = p.promise,
                            receiveQuantity = p.receiveQuantity,
                            onhand = p.onhand,
                            saftyQuantity = p.saftyQuantity,
                            availableQuantity = p.availableQuantity
                        }).ToList();
                        #endregion

                        #region 添加或修改收货数据操作
                        if (Insert(header, item))
                        {
                            meMemo.Text = "";
                            //获得单号
                            GetDocId();
                            teDocDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            gcReplenishment.DataSource = null;
                            itemList.Clear();
                            m_frm.closeTab();
                            return;
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
        }
        #endregion

        #region 修改列表事件
        private void gvReplenishment_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    if (e.Column == Quantity)
                    {
                        num = gvReplenishment.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvReplenishment.GetFocusedRowCellValue(Quantity));
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
        private void gvReplenishment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvReplenishment.RowCount > 0)
                {
                    if (e.Column == Quantity)
                    {
                        int updateNum = gvReplenishment.GetFocusedRowCellValue(Quantity) == null ? 0 : Convert.ToInt32(gvReplenishment.GetFocusedRowCellValue(Quantity));
                        if (updateNum == 0)
                        {
                            return;
                        }
                        if (updateNum < 0)
                        {
                            gvReplenishment.SetFocusedRowCellValue(Quantity, num);
                            gcReplenishment.DataSource = itemList.OrderByDescending(p => p.quantity);
                            gcReplenishment.RefreshDataSource();
                        }
                        gcReplenishment.DataSource = itemList.OrderByDescending(p => p.quantity);
                        gcReplenishment.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
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
                    #region 根据商品代码、商品名称、条形码查询
                    if (sender.Equals(btnProductId) || sender.Equals(btnProductName) || sender.Equals(btnBarCode))
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
                        else if (sender.Equals(btnBarCode))
                        {
                            if (string.IsNullOrEmpty(btnBarCode.Value))
                            {
                                condition.idValue = btnBarCode.Text.Trim();
                            }
                            else
                            {
                                condition.productId = btnBarCode.Value.Trim();
                                condition.idValue = btnBarCode.Text.Trim();
                            }
                        }
                        var con = new
                             {
                                 condition.productId,
                                 condition.productName,
                                 condition.idValue
                             };
                        List<ReplenishmentProduct> productList = null;
                        if (DevCommon.getDataByWebService("ProductInfo", "ProductInfo", con, ref productList) == RetCode.OK)
                        {
                            if (productList == null)
                            {
                                ((SearchButton)sender).ShowForm();
                                return;
                            }
                            else
                            {
                                ReplenishmentData(productList, ((SearchButton)sender));
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

        #region 添加补货行数据
        public void ReplenishmentData(List<ReplenishmentProduct> list, SearchButton button)
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
                    List<ReplenishmentItemDetail> listDetail = itemList.Where(p =>
                        p.productId == list[0].productId
                        ).ToList();
                    if (listDetail.Count > 0)
                    {
                        for (int i = 0; i < itemList.Count; i++)
                        {
                            if (itemList[i].productId == list[0].productId)
                            {
                                gvReplenishment.FocusedRowHandle = i;
                                break;
                            }
                        }
                        m_frm.PromptInformation("商品：" + list[0].productId + "已在此补货单！");
                        Clear();
                        return;
                    }
                    else
                    {
                        AddReplenishmentItem(list[0]);
                        Clear();

                    }
                }
                //查询到的值是多条数据，进入搜索帮助页面
                if (list.Count > 1)
                {
                    button.ShowForm();
                    return;
                }
            }
        }
        #endregion

        #region 清空文本
        public void Clear()
        {
            btnProductId.Text = "";
            btnProductId.Value = null;
            btnProductName.Text = "";
            btnProductName.Value = null;
            btnBarCode.Text = "";
            btnBarCode.Value = null;
        }
        #endregion

        #region 添加补货信息
        public void AddReplenishmentItem(ReplenishmentProduct product)
        {
            if (itemList == null)
            {
                itemList = new List<ReplenishmentItemDetail>();
            }
            ReplenishmentItemDetail item = new ReplenishmentItemDetail();
            item.productId = product.productId;
            item.productName = product.productName;
            item.lineNo = itemList.Count == 0 ? "0" : (Convert.ToInt32(itemList.Max(p => Convert.ToInt32(p.lineNo))) + 1).ToString();
            item.docId = teOrderNo.Text;
            itemList.Add(item);
            int rowhandel = itemList.Count;
            gcReplenishment.DataSource = itemList;
            gcReplenishment.RefreshDataSource();
            gvReplenishment.FocusedRowHandle = rowhandel - 1;
        }
        #endregion
    }
}