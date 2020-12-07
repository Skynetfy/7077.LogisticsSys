using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Alipay.MD5;

namespace Sys.Alipay
{
    public class AlipayInstance
    {
       
        public static bool Verify(SortedDictionary<string, string> sPara, string notifyId, string sign)
        {
            AlipayNotify aliNotify = new AlipayNotify();
            return aliNotify.Verify(sPara, notifyId, sign);
        }
        /// <summary>
        /// 交易
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <param name="usdAmount"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string CreateForexTrade(string tradeNo,decimal usdAmount,string subject = "运费",string body= "国际航空运费",string currency = "USD")
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", "create_forex_trade");
            sParaTemp.Add("partner", AlipayConfig.partner);
            sParaTemp.Add("_input_charset", AlipayConfig.input_charset.ToLower());
            sParaTemp.Add("notify_url", AlipayConfig.notify_url);
            sParaTemp.Add("return_url", AlipayConfig.return_url);
            sParaTemp.Add("currency", currency);
            sParaTemp.Add("out_trade_no", tradeNo);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", usdAmount.ToString("F"));
            sParaTemp.Add("body", body);
            sParaTemp.Add("product_code", "NEW_OVERSEAS_SELLER");
            //sParaTemp.Add("split_fund_info", "[{\"transIn\":\"2088621891276664\",\"amount\":\"0.01\",\"currency\":\"USD\",\"desc\":\"Split _test1\"}]");
            //其他业务参数根据在线开发文档，添加参数：
            //To add other parameters,please refer to development documents.Document address:https://global.alipay.com/service
            //如sParaTemp.Add("参数名","参数值");
            //eg:sParaTemp.put("parameter name","parameter value");	
            //建立请求
            //build request
            return AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");
        }
    }
}
