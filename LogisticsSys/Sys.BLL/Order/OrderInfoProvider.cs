using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL.Order
{

    public class OrderInfoProvider
    {
        private readonly ISysOrderInfoRepository orderDao = DALFactory.SysOrderInfoDao;

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo,
            SysReceiverInfo receiverinfo, ref int status)
        {
            return orderDao.AddOrderInfo(username, orderinfo, addresserinfo, receiverinfo, ref status);
        }

        public int GetOrderViewPagerCount(string search)
        {
            return orderDao.GetOrderViewPagerCount(search);
        }

        public List<OrderView> GetOrderViewPagerList(string search, int offset, int limit, string order, string sort)
        {
            return orderDao.GetOrderViewPagerList(search, offset, limit, order, sort).ToList();
        }
    }
}
