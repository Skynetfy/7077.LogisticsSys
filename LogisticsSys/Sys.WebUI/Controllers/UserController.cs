using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.BLL;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditProfile(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var provider = new UserLoginProvider();
                var _user = provider.GetUser(username);
                ViewBag.id = _user.Id;
                ViewBag.username = _user.UserName;
                ViewBag.displayname = _user.DisplayName;
                ViewBag.email = _user.Email;
                ViewBag.phone = _user.Phone;
                ViewBag.createdate = _user.CreateDate.ToString("s");
                ViewBag.status = _user.Status == 1 ? "正常" : "已无效";
            }
           
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(string id,string username, string password, string email, string displayname, string phone)
        {
            var provider = new UserLoginProvider();
            if (!string.IsNullOrEmpty(id))
            {
                var _user = provider.GetUser(username);
                var entity = new SysUser();
                entity.UserName = _user.UserName;
                entity.Password = _user.Password;
                entity.Email = email.Trim();
                entity.Phone = phone.Trim();
                entity.DisplayName = displayname.Trim();
                entity.CreateDate = _user.CreateDate;

                var i = provider.InsertUser(entity);
            }
            else
            {
                var entity = new SysUser();
                entity.Id = Convert.ToInt64(id);
                entity.UserName = username.Trim();
                entity.Password = DEncrypt.Md5(password.Trim());
                entity.Email = email.Trim();
                entity.Phone = phone.Trim();
                entity.DisplayName = displayname.Trim();
                entity.CreateDate = DateTime.Now;
                var i = provider.UpdateUser(entity);
            }
            return Content("ok");
        }

        public ActionResult DeleteUser(string ids)
        {
            var provider = new UserLoginProvider();
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysUser();
                    entity.Id = Convert.ToInt64(id);
                    provider.DeleteUser(entity);
                }
            }
            return Content("ok");
        }

        public ActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditPassword(string oldpassword,string newpassword)
        {
            var message = "";
            var provider = new UserLoginProvider();
            var username = User.Identity.Name;
            var user = provider.GetUser(username, DEncrypt.Md5(oldpassword));
            if (user != null)
            {
                user.Password = DEncrypt.Md5(newpassword);
                provider.UpdateUser(user);
                message = "success";
            }
            else
            {
                message = "旧密码错误";
            }
            return Content(message);
        }
        
        public ActionResult Profile()
        {
            var username = User.Identity.Name;

            var provider = new UserLoginProvider();
            var _user = provider.GetUser(username);
            ViewBag.username = _user.UserName;
            ViewBag.displayname = _user.DisplayName;
            ViewBag.email = _user.Email;
            ViewBag.phone = _user.Phone;
            ViewBag.createdate = _user.CreateDate.ToString("s");
            ViewBag.status = _user.Status == 1 ? "正常" : "已无效";
            return View();
        }

        public ActionResult GetUserPagerList(string search, int offset, int limit, string order, string sort)
        {
            var provider = new UserLoginProvider();
            var btdata = new BootstrapTableData<SysUser>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [CityName] Like '%{0}%'", search);
            btdata.total = provider.GetPagerCount(where);
            btdata.rows = provider.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}