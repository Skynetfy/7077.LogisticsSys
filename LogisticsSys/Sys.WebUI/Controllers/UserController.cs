using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sys.BLL;
using Sys.BLL.Order;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Dal;
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
            var provider = new UserLoginProvider();
            var _user = provider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                ViewBag.RuleType = _user.RuleType;
            }
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
        public ActionResult EditJifen(string uid, string jifen)
        {
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(jifen))
            {
                UserService.AdminUpdateIntegral(Convert.ToInt64(uid), Convert.ToInt32(jifen), 0, "管理员重置");
            }
            return Content("ok");
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
                        ViewBag.Integral = cusmer.Integral;
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
        [HttpPost]
        public ActionResult AdminEditPassword(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var provider = new UserLoginProvider();
                var _user = provider.GetUser(username);
                if (_user != null)
                {
                    _user.Password = DEncrypt.Md5(password);
                    provider.UpdateUser(_user);
                }
            }
            return Content("ok");
        }

        public ActionResult Points()
        {
            return View();
        }

        public ActionResult GetIntegralLogPagerList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysIntegralLog>();
            var where = " ";
            var provider = new UserLoginProvider();
            var user = provider.GetUser(User.Identity.Name);
            if (user != null && user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                where = string.Format(@" and [Uid]=" + user.Id);
            btdata.total = DALFactory.IntegralLogDao.GetPagerCount(where);
            var data= DALFactory.IntegralLogDao.GetPagerList(where, offset, limit, order, sort).ToList();
            foreach (var item in data)
            {
                item.DisplayName = DALFactory.SysUserDao.FindByPk(item.Uid).DisplayName;
            }
            btdata.rows = data;

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }

        public void ExportCustomerExcel()
        {
            string filename = "客户列表.xlsx";

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));

           
            var data = DALFactory.CustomerInfoDao.GetFulList();
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("客户编号");
            row0.CreateCell(1).SetCellValue("用户名");
            row0.CreateCell(2).SetCellValue("姓名");
            row0.CreateCell(3).SetCellValue("手机号");
            row0.CreateCell(4).SetCellValue("邮箱");
            row0.CreateCell(5).SetCellValue("地址");
            row0.CreateCell(6).SetCellValue("QQ");
            row0.CreateCell(7).SetCellValue("微信");
            row0.CreateCell(8).SetCellValue("积分");
         
            for (var c = 0; c < data.Count; c++)
            {
                dynamic item = data[c];
                IRow row = sheet1.CreateRow(c + 1);
                row.CreateCell(0).SetCellValue(item.CustomerID);
                row.CreateCell(1).SetCellValue(item.UserName);
                row.CreateCell(2).SetCellValue(item.DisplayName);
                row.CreateCell(3).SetCellValue(item.Phone);
                row.CreateCell(4).SetCellValue(item.Email);
                row.CreateCell(5).SetCellValue(item.Address);
                row.CreateCell(6).SetCellValue(item.QQNumber);
                row.CreateCell(7).SetCellValue(item.WebChatNo);
                row.CreateCell(8).SetCellValue(item.Integral);
               
            }
            var path = Server.MapPath("~/ExcelFiles/客户列表.xlsx");
            using (var f = System.IO.File.Create(path))
            {
                workbook.Write(f);
            }
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }
        [HttpPost]
        public ActionResult OnAgent(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var agent = DALFactory.SysUserDao.FindByPk(Convert.ToInt64(id));
                if (agent != null)
                {
                    agent.Status = 1;
                    DALFactory.SysUserDao.Update(agent);
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult DeleteAgent1(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var agent = DALFactory.SysUserDao.FindByPk(Convert.ToInt64(id));
                if (agent != null)
                {
                    
                    DALFactory.SysUserDao.Delete(agent);
                }
            }
            return Content("ok");
        }
    }
}