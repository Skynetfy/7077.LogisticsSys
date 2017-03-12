using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public class UserService
    {
        private static ISysAgentInfoDao agentInfoDao = DALFactory.SysAgentInfoDao;
        private static ISysUserRepository userDao = DALFactory.SysUserDao;
        private static ISysCustomerInfoDao customerDao = DALFactory.CustomerInfoDao;

        public static string GetCustomerNo()
        {
            var user = customerDao.GetAll().OrderByDescending(x => x.CreateDate).FirstOrDefault(x => !string.IsNullOrEmpty(x.CustomerID));
            if (user != null)
            {
                var sn = user.CustomerID;
                string no = sn.Substring(7, 5);
                return "RUS7077" + (Convert.ToInt32(no) + 1);
            }
            else
            {
                return "RUS707710001";
            }
        }

        public static SysUser FindByPk(long id)
        {
            return userDao.FindByPk(id);
        }

        public static SysCustomerInfo GetCustomerByUid(long id)
        {
            return customerDao.GetAll().FirstOrDefault(x => !x.IsDelete && x.UserId == id);
        }

        public static long InsertCustomer(SysCustomerInfo entity)
        {
            return customerDao.Insert(entity);
        }

        public static int UpdateCustomer(SysCustomerInfo entity)
        {
            return customerDao.Update(entity);
        }

        public static long InsertAgentInfo(SysAgentInfo entity)
        {
            return agentInfoDao.Insert(entity);
        }

        public static int UpdateAgentInfo(SysAgentInfo entity)
        {
            return agentInfoDao.Update(entity);
        }

        public static int DeleteAgent(SysAgentInfo entity)
        {
            return agentInfoDao.Delete(entity);
        }

        public static SysAgentInfo GetAgentInfoByUserId(long id)
        {
            return agentInfoDao.GetAll().FirstOrDefault(x => !x.IsDelete && x.UserId == id);
        }

        public static List<SysAgentInfo> GetAgentPagerData(string search, int offset, int limit, string order,
            string sort, out int count)
        {
            count = agentInfoDao.GetPagerCount(search);
            return agentInfoDao.GetPagerList(search, offset, limit, order,
                sort).ToList();
        }
    }
}
