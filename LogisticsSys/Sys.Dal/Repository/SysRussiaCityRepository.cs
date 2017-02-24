﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public class SysRussiaCityRepository:ISysRussiaCityRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public int Insert(SysRussiaCity entity)
        {
            try
            {
                Object result = baseDao.Insert<SysRussiaCity>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysRussiaCity entity)
        {
            try
            {
                Object result = baseDao.Update<SysRussiaCity>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysRussiaCity entity)
        {
            try
            {
                Object result = baseDao.Delete<SysRussiaCity>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysRussiaCity FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysRussiaCity>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysRussiaCity> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysRussiaCity>();
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
                String sql = "SELECT count(1) from SysRussiaCity  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysRussiaCity> list)
        {
            try
            {
                return baseDao.BulkInsert<SysRussiaCity>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }
    }
}
