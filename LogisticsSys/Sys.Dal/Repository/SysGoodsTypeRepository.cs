using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public partial class SysGoodsTypeRepository : ISysGoodsTypeRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public int Insert(SysGoodsType entity)
        {
            try
            {
                Object result = baseDao.Insert<SysGoodsType>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysGoodsType entity)
        {
            try
            {
                Object result = baseDao.Update<SysGoodsType>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysGoodsType entity)
        {
            try
            {
                Object result = baseDao.Delete<SysGoodsType>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysGoodsType FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysGoodsType>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysGoodsType> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysGoodsType>();
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
                String sql = "SELECT count(1) from SysGoodsType  with (nolock) where [IsDelete]=0  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysGoodsType> list)
        {
            try
            {
                return baseDao.BulkInsert<SysGoodsType>(list);
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
                String sql = string.Format(@"SELECT count(1) from SysGoodsType  with (nolock) where 1=1 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysGoodsType> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from SysGoodsType(nolock) where Id not in(
                  SELECT TOP {4} Id FROM SysGoodsType(NOLOCK)
                  WHERE [IsDelete]=0 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysGoodsType>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }
    }
}
