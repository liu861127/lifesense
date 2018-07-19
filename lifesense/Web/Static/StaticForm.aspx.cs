using lifesense.Common;
using lifesense.Web.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.Static
{
    public partial class StaticForm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime currentTime = DateTime.Now;
                txtstartime.Text = currentTime.AddDays(1 - currentTime.Day).ToString("yyyy-MM-dd");
                if (currentTime.Hour > 12)
                {
                    txtenddate.Text = currentTime.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    txtenddate.Text = currentTime.AddDays(-2).ToString("yyyy-MM-dd");
                }
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
                                           u.UserName as 用户名称,
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
                                         LEFT JOIN t_heartrateinfo AS h ON u.UserID = h.UserID and (CAST(s.SleepingTime AS DATE) = CAST(h.StartTime AS DATE)
                                            or CAST(w.MeasureTime  AS DATE) = CAST(h.StartTime AS DATE))  where 1=1 {0} ", sb.ToString());
            DataSet ds2 = userbll.GetSqlList(sql);
            DataSet ds = userbll.ExecuteSqlPager(sql, "测量时间,用户ID", AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            DataColumn dc;
            string tempCaption = string.Empty;
            int baseInt = 0;
            int num = 0;
            int m = 0;
            for (int i = 1; i <= 288; i++)
            {
                num = i / 12;
                m = i % 12;
                //tempCaption = "心率" + (i * 0.05).ToString();
                if (m== 0)
                {
                    tempCaption = "心率" + num.ToString();
                }
                else
                {
                    tempCaption = "心率" + (num + (m* 0.05)).ToString();
                }
                if (tempCaption.Length < 6)
                {
                    if (tempCaption.Contains('.'))
                    {
                        tempCaption = tempCaption.PadRight(6, '0');
                    }
                    else
                    {
                        tempCaption = tempCaption + ".";
                        tempCaption = tempCaption.PadRight(6, '0');
                    }
                }
                dc = new DataColumn(tempCaption, typeof(Int32));
                if (!ds.Tables[0].Columns.Contains(tempCaption))
                {
                    ds.Tables[0].Columns.Add(dc);
                }
            }
            tempCaption = string.Empty;
            for (int j = 0; j < ds.Tables[0].Rows.Count;j++)
            {
                if (ds.Tables[0].Rows[j]["心率"] != null && ds.Tables[0].Rows[j]["心率"].ToString() != "")
                {
                   
                    for (int i = 1; i <= 288; i++)
                    {
                        num = i / 12;
                        m = i % 12;
                        //tempCaption = "心率" + (i * 0.05).ToString();
                        if (m == 0)
                        {
                            tempCaption = "心率" + num.ToString();
                        }
                        else
                        {
                            tempCaption = "心率" + (num + (m * 0.05)).ToString();
                        }
                        if(tempCaption.Length<6)
                        {
                            if (tempCaption.Contains('.'))
                            {
                                tempCaption = tempCaption.PadRight(6, '0');
                            }else
                            {
                                tempCaption=tempCaption+".";
                                tempCaption = tempCaption.PadRight(6, '0');
                            }
                         }
                        ds.Tables[0].Rows[j][tempCaption] = Convert.ToInt32(ds.Tables[0].Rows[j]["心率"].ToString().Substring((i-1) * 2, 2), 16);
                       
                        //ds.Tables[0].Rows[j]["心率" + i.ToString()] = Convert.ToInt32(ds.Tables[0].Rows[j]["心率"].ToString().Substring((i - 1) * 2, 2), 16);
                    }
                }
            }
            ds.Tables[0].AcceptChanges();
            tempCaption = string.Empty;
            for (int i = 1; i <= 288; i++)
            {
                num = i / 12;
                m = i % 12;
                if (m == 0)
                {
                    tempCaption = "心率" + num.ToString();
                }
                else
                {
                    tempCaption = "心率" + (num + (m * 0.05)).ToString();
                }
                if (tempCaption.Length < 6)
                {
                    if (tempCaption.Contains('.'))
                    {
                        tempCaption = tempCaption.PadRight(6, '0');
                    }
                    else
                    {
                        tempCaption = tempCaption + ".";
                        tempCaption = tempCaption.PadRight(6, '0');
                    }
                }
                if (gvStatic.Columns.Count<300)
                {
                    BoundField column = new BoundField();
                    column.HeaderText = tempCaption;
                    column.DataField = tempCaption;//数据字段名称（类的属性） 
                    column.ItemStyle.Width = 50;
                    gvStatic.Columns.Add(column);
                }
                else
                {
                    break;
                }
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
            if (endTime >= MaxTime)
            {
                Maticsoft.Common.MessageBox.Show(this,string.Format("截止时间不能最大不能超过{0}", MaxTime.ToString("yyyy-MM-dd")));
                txtenddate.Focus();
                return ;
            }
            if(endTime<beginTime)
            {
                Maticsoft.Common.MessageBox.Show(this, string.Format("截止时间{0}不能小于开始时间{1}", endTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd")));
                txtenddate.Focus();
                return ;
            }
            ConsoleLifesense.SyncDataManager sycdataBll = new ConsoleLifesense.SyncDataManager();
            sycdataBll.beginTime = beginTime;
            sycdataBll.endTime = endTime;
            Maticsoft.Common.MessageBox.Show(this, "开始同步数据!请稍后查看情况。");
            Thread t = new Thread(new ThreadStart(sycdataBll.syncDateSegmentData));
            t.IsBackground = true;
            t.Start(); 
            //sycdataBll.syncDateSegmentData();
        
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
            ExportExcel.GetExportExcel(gvStatic, "用户测量数据列表.xls");
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