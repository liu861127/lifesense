using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lifesenseSystem.Controllers
{
    /// <summary>
    /// 后台权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (filterContext.HttpContext.Session["User"] != null)
            {
                string username = filterContext.HttpContext.Session["User"].ToString();
                if (HttpContext.Current.Cache[username] != null && HttpContext.Current.Session.SessionID.ToString() != HttpContext.Current.Cache[username].ToString())
                {
                    if (request.IsAjaxRequest())
                    {

                        response.Status = "401 Session Timeout";
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('您的账户从其他客户端登陆!');location.href='/main/Login';</script>");
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Main", action = "Login" }));
                    }
                    filterContext.Result = new HttpUnauthorizedResult();//这一行保证不再执行Action的代码
                    response.End();//必须加上这句，否则返回前台status始终是200
                }

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Main", action = "Login" }));
            }
        }

    }
}
