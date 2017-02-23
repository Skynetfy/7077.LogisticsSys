using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL.Users
{
    public class UserLoginProvider
    {
        private ISysUserRepository userDao = DALFactory.SysUserDao;

        public int InsertUser(SysUser user)
        {
            return userDao.Insert(user);
        }

        public bool CheckUserName(string name)
        {
            return GetUser(name) != null;
        }

        public SysUser GetUser(string username)
        {
            return userDao.GetUser(username);
        }

        public SysUser GetUser(string username,string password)
        {
            return userDao.GetUser(username, password);
        }
    }
}
