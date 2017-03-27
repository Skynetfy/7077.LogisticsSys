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

        public List<SysLogisticsInfo> GetLogisticsBySingle(string single)
        {
            return logisticsInfoRepository.GetLogisticsInfoList(single).ToList();
        }

        public List<SysLogisticsInfo> GetListBySingle(string single)
        {
            return logisticsInfoRepository.GetLogisticsListBySingle(single).ToList();
        }

        public SysLogisticsInfo GetById(long id)
        {
            return logisticsInfoRepository.FindByPk(id);
        }

        public List<SysLogisticsInfo> GetLogisticsPagerData(string search, int offset, int limit, string order, string sort)
        {
            return logisticsInfoRepository.GetPagerList(search, offset, limit, order, sort).ToList();
        }

        public int GetLogisticsPagerCount(string search)
        {
            return logisticsInfoRepository.GetPagerCount(search);
        }
        public IList<SysLogisticsInfo> GetLogisticsListBySingleGroup(string single)
        {
            return logisticsInfoRepository.GetLogisticsListBySingleGroup(single);
        }
        public IList<SysLogisticsInfo> GetLogisticsInfoList(string single)
        {
            ISysReceiverInfoRepository receiverInfo = DALFactory.SysReceiverInfoDao;
            if (!string.IsNullOrEmpty(single))
            {
                var receiver = receiverInfo.GetByCourierNo(single);
                if (receiver != null)
                {
                    var logics = DALFactory.SysLogisticsInfoDao;
                    return logics.GetLogisticsInfoList(receiver.OrderId.ToString());
                }
            }
            return new List<SysLogisticsInfo>();
            //return logisticsInfoRepository.GetLogisticsInfoList(single);
        }
    }
}
