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
        private readonly ISysAddresserInfoRepository addresserInfoDao = DALFactory.SysAddresserInfoDao;
        private readonly ISysReceiverInfoRepository receiverInfoDao = DALFactory.SysReceiverInfoDao;

        public SysAddresserInfo GetAddressInfoByOid(long id)
        {
            return addresserInfoDao.GetByOrderId(id);
        }

        public IList<SysReceiverInfo> GetReceiverInfo(long id)
        {
            return receiverInfoDao.GetByOrderId(id);
        }

        public int UpdateAddressInfo(SysAddresserInfo entity)
        {
            return addresserInfoDao.Update(entity);
        }

        public int UpdateReceiverInfo(SysReceiverInfo entity)
        {
            return receiverInfoDao.Update(entity);
        }

        public SysOrderInfo GetOrderInfoById(long id)
        {
            return orderDao.FindByPk(id);
        }

        public int UpdateOrderInfo(SysOrderInfo entity)
        {
            return orderDao.Update(entity);
        }

        public string GetOrderNumber()
        {
            var neworder = OrderNumberRandom.GetOrderNumber();
            if (orderDao.GetOrderByNumber(neworder) != null)
            {
                neworder = GetOrderNumber();
            }
            return neworder;
        }

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo,List<SysReceiverInfo> receiverInfos, ref int status)
        {
            return orderDao.AddOrderInfo(username, orderinfo, addresserinfo, receiverInfos, ref status);
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
