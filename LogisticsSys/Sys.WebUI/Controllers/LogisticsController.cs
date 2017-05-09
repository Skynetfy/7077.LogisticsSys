using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
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
    public class LogisticsController : BaseController
    {
        // GET: Logistics
        public ActionResult Index()
        {
            ISysKuaiDiComRepository dao = DALFactory.KuaiDiDao;
            ViewData["KuaiDiData"] = dao.GetAll().Select(x => new SelectBinding()
            {
                Value = x.ComSn,
                Text = x.ComName
            }).ToList();
            return View();
        }
        public ActionResult GetLogisticsList(string single)
        {
            var dataList = new WuliuGenZongInfo();
            if (!string.IsNullOrEmpty(single))
            {
                var logins = LogisticsService.Current.GetListBySingle(single);
                dataList.Single = single;
                foreach (var i in logins)
                {
                    var order = new WuliuGenZongOrderNos();

                    order.OrderNumber = DALFactory.OrderNumberDao.FindByPk(Convert.ToInt64(i.OrderNos)).Number;
                    dataList.Orders.Add(order);
                }
                return Json(dataList, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public ActionResult LogisticsDetail(string type, string single, string com)
        {
            if (!string.IsNullOrEmpty(single))
            {
                if (com != null && !com.Equals("other"))
                {
                    var host = ConfigHelper.GetValue("KD100Host");
                    var apiKey = ConfigHelper.GetValue("KD100ApiKey");
                    var client = new RestClient(host);
                    var request = new RestRequest("/api", Method.GET);
                    request.AddQueryParameter("id", apiKey);
                    request.AddQueryParameter("com", com);
                    request.AddQueryParameter("nu", single);
                    request.AddQueryParameter("show", "0");
                    request.AddQueryParameter("muti", "1");
                    request.AddQueryParameter("order", "desc");

                    var response = client.Execute<DeliveryMessage>(request);
                    if (response.Data != null)
                    {
                        ViewData["chinalogistics"] = response.Data;
                    }
                }
            }
            if (type.Equals("1"))
            {
                ViewData["LogisticsInfoList"] = LogisticsService.Current.GetLogisticsListBySingleGroup(single);
            }
            else if (type.Equals("2"))
            {
                ViewData["LogisticsInfoList"] = LogisticsService.Current.GetLogisticsInfoList(single);
            }

            return View();
        }

        public ActionResult RussiaLogistics()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLogistics(string id, string[] ordernumbers, string UpdateDate, string LogisticsDesc, string wuliudanhao, string gender)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var logins = LogisticsService.Current.GetListBySingle(id);

                foreach (var d in logins)
                {
                    var entity = new SysLogisticsInfo();
                    entity.LogisticsDesc = LogisticsDesc.Trim();
                    entity.LogisticsSingle = d.LogisticsSingle;
                    entity.UpdateDate = Convert.ToDateTime(UpdateDate.Trim());
                    entity.Status = gender.Equals("1");
                    entity.OrderNos = d.OrderNos;
                    entity.UserName = d.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.IsDelete = false;
                    LogisticsService.Current.AddLogistics(entity);
                }

            }
            else
            {
                if (ordernumbers != null)
                {
                    foreach (var arg in ordernumbers)
                    {
                        var entity = new SysLogisticsInfo();
                        entity.LogisticsDesc = LogisticsDesc.Trim();
                        entity.LogisticsSingle = wuliudanhao.Trim();
                        entity.UpdateDate = Convert.ToDateTime(UpdateDate.Trim());
                        entity.Status = gender.Equals("1");
                        entity.OrderNos = arg;
                        entity.UserName = User.Identity.Name;
                        entity.CreateDate = DateTime.Now;
                        entity.IsDelete = false;
                        LogisticsService.Current.AddLogistics(entity);

                        var ordernumber = DALFactory.OrderNumberDao.FindByPk(Convert.ToInt64(arg));
                        ordernumber.Status = true;
                        DALFactory.OrderNumberDao.Update(ordernumber);
                    }
                }
            }
            return Content("ok");
        }

        public ActionResult GetOrderNumberList()
        {
            var list = DALFactory.OrderNumberDao.GetAll().Where(x => !x.IsDelete && !x.Status).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLogisticsPagerList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysLogisticsInfo>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [LogisticsSingle] Like '%{0}%'", search);
            btdata.total = LogisticsService.Current.GetLogisticsPagerCount(where);
            btdata.rows = LogisticsService.Current.GetLogisticsPagerData(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}