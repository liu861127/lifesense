using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.User
{
    public partial class UserEditorForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int ID = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
            lifesense.Model.t_userinfo model = userbll.GetModel(ID);
            txtUserID.Text = model.UserID;
            txtUserName.Text = model.UserName;
            txtUserPwd.Text = model.UserPwd;
            lblID.Text = model.ID.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool bolResult = false;
            lifesense.BLL.t_userinfo userbll = new BLL.t_userinfo();
            lifesense.Model.t_userinfo model = new Model.t_userinfo();
            model.UserID = txtUserID.Text.Trim();
            model.UserName = txtUserName.Text.Trim();
            model.UserPwd = txtUserPwd.Text.Trim();
            if (!string.IsNullOrEmpty(lblID.Text))
            {
                model.ID =Convert.ToInt32 (lblID.Text);
                bolResult=userbll.Update(model);
            }
            else
            {
               if (userbll.Add(model)>0)
               {
                   bolResult = true;
               }
            }
            if(bolResult)
            {
               
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "<script language=javascript>alert('保存成功!');window.location='UserList.aspx';</script>");
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('你输入的原来密码有误，请重输！');</script>");
                //Response.Redirect("UserListForm.aspx");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.Show(this, "退出成功!");
            Response.Redirect("UserList.aspx");
        }
    }
}