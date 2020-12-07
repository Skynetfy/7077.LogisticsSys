using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using Sys.BLL;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Dal;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    public class AlipayController : Controller
    {
        // GET: ForexTrade
        public ActionResult ForexTrade(int id)
        {
            OrderTradeService tradeService = new OrderTradeService();
            var formHtml = tradeService.CreateForexTrade(id);
            Response.Write(formHtml);
            return View();
        }

        public ActionResult Notify()
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (var i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            OrderTradeService tradeService = new OrderTradeService();
            var result = tradeService.ForexTradeNotify(sArray, Request.Form["notify_id"], Request.Form["sign"], Request.Form["out_trade_no"], Request.Form["trade_no"], Request.Form["trade_status"]);
            return Content(result);
        }

        public ActionResult Complate()
        {
            return View();
        }

        public ActionResult TradeList()
        {
            return View();
        }

        public ActionResult GetUserPagerList(string search, int offset, int limit, string order, string sort)
        {
            var where = " and a.[Status] in(1,-1) ";
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    var city = UserService.GetAgentInfoByUserId(_user.Id);
                    if (city != null)
                        where += " and c.[RussiaCityId]=" + city.AgentCityId;
                    else
                        where += " and c.[RussiaCityId]=0 ";
                }
                else if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    where += " and a.[UserId]=" + _user.Id;
                }
            }
            var btdata = new BootstrapTableData<SysOrderTrade>();
           
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and (b.[UserName] Like '%{0}%' or b.[DisplayName] Like '%{0}%' or b.[Phone] Like '%{0}%'  or b.[Email] Like '%{0}%')", search);
            btdata.total = DALFactory.OrderTradeRepository.GetPagerCount(where);
            btdata.rows = DALFactory.OrderTradeRepository.GetPagerList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}