using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysReceiverInfoRepository : IBaseRepository<SysReceiverInfo>
    {
        SysReceiverInfo GetByOrderId(long id);

        int UpdateReciverInfo(SysReceiverInfo entity);

        SysReceiverInfo GetByCourierNo(string no);

    }
}
