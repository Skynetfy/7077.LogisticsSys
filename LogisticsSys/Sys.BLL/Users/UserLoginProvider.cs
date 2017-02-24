using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Common;
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

        public SysUser GetUser(string username, string password)
        {
            return userDao.GetUser(username, password);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetMenusByUserName(string username)
        {
            var dic = new Dictionary<string, string>();
            var user = GetUser(username);
            if (user != null && user.RuleType != RuleTypeEnum.None.ToString())
            {
                if (user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    foreach (MenusEnum item in Enum.GetValues(typeof(MenusEnum)))
                    {
                        string strVaule = item.GetDescription();//获取名称
                        string strName = item.ToString();//获取值
                        dic.Add(strName, strVaule);
                    }
                }
                else if (user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    dic.Add(MenusEnum.主页.ToString(), MenusEnum.主页.GetDescription());
                    dic.Add(MenusEnum.订单管理.ToString(), MenusEnum.个人中心.GetDescription());
                    dic.Add(MenusEnum.个人中心.ToString(), MenusEnum.个人中心.GetDescription());
                }
                else if (user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {

                }
            }
            return dic;
        }
    }
}
