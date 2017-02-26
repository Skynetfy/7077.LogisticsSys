using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Sys.BLL;
using Sys.Common;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RussiaCity()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult EditRussiaCity(string id, string cityname, string citydesc, string landtransporttime,
            string airtransporttime)
        {
            if (string.IsNullOrEmpty(id))
            {
                var entity=new SysRussiaCity();
                entity.CityName = cityname.Trim();
                entity.CityDesc = citydesc.Trim();
                entity.AirTransportTime = airtransporttime.Trim();
                entity.LandTransportTime = landtransporttime.Trim();
                entity.CreateDate=DateTime.Now;
                entity.IsDelete = false;
                RussiaCityService.Current.AddRussiaCity(entity);
            }
            else
            {
                var entity = new SysRussiaCity();
                entity.Id = Convert.ToInt64(id);
                entity.CityName = cityname.Trim();
                entity.CityDesc = citydesc.Trim();
                entity.AirTransportTime = airtransporttime.Trim();
                entity.LandTransportTime = landtransporttime.Trim();
                entity.CreateDate = DateTime.Now;
                RussiaCityService.Current.UpdateRussiaCity(entity);
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult Delete(string[] ids)
        {
            var s = Request.Form["ids"];
            if (ids!=null)
            {
                foreach (var id in ids)
                {
                    var entity=new SysRussiaCity();
                    entity.Id = Convert.ToInt64(id);
                    RussiaCityService.Current.DeleteRussiaCity(entity);
                }
            }
            return Content("ok");
        }

        public ActionResult GetRussiaCityPagerDateList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysRussiaCity>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [CityName] Like '%{0}%'", search);
            btdata.total = RussiaCityService.Current.GetPagerCount(where);
            btdata.rows = RussiaCityService.Current.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}