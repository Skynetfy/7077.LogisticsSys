using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public class RussiaCityService
    {
        private static object _obj = new object();
        private readonly ISysRussiaCityRepository russiaCityRepository = DALFactory.SysRussiaCityDao;
        public static RussiaCityService Current { get; }

        static RussiaCityService()
        {
            if (Current == null)
            {
                lock (_obj)
                {
                    Current = new RussiaCityService();
                }
            }
        }

        public int AddRussiaCity(SysRussiaCity entity)
        {
            return russiaCityRepository.Insert(entity);
        }

        public int UpdateRussiaCity(SysRussiaCity entity)
        {
            return russiaCityRepository.Update(entity);
        }

        public int DeleteRussiaCity(SysRussiaCity entity)
        {
            return russiaCityRepository.Delete(entity);
        }

        public int GetPagerCount(string search)
        {
            return russiaCityRepository.GetPagerCount(search);
        }

        public List<SysRussiaCity> GetPagerDataList(string search, int offset, int limit, string order, string sort)
        {
            return russiaCityRepository.GetPagerList(search, offset, limit, order, sort).ToList();
        }
    }
}
