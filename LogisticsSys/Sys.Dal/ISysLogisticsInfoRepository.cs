using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysLogisticsInfoRepository : IBaseRepository<SysLogisticsInfo>
    {
        IList<SysLogisticsInfo> GetLogisticsInfoList(string single);

        IList<SysLogisticsInfo> GetLogisticsListBySingle(string single);

        IList<SysLogisticsInfo> GetLogisticsListBySingleGroup(string single);

        int Delete(string single);

        int DeleteByNumber(string number);
        IList<string> GetLoginsticsNosByOrderId(long id);
    }
}
