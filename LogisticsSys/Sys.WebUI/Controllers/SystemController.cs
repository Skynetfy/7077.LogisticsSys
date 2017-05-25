using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Sys.BLL;
using Sys.Common;
using Sys.Dal;
using Sys.Entities;
using Sys.WebUI.Models;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class SystemController : BaseController
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        #region 俄罗斯城市管理

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
                var entity = new SysRussiaCity();
                entity.CityName = cityname.Trim();
                entity.CityDesc = citydesc.Trim();
                entity.AirTransportTime = airtransporttime.Trim();
                entity.LandTransportTime = landtransporttime.Trim();
                entity.CreateDate = DateTime.Now;
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
        public ActionResult DeleteRussiaCity(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysRussiaCity();
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

        #endregion


        #region 国内城市管理

        public ActionResult ChinaCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChinaCity(string id, string cityname, string UnitPrice, string ExpressBeavyPrice, string SfUnitPrice, string SflogisticsBeavyPrice)
        {
            if (string.IsNullOrEmpty(id))
            {
                var entity = new SysChinaCity();
                entity.CityName = cityname.Trim();
                entity.UnitPrice = Convert.ToDecimal(UnitPrice.Trim());
                entity.ExpressBeavyPrice = Convert.ToDecimal(ExpressBeavyPrice.Trim());
                entity.SfUnitPrice = Convert.ToDecimal(SfUnitPrice.Trim());
                entity.SflogisticsBeavyPrice = Convert.ToDecimal(SflogisticsBeavyPrice.Trim());
                entity.CreateDate = DateTime.Now;
                entity.IsDelete = false;
                ChinaCityService.Current.AddChinaCity(entity);
            }
            else
            {
                var entity = new SysChinaCity();
                entity.Id = Convert.ToInt64(id);
                entity.CityName = cityname.Trim();
                entity.UnitPrice = Convert.ToDecimal(UnitPrice.Trim());
                entity.ExpressBeavyPrice = Convert.ToDecimal(ExpressBeavyPrice.Trim());
                entity.SfUnitPrice = Convert.ToDecimal(SfUnitPrice.Trim());
                entity.SflogisticsBeavyPrice = Convert.ToDecimal(SflogisticsBeavyPrice.Trim());
                entity.CreateDate = DateTime.Now;
                ChinaCityService.Current.UpdateChinaCity(entity);
            }
            return Content("ok");
        }

        public ActionResult GetChinaCityPagerList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysChinaCity>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [CityName] Like '%{0}%'", search);
            btdata.total = ChinaCityService.Current.GetPagerCount(where);
            btdata.rows = ChinaCityService.Current.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteChinaCity(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysChinaCity();
                    entity.Id = Convert.ToInt64(id);
                    ChinaCityService.Current.DeleteChinaCity(entity);
                }
            }
            return Content("ok");
        }
        #endregion

        #region 货物类型维护

        public ActionResult GoodsType()
        {
            return View();
        }

        public ActionResult GetGoodsTypeById(string id)
        {
            var data = new SysGoodsType();
            if (!string.IsNullOrEmpty(id))
            {
                data = GoodsTypeService.Current.GetGoodsTypeById(Convert.ToInt64(id));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditGoodsType(string id, string GoodsType, string GoodsTypeDesc, string MinWeight, string PremiumAmount)
        {
            if (string.IsNullOrEmpty(id))
            {
                var entity = new SysGoodsType();
                entity.GoodsType = GoodsType.Trim();
                entity.GoodsTypeDesc = GoodsTypeDesc.Trim();
                entity.MinWeight = Convert.ToDecimal(MinWeight.Trim());
                entity.PremiumAmount = Convert.ToDecimal(PremiumAmount.Trim());
                entity.CreateDate = DateTime.Now;
                entity.IsDelete = false;
                GoodsTypeService.Current.AddGoodsType(entity);
            }
            else
            {
                var entity = new SysGoodsType();
                entity.Id = Convert.ToInt64(id);
                entity.GoodsType = GoodsType.Trim();
                entity.GoodsTypeDesc = GoodsTypeDesc.Trim();
                entity.MinWeight = Convert.ToDecimal(MinWeight.Trim());
                entity.PremiumAmount = Convert.ToDecimal(PremiumAmount.Trim());
                entity.CreateDate = DateTime.Now;
                GoodsTypeService.Current.UpdateGoodsType(entity);
            }
            return Content("ok");
        }

        public ActionResult DeleteGoodsTypes(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysGoodsType();
                    entity.Id = Convert.ToInt64(id);
                    GoodsTypeService.Current.DeleteGoodsType(entity);
                }
            }
            return Content("ok");
        }

        public ActionResult GetGoodsTypePagerDateList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysGoodsType>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [GoodsType] Like '%{0}%'", search);
            btdata.total = GoodsTypeService.Current.GetPagerCount(where);
            btdata.rows = GoodsTypeService.Current.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
        #endregion  货物类型维护

        #region 单价信息维护


        public ActionResult UnitPrice()
        {
            return View();
        }

        public ActionResult EditUnitPrice(string id, string RCityId, string GoodsTypeId, string LandPrice1, string LandPrice2, string AirPrice1, string AirPrice2)
        {
            if (string.IsNullOrEmpty(id) || id.Equals("0"))
            {
                var entity = new SysUnitPrice();
                entity.GoodsTypeId = Convert.ToInt64(GoodsTypeId.Trim());
                entity.RCityId = Convert.ToInt32(RCityId.Trim());
                entity.LandPrice1 = Convert.ToDecimal(LandPrice1.Trim());
                entity.LandPrice2 = Convert.ToDecimal(LandPrice2.Trim());
                entity.AirPrice1 = Convert.ToDecimal(AirPrice1.Trim());
                entity.AirPrice2 = Convert.ToDecimal(AirPrice2.Trim());
                entity.CreateDate = DateTime.Now;
                entity.IsDelete = false;
                UnitPriceService.Current.AddUnitPrice(entity);
            }
            else
            {
                var entity = new SysUnitPrice();
                entity.Id = Convert.ToInt64(id);
                entity.GoodsTypeId = Convert.ToInt64(GoodsTypeId.Trim());
                entity.RCityId = Convert.ToInt32(RCityId.Trim());
                entity.LandPrice1 = Convert.ToDecimal(LandPrice1.Trim());
                entity.LandPrice2 = Convert.ToDecimal(LandPrice2.Trim());
                entity.AirPrice1 = Convert.ToDecimal(AirPrice1.Trim());
                entity.AirPrice2 = Convert.ToDecimal(AirPrice2.Trim());
                entity.CreateDate = DateTime.Now;
                UnitPriceService.Current.UpdateUnitPrice(entity);
            }
            return Content("ok");
        }

        public ActionResult DeleteUnitPrice(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysUnitPrice();
                    entity.Id = Convert.ToInt64(id);
                    UnitPriceService.Current.DeleteUnitPrice(entity);
                }
            }
            return Content("ok");
        }

        public ActionResult GetUnitPricePagerDateList(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysUnitPrice>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [AirPrice1] Like '%{0}%'", search);
            btdata.total = UnitPriceService.Current.GetPagerCount(where);
            btdata.rows = UnitPriceService.Current.GetPagerDataList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 汇率管理
        public ActionResult Exchange()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Exchange(string evalue, string edate)
        {
            if (!string.IsNullOrEmpty(evalue))
            {
                var entity = new SysExchange();
                entity.CurrentDate = Convert.ToDateTime(edate);
                entity.ExchangeValue = Convert.ToDouble(evalue);
                entity.CreateDate = DateTime.Now;
                DALFactory.ExchangeDao.Insert(entity);
            }
            return Content("ok");
        }

        public ActionResult DeleteExchange(string[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var entity = new SysExchange();
                    entity.Id = Convert.ToInt64(id);
                    DALFactory.ExchangeDao.Delete(entity);
                }
            }
            return Content("ok");
        }

        public ActionResult GetExchangePager(string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<SysExchange>();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [AirPrice1] Like '%{0}%'", search);
            btdata.total = DALFactory.ExchangeDao.GetPagerCount(where);
            btdata.rows = DALFactory.ExchangeDao.GetPagerList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 二维码管理

        public ActionResult QrCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QrCode(HttpPostedFileBase inputdim1, HttpPostedFileBase inputdim2)
        {
            if (inputdim1 != null)
            {
                string fileSave = Server.MapPath("~/ExcelFiles/QrCodes/");
                //获取文件的扩展名
                string extName = Path.GetExtension(inputdim1.FileName);
                //得到一个新的文件名称
                string newName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + extName;
                inputdim1.SaveAs(Path.Combine(fileSave, newName));
                var data = DALFactory.DbconfigDao.GetAll();
                var entity = data.FirstOrDefault(x => x.Key.Equals("AllPayQrCodePath"));
                if (entity == null)
                {
                    entity = new SysDbConfig();
                    entity.Key = "AllPayQrCodePath";
                    entity.Value = newName;
                    entity.CreateDate = DateTime.Now;
                    entity.IsDelete = false;
                    DALFactory.DbconfigDao.Insert(entity);
                }
                else
                {
                    entity.Value = newName;
                    DALFactory.DbconfigDao.Update(entity);
                }
            }
            if (inputdim2 != null)
            {
                string fileSave = Server.MapPath("~/ExcelFiles/QrCodes/");
                //获取文件的扩展名
                string extName = Path.GetExtension(inputdim2.FileName);
                //得到一个新的文件名称
                string newName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + extName;
                inputdim2.SaveAs(Path.Combine(fileSave, newName));
                var data = DALFactory.DbconfigDao.GetAll();
                var entity = data.FirstOrDefault(x => x.Key.Equals("WebChatPayQrCodePath"));
                if (entity == null)
                {
                    entity = new SysDbConfig();
                    entity.Key = "WebChatPayQrCodePath";
                    entity.Value = newName;
                    entity.CreateDate = DateTime.Now;
                    entity.IsDelete = false;
                    DALFactory.DbconfigDao.Insert(entity);
                }
                else
                {
                    entity.Value = newName;
                    DALFactory.DbconfigDao.Update(entity);
                }
            }
            return Json("ok");
        }
        #endregion
    }
}