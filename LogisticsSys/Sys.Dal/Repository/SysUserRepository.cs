using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;
using Arch.Data;
using Arch.Data.DbEngine;

namespace Sys.Dal.Repository
{
    public class SysUserRepository : ISysUserRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");
        public int Insert(SysUser entity)
        {
            try
            {
                Object result = baseDao.Insert<SysUser>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysUser entity)
        {
            try
            {
                Object result = baseDao.Update<SysUser>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysUser entity)
        {
            try
            {
                Object result = baseDao.Delete<SysUser>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysUser FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysUser>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysUser> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysUser>();
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
                String sql = "SELECT count(1) from SysUser  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysUser> list)
        {
            try
            {
                return baseDao.BulkInsert<SysUser>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }
        
    }
}
