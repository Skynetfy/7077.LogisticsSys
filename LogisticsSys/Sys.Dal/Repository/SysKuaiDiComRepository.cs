using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Arch.Data.DbEngine;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public partial class SysKuaiDiComRepository: ISysKuaiDiComRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public long Insert(SysKuaiDiCom entity)
        {
            try
            {
                Object result = baseDao.Insert<SysKuaiDiCom>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysKuaiDiCom entity)
        {
            try
            {
                Object result = baseDao.Update<SysKuaiDiCom>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysKuaiDiCom entity)
        {
            try
            {
                Object result = baseDao.Delete<SysKuaiDiCom>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysKuaiDiCom FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysKuaiDiCom>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysKuaiDiCom> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysKuaiDiCom>();
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
                String sql = "SELECT count(1) from SysKuaiDiCom  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysKuaiDiCom> list)
        {
            try
            {
                return baseDao.BulkInsert<SysKuaiDiCom>(list);
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
                String sql = string.Format(@"SELECT count(1) from SysKuaiDiCom  with (nolock) where [IsDelete]=0 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysKuaiDiCom> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from SysChinaCity(nolock) where Id not in(
                  SELECT TOP {4} Id FROM SysKuaiDiCom(NOLOCK)
                  WHERE [IsDelete]=0 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysKuaiDiCom>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }

    }
}
