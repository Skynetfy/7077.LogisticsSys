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
            SysReceiverInfo receiverinfo,ref int status)
        {
            return orderDao.AddOrderInfo(username, orderinfo, addresserinfo, receiverinfo, ref status);
        }
    }
}
