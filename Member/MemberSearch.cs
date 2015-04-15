using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model.Member;
using DevExpress.XtraTab;
using System.Linq;
using Commons.Model;

namespace Member
{
    public partial class MemberSearch : BaseForm
    {
        #region 参数
        List<MemberDetailModel> memberList = null;
        string name = null;
        string gender = null;
        string phoneMobile = null;
        string startCreatedDate = null;
        string endCreatedDate = null;
        string productStoreId = null;
        string cardId = null;
        string partyClassificationGroupId = null;
        string address = null;
        #endregion

        #region 构造
        public MemberSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void MemberSearch_Load(object sender, EventArgs e)
        {
            //获得性别
            createGender();
            //会员等级
            memberGrade();
        }
        #endregion

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            name = teName.Text.Trim();
            gender = string.IsNullOrEmpty(cboGender.Text.Trim()) ? null : cboGender.EditValue.ToString();
            phoneMobile = tePhoneMobile.Text.Trim();
            startCreatedDate = string.IsNullOrEmpty(deStartCerated.Text.Trim()) ? null : Convert.ToDateTime(deStartCerated.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
            endCreatedDate = string.IsNullOrEmpty(deEndCreated.Text.Trim()) ? null : Convert.ToDateTime(deEndCreated.EditValue).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            productStoreId = btnStore.Value;
            cardId = teCardId.Text.Trim();
            partyClassificationGroupId = string.IsNullOrEmpty(cboDescription.Text.Trim()) ? null : cboDescription.EditValue.ToString();
            address = teAddress.Text.Trim();
            Data();
        }
        #endregion

        #region 修改会员事件
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvMemberInfo.RowCount > 0)
                {
                    //会员编号
                    string partyId = gvMemberInfo.GetFocusedRowCellValue("partyId").ToString();
                    //开卡店铺
                    string productStoreId = gvMemberInfo.GetFocusedRowCellValue("productStoreId") == null ? null : gvMemberInfo.GetFocusedRowCellValue("productStoreId").ToString();
                    if (productStoreId == LoginInfo.ProductStoreId&&!string.IsNullOrEmpty(productStoreId))
                    {
                        MemberDetailModel member = memberList.FirstOrDefault(p => p.partyId == partyId);
                        AddMember add = new AddMember();
                        add.m_frm = m_frm;
                        add.updateMember = member;
                        add.memberSearch = this;
                        add.Location = new Point(0, 0);
                        add.TopLevel = false;
                        add.TopMost = false;
                        add.ControlBox = false;
                        add.FormBorderStyle = FormBorderStyle.None;
                        add.Dock = DockStyle.Fill;
                        this.Visible = false;
                        ((XtraTabPage)this.Parent).Controls.Add(add);
                        ((XtraTabPage)this.Parent).Text = "会员修改";
                        add.Show();
                        add.BringToFront();
                        m_frm.PromptInformation("");
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
            m_frm.closeTab();
        }
        #endregion

        #region 会员信息方法
        public void Data()
        {
            try
            {
                var searchConditions = new
               {
                   name = name,
                   gender = gender,
                   phoneMobile = phoneMobile,
                   startCreatedDate = startCreatedDate,
                   endCreatedDate = endCreatedDate,
                   productStoreId = productStoreId,
                   cardId = cardId,
                   partyClassificationGroupId = partyClassificationGroupId,
                   address1 = address
               };
                memberList = null;
                if (DevCommon.getDataByWebService("MemberSearch", "MemberSearch", searchConditions, ref memberList) == RetCode.OK)
                {
                    gcMember.DataSource = memberList;
                }
                else
                {
                    gcMember.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 店铺搜索
        private void btnStore_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (!string.IsNullOrEmpty(btnStore.Text.Trim()))
                    {
                        btnStore.ShowForm();
                    }
                    else
                    {
                        btnStore.Text = "";
                        btnStore.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 性别
        private void createGender()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Desc", typeof(string));
                DataRow drMale = dt.NewRow();
                drMale["Id"] = "1";
                drMale["Desc"] = "男";
                dt.Rows.Add(drMale);
                DataRow drFemale = dt.NewRow();
                drFemale["Id"] = "2";
                drFemale["Desc"] = "女";
                dt.Rows.Add(drFemale);
                if (dt != null)
                {
                    DevCommon.initCmb(cboGender, dt, true);
                    DevCommon.initCmb(repositoryItemImageComboBox1, dt, false);
                }
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 重新加载数据
        public void Reload()
        {
            try
            {
                int rowhandel = gvMemberInfo.FocusedRowHandle;
                Data();
                gvMemberInfo.FocusedRowHandle = rowhandel;
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 会员等级
        private void memberGrade()
        {
            try
            {
                DataTable dtGrade = DevCommon.MemberGrade();
                if (dtGrade != null)
                {
                    DevCommon.initCmb(cboDescription, dtGrade, "partyClassificationGroupId", "description", true);
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