using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL
{
    public class WuliuGenZongInfo
    {
        public string Single { get; set; }

        public List<WuliuGenZongOrderNos> Orders { get; set; }

        public WuliuGenZongInfo()
        {
            Orders = new List<WuliuGenZongOrderNos>();
        }
    }

    public class WuliuGenZongOrderNos
    {
        public string OrderNumber { get; set; }
        public List<string> LoginsNo { get; set; }
    }
}
