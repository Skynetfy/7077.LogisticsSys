using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysReceiverInfoRepository : IBaseRepository<SysReceiverInfo>
    {
        IList<SysReceiverInfo> GetByOrderId(long id);
    }
}
