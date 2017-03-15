using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Common;
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

        public long AddRussiaCity(SysRussiaCity entity)
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

        public string GetCityName(long id)
        {
            return russiaCityRepository.FindByPk(id).CityName;
        }

        public List<SelectBinding> GetBindings(long id)
        {
            return russiaCityRepository.GetAll().Where(x => !x.IsDelete).Select(x => new SelectBinding()
            {
                Value = x.Id.ToString(),
                Text = x.CityName,
                Selected = x.Id == id
            }).ToList();
        }
    }
}
