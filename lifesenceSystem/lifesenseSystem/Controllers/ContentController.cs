using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifesenseSystem.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/
            [AdminPermission]
        public ActionResult Index()
        {
            return View();
        }

    }
}
