using lifesense.Common;
using lifesense.Web.Admin;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.User
{
    public partial class UserList : PageBase
    {
        lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
             {
                 LoadData();
             }
        }

        private void LoadData()
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                strWhere = string.Format(" UserName like '%{0}%'", txtUserName.Text.Trim());
            }
           string sql = string.Format("select * from t_userinfo   {0} ", strWhere!=""?"where "+strWhere:"");
           DataSet ds2=  userbll.GetList(strWhere);
           DataSet ds = userbll.ExecuteSqlPager(sql, "ID", AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
           Gdv_data.DataSource = ds.Tables[0];
           Gdv_data.DataBind();
           int RecordCount = ds2.Tables[0].Rows.Count;
           AspNetPager1.RecordCount = RecordCount;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
           if (userbll.DeleteList(idlist))
           {
               MessageBox.ShowConfirm(btnDelete, "删除成功");
               LoadData();
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnShow_Click(object sender, EventArgs e)
        {
            //Maticsoft.Common.DataToExcel excel = new DataToExcel();
           // Gdv_data.AllowPaging = false;
           // Gdv_data.ShowFooter = false;
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = AspNetPager1.RecordCount;
            LoadData();
            ExportExcel.GetExportExcel(Gdv_data, "用户列表.xls");
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = 10;
            LoadData();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserEditorForm.aspx");
        }
    }
}