using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sys.Alipay;
using Sys.Alipay.MD5;
using Sys.Common;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public class OrderTradeService
    {
        private readonly ISysOrderInfoRepository orderDao = DALFactory.SysOrderInfoDao;
        private readonly ISysOrderTradeRepository orderTradeDao = DALFactory.OrderTradeRepository;

        /// <summary>
        /// 交易
        /// </summary>
        /// <param name="orderId"></param>
        public string CreateForexTrade(int orderId)
        {
            OrderView order = orderDao.GetOrderViewById(orderId);
            if (order != null)
            {
                string tradeNo = DateTime.Now.ToString("yyyyMMddHHmmss") + orderId.GetHashCode() + Guid.NewGuid().GetHashCode();
                decimal total = order.OrderFrees + order.DomesticCost;

                var rateStrinig = AlipayExchangeService.Convert(total, "CNY", "USD");
                //dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(rateStrinig);
                //double rate = jsonObj.result.rate;
                //decimal usdAmount = jsonObj.result.camount;

                double rate = 0;
                decimal usdAmount = 100;

                var orderTrade = new SysOrderTrade();
                orderTrade.UserId = order.UserId;
                orderTrade.OrderId = orderId;
                orderTrade.Amount = total;
                orderTrade.Rate = rate;
                orderTrade.Status = 0;
                orderTrade.OutTradeNo = tradeNo;
                orderTrade.TradeDate = DateTime.Now;
                orderTrade.CreateDate = DateTime.Now;
                orderTradeDao.Insert(orderTrade);
                return AlipayInstance.CreateForexTrade(tradeNo, usdAmount);
            }

            return "交易出错，请联系系统管理员！";
        }

        /// <summary>
        /// 交易异步通知
        /// </summary>
        /// <param name="sPara"></param>
        /// <param name="notifyId"></param>
        /// <param name="sign"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <param name="trade_status"></param>
        /// <returns></returns>
        public string ForexTradeNotify(SortedDictionary<string, string> sPara, string notifyId, string sign, string out_trade_no, string trade_no, string trade_status)
        {
            if (sPara.Count > 0 && !string.IsNullOrEmpty(out_trade_no))//判断是否有带返回参数check whether there's any para
            {
                bool verifyResult = AlipayInstance.Verify(sPara, notifyId, sign);

                if (verifyResult)//验证成功 verification is succeeded
                {
                    SysOrderTrade orderTrade = orderTradeDao.FindByOutTradeNo(out_trade_no);

                    if (orderTrade != null)
                    {
                        if (trade_status == "TRADE_FINISHED")
                        {
                            orderTrade.TradeNo = trade_no;
                            orderTrade.RealAmount = Convert.ToDecimal(sPara["total_fee"]);
                            orderTrade.Status = -1;
                        }
                        else if (trade_status == "TRADE_SUCCESS")
                        {
                            orderTrade.TradeNo = trade_no;
                            orderTrade.RealAmount = Convert.ToDecimal(sPara["total_fee"]);
                            orderTrade.Status = 1;
                        }
                        orderTradeDao.Update(orderTrade);
                    }
                    return "success";

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败verification failed
                {
                    return "fail";
                }
            }
            else
            {
                return "无通知参数No params";
            }
        }
    }
}
