﻿using lifesense.Common;
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
                 sb.AppendFormat("and UserID like '%{0}%' ", txtUserID.Text.Trim());
             }
            string sql = string.Format(@"select u.UserID as 用户ID,w.MeasureTime as 测量时间,w.StepNum as 步数,w.Calorie as 卡里路, w.Mileage as 里程,s.SleepingTime as 入睡时间,s.WakingTime as 醒来时间,s.LongSleepNum as '深睡时长(分钟)',s.ShallowSleepNum as '浅睡时长(分钟)',s.WakeUpLong as '醒来时长（分钟)',s.WakingNum as 醒来次数,h.StartTime as '心率测量开始时间',h.HeartRate as '心率' from t_userinfo as u left join t_walkinfo as w 
                                         on u.UserID=w.UserID left join t_sleepinfo as s  on u.UserID=s.UserID
                                         left join t_heartrateinfo as h on u.UserID=h.UserID  where 1=1 {0}", sb.ToString());
            DataSet ds2 = userbll.GetSqlList(sql);
            DataSet ds = userbll.ExecuteSqlPager(sql, "用户ID", AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
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
            LoadData();
            ExportExcel.GetExportExcel(gvStatic, "用户测量数据列表");
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = 2;
            LoadData();
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}