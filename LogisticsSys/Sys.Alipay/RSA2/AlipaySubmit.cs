using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sys.RSA2.Alipay
{
    public class AlipaySubmit
    {
        #region 字段
        //支付宝网关地址（新）
        //The Alipay gateway provided to merchants
        //沙箱网关The Alipay gateway of sandbox environment.		
        private static string GATEWAY_NEW = "https://mapi.alipaydev.com/gateway.do?";
        //生产环境网关，如果商户用的生产环境请换成下面的正式网关
        //The Alipay gateway of production environment.(pls use the below line instead if you were in production environment)
        //private static string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";
        //商户的私钥
        private static string _private_key = "";
        //编码格式
        //charset
        private static string _input_charset = "";
        //签名方式
        private static string _sign_type = "RSA";
        #endregion
        static AlipaySubmit()
        {
            _private_key = AlipayConfig.private_key;
            _input_charset = AlipayConfig.input_charset.Trim().ToLower();
            _sign_type = AlipayConfig.sign_type.Trim().ToUpper();
        }

        /// <summary>
        /// Generate the sign
        /// </summary>
        /// <param name="sPara"></param>
        /// <returns></returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            //Rearrange parameters in the data set alphabetically and connect rearranged parameters with & like "parametername=value"
            string prestr = AlipayCore.SetQueryString(sPara);

            //把最终的字符串签名，获得签名结果
            //get the sign
            string mysign = "";
            switch (_sign_type)
            {
                case "RSA":
                    mysign = RSAFromPkcs8.sign(prestr, _private_key, _input_charset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }
        /// <summary>
        /// Generate a set of parameters need in the request of Alipay
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <returns></returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            //params to be signed
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            //sign generated
            string mysign = "";

            //过滤签名参数数组
            //filter the parameters
            sPara = AlipayCore.FilterParams(sParaTemp);

            //获得签名结果
            //Generate the sign
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            //Add the sign and sign_type into the sPara
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", _sign_type);

            return sPara;
        }

        /// <summary>
        /// Generate a set of parameters need in the request of Alipay
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            //params to be signed
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            //connect the parameters with "&" like "parameter=value",and do urlencode to the value
            string strRequestData = AlipayCore.SetQueryStringUrlencode(sPara, code);

            return strRequestData;
        }

        /// <summary>
        /// Build the request,costruct in the format of HTML form
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <param name="strMethod"></param>
        /// <param name="strButtonValue"></param>
        /// <returns></returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组pre-request params

            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + GATEWAY_NEW + "_input_charset=" + _input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            //Pls don't set name attribute for the submit button 
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// Used to anti-phishing，use interface "query_timestamp" to get the function to get the timestamp
        /// </summary>
        /// <returns></returns>
        public static string Query_timestamp()
        {
            string url = GATEWAY_NEW + "service=query_timestamp&partner=" + AlipayConfig.partner + "&_input_charset=" + AlipayConfig.input_charset;
            string encrypt_key = "";

            XmlTextReader Reader = new XmlTextReader(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Reader);

            encrypt_key = xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;

            return encrypt_key;
        }
    }
}
