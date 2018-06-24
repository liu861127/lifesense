using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.Admin
{
    public partial class EditPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            lifesense.BLL.t_Admin adminBll = new BLL.t_Admin();
            int intID = Convert.ToInt32(Session["AdminID"]);
            //原来密码
            lifesense.Model.t_Admin model = new Model.t_Admin();
            model = adminBll.GetModel(intID);
            if (model != null)
            {
                if (model.LoginPwd == FormsAuthentication.HashPasswordForStoringInConfigFile(this.oldPwd.Text.Trim(), "MD5"))
                {
                    if (this.newPwd.Text.Trim().Length < 6)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "<script>alert('密码长度不能小于六位！');</script>");
                    }
                    else
                    {
                        string newPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(this.newPwd.Text.Trim(), "MD5");
                        lifesense.Model.t_Admin newmodel = new Model.t_Admin();
                        newmodel.LoginName=model.LoginName;
                        newmodel.LoginPwd=newPwd;
                        newmodel.IsUse = model.IsUse;
                        if (adminBll.Update(newmodel))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "<script>alert('修改成功，请使用新密码！');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('修改失败，请稍后重试！');</script>");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('你输入的原来密码有误，请重输！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('你输入的原来密码有误，请重输！');</script>");
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            //Maticsoft.Common.MessageBox.Show(this,"退出成功!");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('你输入的原来密码有误，请重输！');</script>",false);
            this.newPwd.Text = "";
            this.surePwd.Text = "";
        }
    }
}