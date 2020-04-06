using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public interface ISysOrderNumberRepository : IBaseRepository<SysOrderNumber>
    {
        SysOrderNumber FindBySingle(string single);


        IList<SysOrderNumber> GetByOrderid(long orderid);
    }
}
