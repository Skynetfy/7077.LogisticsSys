using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using Sys.BLL.Users;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("../Home/Index");
            return View();
        }

        public ActionResult CheckUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return Content("0");
            var provider = new UserLoginProvider();
            if (provider.CheckUserName(username))
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        [HttpPost]
        public ActionResult Index(string username, string password, string returl, string remember_me)
        {
            string userData = string.Empty;
            var provider = new UserLoginProvider();
            if (!provider.CheckUserName(username))
            {
                ViewBag.message = "用户名不存在";
                return View();
            }
            var user = provider.GetUser(username, password);
            if (user == null)
            {
                ViewBag.message = "用户名或密码不正确";
                return View();
            }
            userData = user.UserName + "|" + user.DisplayName + "|" + user.Email;
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     user.UserName,
                     DateTime.Now,
                    remember_me!=null ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
            return new RedirectResult(returl==null?"/":returl);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(string username, string password, string email, string displayname, string phone)
        {
            var provider = new UserLoginProvider();
            if (!provider.CheckUserName(username))
            {
                var entity = new SysUser();
                entity.UserName = username.Trim();
                entity.Password = password.Trim();
                entity.Email = email.Trim();
                entity.Phone = phone.Trim();
                entity.DisplayName = displayname.Trim();
                entity.CreateDate = DateTime.Now;

                var i = provider.InsertUser(entity);
            }
            else
            {
                ViewBag.message = "用户名已存在";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}