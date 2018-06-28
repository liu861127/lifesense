using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace lifesense.Web.Admin
{
    /// <summary>
    /// 页面基类,提过常用页面方法
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        protected int AdminID;
        /// <summary>
        /// 管理员所能访问的页面
        /// </summary>
        protected string AdminDomain
        {
            get
            {
                if (Session["adminDomain"] == null)
                    return null;
                else
                    return Session["adminDomain"].ToString();
            }
        }
        /// <summary>
        /// 是否文系统
        /// </summary>
        protected bool IsSys
        {
            get { return AdminDomain.IndexOf("所有文件") != -1; }
        }

        protected string error = "";
        protected override void OnLoad(EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToLower().IndexOf("localhost") < 0)
            {
                if (Session["adminDomain"] == null)
                {
                    //尚未登陆
                    Response.Redirect("/Admin/Login.aspx?url=" + Request.Url.AbsoluteUri);
                    Response.End();
                }
                if (Session["error"] != null)
                {
                    //权限xml是否存在
                    error = "false";
                }
                string adminDomain = "," + Session["adminDomain"].ToString() + ",";
                Session["adminDomain"] = adminDomain;
                string FilePath = Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf('/') + 1);
                if (adminDomain == "" || error == "false")
                {
                    //没有访问权限
                    Response.Write("<script>alert('你没有访问该页面的权限!如有问题,请与管理员联系.');</script>");
                    Response.End();
                }
            }
            base.OnLoad(e);
        }
        /// <summary>
        /// 获取页面生成的HTML
        /// </summary>
        /// <returns></returns>
        protected string GetPageHtml()
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            System.IO.StringWriter sWriter = new System.IO.StringWriter(sBuilder);
            HtmlTextWriter html = new HtmlTextWriter(sWriter);
            base.Render(html);
            return sBuilder.ToString();
        }
        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="key"></param>
        public void RegScript(string script, string key)
        {
            System.Web.UI.ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.Page.GetType(), key, script, true);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        public void Alert(string msg)
        {
            RegScript("alert('" + msg.Replace("\\'", "'").Replace("'", "\\'") + "');", "onload");
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        public void Alert(System.Web.UI.Control control, string msg)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "UpdateScript", "alert('" + msg.Replace("\\'", "'").Replace("'", "\\'") + "');", true);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="key">onload 或 scriptUpdate</param>
        public void Alert(string msg, string key)
        {
            RegScript("alert('" + msg.Replace("\\'", "'").Replace("'", "\\'") + "');", key);
        }
        /// <summary>
        /// 隐藏邮箱地址
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        protected string HideEmail(string strEmail)
        {
            string str = "";
            int idx = strEmail.IndexOf("@");
            if (!string.IsNullOrEmpty(strEmail) && idx > 3)
            {
                str = "***" + strEmail.Substring(idx - 3, strEmail.Length - idx + 3);
            }
            return str;
        }
        /// <summary>
        /// 隐藏手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pos">隐藏多少位</param>
        /// <param name="rpos">从倒数第几位开始隐藏</param>
        /// <returns></returns>
        protected string HideMobileNum(string str, int pos, int rpos)
        {
            int strLength = str.Length;
            string replaceLastPostion = str.Substring(0, strLength > pos ? strLength - pos : 0);
            int end = str.Length - rpos;
            int start = end - pos;
            char[] s = str.ToCharArray();
            string newStr = "";
            for (int k = 0; k < s.Length; k++)
            {
                if (k >= start && k < end) newStr += "*";
                else newStr += s[k];
            }
            return newStr;
        }

    }
}