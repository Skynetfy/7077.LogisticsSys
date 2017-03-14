using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.Dal;
using Sys.WebUI.Models;
using System.Web.Security;


namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var urlRefer = HttpContext.Request.UrlReferrer;
            if (urlRefer != null)
            {
                var sege = urlRefer.Segments;
                if (sege.Length > 0)
                {
                    if (sege[sege.Length - 1].Equals("SignUp", StringComparison.OrdinalIgnoreCase))
                        ViewBag.refer = "1";
                }
            }
            return View();
        }
        public ActionResult Header()
        {
            var identity = User.Identity as FormsIdentity;
            var userDate = identity.Ticket.UserData;
            var users = userDate.Split('|');
            ViewBag.UserName = users[0];
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