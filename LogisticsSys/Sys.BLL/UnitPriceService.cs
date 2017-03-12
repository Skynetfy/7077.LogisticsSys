using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public class UnitPriceService
    {
        public static object _obj = new object();
        private readonly ISysUnitPriceRepository _dao = DALFactory.SysUnitPriceDao;

        public static UnitPriceService Current { get; }

        static UnitPriceService()
        {
            if (Current == null)
            {
                lock (_obj)
                {
                    Current = new UnitPriceService();
                }
            }
        }

        public long AddUnitPrice(SysUnitPrice entity)
        {
            return _dao.Insert(entity);
        }

        public int UpdateUnitPrice(SysUnitPrice entity)
        {
            return _dao.Update(entity);
        }

        public int DeleteUnitPrice(SysUnitPrice entity)
        {
            return _dao.Delete(entity);
        }

        public int GetPagerCount(string search)
        {
            return _dao.GetPagerCount(search);
        }

        public List<SysUnitPrice> GetPagerDataList(string search, int offset, int limit, string order, string sort)
        {
            return _dao.GetPagerList(search, offset, limit, order, sort).ToList();
        }
    }
}
