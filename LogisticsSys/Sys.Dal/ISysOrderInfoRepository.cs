using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysOrderInfoRepository : IBaseRepository<SysOrderInfo>
    {
        string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo,
            SysReceiverInfo receiverinfo,ref int status);

        int GetOrderViewPagerCount(string search);

        IList<OrderView> GetOrderViewPagerList(string search, int offset, int limit, string order, string sort);
    }
}
