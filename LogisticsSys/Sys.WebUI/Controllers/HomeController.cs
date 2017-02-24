using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.Dal;

namespace Sys.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var i = User.Identity.IsAuthenticated;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}