﻿using System;
using System.Web.Security;
using System.Data;
using System.Collections.Generic;


namespace Maticsoft.Web.Admin
{
    /// <summary>
    /// Login 的摘要说明。
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ViewState["GUID"] = System.Guid.NewGuid().ToString();
                this.lblGUID.Text = this.ViewState["GUID"].ToString();
            }
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);

        }
        #endregion

        private void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

            if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
            {
                int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                if (PassErroeCount > 3)
                {
                    txtUsername.Disabled = true;
                    txtPass.Disabled = true;
                    btnLogin.Enabled = false;
                    this.lblMsg.Text = "对不起，你错误登录了三次，系统登录锁定！";
                    return;
                }

            }

            #region 检查验证码
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {
                if (Session["CheckCode"].ToString().ToLower() != this.CheckCode.Value.ToLower())
                {
                    this.lblMsg.Text = "所填写的验证码与所给的不符 !";
                    Session["CheckCode"] = null;
                    return;
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
            #endregion

            string userName = Maticsoft.Common.PageValidate.InputText(txtUsername.Value.Trim(), 30);
            string Password = Maticsoft.Common.PageValidate.InputText(txtPass.Value.Trim(), 30);
            Password= FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5");
            //验证登录信息，如果验证通过则返回当前用户对象的安全上下文信息
            lifesense.BLL.t_Admin bll = new lifesense.BLL.t_Admin();
            string strWhere=string.Format("LoginName='{0}' and LoginPwd='{1}'",userName,Password);
            List<lifesense.Model.t_Admin> listmodel = bll.GetModelList(strWhere);
            if (listmodel == null||listmodel.Count!=1)//登录信息不对
            {
                this.lblMsg.Text = "登陆失败： " + userName;
                if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                    Session["PassErrorCountAdmin"] = PassErroeCount + 1;
                }
                else
                {
                    Session["PassErrorCountAdmin"] = 1;
                }
            }
            else
            {
                
                //保存当前用户对象信息
                FormsAuthentication.SetAuthCookie(userName, false);
                Session["adminDomain"] = userName;
                Session["AdminID"] = listmodel[0].ID;
                if (Session["returnPage"] != null)
                {
                    string returnpage = Session["returnPage"].ToString();
                    Session["returnPage"] = null;
                    Response.Redirect(returnpage);
                }
                else
                {
                    Response.Redirect("/Admin/admins.aspx");
                }
            }
        }





    }
}
