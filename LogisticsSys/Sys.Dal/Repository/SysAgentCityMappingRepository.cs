using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public class SysAgentCityMappingRepository : ISysAgentCityMappingRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public long Insert(SysAgentCityMapping entity)
        {
            try
            {
                Object result = baseDao.Insert<SysAgentCityMapping>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysAgentCityMapping entity)
        {
            try
            {
                Object result = baseDao.Update<SysAgentCityMapping>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysAgentCityMapping entity)
        {
            try
            {
                Object result = baseDao.Delete<SysAgentCityMapping>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysAgentCityMapping FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysAgentCityMapping>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysAgentCityMapping> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysAgentCityMapping>();
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
                String sql = "SELECT count(1) from SysAgentCityMapping  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysAgentCityMapping> list)
        {
            try
            {
                return baseDao.BulkInsert<SysAgentCityMapping>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }
        public int GetPagerCount(string search)
        {
            return 0;
        }

        public IList<SysAgentCityMapping> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }
    }
}
