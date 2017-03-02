using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL.Order
{
    public class OrderNumberRandom
    {
        public static object _lock = new object();
        public static int count = 1;

        public static string GetOrderNumber()
        {
            lock (_lock)
            {
                if (count > 10000)
                {
                    count = 1;
                }
                var number = "T" + DateTime.Now.ToString("yyMMddHHmmss") + count.ToString("0000");
                count++;
                return number;
            }
        }
    }
}
