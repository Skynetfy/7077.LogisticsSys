﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public interface ISysOrderPayInfoRepository : IBaseRepository<SysOrderPayInfo>
    {
        IList<SysOrderPayInfo> GetList(long oid);
    }
}
