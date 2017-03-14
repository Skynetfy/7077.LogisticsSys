using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using Sys.BLL;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Entities;
using Sys.WebUI.Models;

namespace Sys.WebUI.Controllers
{
    public class LoginController : BaseController
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
            var user = provider.GetUser(username, DEncrypt.Md5(password));
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
                    remember_me != null ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
            return new RedirectResult(returl == null ? "/" : returl);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(string username, string password, string email)
        {
            var provider = new UserLoginProvider();
            if (!provider.CheckUserName(username))
            {
                var entity = new SysUser();
                entity.UserName = username.Trim();
                entity.Password = DEncrypt.Md5(password.Trim());
                entity.Email = email.Trim();
                //entity.Phone = phone.Trim();
                entity.RuleType = RuleTypeEnum.Customer.ToString();
                //entity.DisplayName = displayname.Trim();
                entity.CreateDate = DateTime.Now;
                entity.Status = 1;
                var i = provider.InsertUser(entity);
                if (i > 0)
                {
                    var customer = new SysCustomerInfo();
                    customer.CustomerID = UserService.GetCustomerNo();
                    customer.UserId = i;
                    customer.IsDelete = false;
                    customer.CreateDate = DateTime.Now;
                    customer.Address = "";
                    customer.CityId = 0;
                    customer.QQNumber = "";
                    customer.WebChatNo = "";
                    customer.Phone = "";
                    var x = UserService.InsertCustomer(customer);
                    if (x > 0)
                    {
                        var userData = entity.UserName + "|" + entity.DisplayName + "|" + entity.Email;
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,
                            entity.UserName,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            false,
                            userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                    }
                }
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