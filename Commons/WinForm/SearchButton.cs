using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Commons.WinForm
{
    public partial class SearchButton : ButtonEdit
    {
        #region 构造
        public SearchButton()
        {
            InitializeComponent();
            this.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.SearchButton_ButtonClick);
        }
        #endregion

        #region 按钮单击事件
        private void SearchButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ShowSearchForm(false);
        }
        #endregion

        #region 弹出窗体
        public void ShowForm()
        {
            ShowSearchForm(true);
        }
        #endregion

        #region 属性设置
        /// <summary>
        /// 查询条件列名
        /// </summary>
        [Category("Alignment"), Description("FieldColunm")]
        public string FieldColumn { get; set; }

        /// <summary>
        /// 要返回的Key值列
        /// </summary>
        [Category("Alignment"), Description("ValueFieldColumn")]
        public string ValueFieldColumn { set; get; }
        /// <summary>
        /// 要返回的Value值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// XML条件名称
        /// </summary>
        [Category("Alignment"), Description("XMLConditionModel")]
        public string XMLConditionModel { get; set; }
        /// <summary>
        /// XML条件路径
        /// </summary>
        [Category("Alignment"), Description("XMLConditionPath")]
        public string XMLConditionPath { get; set; }
        /// <summary>
        /// XML条件名称
        /// </summary>
        [Category("Alignment"), Description("XMLWhereAndColumnModel")]
        public string XMLWhereAndColumnModel { get; set; }
        /// <summary>
        /// XML条件文件路径
        /// </summary>
        [Category("Alignment"), Description("XMLWhereAndColumnPath")]
        public string XMLWhereAndColumnPath { get; set; }
        /// <summary>
        /// 返回值列名
        /// </summary>
        [Category("Alignment"), Description("XMLWhereAndColumnPath")]
        public string FieldColumnOtherName { get; set; }
        /// <summary>
        /// 缓存主键
        /// </summary>
        [Category("Alignment"), Description("PrimaryKey")]
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 查询完成时候是否触发回车事件
        /// </summary>
        [Category("Alignment"), Description("PrimaryKey")]
        public bool EnterEnent { get; set; }
        /// <summary>
        /// 是否设置返回
        /// </summary>
        public bool RtnEvent { get; set; }
        /// <summary>
        /// Where条件
        /// </summary>
        [Category("Alignment"), Description("where")]
        public DataTable where { get; set; }
        #endregion

        #region 查询窗体调用
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="status"></param>
        private void ShowSearchForm(bool status)
        {
            SearchForm form = new SearchForm(
                this.XMLConditionPath,
                this.XMLConditionModel,
                this.XMLWhereAndColumnPath,
                this.XMLWhereAndColumnModel,
                this.FieldColumn,
                this.Text.Trim(),
                this.ValueFieldColumn,
                this.FieldColumnOtherName,
                this.PrimaryKey,
                status,
                this.RtnEvent
                );
            if (where != null)
            {
                form.dtWhere = where.Copy();
            }
            else
            {
                form.dtWhere = null;
            }
            form.ShowDialog();
            if (!string.IsNullOrEmpty(form.RtnValue))
            {
                this.Value = form.RtnValue;
                if (EnterEnent)
                {
                    SendKeys.Send("{enter}");
                }
            }
            if (!string.IsNullOrEmpty(form.RtnText))
            {
                this.Text = form.RtnText;
            }
        }
        #endregion
    }
}
