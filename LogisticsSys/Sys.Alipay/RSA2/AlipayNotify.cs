using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sys.RSA2.Alipay
{
    public class AlipayNotify
    {
        #region 字段
        private string _partner = "";               //合作身份者ID
        private string alipay_public_key = "";            //支付宝的公钥
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式

        //支付宝消息验证地址
        //The URL of verification of Alipay notification.
        //沙箱网关异步消息验证地址
        //The verification URL of Alipay notification,sandbox environment.
        private string Https_veryfy_url = "https://openapi.alipaydev.com/gateway.do?service=notify_verify&";
        //线上网关异步消息验证地址，如商户使用的生产环境，请换成下面的生产环境的地址
        //The verification URL of Alipay notification,production environment.(pls use the below line instead if you were in production environment)	
        //private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";
        #endregion

        public AlipayNotify()
        {
            //初始化基础配置信息
            //Initialize the basic configuration information
            _partner = AlipayConfig.partner.Trim();
            alipay_public_key = getPublicKeyStr(AlipayConfig.alipay_public_key.Trim());
            _input_charset = AlipayConfig.input_charset.Trim().ToLower();
            _sign_type = AlipayConfig.sign_type.Trim().ToUpper();
        }

        /// <summary>
        /// Get public key from file and transform to a string
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string getPublicKeyStr(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        public bool VerifyReturn(SortedDictionary<string, string> inputPara, string sign)
        {
            //获取返回时的签名验证结果
            //get the verification result of the return
            bool isSign = GetSignVeryfy(inputPara, sign);
            return isSign;
        }

        /// <summary>
        /// Verify whether it's a legal notification sent from Alipay
        /// </summary>
        /// <param name="inputPara"></param>
        /// <param name="notify_id"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
        {
            //获取返回时的签名验证结果
            //get the result of verification
            bool isSign = GetSignVeryfy(inputPara, sign);
            //获取是否是支付宝服务器发来的请求的验证结果
            //check whether the notification is from Alipay
            string responseTxt = "false";
            if (notify_id != null && notify_id != "") { responseTxt = GetResponseTxt(notify_id); }

            //写日志记录（若要调试，请取消下面两行注释）
            //write the log(pls uncomment the below two lines if you would like to debug)
            //string sWord = "responseTxt=" + responseTxt + "\n isSign=" + isSign.ToString() + "\n 返回回来的参数：" + GetPreSignStr(inputPara) + "\n ";
            //Core.LogResult(sWord);

            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            //judgue whether responsetTxt and isSign is true
            //if responsetTxt is not true,the cause might be related to sever setting,merchant account and expiration time of notify_id(one minute).
            //if isSign is not true，the cause might be related to sign,charset and format of request str(eg:request with custom parameter etc.) 
            if (responseTxt == "true" && isSign)//验证成功verification succeed
            {
                return true;
            }
            else//验证失败verification failed
            {
                return false;
            }
        }

        /// <summary>
        /// get the pre-sign string (for debug)
        /// </summary>
        /// <param name="inputPara"></param>
        /// <returns></returns>
        private string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            //filter the blank,sign and sign_type
            sPara = AlipayCore.FilterParams(inputPara);

            //获取待签名字符串 get the pre-sign string
            string preSignStr = AlipayCore.SetQueryString(sPara);

            return preSignStr;
        }
        /// <summary>
        /// get the result of verification of returned notification
        /// </summary>
        /// <param name="inputPara"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            //Filter parameters with null value ,sign and sign_type
            sPara = AlipayCore.FilterParams(inputPara);

            //获取待签名字符串
            //Generate the pre-sign string
            string preSignStr = AlipayCore.SetQueryString(sPara);

            //获得签名验证结果
            //get the result of verification
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                switch (_sign_type)
                {
                    case "RSA":
                        isSgin = RSAFromPkcs8.verify(preSignStr, sign, alipay_public_key, _input_charset);
                        break;
                    default:
                        break;
                }
            }

            return isSgin;
        }
        private string GetResponseTxt(string notify_id)
        {
            string veryfy_url = Https_veryfy_url + "partner=" + _partner + "&notify_id=" + notify_id;

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            //Get the remote server ATN result,verify whether it's from Alipay
            string responseTxt = Get_Http(veryfy_url, 120000);

            return responseTxt;
        }

        /// <summary>
        /// Get the remote server ATN result
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误error：" + exp.Message;
            }

            return strResult;
        }
    }
}
