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
    public partial class StaticForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime cuttentTime=DateTime.Now;
                txtstartime.Text = cuttentTime.AddDays(1 - cuttentTime.Day).ToString("yyyy-MM-dd");
                txtenddate.Text = cuttentTime.ToString("yyyy-MM-dd");
                LoadData();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
            StringBuilder sb=new StringBuilder();
            if (!string.IsNullOrEmpty(ddltimType.Text.Trim()))
            {
                sb.AppendFormat("and cast ({0} as date) between '{1}' and '{2}'", ddltimType.SelectedValue.ToString(), Convert.ToDateTime(txtstartime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MM-dd"));
            }
             if (!string.IsNullOrEmpty(txtUserID.Text.Trim()))
             {
                 sb.AppendFormat("and u.UserID like '%{0}%' ", txtUserID.Text.Trim());
             }
             string sql = string.Format(@"SELECT u.UserID AS 用户ID,
                                           w.MeasureTime AS 测量时间,
                                           w.StepNum AS 步数,
                                           w.Calorie AS 卡里路,
                                           w.Mileage AS 里程,
                                           s.SleepingTime AS 入睡时间,
                                           s.WakingTime AS 醒来时间,
                                           s.LongSleepNum AS '深睡时长(分钟)',
                                           s.ShallowSleepNum AS '浅睡时长(分钟)',
                                           s.WakeUpLong AS '醒来时长（分钟)',
                                           s.WakingNum AS 醒来次数,
                                           h.StartTime AS '心率测量开始时间',
                                           h.HeartRate AS '心率'
                                    FROM t_userinfo AS u
                                         LEFT JOIN t_walkinfo AS w ON u.UserID = w.UserID 
                                         LEFT JOIN t_sleepinfo AS s ON u.UserID = s.UserID and cast(w.MeasureTime as date)=cast(s.SleepingTime as date)
                                         LEFT JOIN t_heartrateinfo AS h ON u.UserID = h.UserID and cast(s.SleepingTime as date)=  cast(h.StartTime as date)  where 1=1 {0} ", sb.ToString());
            DataSet ds2 = userbll.GetSqlList(sql);
            DataSet ds = userbll.ExecuteSqlPager(sql, "测量时间,用户ID", AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            DataColumn dc;
            for (int i = 1; i <= 288; i++)
            {
                dc = new DataColumn("心率"+i.ToString(),typeof(Int32));
                ds.Tables[0].Columns.Add(dc);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count;j++)
            {
                if (ds.Tables[0].Rows[j]["心率"] != null && ds.Tables[0].Rows[j]["心率"].ToString() != "")
                {
                   
                    for (int i = 1; i <= 288; i++)
                    {
                        ds.Tables[0].Rows[j]["心率" + i.ToString()] = Convert.ToInt32(ds.Tables[0].Rows[j]["心率"].ToString().Substring((i - 1) * 2, 2), 16);
                    }
                }
            }
            ds.Tables[0].AcceptChanges();
            for (int i = 1; i <= 288; i++)
            {
                BoundField column = new BoundField();
                column.HeaderText = "心率" + i.ToString();
                column.DataField = "心率" + i.ToString();//数据字段名称（类的属性） 
                column.ItemStyle.Width = 50;
                gvStatic.Columns.Add(column);
            }
            gvStatic.DataSource = ds.Tables[0];
          
                gvStatic.DataBind();
            int RecordCount = ds2.Tables[0].Rows.Count;
            AspNetPager1.RecordCount = RecordCount;
        }
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSynchronization_Click(object sender, EventArgs e)
        {
            DateTime beginTime = Convert.ToDateTime(txtstartime.Text);
            DateTime endTime = Convert.ToDateTime(txtenddate.Text);
            DateTime MaxTime = DateTime.Now.AddDays(-2);
            if(DateTime.Now.Hour>13)
            {
                MaxTime = DateTime.Now.AddDays(-1);
            }
            if (endTime < MaxTime)
            {
                Maticsoft.Common.MessageBox.Show(this,string.Format("截止时间不能最大不能超过{0}", MaxTime.ToString("yyyy-MM-dd")));
                txtenddate.Focus();
                return ;
            }
            if(endTime<=beginTime)
            {
                Maticsoft.Common.MessageBox.Show(this, string.Format("截止时间{0}不能小于开始时间{1}", endTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd")));
                txtenddate.Focus();
                return ;
            }
            ConsoleLifesense.SyncDataManager sycdataBll = new ConsoleLifesense.SyncDataManager();
            sycdataBll.syncDateSegmentData(beginTime, endTime);
            Maticsoft.Common.MessageBox.Show(this, "同步完成!");
            LoadData();
        }
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnShow_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = AspNetPager1.RecordCount;
            if (AspNetPager1.RecordCount>6000)
            {
                Maticsoft.Common.MessageBox.Show(this, "超过最大导出数，请先过滤条件后再导出!");
                return;
            }
            LoadData();
            ExportExcel.GetExportExcel(gvStatic, "用户测量数据列表");
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = 50;
            LoadData();
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }
        /// <summary>
        /// 去掉"....必须放在具有 runat=server 的窗体标记内"异常
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }
        protected void gvStatic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变   
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#66CCFF'");
                //鼠标移出时，行背景色变   
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                //e.Row.Attributes.Add("ItemStyle-Width", "50");
            }  
        }
    }
}