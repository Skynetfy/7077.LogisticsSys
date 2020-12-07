using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sys.Alipay.MD5
{
    public class AlipayCore
    {
        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
		/// Remove the blank ,sign and sign_type,and rearrange alphabetical from a to z
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组the array to be filter</param>
        /// <returns>过滤后的参数组Array filterred</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && temp.Value != "" && temp.Value != null)
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
		/// connect all parameters with & like "parameter name=value"
        /// </summary>
        /// <param name="sArray">需要拼接的数组parameters need to be connected </param>
        /// <returns>拼接完成以后的字符串pre-sign string</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符remove the last &
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
		/// connect all parameters with & like "parameter name=value",and do urlencode to the value of parameters
        /// </summary>
        /// <param name="sArray">需要拼接的数组parameters need to be connected</param>
        /// <param name="code">字符编码charset</param>
        /// <returns>拼接完成以后的字符串String connected</returns>
        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, code) + "&");
            }

            //去掉最後一個&字符
            //remove the last &
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 写日志，方便测试（看网站需求，也可以改成把记录存入数据库）
        /// Write the log for your convienence in testing(You can also load these record into database ,it depends on your requirement)
        /// <param name="sWord">要写入日志里的文本内容The string to be written in your log</param>
        public static void LogResult(string sWord)
        {
            string strPath = AlipayConfig.log_path + "\\" + "alipay_log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            StreamWriter fs = new StreamWriter(strPath, false, System.Text.Encoding.Default);
            fs.Write(sWord);
            fs.Close();
        }

        /// <summary>
        /// 获取文件的md5摘要generate md5 summary of file
        /// </summary>
        /// <param name="sFile">文件流 file stream</param>
        /// <returns>MD5摘要结果summary result of MD5</returns>
        public static string GetAbstractToMD5(Stream sFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(sFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件的md5摘要
		/// 获取文件的md5摘要generate md5 summary of file
        /// </summary>
        /// <param name="dataFile">文件流file stream</param>
        /// <returns>MD5摘要结果summary result of MD5</returns>
        public static string GetAbstractToMD5(byte[] dataFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(dataFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
