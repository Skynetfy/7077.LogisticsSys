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

        public static int UpdateUser(SysUser entity)
        {
            return userDao.Update(entity);
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
        public static void UpdateIntegral(SysOrderInfo order, int value)
        {
            if (order != null)
            {
                order.Integral = value;
                DALFactory.SysOrderInfoDao.Update(order);
            }
        }
        public static void AdminUpdateIntegral(long uid, int value, int type, string msg)
        {
            var customer = customerDao.FindByUid(uid);
            if (customer != null)
            {
                customer.Integral = value;
                customer.CreateDate = DateTime.Now;
                if (customerDao.Update(customer) > 0)
                {
                    var log = new SysIntegralLog();
                    log.Type = type;
                    log.Uid = uid;
                    log.Value = value;
                    log.Desc = msg;
                    log.CreateDate = DateTime.Now;
                    log.IsDelete = false;
                    DALFactory.IntegralLogDao.Insert(log);
                }
            }
        }
        public static void UpdateCustomerIntegral(long oid, long uid, int upoints)
        {
            var customer = customerDao.FindByUid(uid);
            if (customer != null)
            {
                var paylogs = DALFactory.OrderPayInfoDao.GetList(oid);
                var sumIn = paylogs.Sum(x => x.PayAmount);
                if (upoints > 0)
                {
                    customer.Integral -= upoints;
                    var log1 = new SysIntegralLog();
                    log1.Type = -1;
                    log1.Uid = uid;
                    log1.Value = -upoints;
                    log1.Desc = "积分抵重量";
                    log1.CreateDate = DateTime.Now;
                    log1.IsDelete = false;
                    DALFactory.IntegralLogDao.Insert(log1);
                }

                customer.Integral += (int)Math.Floor(sumIn);
                customer.CreateDate = DateTime.Now;
                if (customerDao.Update(customer) > 0)
                {
                    var log = new SysIntegralLog();
                    log.Type = 1;
                    log.Uid = uid;
                    log.Value = (int)sumIn;
                    log.Desc = "订单付款赠送";
                    log.CreateDate = DateTime.Now;
                    log.IsDelete = false;
                    DALFactory.IntegralLogDao.Insert(log);
                }
            }
        }
    }
}
