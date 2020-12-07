using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sys.Common
{
    public class GookingManager
    {
        public static decimal GetGookingRate(string fromCode, string toCode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.it120.cc/gooking/forex/rate?fromCode={fromCode}&toCode={toCode}");
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }

            var obj = JsonConvert.DeserializeObject<dynamic>(retString);
            return (decimal)obj.data.rate;
        }
    }
}
