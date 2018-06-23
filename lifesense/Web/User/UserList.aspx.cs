using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.User
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
             {
                 LoadData("");
             }
        }

        private void LoadData(string strWhere)
        {
            lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
            DataSet ds = userbll.GetList(strWhere);
            Gdv_data.DataSource = ds.Tables[0];
            Gdv_data.DataBind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for(int i=0;i<Gdv_data.Rows.Count;i++)
            {
                var check =Convert.ToBoolean (Gdv_data.Rows[i].Cells[0].Text);
              
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string strWhere = string.Empty;
            if(!string.IsNullOrEmpty(txtUserName.Text))
            {
                strWhere = string.Format("UserName like '%{0}%'", txtUserName.Text.Trim());
            }
            LoadData(strWhere);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnShow_Click(object sender, EventArgs e)
        {

        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserEditorForm.aspx");
        }
    }
}