using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Commons.WinForm;
using Commons.Model;
using Commons.XML;
using System.Collections;
using Commons.WebService;

namespace SyncList
{
    public partial class SyncForm : BaseForm
    {
        public SyncForm()
        {
            InitializeComponent();
            m_BackgroundWorker = new BackgroundWorker(); // 实例化后台对象



            m_BackgroundWorker.WorkerReportsProgress = true; // 设置可以通告进度

            m_BackgroundWorker.WorkerSupportsCancellation = true; // 设置可以取消



            m_BackgroundWorker.DoWork += new DoWorkEventHandler(DoWork);

            m_BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgress);

            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompletedWork);
        }
        private BackgroundWorker m_BackgroundWorker;// 申明后台对象
        List<SyncModel> syncList=null;
        private void SyncForm_Load(object sender, EventArgs e)
        {
            
            XMLBase xmlbase = new XMLBase("Sync.xml");
            string str = xmlbase.ToString();
            syncList = (List<SyncModel>)XmlUtil.Deserialize(typeof(List<SyncModel>), str);
            

            gcSync.DataSource = syncList;
        }

        private void simpleButton_Detail_Click(object sender, EventArgs e)
        {
            m_BackgroundWorker.RunWorkerAsync(this);
        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            m_BackgroundWorker.CancelAsync();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

            string syncId = this.gvSync.GetFocusedRowCellValue(SyncName).ToString();
            string syncfun = this.gvSync.GetFocusedRowCellValue(WebFunc).ToString();
            string url = "";
            string func = "";
            GetData.GetUrl(syncfun, syncfun, out url, out func);

            Hashtable Pars = new Hashtable();
            Pars.Add("entityKey", syncId);
            String jsonOut = WebSvcCaller.QuerySoapWebService(Pars, url,
            func);
            int index = this.gvSync.GetFocusedDataSourceRowIndex();
            if (jsonOut.Equals("success"))
            {
               // this.gvSync.GetFocusedDataRow()["Result"] = "√";
                syncList[index].Result = "√";
            }
            else
            {
                syncList[index].Result  = "×";
                //this.gvSync.GetFocusedDataSourceRowIndex()["Result"] = "×";
            }
            gvSync.RefreshData();
            gcSync.Refresh();
                
            //this.gvSync.GetFocusedDataRow()["Result"];
        }
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string url = "";
            string func = "";
            int i = 0;
            foreach (SyncModel model in syncList)
            {

                GetData.GetUrl(model.WebFunc, model.WebFunc, out url, out func);

                Hashtable Pars = new Hashtable();
                Pars.Add("entityKey", model.SyncName);
                String jsonOut = WebSvcCaller.QuerySoapWebService(Pars, url,
                func);
                if (jsonOut.Equals("success"))
                {
                    // this.gvSync.GetFocusedDataRow()["Result"] = "√";
                    model.Result = "√";
                }
                else
                {
                    model.Result = "×";
                    //this.gvSync.GetFocusedDataSourceRowIndex()["Result"] = "×";
                }
                bw.ReportProgress(i++);
            }
        }
        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {

            int progress = e.ProgressPercentage;
            gvSync.RefreshData();
            gcSync.Refresh();


           // label1.Content = string.Format("{0}", progress);

        }





        void CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {

                MessageBox.Show("同步失败");

            }

            else if (e.Cancelled)
            {

                MessageBox.Show("同步取消");

            }

            else
            {

                MessageBox.Show("同步完成");

            }

        }
    }
}