using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL
{
    public  class GoodsTypeService
    {
        public static object _obj = new object();
        private readonly ISysGoodsTypeRepository _dao = DALFactory.SysGoodsTypeDao;

        public static GoodsTypeService Current { get; }

        static GoodsTypeService()
        {
            if (Current == null)
            {
                lock (_obj)
                {
                    Current = new GoodsTypeService();
                }
            }
        }

        public int AddGoodsType(SysGoodsType entity)
        {
            return _dao.Insert(entity);
        }

        public int UpdateGoodsType(SysGoodsType entity)
        {
            return _dao.Update(entity);
        }

        public int DeleteGoodsType(SysGoodsType entity)
        {
            return _dao.Delete(entity);
        }

        public int GetPagerCount(string search)
        {
            return _dao.GetPagerCount(search);
        }

        public List<SysGoodsType> GetPagerDataList(string search, int offset, int limit, string order, string sort)
        {
            return _dao.GetPagerList(search, offset, limit, order, sort).ToList();
        }
    }
}
