using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.Dal;
using Sys.WebUI.Models;
using System.Web.Security;
using Sys.BLL;
using Sys.BLL.Users;


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
            ViewBag.UserName = User.Identity.Name;
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                var cusmer = UserService.GetCustomerByUid(_user.Id);
                if (cusmer != null)
                {
                    ViewBag.Integral = cusmer.Integral;
                }
            }
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