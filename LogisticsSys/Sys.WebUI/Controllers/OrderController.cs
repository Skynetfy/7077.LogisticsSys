using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sys.BLL;
using Sys.BLL.Order;
using Sys.BLL.Users;
using Sys.Common;
using Sys.Dal;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string id)
        {
            var provider = new OrderInfoProvider();
            ViewBag.OrderNumber = provider.GetOrderNumber();
            ViewData["RCityDataList"] = RussiaCityService.Current.GetBindings(0);
            ViewData["CCityDataList"] = ChinaCityService.Current.GetChinaCities(0);
            ViewData["RoomTypeDataList"] = GoodsTypeService.Current.GetRoomTypeSelect(0);

            if (!string.IsNullOrEmpty(id))
            {

            }
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
                if (!string.IsNullOrEmpty(addresserInfo.LogisticsSingle))
                {
                    var logistics = new SysLogisticsInfo();
                    logistics.LogisticsDesc = "订单处理中";
                    logistics.LogisticsSingle = addresserInfo.LogisticsSingle;
                    logistics.Status = false;
                    logistics.UpdateDate = DateTime.Now;
                    logistics.CreateDate = DateTime.Now;
                    logistics.IsDelete = false;
                    ISysLogisticsInfoRepository logisticsInfo = DALFactory.SysLogisticsInfoDao;
                    logisticsInfo.Insert(logistics);
                }
            }
            result.Message = message;
            return Json(result);
        }
        public ActionResult EditOrder()
        {

            return View();
        }
        [HttpPost]
        public ActionResult EditOrder(string orderId, string sjjg, string hwsjzl, string hwsjtjzl, string ddsjjg, string csjzl, string csjjg)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var orderPrivder = new OrderInfoProvider();
                var order = orderPrivder.GetOrderInfoById(Convert.ToInt64(orderId));
                if (order != null)
                {
                    order.OrderRealPrice = Convert.ToDecimal(sjjg);
                    order.Status = (int)OrderStatusEnum.Processed;
                    var i = orderPrivder.UpdateOrderInfo(order);
                    if (i > 0)
                    {
                        var addressinfo = orderPrivder.GetAddressInfoByOid(order.Id);
                        if (addressinfo != null)
                        {
                            addressinfo.GoodsRealWeight = Convert.ToDecimal(hwsjzl);
                            addressinfo.GoodsVolumeWeight = Convert.ToDecimal(hwsjtjzl);
                            addressinfo.AddressRealPrice = Convert.ToDecimal(ddsjjg);
                            orderPrivder.UpdateAddressInfo(addressinfo);
                        }

                        var rec = orderPrivder.GetReceiverInfo(order.Id);
                        if (rec != null)
                        {
                            rec.RealWeight = Convert.ToDecimal(csjzl);
                            rec.RealPrice = Convert.ToDecimal(csjjg);
                            orderPrivder.UpdateReceiverInfo(rec);
                        }

                    }
                }
            }
            return Content("ok");
        }
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult MyOrderList()
        {
            return View();
        }

        public ActionResult PayOrder(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var orderprovider = new OrderInfoProvider();
                var order = orderprovider.GetOrderInfoById(Convert.ToInt16(id));
                if (order != null )
                {
                    ViewBag.Id = order.Id;
                    ViewBag.SNO = order.OrderNo;
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult PayOrder(string id, string pay)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var orderprovider = new OrderInfoProvider();
                var order = orderprovider.GetOrderInfoById(Convert.ToInt16(id));
                if (order != null && order.PayStatus <= (int)OrderPayStatusEnum.Recivied)
                {
                    order.PayStatus = (int)OrderPayStatusEnum.Paied;
                    orderprovider.UpdateOrderInfo(order);
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult Filled(string fahuoid, string fhsj, string fhnr)
        {
            if (!string.IsNullOrEmpty(fahuoid))
            {
                var userprovider = new UserLoginProvider();
                var _user = userprovider.GetUser(User.Identity.Name);
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()) || _user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    var orderprovider = new OrderInfoProvider();
                    var order = orderprovider.GetOrderInfoById(Convert.ToInt16(fahuoid));
                    if (order != null && order.Status < (int)OrderStatusEnum.Unfilled)
                    {
                        var alog = new SysActionLog();
                        alog.ActionDate = Convert.ToDateTime(fhsj);
                        alog.ActionDesc = fhnr.Trim();
                        alog.LogType = (int)ActionLogTypeEnum.FilledAction;
                        alog.OrderId = order.Id;
                        alog.UserId = _user.Id;
                        alog.CreateDate = DateTime.Now;
                        alog.IsDelete = false;
                        var i = DALFactory.ActionLogDao.Insert(alog);
                        if (i > 0)
                        {
                            order.Status = (int)OrderStatusEnum.Filled;
                            orderprovider.UpdateOrderInfo(order);
                        }
                    }
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult Collection(string shoukuanid, string shoukuanshijian, string shoukuanxiangqing)
        {
            if (!string.IsNullOrEmpty(shoukuanid))
            {
                var userprovider = new UserLoginProvider();
                var _user = userprovider.GetUser(User.Identity.Name);
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()) || _user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    var orderprovider = new OrderInfoProvider();
                    var order = orderprovider.GetOrderInfoById(Convert.ToInt16(shoukuanid));
                    if (order != null && order.PayStatus < (int)OrderPayStatusEnum.Recivied)
                    {
                        var alog = new SysActionLog();
                        alog.ActionDate = Convert.ToDateTime(shoukuanshijian);
                        alog.ActionDesc = shoukuanxiangqing.Trim();
                        alog.LogType = (int)ActionLogTypeEnum.PayAction;
                        alog.OrderId = order.Id;
                        alog.UserId = _user.Id;
                        alog.CreateDate = DateTime.Now;
                        alog.IsDelete = false;
                        var i = DALFactory.ActionLogDao.Insert(alog);
                        if (i > 0)
                        {
                            order.PayStatus = (int)OrderPayStatusEnum.Recivied;
                            orderprovider.UpdateOrderInfo(order);
                        }
                    }
                }
            }
            return Content("ok");
        }
        public ActionResult OrderManage()
        {
            return View();
        }

        public ActionResult GetOrderViewPagerData(string type, string orderstatus, string search, int offset, int limit, string order, string sort)
        {
            var where = string.Empty;
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    var cityid = UserService.GetAgentInfoByUserId(_user.Id).AgentCityId;
                    where += " and [RussiaCityId]=" + cityid;
                }
                else if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    where += " and [UserId]=" + _user.Id;
                }
            }
            var btdata = new BootstrapTableData<OrderView>();
            var provider = new OrderInfoProvider();

            if (!string.IsNullOrEmpty(search))
                where += string.Format(@" and [OrderNo] Like '%{0}%'", search);
            if (!string.IsNullOrEmpty(orderstatus))
                where += string.Format(@" and [Status]={0}", orderstatus);
            btdata.total = provider.GetOrderViewPagerCount(where);
            btdata.rows = provider.GetOrderViewPagerList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
    }
}