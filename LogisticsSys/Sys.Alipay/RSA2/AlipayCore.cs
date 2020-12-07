using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sys.RSA2.Alipay
{
    public class AlipayCore
    {
        /// <summary>
        /// Remove the blank ,sign and sign_type,and rearrange alphabetical from a to z
        /// </summary>
        /// <param name="dicArrayPre">the array to be filter</param>
        /// <returns></returns>
        public static Dictionary<string, string> FilterParams(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type1" && temp.Value != "" && temp.Value != null)
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// connect all parameters with & like "parameter name=value"
        /// </summary>
        /// <param name="dicArray">parameters need to be connected</param>
        /// <returns></returns>
        public static string SetQueryString(Dictionary<string, string> dicArray)
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
        /// connect all parameters with & like "parameter name=value",and do urlencode to the value of parameters
        /// </summary>
        /// <param name="dicArray">parameters need to be connected</param>
        /// <param name="code">charset</param>
        /// <returns></returns>
        public static string SetQueryStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
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
        /// generate md5 summary of file
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public static string GetAbstractToMD5(Stream sFile)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(sFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件的md5摘要generate md5 summary of file
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public static string GetAbstractToMD5(byte[] dataFile)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
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
