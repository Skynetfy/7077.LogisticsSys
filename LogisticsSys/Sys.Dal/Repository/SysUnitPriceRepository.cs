using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public class SysUnitPriceRepository : ISysUnitPriceRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public long Insert(SysUnitPrice entity)
        {
            try
            {
                Object result = baseDao.Insert<SysUnitPrice>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysUnitPrice entity)
        {
            try
            {
                Object result = baseDao.Update<SysUnitPrice>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysUnitPrice entity)
        {
            try
            {
                Object result = baseDao.Delete<SysUnitPrice>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysUnitPrice FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysUnitPrice>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysUnitPrice> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysUnitPrice>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }

        public long Count()
        {
            try
            {
                String sql = "SELECT count(1) from SysUnitPrice  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysUnitPrice> list)
        {
            try
            {
                return baseDao.BulkInsert<SysUnitPrice>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }
        public int GetPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from v_UnitPrice  with (nolock) where 1=1 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysUnitPrice> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from v_UnitPrice(nolock) where Id not in(
                  SELECT TOP {4} Id FROM v_UnitPrice(NOLOCK)
                  WHERE 1=1 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                var dt = baseDao.SelectDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.AsEnumerable().Select(x => new SysUnitPrice()
                    {
                        Id = x["Id"] == DBNull.Value ? 0 : x.Field<long>("Id"),
                        CityName = x.Field<string>("CityName"),
                        RCityId = x["Cid"] ==DBNull.Value?0:(int)x.Field<long>("Cid"),
                        GoodsType = x.Field<string>("GoodsTypeName"),
                        GoodsTypeId = x["Gid"] ==DBNull.Value?0:x.Field<long>("Gid"),
                        LandPrice1=x["LandPrice1"]==DBNull.Value?0:x.Field<decimal>("LandPrice1"),
                        LandPrice2=x["LandPrice2"]==DBNull.Value?0:x.Field<decimal>("LandPrice2"),
                        AirPrice1=x["AirPrice1"]==DBNull.Value?0:x.Field<decimal>("AirPrice1"),
                        AirPrice2=x["AirPrice2"]==DBNull.Value?0:x.Field<decimal>("AirPrice2"),
                        CreateDate = x["CreateDate"]==DBNull.Value?DateTime.MinValue: x.Field<DateTime>("CreateDate"),
                        IsDelete = x["IsDelete"]==DBNull.Value?false:x.Field<bool>("IsDelete")
                    }).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }
    }
}
