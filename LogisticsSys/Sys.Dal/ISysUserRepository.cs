﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface ISysUserRepository : IBaseRepository<SysUser>
    {
        SysUser GetUser(string username);

        SysUser GetUser(string username, string password);
    }
}
