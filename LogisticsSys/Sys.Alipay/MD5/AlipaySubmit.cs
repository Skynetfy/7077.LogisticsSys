using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sys.Alipay.MD5
{
    public class AlipaySubmit
    {
        #region 字段
        //支付宝网关地址（新）
        //The Alipay gateway provided to merchants
        //沙箱网关The Alipay gateway of sandbox environment.		
        //private static string GATEWAY_NEW = "https://mapi.alipaydev.com/gateway.do?";
        //生产环境网关，如果商户用的生产环境请换成下面的正式网关
        //The Alipay gateway of production environment.(pls use the below line instead if you were in production environment)
        private static string GATEWAY_NEW = ConfigurationManager.AppSettings["alipay_GATEWAY_NEW"];

        //商户的私钥
        //Private key of merchant
        private static string _key = "";
        //编码格式
        //charset
        private static string _input_charset = "";
        //签名方式
        //sign type
        private static string _sign_type = "";
        #endregion

        static AlipaySubmit()
        {
            _key = AlipayConfig.key.Trim();
            _input_charset = AlipayConfig.input_charset.Trim().ToLower();
            _sign_type = AlipayConfig.sign_type.Trim().ToUpper();
        }

        /// <summary>
        /// 生成请求时的签名Generate the sign
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组Parameters to sign</param>
        /// <returns>签名结果sign generated</returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            //Rearrange parameters in the data set alphabetically and connect rearranged parameters with & like "parametername=value"
            string prestr = AlipayCore.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            //get the sign
            string mysign = "";
            switch (_sign_type)
            {
                case "MD5":
                    mysign = AlipayMD5.Sign(prestr, _key, _input_charset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
		/// Generate a set of parameters need in the request of Alipay
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组Pre-sign string</param>
        /// <returns>要请求的参数数组parameters need to be in the request</returns>
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
            sPara = AlipayCore.FilterPara(sParaTemp);

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
        /// 生成要请求给支付宝的参数数组
		/// Generate a set of parameters need in the request of Alipay
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组Pre-sign string</param>
        /// <param name="code">字符编码charset</param>
        /// <returns>要请求的参数数组字符串string need to be in the request</returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            //params to be signed
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            //connect the parameters with "&" like "parameter=value",and do urlencode to the value
            string strRequestData = AlipayCore.CreateLinkStringUrlencode(sPara, code);

            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
		/// Build the request,costruct in the format of HTML form
        /// </summary>
        /// <param name="sParaTemp">请求参数数组the request params</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get  request form.support two types:post and get</param>
        /// <param name="strButtonValue">确认按钮显示文字The text of confirmation button</param>
        /// <returns>提交表单HTML文本the text of requested HTML form</returns>
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
        /// 用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        /// 注意：远程解析XML出错，与IIS服务器配置有关
		///	Used to anti-phishing，use interface "query_timestamp" to get the function to get the timestamp
        /// note：If you get error when parsing XML from remote services,it is related to sever setting like whether the server supports SSL etc.
        /// </summary>
        /// <returns>时间戳字符串String of timestamp</returns>
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
