using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public class LogisticsService
    {
        private static object _obj = new object();
        private readonly ISysLogisticsInfoRepository logisticsInfoRepository = DALFactory.SysLogisticsInfoDao;
        public static LogisticsService Current { get; }

        static LogisticsService()
        {
            if (Current == null)
            {
                lock (_obj)
                {
                    Current = new LogisticsService();
                }
            }
        }

        public long AddLogistics(SysLogisticsInfo entity)
        {
            return logisticsInfoRepository.Insert(entity);
        }

        public List<SysLogisticsInfo> GetLogisticsPagerData(string search, int offset, int limit, string order, string sort)
        {
            return logisticsInfoRepository.GetPagerList(search, offset, limit, order, sort).ToList();
        }

        public int GetLogisticsPagerCount(string search)
        {
            return logisticsInfoRepository.GetPagerCount(search);
        }

        public IList<SysLogisticsInfo> GetLogisticsInfoList(string single)
        {
            return logisticsInfoRepository.GetLogisticsInfoList(single);
        }
    }
}
