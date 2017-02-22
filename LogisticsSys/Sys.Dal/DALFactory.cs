using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal.Repository;
namespace Sys.Dal
{
    public partial class DALFactory
    {
        private static readonly ISysUserRepository _sysUserDao = new SysUserRepository();
        public static ISysUserRepository SysUserDao
        {
            get
            {
                return _sysUserDao;
            }
        }
    }

}
