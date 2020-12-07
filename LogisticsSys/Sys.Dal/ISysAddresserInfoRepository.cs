using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysAddresserInfoRepository : IBaseRepository<SysAddresserInfo>
    {
        SysAddresserInfo GetByOrderId(long id);

        int UpdateAddresser(SysAddresserInfo entity);

        int UpdateAddresserByOrderNo(string orderNo, decimal goodWeight, decimal orderFrees);

        int UpdateSysReceiverInfoByOrderNo(string orderNo, string number);
    }
}
