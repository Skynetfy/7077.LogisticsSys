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

    public class ChinaCityService
    {
        public static object _obj = new object();
        private readonly ISysChinaCityRepository _dao = DALFactory.SysChinaCityDao;

        public static ChinaCityService Current { get; }

        static ChinaCityService()
        {
            if (Current == null)
            {
                lock (_obj)
                {
                    Current = new ChinaCityService();
                }
            }
        }

        public int AddChinaCity(SysChinaCity entity)
        {
            return _dao.Insert(entity);
        }

        public int UpdateChinaCity(SysChinaCity entity)
        {
            return _dao.Update(entity);
        }

        public int DeleteChinaCity(SysChinaCity entity)
        {
            return _dao.Delete(entity);
        }

        public int GetPagerCount(string search)
        {
            return _dao.GetPagerCount(search);
        }

        public List<SysChinaCity> GetPagerDataList(string search, int offset, int limit, string order, string sort)
        {
            return _dao.GetPagerList(search, offset, limit, order, sort).ToList();
        }

        public List<SelectBinding> GetChinaCities(int id)
        {
            return _dao.GetAll().Where(x => !x.IsDelete).Select(x => new SelectBinding()
            {
                Value = x.Id.ToString(),
                Text = x.CityName,
                Selected = x.Id == id
            }).ToList();
        }
    }
}
