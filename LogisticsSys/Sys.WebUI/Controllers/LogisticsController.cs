using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.BLL;
using Sys.Common;
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
            return View();
        }

        public ActionResult LogisticsDetail(string single)
        {
            if (!string.IsNullOrEmpty(single))
                ViewData["LogisticsInfoList"] = LogisticsService.Current.GetLogisticsInfoList(single);
            return View();
        }

        public ActionResult RussiaLogistics()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLogistics(string id, string UpdateDate, string LogisticsDesc,string gender)
        {
            var entity = new SysLogisticsInfo();
            entity.LogisticsDesc = LogisticsDesc.Trim();
            entity.LogisticsSingle = id;
            entity.UpdateDate = Convert.ToDateTime(UpdateDate.Trim());
            entity.Status = gender.Equals("1");
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            LogisticsService.Current.AddLogistics(entity);
            return Content("ok");
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