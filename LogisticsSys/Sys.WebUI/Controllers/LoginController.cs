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
        [HttpPost]
        public ActionResult Index(string username, string password, string returl, string remember_me)
        {
            string userData = string.Empty;
            var provider = new UserLoginProvider();
            if (!provider.CheckUserName(username))
            {
                Response.Write("<script>alert('用户名或者密码错误');</script>");
                return View();
            }
            var user = provider.GetUser(username, password);
            if (user == null)
            {
                Response.Write("<script>alert('用户名或者密码错误');</script>");
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
            return new RedirectResult(returl==null?"../Home/Index":returl);
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
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}