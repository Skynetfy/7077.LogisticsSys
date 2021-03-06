﻿using System;
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
        public long Insert(SysUser entity)
        {
            try
            {
                Object result = baseDao.Insert<SysUser>(entity);
                long iReturn = Convert.ToInt64(result);
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
        public SysUser GetUser(string username, string password)
        {
            try
            {
                string sql = @"select top 1 * from SysUser(nolock)
                       where UserName=@username and Password=@password";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@username", System.Data.DbType.AnsiString, username);
                parameters.AddInParameter("@password", System.Data.DbType.AnsiString, password);
                return baseDao.SelectFirst<SysUser>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }

        public SysUser GetUser(string username)
        {
            try
            {
                string sql = @"select top 1 * from SysUser(nolock)
                       where UserName=@username";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@username", System.Data.DbType.AnsiString, username);
                return baseDao.SelectFirst<SysUser>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
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
        public int GetPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from [SysUser]  with (nolock) where 1=1 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysUser> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from [SysUser](nolock) where Id not in(
                  SELECT TOP {4} Id FROM [SysUser](NOLOCK)
                  WHERE 1=1 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysUser>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }
    }
}
