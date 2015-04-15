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
using Commons.Model;
using DevExpress.XtraTab;

namespace Member
{
    public partial class AddMember : BaseForm
    {
        #region 参数
        MemberModel member = new MemberModel();
        public MemberSearch memberSearch = null;
        public MemberDetailModel updateMember = new MemberDetailModel();
        #endregion

        #region 构造
        public AddMember()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void AddMember_Load(object sender, EventArgs e)
        {
            //性别
            createGender();
            //会员等级
            memberGrade();
            if (memberSearch != null)
            {
                //文本框赋值
                assignTextBox();
                btnAdd.Text = "修改";
                teUserName.Properties.ReadOnly = true;
                teProductStoreId.Properties.ReadOnly = true;
            }
            else
            {
                //创建时间
                deCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //门店名称
                teProductStoreId.Text = LoginInfo.StoreName;
            }
        }
        #endregion

        #region 性别
        private void createGender()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Gender", typeof(string));
                dt.Columns.Add("Desc", typeof(string));
                DataRow drMale = dt.NewRow();
                drMale["Gender"] = "1";
                drMale["Desc"] = "男";
                dt.Rows.Add(drMale);
                DataRow drFemale = dt.NewRow();
                drFemale["Gender"] = "2";
                drFemale["Desc"] = "女";
                dt.Rows.Add(drFemale);
                if (dt != null)
                {
                    DevCommon.initCmb(cboGender, dt, false);
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
            if (memberSearch != null)
            {
                ((XtraTabPage)this.Parent).Text = "会员查询";
                memberSearch.Visible = true;
                memberSearch.BringToFront();
                m_frm.PromptInformation("");
                this.Close();
            }
            else
            {
                m_frm.closeTab();
            }
        }
        #endregion

        #region 添加和修改事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region 验证信息
                //姓名
                if (string.IsNullOrEmpty(teUserName.Text.Trim()))
                {
                    m_frm.PromptInformation("请填写姓名！");
                    return;
                }
                //卡号
                if (string.IsNullOrEmpty(teCardId.Text.Trim()))
                {
                    m_frm.PromptInformation("请填写卡号！");
                    return;
                }
                //创建时间
                if (string.IsNullOrEmpty(deCreateDate.Text.Trim()))
                {
                    m_frm.PromptInformation("请填写创建时间！");
                    return;
                }
                //电话号码
                if (string.IsNullOrEmpty(tePhoneMobile.Text.Trim()))
                {
                    m_frm.PromptInformation("请填写电话号码！");
                    return;
                }
                //会员等级
                if (string.IsNullOrEmpty(cboDescription.Text.Trim()))
                {
                    m_frm.PromptInformation("会员等级不能为空！");
                    return;
                }
                #endregion

                #region 验证电话号码是否存在
                MemberDetailModel detail = null;
                if (memberSearch != null)
                {
                    if (!tePhoneMobile.Text.Trim().Equals(updateMember.phoneMobile))
                    {
                        var phoneMobileCondition = new
                        {
                            phoneMobile = tePhoneMobile.Text.Trim()
                        };
                        if (DevCommon.getDataByWebService("getMemberByPhoneNo", "getMemberByPhoneNo", phoneMobileCondition, ref detail) == RetCode.OK)
                        {
                            if (detail != null)
                            {
                                m_frm.PromptInformation("手机号码已存在，请从新输入！");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    var phoneMobileCondition = new
                    {
                        phoneMobile = tePhoneMobile.Text.Trim()
                    };
                    if (DevCommon.getDataByWebService("getMemberByPhoneNo", "getMemberByPhoneNo", phoneMobileCondition, ref detail) == RetCode.OK)
                    {
                        if (detail != null)
                        {
                            m_frm.PromptInformation("手机号码已存在，请从新输入！");
                            return;
                        }
                    }
                }
                #endregion

                #region 添加会员信息
                //姓名
                if (memberSearch != null)
                {
                    member.partyId = updateMember.partyId;
                }
                member.firstName = teUserName.Text.Trim();
                //性别
                member.gender = cboGender.EditValue.ToString();
                //身份证号码
                member.socialSecurityNumber = teSocialSecurityNumber.Text.Trim();
                //门店编号
                member.productStoreId = LoginInfo.ProductStoreId;
                //卡号
                member.cardId = teCardId.Text.Trim();
                //等级
                member.partyClassificationGroupId = cboDescription.EditValue.ToString() ;
                //积分
                member.integral = teIntegral.Text.Trim();
                //创建时间
                member.createdDate = Convert.ToDateTime(deCreateDate.EditValue);
                //手机号码
                member.phoneMobile = tePhoneMobile.Text.Trim();
                //家庭电话
                member.phoneHome = tePhoneHome.Text.Trim();
                //办公电话
                member.phoneWork = tePhoneWork.Text.Trim();
                //传真号码
                member.faxNumber = teFaxNumber.Text.Trim();
                //电子邮箱
                member.emailAddress = teEmailAddress.Text.Trim();
                //联系地址
                member.address1 = teAddress1.Text.Trim();
                #endregion

                #region 添加和修改
                var searchConditions = new
                {
                   member=member
               };
                string result = null;
                if (memberSearch != null)
                {
                    if (DevCommon.getDataByWebService("MemberUpdate", "MemberUpdate", searchConditions, ref result) == RetCode.OK)
                    {
                        memberSearch.Reload();
                        memberSearch.Visible = true;
                        memberSearch.BringToFront();
                        this.Close();
                        return;
                    }
                    else
                    {
                        m_frm.PromptInformation("修改失败！");
                        return;
                    }
                }
                else
                {
                    if (DevCommon.getDataByWebService("MemberInsert", "MemberInsert", searchConditions, ref result) == RetCode.OK)
                    {
                        clear();
                        m_frm.PromptInformation("添加成功！");
                        return;
                    }
                    else
                    {
                        m_frm.PromptInformation("添加失败！");
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                m_frm.PromptInformation(ex.Message);
            }
        }
        #endregion

        #region 清空信息
        private void clear()
        {
            teUserName.Text = "";
            teSocialSecurityNumber.Text = "";
            teCardId.Text = "";
            deCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tePhoneMobile.Text = "";
            tePhoneHome.Text = "";
            tePhoneWork.Text = "";
            teFaxNumber.Text = "";
            teEmailAddress.Text = "";
            teAddress1.Text = "";
            member = new MemberDetailModel();
        }
        #endregion

        #region 文本框赋值
        private void assignTextBox()
        {
            teUserName.Text = updateMember.firstName;
            teSocialSecurityNumber.Text = updateMember.socialSecurityNumber;
            teCardId.Text = updateMember.cardId;
            deCreateDate.Text = updateMember.createdDate == null ? null : Convert.ToDateTime(updateMember.createdDate).ToString("yyyy-MM-dd HH:mm:ss");
            tePhoneMobile.Text = updateMember.phoneMobile;
            tePhoneHome.Text = updateMember.phoneHome;
            tePhoneWork.Text = updateMember.phoneWork;
            teFaxNumber.Text = updateMember.faxNumber;
            teEmailAddress.Text = updateMember.email;
            teAddress1.Text = updateMember.address1;
            teProductStoreId.Text = updateMember.storeName;
            cboGender.EditValue = updateMember.gender;
            cboDescription.EditValue = updateMember.partyClassificationGroupId;
        }
        #endregion

        #region 会员等级
        private void memberGrade()
        {
            try
            {
                DataTable dtGrade= DevCommon.MemberGrade();
                if (dtGrade != null)
                {
                    DevCommon.initCmb(cboDescription, dtGrade, "partyClassificationGroupId", "description", false);
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