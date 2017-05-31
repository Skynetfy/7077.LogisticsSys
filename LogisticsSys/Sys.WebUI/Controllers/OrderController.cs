using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
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
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index(string id)
        {
            ISysKuaiDiComRepository kuaidiDao = DALFactory.KuaiDiDao;
            var provider = new OrderInfoProvider();
            ViewBag.OrderNumber = provider.GetOrderNumber();
            ViewData["RCityDataList"] = RussiaCityService.Current.GetBindings(0).OrderByDescending(x => x.Value).ToList();
            ViewData["CCityDataList"] = ChinaCityService.Current.GetChinaCities(0);
            ViewData["RoomTypeDataList"] = GoodsTypeService.Current.GetRoomTypeSelect(0);
            ViewData["CourierComData"] = kuaidiDao.GetAll().Select(x => new SelectBinding()
            {
                Value = x.Id.ToString(),
                Text = x.ComName
            }).ToList();
            var username = User.Identity.Name;
            var userProvider = new UserLoginProvider();
            var _user = userProvider.GetUser(username);
            ViewBag.disabled = "false";
            if (_user != null)
            {
                var cusmer = UserService.GetCustomerByUid(_user.Id);
                if (cusmer == null || string.IsNullOrEmpty(_user.DisplayName) || string.IsNullOrEmpty(_user.Phone))
                {
                    ViewBag.disabled = "true";
                }
            }
            ViewBag.ExchangeValue = SystemService.GetCurrentExchangeValue();
            return View();
        }

        public ActionResult OrderView(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var provider = new OrderInfoProvider();
                var orderinfo = provider.GetOrderViewById(Convert.ToInt64(id));

                orderinfo.OrderNumbers = string.Join(",", DALFactory.OrderNumberDao.GetAll()
                        .Where(x => x.OrderId == Convert.ToInt64(id))
                        .Select(x => x.Number)
                        .ToList());
                ViewData["OrderInfo"] = orderinfo;

                var orderpayinfo =
                    DALFactory.OrderPayInfoDao.GetAll().Where(x => !x.IsDelete && x.OrderId == orderinfo.Id).ToList();
                ViewData["OrderPayInfo"] = orderpayinfo;

                var filledinfo =
                    DALFactory.ActionLogDao.GetAll()
                        .FirstOrDefault(
                            x =>
                                !x.IsDelete && x.OrderId == orderinfo.Id &&
                                x.LogType == (int)ActionLogTypeEnum.FilledAction);
                ViewData["SysActionLog"] = filledinfo;
                //var address = provider.GetAddressInfoByOid(Convert.ToInt64(id));
                //ViewData["AddressInfo"] = address;
            }
            return View();
        }

        //public ActionResult GetRevicerInfos(string oid)
        //{
        //    var provider = new OrderInfoProvider();
        //    var data = provider.GetReceiverInfo(Convert.ToInt64(oid)).ToList();
        //    var btdata = new BootstrapTableData<SysReceiverInfo>();
        //    btdata.rows = data;
        //    btdata.total = data.Count;
        //    return Json(btdata, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetAddressBudgetPrice(string cityId, string goodstype, string transType, string weight)
        {
            var result = new ResponseJsonResult<decimal>();
            result.Status = 1;
            var unprice = UnitPriceService.Current.GetByCityIdGoodsTypeId(Convert.ToInt64(cityId),
                Convert.ToInt64(goodstype));
            if (unprice != null)
            {
                decimal w = Convert.ToDecimal(weight);
                if (transType.Equals("1"))
                {

                    if (w <= 20)
                    {
                        result.Message = (unprice.AirPrice1 * w).ToString();
                    }
                    else if (20 < w && w <= 50)
                    {
                        result.Message = (unprice.AirPrice2 * w).ToString();
                    }
                    else
                    {
                        result.Message = (unprice.LandPrice1 * w).ToString();
                    }
                }
                else
                {
                    result.Message = (unprice.LandPrice2 * w).ToString();
                }
            }
            else
            {
                result.Message = "0";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRBudgetPrice(string cid, string eway, string weight)
        {
            var result = new ResponseJsonResult<decimal>();
            result.Status = 1;
            var chnna = ChinaCityService.Current.GetById(Convert.ToInt64(cid));
            if (chnna != null)
            {
                decimal w = Convert.ToDecimal(weight);
                if (eway.Equals("1"))
                {
                    result.Message = (chnna.UnitPrice * w).ToString();
                }
                else if (eway.Equals("2"))
                {
                    result.Message = (chnna.ExpressBeavyPrice * w).ToString();
                }
                else if (eway.Equals("3"))
                {
                    result.Message = (chnna.SfUnitPrice * w).ToString();
                }
                else
                {
                    result.Message = (chnna.SflogisticsBeavyPrice * w).ToString();
                }
            }
            else
            {
                result.Message = "0";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult CreateOrder(string russiacityid, string russiaaddress, string weburl, string logisticsSingle, string cargonumber, string pickupdate, string goodstype, string transportationway, string protectprice, string policyfee, string isarrivepay, string arrivepayvalue, string isoutphoto, string exchangerate, string chinacityid, string chinaaddress, string receivername, string receiverphone, string packagingway, string expressway, HttpPostedFileBase goodsdesc, string kdgs, string desc)
        {
            var result = new ResponseJsonResult<string>();
            result.Status = 0;
            result.Message = "未知错误";

            var provider = new OrderInfoProvider();
            var username = User.Identity.Name;
            var userProvider = new UserLoginProvider();
            var _user = userProvider.GetUser(username);
            if (_user != null)
            {
                var cusmer = UserService.GetCustomerByUid(_user.Id);
                if (cusmer != null && !string.IsNullOrEmpty(_user.DisplayName) && !string.IsNullOrEmpty(_user.Phone))
                {

                    var orderinfo = new SysOrderInfo();
                    orderinfo.OrderNo = provider.GetOrderNumber(); ;
                    orderinfo.PickupNumber = 0;
                    orderinfo.ShipperName = _user.UserName;
                    orderinfo.ShipperPhone = _user.Phone;
                    orderinfo.Status = (int)OrderStatusEnum.Processing;
                    orderinfo.PayStatus = (int)OrderPayStatusEnum.UnPaying;
                    orderinfo.OrderRealPrice = 0;

                    var addresserInfo = new SysAddresserInfo();
                    addresserInfo.LogisticsSingle = !string.IsNullOrEmpty(logisticsSingle) ? logisticsSingle.Trim() : "";
                    addresserInfo.RussiaCityId = !string.IsNullOrEmpty(russiacityid) ? Convert.ToInt64(russiacityid) : 0;
                    addresserInfo.RussiaAddress = !string.IsNullOrEmpty(russiaaddress) ? russiaaddress.Trim() : "";
                    addresserInfo.PickupDate = !string.IsNullOrEmpty(pickupdate) ? Convert.ToDateTime(pickupdate) : DateTime.MinValue;
                    addresserInfo.GoodsType = !string.IsNullOrEmpty(goodstype) ? Convert.ToInt32(goodstype) : 0;
                    addresserInfo.TransportationWay = !string.IsNullOrEmpty(transportationway) ? Convert.ToInt32(transportationway) : 0;
                    addresserInfo.GoodsWeight = 0;
                    addresserInfo.ProtectPrice = !string.IsNullOrEmpty(protectprice) ? Convert.ToDecimal(protectprice) : 0;
                    addresserInfo.PolicyFee = !string.IsNullOrEmpty(policyfee) ? Convert.ToDecimal(policyfee) : 0;
                    addresserInfo.ExchangeRate = !string.IsNullOrEmpty(exchangerate)
                        ? Convert.ToDecimal(exchangerate)
                        : 1;
                    addresserInfo.IsArrivePay = !string.IsNullOrEmpty(isarrivepay);
                    if (addresserInfo.IsArrivePay)
                    {
                        addresserInfo.ArrivePayValue = Math.Round(Convert.ToDecimal(arrivepayvalue) / addresserInfo.ExchangeRate, 2);
                    }
                    //addresserInfo.ArrivePayValue = addresserInfo.IsArrivePay ? Convert.ToDecimal(arrivepayvalue) : 0;
                    addresserInfo.IsOutPhoto = !string.IsNullOrEmpty(isoutphoto);
                    addresserInfo.WebUrl = !string.IsNullOrEmpty(weburl) ? weburl.Trim() : "";

                    addresserInfo.OrderFrees = 0;

                    var receiverInfo = new SysReceiverInfo();
                    receiverInfo.ChinaCityId = !string.IsNullOrEmpty(chinacityid) ? Convert.ToInt64(chinacityid) : 0;
                    receiverInfo.ChinaAddress = !string.IsNullOrEmpty(chinaaddress) ? chinaaddress.Trim() : "";
                    receiverInfo.ReceiverName = !string.IsNullOrEmpty(receivername) ? receivername.Trim() : "";
                    receiverInfo.ReceiverPhone = !string.IsNullOrEmpty(receiverphone) ? receiverphone.Trim() : "";
                    receiverInfo.PackagingWay = !string.IsNullOrEmpty(packagingway) ? Convert.ToInt32(packagingway) : 0;
                    receiverInfo.ExpressWay = !string.IsNullOrEmpty(expressway) ? Convert.ToInt32(expressway) : 0;
                    //HttpPostedFileBase goodsdesc = Request.Files[0];
                    if (goodsdesc != null)
                    {
                        string fileSave = Server.MapPath("~/ExcelFiles/");
                        //获取文件的扩展名
                        string extName = Path.GetExtension(goodsdesc.FileName);
                        //得到一个新的文件名称
                        string newName = orderinfo.OrderNo + extName;
                        receiverInfo.GoodsDesc = newName;
                        goodsdesc.SaveAs(Path.Combine(fileSave, newName));
                    }
                    //receiverInfo.GoodsDesc = !string.IsNullOrEmpty(goodsdesc) ? goodsdesc.Trim() : "";
                    receiverInfo.ParcelWeight = 0;
                    receiverInfo.CourierComId = !string.IsNullOrEmpty(kdgs) ? Convert.ToInt64(kdgs) : 0;
                    receiverInfo.ChinaCourierNumber = "";
                    receiverInfo.Desc = !string.IsNullOrEmpty(desc) ? desc.Trim() : "";
                    receiverInfo.CourierFees = 0;

                    long status = 0;
                    var message = provider.AddOrderInfo(username, orderinfo, addresserInfo, receiverInfo, ref status);

                    if (status > 0)
                    {
                        if (!string.IsNullOrEmpty(cargonumber))
                        {
                            var args = cargonumber.Split(',');
                            foreach (var arg in args)
                            {
                                var ordernumber = new SysOrderNumber();
                                ordernumber.Number = arg.Trim();
                                ordernumber.OrderId = status;
                                ordernumber.Status = false;
                                ordernumber.IsDelete = false;
                                ordernumber.CreateDate = DateTime.Now;
                                DALFactory.OrderNumberDao.Insert(ordernumber);
                            }
                        }
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
                    result.Message = orderinfo.OrderNo;
                }
            }
            else
            {
                result.Message = "客户信息不完整，暂时不能下单";
            }
            return Json(result);
        }
        public ActionResult EditOrder(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var provider = new OrderInfoProvider();
                var orderinfo = provider.GetOrderViewById(Convert.ToInt64(id));
                ViewData["OrderInfo"] = orderinfo;

                //var address = provider.GetAddressInfoByOid(Convert.ToInt64(id));
                //ViewData["AddressInfo"] = address;
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditOrder(string orderId, string goodsweight, string chinacouriernumber, string packagingCosts, string orderfrees, string gjdfy, string kdfy, string insuranceCost, string domesticcost)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var orderPrivder = new OrderInfoProvider();
                var order = orderPrivder.GetOrderInfoById(Convert.ToInt64(orderId));
                if (order != null)
                {
                    order.OrderRealPrice = Convert.ToDecimal(orderfrees);
                    order.Status = (int)OrderStatusEnum.Processed;
                    var i = orderPrivder.UpdateOrderInfo(order);
                    if (i > 0)
                    {
                        var addressinfo = orderPrivder.GetAddressInfoByOid(order.Id);
                        if (addressinfo != null)
                        {
                            addressinfo.GoodsWeight = Convert.ToDecimal(goodsweight);
                            addressinfo.OrderFrees = Convert.ToDecimal(gjdfy);
                            addressinfo.InsuranceCost = Convert.ToDecimal(insuranceCost);
                            orderPrivder.UpdateAddressInfo(addressinfo);
                        }

                        var rec = orderPrivder.GetReceiverInfo(order.Id);
                        if (rec != null)
                        {
                            //rec.ParcelWeight = Convert.ToDecimal(parcelweight);
                            rec.ChinaCourierNumber = chinacouriernumber ?? "";
                            rec.PackagingCosts = Convert.ToDecimal(packagingCosts);
                            rec.CourierFees = Convert.ToDecimal(kdfy);
                            rec.DomesticCost = Convert.ToDecimal(domesticcost);
                            orderPrivder.UpdateReceiverInfo(rec);
                        }

                    }
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult UpdateOrder(string updateorderid, string editguoneidingdanhao, HttpPostedFileBase editgoodsdesc)
        {
            if (!string.IsNullOrEmpty(updateorderid))
            {
                var orderPrivder = new OrderInfoProvider();
                var order = orderPrivder.GetOrderInfoById(Convert.ToInt64(updateorderid));
                if (order != null)
                {
                    var rec = orderPrivder.GetReceiverInfo(order.Id);
                    if (rec != null)
                    {
                        rec.ChinaCourierNumber = !string.IsNullOrEmpty(editguoneidingdanhao) ? editguoneidingdanhao.Trim() : "";
                        if (editgoodsdesc != null)
                        {
                            string fileSave = Server.MapPath("~/ExcelFiles/");
                            //获取文件的扩展名
                            string extName = Path.GetExtension(editgoodsdesc.FileName);
                            //得到一个新的文件名称
                            string newName = order.OrderNo + extName;
                            rec.GoodsDesc = newName;
                            editgoodsdesc.SaveAs(Path.Combine(fileSave, newName));
                        }
                        orderPrivder.UpdateReceiverInfo(rec);
                    }
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult EiditRevecierInfo(string shoujianId, string realweight, string realprice)
        {
            if (!string.IsNullOrEmpty(shoujianId))
            {
                ISysReceiverInfoRepository dao_ = DALFactory.SysReceiverInfoDao;
                var reinfo = dao_.FindByPk(Convert.ToInt64(shoujianId));
                if (reinfo != null)
                {
                    //reinfo.RealWeight = Convert.ToDecimal(realweight);
                    //reinfo.RealPrice = Convert.ToDecimal(realprice);
                    dao_.Update(reinfo);
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
        [HttpPost]
        public ActionResult DeleteOrdeer(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var orderprovider = new OrderInfoProvider();
                var order = orderprovider.GetOrderInfoById(Convert.ToInt16(id));
                if (order != null)
                {
                    order.Status = (int)OrderStatusEnum.Deleted;
                    //order.IsDelete = true;
                    orderprovider.UpdateOrderInfo(order);
                }
            }
            return Content("ok");
        }
        public ActionResult PayOrder(string id, string type)
        {
            var dbconfig = DALFactory.DbconfigDao.GetAll();
            var allpay = dbconfig.FirstOrDefault(x => x.Key.Equals("AllPayQrCodePath"));
            if (allpay != null)
            {
                ViewBag.AllPay = allpay.Value;
            }
            var webpay = dbconfig.FirstOrDefault(x => x.Key.Equals("WebChatPayQrCodePath"));
            if (webpay != null)
            {
                ViewBag.WebPay = webpay.Value;
            }
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
            {
                CostTypeEnums costType = (CostTypeEnums)Enum.Parse(typeof(CostTypeEnums), type);
                var orderprovider = new OrderInfoProvider();
                var order = orderprovider.GetOrderViewById(Convert.ToInt16(id));
                if (order != null)
                {
                    ViewBag.Id = order.Id;
                    ViewBag.SNO = order.OrderNo;
                    ViewBag.OrderPrice = order.OrderRealPrice;
                    ViewBag.Type = type;
                    ViewBag.ArrivePayValue = order.ArrivePayValue;
                    ViewBag.OrderFrees = order.OrderFrees;
                    ViewBag.DomesticCost = order.DomesticCost;
                    //if (CostTypeEnums.WorldPay == costType)
                    //{
                    //    var addressorder = orderprovider.GetAddressInfoByOid(order.Id);
                    //    if (addressorder != null)
                    //    {
                    //        ViewBag.GoodsWeight = addressorder.GoodsWeight;
                    //        ViewBag.TransportationWay = addressorder.GoodsWeight >= 50
                    //            ? addressorder.TransportationWay
                    //            : 2;
                    //        var _user = UserService.GetCustomerByUid(order.UserId);
                    //        if (_user != null)
                    //        {
                    //            ViewBag.Integral = _user.Integral;
                    //            int realWeight = _user.Integral / 10000;
                    //            int realIntegral = 0;
                    //            decimal youhuiPrice = 0;
                    //            decimal realPrice = order.OrderFrees;
                    //            if (addressorder.GoodsWeight < realWeight)
                    //            {
                    //                realWeight = (int)Math.Abs(addressorder.GoodsWeight);
                    //            }
                    //            realIntegral = realWeight * 10000;
                    //            ViewBag.RealIntegral = realIntegral;
                    //            if (addressorder.GoodsWeight >= 50)
                    //            {
                    //                if (addressorder.TransportationWay == 2)
                    //                {
                    //                    youhuiPrice = realWeight * 85;
                    //                }
                    //                else
                    //                {
                    //                    youhuiPrice = realWeight * 60;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                youhuiPrice = realWeight * 85;
                    //            }
                    //            ViewBag.YouHuiPrice = youhuiPrice;
                    //            realPrice = (realPrice - youhuiPrice) >= 0 ? realPrice - youhuiPrice : 0;
                    //            ViewBag.RealPrice = realPrice;
                    //            ViewBag.ekc = DEncrypt.Md5(realIntegral.ToString() + realPrice.ToString());
                    //        }
                    //    }
                    //}

                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult PayOrder(string type, string orderId, string cardnumber, string cardusername, string optionsRadios, string payamount, string realprice, string useJifen, string keyongjifen, string ekc)
        {
            if (!string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(type))
            {
                var orderprovider = new OrderInfoProvider();
                var order = orderprovider.GetOrderInfoById(Convert.ToInt16(orderId));
                if (order != null && order.PayStatus <= (int)OrderPayStatusEnum.Recivied)
                {
                    CostTypeEnums costType = (CostTypeEnums)Enum.Parse(typeof(CostTypeEnums), type);
                    decimal p = Convert.ToDecimal(payamount);
                    switch (costType)
                    {
                        case CostTypeEnums.ArrivePay:
                            order.ArrivePayStatus = 1;
                            break;
                        case CostTypeEnums.WorldPay:
                            order.WorldPayStatus = 1;
                            //if (!string.IsNullOrEmpty(useJifen) && !string.IsNullOrEmpty(ekc))
                            //{
                            //    var e = DEncrypt.Md5(keyongjifen + realprice);
                            //    if (e.Equals(ekc))
                            //    {
                            //        var _user = UserService.GetCustomerByUid(order.UserId);
                            //        if (_user != null)
                            //        {
                            //            var integl = _user.Integral;
                            //            var realIntegral = (integl / 10000) * 10000;
                            //            if (Convert.ToInt32(keyongjifen) <= realIntegral && realIntegral > 0)
                            //            {
                            //                UserService.UpdateIntegral(order, Convert.ToInt32(keyongjifen));
                            //                p = Convert.ToDecimal(realprice);
                            //            }

                            //        }
                            //    }
                            //}
                            break;
                        case CostTypeEnums.ChinaPay:
                            order.ChinaPayStatus = 1;
                            break;
                    }
                    var addressorder = orderprovider.GetAddressInfoByOid(order.Id);
                    if ((order.ArrivePayStatus == 1 || !addressorder.IsArrivePay) && order.WorldPayStatus == 1 && order.ChinaPayStatus == 1)
                        order.PayStatus = (int)OrderPayStatusEnum.Paied;
                    orderprovider.UpdateOrderInfo(order);

                    var payinfo = new SysOrderPayInfo();
                    payinfo.Type = Convert.ToInt32(optionsRadios);
                    payinfo.OrderId = order.Id;
                    payinfo.CardNumber = !string.IsNullOrEmpty(Request.Form["cardnumber" + optionsRadios]) ? Request.Form["cardnumber" + optionsRadios] : "";
                    payinfo.PayAmount = p;
                    payinfo.PayUserName = !string.IsNullOrEmpty(Request.Form["cardusername" + optionsRadios]) ? Request.Form["cardusername" + optionsRadios] : "";
                    payinfo.CreateDate = DateTime.Now;
                    payinfo.CostType = type ?? "";
                    DALFactory.OrderPayInfoDao.Insert(payinfo);
                }
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult Filled(string fahuoid, string gzdh, string fhsj, string fhnr)
        {
            if (!string.IsNullOrEmpty(fahuoid))
            {

                var userprovider = new UserLoginProvider();
                var _user = userprovider.GetUser(User.Identity.Name);
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()) || _user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    var orderprovider = new OrderInfoProvider();
                    var ids = fahuoid.Split(',');
                    foreach (var id in ids)
                    {
                        var order = orderprovider.GetOrderInfoById(Convert.ToInt16(id));
                        if (order != null && order.Status < (int)OrderStatusEnum.Unfilled)
                        {
                            var alog = new SysActionLog();
                            alog.ActionDate = Convert.ToDateTime(fhsj);
                            alog.ActionDesc = fhnr.Trim();
                            //alog.LogisticsSingle = gzdh.Trim();
                            alog.LogType = (int)ActionLogTypeEnum.FilledAction;
                            alog.OrderId = order.Id;
                            alog.CreateDate = DateTime.Now;
                            alog.UserId = _user.Id;
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
        [HttpPost]
        public ActionResult Complete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var userprovider = new UserLoginProvider();
                var _user = userprovider.GetUser(User.Identity.Name);
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()) || _user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    var orderprovider = new OrderInfoProvider();
                    var order = orderprovider.GetOrderInfoById(Convert.ToInt16(id));
                    if (order != null && order.PayStatus == (int)OrderPayStatusEnum.Recivied)
                    {
                        order.Status = (int)OrderStatusEnum.Successed;
                        orderprovider.UpdateOrderInfo(order);

                        //反积分
                        //UserService.UpdateCustomerIntegral(order.Id, order.UserId, order.Integral);
                    }
                }
            }
            return Content("ok");
        }
        public ActionResult OrderManage()
        {
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            ViewBag.Role = _user.RuleType;
            return View();
        }

        public ActionResult GetOrderViewPagerData(string type, string orderstatus, string orderdate, string search, int offset, int limit, string order, string sort)
        {
            var where = string.Empty;
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    var city = UserService.GetAgentInfoByUserId(_user.Id);
                    if (city != null)
                        where += " and [RussiaCityId]=" + city.AgentCityId;
                    else
                        where += " and [RussiaCityId]=0 ";
                }
                else if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    where += " and [UserId]=" + _user.Id;
                }
            }
            var btdata = new BootstrapTableData<OrderView>();
            var provider = new OrderInfoProvider();

            if (!string.IsNullOrEmpty(search))
                where += string.Format(@" and ([OrderNo] Like '%{0}%' or [ShipperName] Like '%{0}%' or [ShipperPhone] Like '%{0}%' or [RussiaCityId] Like '%{0}%')", search);
            if (!string.IsNullOrEmpty(orderstatus))
            {
                int status = Convert.ToInt32(orderstatus);
                if (status > 2 && status <= 6)
                {
                    where += string.Format(@" and [PayStatus]={0}", status - 3);
                }
                else
                {
                    where += string.Format(@" and [Status]={0}", orderstatus);
                }

            }
            if (!string.IsNullOrEmpty(orderdate))
            {
                var ds = orderdate.Split('-');
                where += string.Format(" and [CreateDate] BETWEEN '{0}' AND '{1}'", ds[0], ds[1]);
            }
            btdata.total = provider.GetOrderViewPagerCount(where);
            btdata.rows = provider.GetOrderViewPagerList(where, offset, limit, order, sort);

            return Json(btdata, JsonRequestBehavior.AllowGet);
        }

        public void ExportOrderExcel()
        {
            string filename = "订单列表.xlsx";

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));

            var where = string.Empty;
            var userprovider = new UserLoginProvider();
            var _user = userprovider.GetUser(User.Identity.Name);
            if (_user != null)
            {
                if (_user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    var city = UserService.GetAgentInfoByUserId(_user.Id);
                    if (city != null)
                        where += " and [RussiaCityId]=" + city.AgentCityId;
                    else
                        where += " and [RussiaCityId]=0 ";
                }
                else if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    where += " and [UserId]=" + _user.Id;
                }
            }
            var provider = new OrderInfoProvider();
            var count = provider.GetOrderViewPagerCount(where);
            var data = provider.GetOrderViewPagerList(where, 0, count, "Desc", "Createdate");
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("订单号");
            row0.CreateCell(1).SetCellValue("收件人");
            row0.CreateCell(2).SetCellValue("收件人电话");
            row0.CreateCell(3).SetCellValue("俄罗斯城市");
            row0.CreateCell(4).SetCellValue("商店地址");
            row0.CreateCell(5).SetCellValue("到达国外仓时间");
            row0.CreateCell(6).SetCellValue("货物类型");
            row0.CreateCell(7).SetCellValue("货物运输类型");
            row0.CreateCell(8).SetCellValue("报价价值");
            row0.CreateCell(9).SetCellValue("报价费用");
            row0.CreateCell(10).SetCellValue("货物重量");
            row0.CreateCell(11).SetCellValue("是否到付");
            row0.CreateCell(12).SetCellValue("到付金额");
            row0.CreateCell(13).SetCellValue("当日汇率");
            row0.CreateCell(14).SetCellValue("是否开箱");
            row0.CreateCell(15).SetCellValue("订单省份");
            row0.CreateCell(16).SetCellValue("订单详细地址");
            row0.CreateCell(17).SetCellValue("收件人姓名");
            row0.CreateCell(18).SetCellValue("收件人电话");
            row0.CreateCell(19).SetCellValue("打包方式");
            row0.CreateCell(20).SetCellValue("快递方式");
            row0.CreateCell(21).SetCellValue("快递公司");
            row0.CreateCell(22).SetCellValue("包裹重量");
            row0.CreateCell(23).SetCellValue("快递费用");
            row0.CreateCell(24).SetCellValue("国内快递单号");
            row0.CreateCell(25).SetCellValue("备注");
            row0.CreateCell(26).SetCellValue("订单状态");
            row0.CreateCell(27).SetCellValue("付款状态");
            int x = 27;
            for (var c = 0; c < data.Count; c++)
            {
                var item = data[c];
                IRow row = sheet1.CreateRow(c + 1);
                row.CreateCell(0).SetCellValue(item.OrderNo);
                row.CreateCell(1).SetCellValue(item.ShipperName);
                row.CreateCell(2).SetCellValue(item.ShipperName);
                row.CreateCell(3).SetCellValue(item.ReceiverName);
                row.CreateCell(4).SetCellValue(item.RussiaAddress);
                row.CreateCell(5).SetCellValue(item.PickupDate.ToString("d"));
                row.CreateCell(6).SetCellValue(item.GoodsTypeName);
                row.CreateCell(7).SetCellValue(OrderInfoProvider.GetCargoTransWayString(item.TransportationWay));
                row.CreateCell(8).SetCellValue(Convert.ToDouble(item.ProtectPrice));
                row.CreateCell(9).SetCellValue(Convert.ToDouble(item.PolicyFee));
                row.CreateCell(10).SetCellValue(Convert.ToDouble(item.GoodsWeight));
                row.CreateCell(11).SetCellValue(item.IsArrivePay);
                row.CreateCell(12).SetCellValue(Convert.ToDouble(item.ArrivePayValue));
                row.CreateCell(13).SetCellValue(Convert.ToDouble(item.ExchangeRate));
                row.CreateCell(14).SetCellValue(item.IsOutPhoto);
                row.CreateCell(15).SetCellValue(item.ChinaCityName);
                row.CreateCell(16).SetCellValue(item.ChinaAddress);
                row.CreateCell(17).SetCellValue(item.ReceiverName);
                row.CreateCell(18).SetCellValue(item.ReceiverPhone);
                row.CreateCell(19).SetCellValue(OrderInfoProvider.GetPackagingTypeString(item.PackagingWay));
                row.CreateCell(20).SetCellValue(OrderInfoProvider.GetExpressWayString(item.ExpressWay));
                row.CreateCell(21).SetCellValue(item.CourierComName);
                row.CreateCell(22).SetCellValue(Convert.ToDouble(item.ParcelWeight));
                row.CreateCell(23).SetCellValue(Convert.ToDouble(item.CourierFees));
                row.CreateCell(24).SetCellValue(item.ChinaCourierNumber);
                row.CreateCell(25).SetCellValue(item.Desc);
                row.CreateCell(26).SetCellValue(OrderInfoProvider.GetStatusString(item.Status));
                row.CreateCell(27).SetCellValue(OrderInfoProvider.GetPayStatusString(item.PayStatus));
            }
            var path = Server.MapPath("~/ExcelFiles/订单列表.xlsx");
            using (var f = System.IO.File.Create(path))
            {
                workbook.Write(f);
            }
            Response.WriteFile(path);
            //http://social.msdn.microsoft.com/Forums/en-US/3a7bdd79-f926-4a5e-bcb0-ef81b6c09dcf/responseoutputstreamwrite-writes-all-but-insetrs-a-char-every-64k?forum=ncl
            //workbook.Write(Response.OutputStream); cannot be used 
            //root cause: Response.OutputStream will insert unnecessary byte into the response bytes.
            Response.Flush();
            Response.End();
        }
    }
}