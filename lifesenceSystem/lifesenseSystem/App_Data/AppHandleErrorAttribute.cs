using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifesenseSystem
{


    ///<summary>
    ///错误日志（Controller发生异常时会执行这里）
    ///</summary>

    public class AppHandleErrorAttribute : HandleErrorAttribute
    {

        ///<summary>
        ///异常
        ///</summary>
        ///<param name="filterContext"></param>

        public override void OnException(ExceptionContext filterContext)
        {

            //使用log4net或其他记录错误消息

            Exception Error = filterContext.Exception;

            string Message = Error.Message;//错误信息

            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址

            filterContext.ExceptionHandled = true;
            filterContext.Result = new

           RedirectResult("/ErrorPage/Oops500?q=" + Message);//跳转至错误提示页面

        }

    }

}