using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifesenseSystem.Controllers
{
    public class ErrorPageController : Controller
    {
        public static string comuserid;
        public static string comusertype;
        public static string comcustomername;
        //
        // GET: /ErrorPage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Oops404()
        {
            //SetUser();
            //ViewData["user"] = comuserid;
            //ViewData["usertype"] = comusertype;
            return View();
        }

        public ActionResult Oops500(string q)
        {
            //SetUser();
            //ViewData["user"] = comuserid;
            //ViewData["usertype"] = comusertype;
            //ViewData["message"] = q;
            return View();
        }

        /// <summary>
        /// 获取身份
        /// </summary>
        public void SetUser()
        {
            if (User != null && User.Identity.Name != "")
            {
                comuserid = User.Identity.Name.Split('|')[0];
                comusertype = User.Identity.Name.Split('|')[1];
                comcustomername = User.Identity.Name.Split('|')[2];
            }
            else
            {
                comuserid = "";
                comusertype = "";
                comcustomername = "";
            }
        }
    }
}
