using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.BLL;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Entities;
using Sys.WebUI.Models;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class UserController : BaseController
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
                if (_user != null)
                {

                    ViewBag.id = _user.Id;
                    ViewBag.username = _user.UserName;
                    ViewBag.displayname = _user.DisplayName;
                    ViewBag.email = _user.Email;
                    ViewBag.phone = _user.Phone;
                    ViewBag.createdate = _user.CreateDate.ToString("s");
                    ViewBag.status = _user.Status == 1 ? "正常" : "已无效";

                    var cusmer = UserService.GetCustomerByUid(_user.Id);
                    if (cusmer != null)
                    {
                        ViewData["CCityDataList"] = ChinaCityService.Current.GetChinaCities((int)cusmer.CityId);
                        ViewBag.cid = cusmer.CustomerID;
                        ViewBag.address = cusmer.Address;
                        ViewBag.city = cusmer.CityId;
                        ViewBag.qq = cusmer.QQNumber;
                        ViewBag.webchat = cusmer.WebChatNo;
                    }
                    else
                    {
                        ViewData["CCityDataList"] = ChinaCityService.Current.GetChinaCities(0);
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(string id, string username, string email, string displayname, string phone, string city, string address, string qq, string webchat)
        {
            var provider = new UserLoginProvider();
            if (!string.IsNullOrEmpty(username))
            {
                var _user = provider.GetUser(username);
                if (_user != null)
                {
                    _user.UserName = _user.UserName;
                    _user.Password = _user.Password;
                    _user.Email = email.Trim();
                    _user.Phone = phone.Trim();
                    _user.DisplayName = displayname.Trim();
                    _user.CreateDate = _user.CreateDate;
                    var i = provider.UpdateUser(_user);
                    if (i > 0)
                    {
                        var cusmer = UserService.GetCustomerByUid(_user.Id);
                        if (cusmer != null)
                        {
                            cusmer.Address = address.Trim();
                            cusmer.CityId = Convert.ToInt64(city);
                            cusmer.QQNumber = qq.Trim();
                            cusmer.WebChatNo = webchat.Trim();
                            UserService.UpdateCustomer(cusmer);
                        }
                    }
                }
            }
            return Content("ok");
        }

        public ActionResult CustomerInfo(string username)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteAgent(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var user = UserService.FindByPk(Convert.ToInt64(id));
                    var entity = UserService.GetAgentInfoByUserId(user.Id); ;
                    if (entity != null)
                    {
                        user.Status = 0;
                        UserService.UpdateUser(user);
                        entity.IsDelete = true;
                        UserService.UpdateAgentInfo(entity);
                    }
                }
            }
            return Content("ok");
        }

        [HttpPost]
        public ActionResult EditAgent(string id, string username, string password, string email, string displayname,
            string phone, string qq, string dlcs)
        {
            var provider = new UserLoginProvider();
            if (!string.IsNullOrEmpty(id))
            {
                var _user = provider.GetUser(username);
                if (_user != null)
                {
                    _user.Email = email.Trim();
                    _user.Phone = phone.Trim();
                    _user.DisplayName = displayname.Trim();
                    _user.CreateDate = _user.CreateDate;
                    var i = provider.UpdateUser(_user);
                    if (i > 0)
                    {
                        var agentInfo = UserService.GetAgentInfoByUserId(_user.Id);
                        if (agentInfo != null)
                        {
                            agentInfo.AgentCityId = Convert.ToInt64(dlcs);
                            //agentInfo.UserId = i;
                            agentInfo.QQNumber = qq ?? "";
                            //agentInfo.IsDelete = false;
                            agentInfo.CreateDate = DateTime.Now;
                            UserService.UpdateAgentInfo(agentInfo);
                        }
                    }
                }
            }
            else
            {
                var entity = new SysUser();
                entity.UserName = username.Trim();
                entity.Password = DEncrypt.Md5(password.Trim());
                entity.Email = email.Trim();
                entity.Phone = phone.Trim();
                entity.Status = 1;
                entity.RuleType = RuleTypeEnum.Agents.ToString();
                entity.DisplayName = displayname.Trim();
                entity.CreateDate = DateTime.Now;
                var i = provider.InsertUser(entity);
                if (i > 0)
                {
                    var agent = new SysAgentInfo();
                    agent.AgentCityId = Convert.ToInt64(dlcs);
                    agent.UserId = i;
                    agent.QQNumber = qq ?? "";
                    agent.IsDelete = false;
                    agent.CreateDate = DateTime.Now;
                    UserService.InsertAgentInfo(agent);
                }
            }
            return Content("ok");
        }

        public ActionResult AgentInfo(string username)
        {
            ViewData["RCityDataList"] = RussiaCityService.Current.GetBindings(0);
            return View();
        }

        public ActionResult GetAgentPagerList(string search, int offset, int limit, string order, string sort)
        {
            var count = 0;
            var btdata = new BootstrapTableData<SysAgentInfo>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and ([UserName] Like '%{0}%' or [DisplayName] Like '%{0}%' or [Phone] Like '%{0}%'  or [Email] Like '%{0}%')", search);

            btdata.rows = UserService.GetAgentPagerData(where, offset, limit, order, sort, out count);
            btdata.total = count;
            return Json(btdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(string[] ids)
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
        public ActionResult EditPassword(string oldpassword, string newpassword)
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

        public ActionResult Profile(string name)
        {
            var username = name ?? User.Identity.Name;

            var provider = new UserLoginProvider();
            var _user = provider.GetUser(username);
            if (_user != null)
            {
                ViewBag.username = _user.UserName;
                ViewBag.displayname = _user.DisplayName;
                ViewBag.email = _user.Email;
                ViewBag.phone = _user.Phone;
                ViewBag.createdate = _user.CreateDate.ToString("s");
                ViewBag.status = _user.Status == 1 ? "正常" : "已无效";
                ViewBag.RuleType = _user.RuleType;
                ViewBag.CreateDate = _user.CreateDate.ToString("yyyy-MM-dd hh:mm:ss");
                if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    var cusmer = UserService.GetCustomerByUid(_user.Id);
                    if (cusmer != null)
                    {
                        ViewData["CCityDataList"] = ChinaCityService.Current.GetChinaCities((int)cusmer.CityId);
                        ViewBag.CustomerID = cusmer.CustomerID.Trim();
                        ViewBag.CityId = cusmer.CityId;
                        ViewBag.CityName = ChinaCityService.Current.GetCityName(cusmer.CityId);
                        ViewBag.Address = cusmer.Address;
                        ViewBag.QQNumber = cusmer.QQNumber;
                        ViewBag.WebChatNo = cusmer.WebChatNo;
                    }
                }
                else if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    var agent = UserService.GetAgentInfoByUserId(_user.Id);
                    if (agent != null)
                    {
                        ViewBag.QQNumber = agent.QQNumber;
                        ViewBag.CityName = RussiaCityService.Current.GetCityName(agent.AgentCityId);
                    }
                }
            }
            return View();
        }

        public ActionResult GetUserPagerList(string search, int offset, int limit, string order, string sort)
        {
            var provider = new UserLoginProvider();
            var btdata = new BootstrapTableData<SysUser>();
            var where = " and [RuleType]='Customer' ";
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and ([UserName] Like '%{0}%' or [DisplayName] Like '%{0}%' or [Phone] Like '%{0}%'  or [Email] Like '%{0}%')", search);
            btdata.total = provider.GetPagerCount(where);
            btdata.rows = provider.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgentManage()
        {
            return View();
        }
    }
}