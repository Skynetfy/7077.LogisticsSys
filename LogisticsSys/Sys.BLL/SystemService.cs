using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;

namespace Sys.BLL
{
    public class SystemService
    {
        public static double GetCurrentExchangeValue()
        {
            var value = DALFactory.ExchangeDao.GetCurrentExchangeValue();
            return value != null ? value.ExchangeValue : 0;
        }
    }
}
