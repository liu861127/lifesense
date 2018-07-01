using lifesense.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.Static
{
    public partial class StaticExceptionForm : System.Web.UI.Page
    {
        lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime cuttentTime = DateTime.Now;
                txtstartime.Text = cuttentTime.AddDays(1 - cuttentTime.Day).ToString("yyyy-MM-dd");
                txtenddate.Text = cuttentTime.ToString("yyyy-MM-dd");
                LoadData();
            }
        }

        private void LoadData()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(txtstartime.Text) && !string.IsNullOrEmpty(txtenddate.Text))
            {
                sb.AppendFormat("and cast (WriteTime as date) between '{0}' and '{1}'", Convert.ToDateTime(txtstartime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MM-dd"));
            }
            
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                sb.AppendFormat(" and UserID like '%{0}%'", txtUserName.Text.Trim());
            }
            string sql = string.Format("select *,UserID+'|'+ cast (cast(WriteTime as date) as varchar(10)) as ID from t_failrequestInfo where 1=1   {0} ", sb.ToString());

            DataSet ds2 = userbll.GetSqlList(sql);
            DataSet ds = userbll.ExecuteSqlPager(sql, "UserID", AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            Gdv_data.DataSource = ds.Tables[0];
            Gdv_data.DataBind();
            int RecordCount = ds2.Tables[0].Rows.Count;
            AspNetPager1.PageSize = RecordCount;
            AspNetPager1.RecordCount = RecordCount;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSysData_Click(object sender, EventArgs e)
        {
            ConsoleLifesense.SyncDataManager sycdataBll = new ConsoleLifesense.SyncDataManager();
            List<Model.t_failrequestInfo> listmodel=new List<Model.t_failrequestInfo>();
            string strIDlist = GetSelIDlist();
            BLL.t_failrequestInfo failBll = new BLL.t_failrequestInfo();
            List<string> listKey = strIDlist.Split(',').ToList();
            foreach(string item in listKey)
            {
                listmodel.Add(failBll.GetModel(item.Split('|')[0], Convert.ToDateTime(item.Split('|')[1])));
            }
            string msg=string.Empty;
            sycdataBll.syncExceptionData(listmodel, out msg);
            if(!string.IsNullOrEmpty(msg))
            {
                Maticsoft.Common.MessageBox.Show(this, msg);
            }
        }
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < Gdv_data.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)Gdv_data.Rows[i].FindControl("DeleteThis");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (Gdv_data.DataKeys[i].Value != null)
                    {
                        idlist += Gdv_data.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        protected void BtnShow_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = AspNetPager1.RecordCount;
            LoadData();
            ExportExcel.GetExportExcel(Gdv_data, "异常发送数据列表");
            //AspNetPager1.CurrentPageIndex = 1;
            //AspNetPager1.PageSize = 10;
            //LoadData();
        }

        protected void chkIsAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bolResult = chkIsAll.Checked;
            for (int i = 0; i < Gdv_data.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)Gdv_data.Rows[i].FindControl("DeleteThis");
                ChkBxItem.Checked = bolResult;
            }
        }
        /// <summary>
        /// 去掉"....必须放在具有 runat=server 的窗体标记内"异常
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}