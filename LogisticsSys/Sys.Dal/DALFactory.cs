using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal.Repository;
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
        private static readonly ISysActionLogDao _ActionLogDao=new SysActionLogDao();

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

    }

}
