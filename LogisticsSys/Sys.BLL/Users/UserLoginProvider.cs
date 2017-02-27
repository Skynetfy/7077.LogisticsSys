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

        public int DeleteUser(SysUser user)
        {
            return userDao.Delete(user);
        }
        public int UpdateUser(SysUser user)
        {
            return userDao.Update(user);
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
        public Dictionary<string, Dictionary<string,string>> GetMenusByUserName(string username)
        {
            var dic = new Dictionary<string, Dictionary<string, string>>();
            var user = GetUser(username);
            if (user != null && user.RuleType != RuleTypeEnum.None.ToString())
            {
                if (user.RuleType.Equals(RuleTypeEnum.Admin.ToString()))
                {
                    dic.Add(MenusEnum.主页.ToString()+"|" + MenusEnum.主页.GetDescription(), null);

                    var dic1=new Dictionary<string,string>();
                    dic1.Add(MenusEnum.创建订单.ToString(), MenusEnum.创建订单.GetDescription());
                    dic1.Add(MenusEnum.订单列表.ToString(), MenusEnum.订单列表.GetDescription());
                    dic1.Add(MenusEnum.订单处理.ToString(), MenusEnum.订单处理.GetDescription());
                    dic.Add(MenusEnum.订单管理.ToString() + "|" + MenusEnum.订单管理.GetDescription(), dic1);
                    
                    var dic2=new Dictionary<string,string>();
                    dic2.Add(MenusEnum.物流查询.ToString(), MenusEnum.物流查询.GetDescription());
                    dic2.Add(MenusEnum.物流进度更新.ToString(), MenusEnum.物流进度更新.GetDescription());
                    dic2.Add(MenusEnum.发货管理.ToString(), MenusEnum.发货管理.GetDescription());
                    dic.Add(MenusEnum.物流管理.ToString() + "|" + MenusEnum.物流管理.GetDescription(), dic2);

                    var dic3 = new Dictionary<string, string>();
                    dic3.Add(MenusEnum.俄罗斯城市管理.ToString(), MenusEnum.俄罗斯城市管理.GetDescription());
                    dic3.Add(MenusEnum.国内城市管理.ToString(), MenusEnum.国内城市管理.GetDescription());
                    dic3.Add(MenusEnum.货物类型维护.ToString(), MenusEnum.货物类型维护.GetDescription());
                    dic.Add(MenusEnum.系统维护.ToString() + "|" + MenusEnum.系统维护.GetDescription(), dic3);

                    var dic4 = new Dictionary<string, string>();
                    dic4.Add(MenusEnum.个人信息.ToString(), MenusEnum.个人信息.GetDescription());
                    dic4.Add(MenusEnum.用户管理.ToString(), MenusEnum.用户管理.GetDescription());
                    dic4.Add(MenusEnum.密码修改.ToString(), MenusEnum.密码修改.GetDescription());
                    dic.Add(MenusEnum.个人中心.ToString()+"|"+ MenusEnum.个人中心.GetDescription(), dic4);
                }
                else if (user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    dic.Add(MenusEnum.主页.ToString(), null);

                    var dic1 = new Dictionary<string, string>();
                    dic1.Add(MenusEnum.创建订单.ToString(), MenusEnum.创建订单.GetDescription());
                    dic1.Add(MenusEnum.订单列表.ToString(), MenusEnum.订单列表.GetDescription());
                    dic.Add(MenusEnum.订单管理.ToString() + "|" + MenusEnum.订单管理.GetDescription(), dic1);

                    var dic2 = new Dictionary<string, string>();
                    dic2.Add(MenusEnum.物流查询.ToString(), MenusEnum.物流查询.GetDescription());
                    dic.Add(MenusEnum.物流管理.ToString() + "|" + MenusEnum.物流管理.GetDescription(), dic2);
                    
                    var dic4 = new Dictionary<string, string>();
                    dic4.Add(MenusEnum.个人信息.ToString(), MenusEnum.个人信息.GetDescription());
                    dic4.Add(MenusEnum.密码修改.ToString(), MenusEnum.密码修改.GetDescription());
                    dic.Add(MenusEnum.个人中心.ToString() + "|" + MenusEnum.个人中心.GetDescription(), dic4);
                }
                else if (user.RuleType.Equals(RuleTypeEnum.Agents.ToString()))
                {
                    dic.Add(MenusEnum.主页.ToString(), null);

                    var dic1 = new Dictionary<string, string>();
                    dic1.Add(MenusEnum.订单处理.ToString(), MenusEnum.订单处理.GetDescription());
                    dic.Add(MenusEnum.订单管理.ToString() + "|" + MenusEnum.订单管理.GetDescription(), dic1);

                    var dic2 = new Dictionary<string, string>();
                    dic2.Add(MenusEnum.物流查询.ToString(), MenusEnum.物流查询.GetDescription());
                    dic2.Add(MenusEnum.物流进度更新.ToString(), MenusEnum.物流进度更新.GetDescription());
                    dic2.Add(MenusEnum.发货管理.ToString(), MenusEnum.发货管理.GetDescription());
                    dic.Add(MenusEnum.物流管理.ToString() + "|" + MenusEnum.物流管理.GetDescription(), dic2);

                    var dic4 = new Dictionary<string, string>();
                    dic4.Add(MenusEnum.个人信息.ToString(), MenusEnum.个人信息.GetDescription());
                    dic4.Add(MenusEnum.密码修改.ToString(), MenusEnum.密码修改.GetDescription());
                    dic.Add(MenusEnum.个人中心.ToString() + "|" + MenusEnum.个人中心.GetDescription(), dic4);
                }
            }
            return dic;
        }

        public int GetPagerCount(string search)
        {
            return userDao.GetPagerCount(search);
        }

        public List<SysUser> GetPagerDataList(string search, int offset, int limit, string order, string sort)
        {
            return userDao.GetPagerList(search, offset, limit, order, sort).ToList();
        }
    }
}
