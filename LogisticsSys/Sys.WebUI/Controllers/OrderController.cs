using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sys.BLL;
using Sys.BLL.Order;
using Sys.Common;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var provider = new OrderInfoProvider();
            ViewBag.OrderNumber = provider.GetOrderNumber();
            ViewData["RCityDataList"] = RussiaCityService.Current.GetBindings(0);
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(string ordersingle, string shappername, string shipperphone, string pickupnumber, string russiacityid, string russiaaddress, string logisticsSingle, string cargonumber, string pickupdate, string pickupWay, string goodstype, string transportationway, string protectprice, string policyfee, string goodsweight, string boxlong, string boxwidth, string boxheight, string parcelsingle, string chinacityid, string chinaaddress, string receivername, string receiverphone, string packagingway, string expressway, string goodsdesc, string parcelweight, string chinacouriernumber, string desc)
        {
            var result = new ResponseJsonResult<string>();
            result.Status = 0;
            result.Message = "未知错误";

            var orderinfo = new SysOrderInfo();
            orderinfo.OrderNo = ordersingle.Trim();
            orderinfo.PickupNumber = Convert.ToInt32(pickupnumber);
            orderinfo.ShipperName = shappername.Trim();
            orderinfo.ShipperPhone = shipperphone.Trim();
            orderinfo.Status = (int)OrderStatusEnum.Processing;

            var addresserInfo = new SysAddresserInfo();
            addresserInfo.BoxHeight = Convert.ToDecimal(boxheight);
            addresserInfo.BoxWidth = Convert.ToDecimal(boxwidth);
            addresserInfo.BoxLong = Convert.ToDecimal(boxlong);
            addresserInfo.GoodsWeight = Convert.ToDecimal(goodsweight);
            addresserInfo.PolicyFee = Convert.ToDecimal(policyfee);
            addresserInfo.ProtectPrice = Convert.ToDecimal(protectprice);
            addresserInfo.TransportationWay = Convert.ToInt32(transportationway);
            addresserInfo.GoodsType = Convert.ToInt32(goodstype);
            addresserInfo.PickupWay = Convert.ToInt32(pickupWay);
            addresserInfo.PickupDate = Convert.ToDateTime(pickupdate);
            addresserInfo.CargoNumber = Convert.ToInt32(cargonumber);
            addresserInfo.LogisticsSingle = logisticsSingle.Trim();
            addresserInfo.RussiaAddress = russiaaddress.Trim();
            addresserInfo.RussiaCityId = Convert.ToInt64(russiacityid);

            var receiverInfo = new SysReceiverInfo();
            receiverInfo.ParcelSingle = parcelsingle.Trim();
            receiverInfo.ChinaCityId = Convert.ToInt64(chinacityid);
            receiverInfo.ChinaAddress = chinaaddress.Trim();
            receiverInfo.ReceiverName = receivername.Trim();
            receiverInfo.ReceiverPhone = receiverphone.Trim();
            receiverInfo.PackagingWay = Convert.ToInt32(packagingway);
            receiverInfo.ExpressWay = Convert.ToInt32(expressway);
            receiverInfo.GoodsDesc = goodsdesc.Trim();
            receiverInfo.ParcelWeight = Convert.ToDecimal(parcelweight);
            receiverInfo.ChinaCourierNumber = chinacouriernumber.Trim();
            receiverInfo.Desc = desc.Trim();
            var username = User.Identity.Name;
            var status = 0;
            var provider = new OrderInfoProvider();
            var message = provider.AddOrderInfo(username, orderinfo, addresserInfo, receiverInfo, ref status);

            if (status == 1)
            {
                result.Status = 1;
            }
            result.Message = message;
            return Json(result);
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderManage()
        {
            return View();
        }

        public ActionResult GetOrderViewPagerData(string type,string orderstatus,string search, int offset, int limit, string order, string sort)
        {
            var btdata = new BootstrapTableData<OrderView>();
            var provider = new OrderInfoProvider();
            var where = string.Empty;
            if (!string.IsNullOrEmpty(search))
                where = string.Format(@" and [OrderNo] Like '%{0}%'", search);
            if(!string.IsNullOrEmpty(orderstatus))
                where += string.Format(@" and [Status]={0}", orderstatus);
            btdata.total = provider.GetOrderViewPagerCount(where);
            btdata.rows = provider.GetOrderViewPagerList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}