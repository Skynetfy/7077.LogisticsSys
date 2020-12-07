using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal.Repository;
using Arch.Data;

namespace Sys.Dal
{
    public partial class DALFactory
    {
        private static readonly ISysUserRepository _sysUserDao = new SysUserRepository();
        private static readonly ISysAddresserInfoRepository _sysaddresserDao = new SysAddresserInfoRepository();
        private static readonly ISysAgentCityMappingRepository _sysAgentCityMappingDao = new SysAgentCityMappingRepository();
        private static readonly ISysChinaCityRepository _sysChinaCityDao = new SysChinaCityRepository();
        private static readonly ISysGoodsTypeRepository _sysGoodsTypeDao = new SysGoodsTypeRepository();
        private static readonly ISysLogisticsInfoRepository _sysLogisticsDao = new SysLogisticsInfoRepository();
        private static readonly ISysOrderInfoRepository _sysOrderInfoDao = new SysOrderInfoRepository();
        private static readonly ISysReceiverInfoRepository _sysReceiverInfoDao = new SysReceiverInfoRepository();
        private static readonly ISysRussiaCityRepository _sysRussiaCityDao = new SysRussiaCityRepository();
        private static readonly ISysUnitPriceRepository _sysUnitPriceDao = new SysUnitPriceRepository();
        private static readonly ISysAgentInfoDao _SysAgentInfoDao = new SysAgentInfoDao();
        private static readonly ISysCustomerInfoDao _CustomerInfoDao = new SysCustomerInfoDao();
        private static readonly ISysActionLogDao _ActionLogDao = new SysActionLogDao();
        private static readonly ISysKuaiDiComRepository _KuaiDiComDao = new SysKuaiDiComRepository();

        private static readonly ISysOrderNumberRepository _OrderNumberDao = new SysOrderNumberRepository();
        private static readonly ISysOrderPayInfoRepository _orderpayDao = new SysOrderPayInfoRepository();
        private static readonly ISysExchangeRepository _exchangeDao = new SysExchangeRepository();

        private static readonly ISysDbConfigRepository _dbconfigDao = new SysDbConfigRepository();
        private static readonly ISysIntegralLogRepository _DbIntegralLogDao = new SysIntegralLogRepository();
        private static readonly ISysExportDataInfoRepository _DbSysExportDataInfoDao = new SysExportDataInfoRepository();
        private static readonly ISysOrderTradeRepository _DbSysOrderTradeRepository = new SysOrderTradeRepository();

        public static ISysIntegralLogRepository IntegralLogDao { get { return _DbIntegralLogDao; } }
        public static ISysDbConfigRepository DbconfigDao
        {
            get { return _dbconfigDao; }
        }

        public static string SqlAction(string sql)
        {
            try
            {
                BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");
                baseDao.ExecNonQuery(sql);
                return "执行成功";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static ISysOrderTradeRepository OrderTradeRepository { get { return _DbSysOrderTradeRepository; } }
        public static ISysOrderPayInfoRepository OrderPayInfoDao { get { return _orderpayDao; } }
        public static ISysOrderNumberRepository OrderNumberDao { get { return _OrderNumberDao; } }
        public static ISysExchangeRepository ExchangeDao { get { return _exchangeDao; } }
        public static ISysKuaiDiComRepository KuaiDiDao
        {
            get { return _KuaiDiComDao; }
        }
        public static ISysActionLogDao ActionLogDao
        {
            get { return _ActionLogDao; }
        }

        public static ISysCustomerInfoDao CustomerInfoDao
        {
            get { return _CustomerInfoDao; }
        }

        public static ISysAgentInfoDao SysAgentInfoDao
        {
            get { return _SysAgentInfoDao; }
        }

        public static ISysUserRepository SysUserDao
        {
            get
            {
                return _sysUserDao;
            }
        }
        public static ISysAddresserInfoRepository SysAddresserInfoDao
        {
            get
            {
                return _sysaddresserDao;
            }
        }
        public static ISysAgentCityMappingRepository SysAgentCityMappingDao
        {
            get
            {
                return _sysAgentCityMappingDao;
            }
        }
        public static ISysChinaCityRepository SysChinaCityDao
        {
            get
            {
                return _sysChinaCityDao;
            }
        }
        public static ISysGoodsTypeRepository SysGoodsTypeDao
        {
            get
            {
                return _sysGoodsTypeDao;
            }
        }
        public static ISysLogisticsInfoRepository SysLogisticsInfoDao
        {
            get
            {
                return _sysLogisticsDao;
            }
        }
        public static ISysOrderInfoRepository SysOrderInfoDao
        {
            get
            {
                return _sysOrderInfoDao;
            }
        }
        public static ISysReceiverInfoRepository SysReceiverInfoDao
        {
            get
            {
                return _sysReceiverInfoDao;
            }
        }
        public static ISysRussiaCityRepository SysRussiaCityDao
        {
            get
            {
                return _sysRussiaCityDao;
            }
        }
        public static ISysUnitPriceRepository SysUnitPriceDao
        {
            get
            {
                return _sysUnitPriceDao;
            }
        }
        public static ISysExportDataInfoRepository SysExportDataInfoRepository
        {
            get
            {
                return _DbSysExportDataInfoDao;
            }
        }
    }

}
