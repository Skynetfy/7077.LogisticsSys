using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Alipay.MD5
{
    public class AlipayMD5
    {
        /// <summary>
        /// 签名字符串sign
        /// </summary>
        /// <param name="prestr">需要签名的字符串pre-sign string</param>
        /// <param name="key">密钥key</param>
        /// <param name="_input_charset">编码格式charset</param>
        /// <returns>签名结果sign generated</returns>
        public static string Sign(string prestr, string key, string _input_charset)
        {
            StringBuilder sb = new StringBuilder(32);

            prestr = prestr + key;

            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 验证签名 get the sign verified
        /// </summary>
        /// <param name="prestr">需要签名的字符串pre-sign string </param>
        /// <param name="sign">签名结果sign</param>
        /// <param name="key">密钥key</param>
        /// <param name="_input_charset">编码格式charset</param>
        /// <returns>验证结果sign generated</returns>
        public static bool Verify(string prestr, string sign, string key, string _input_charset)
        {
            string mysign = Sign(prestr, key, _input_charset);
            if (mysign == sign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
