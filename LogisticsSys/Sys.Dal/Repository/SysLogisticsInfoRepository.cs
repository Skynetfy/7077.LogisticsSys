﻿using System;
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
    public class SysLogisticsInfoRepository : ISysLogisticsInfoRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public int Insert(SysLogisticsInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysLogisticsInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysLogisticsInfo entity)
        {
            try
            {
                Object result = baseDao.Update<SysLogisticsInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysLogisticsInfo entity)
        {
            try
            {
                Object result = baseDao.Delete<SysLogisticsInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysLogisticsInfo FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysLogisticsInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysLogisticsInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysLogisticsInfo>();
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
                String sql = "SELECT count(1) from SysLogisticsInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public bool BulkInsert(IList<SysLogisticsInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysLogisticsInfo>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }

        public IList<SysLogisticsInfo> GetLogisticsInfoList(string single)
        {
            try
            {
                string sql = @"select * from [SysLogisticsInfo] (nolock)
                             where [IsDelete]=0 and [LogisticsSingle]=@single order by [UpdateDate] desc";
                StatementParameterCollection parameters=new StatementParameterCollection();
                parameters.AddInParameter("@single",DbType.AnsiString, single);
                return baseDao.SelectList<SysLogisticsInfo>(sql, parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetPagerCount(string search)
        {
            try
            {
                string sql = @"select count(1) from v_LogisticsInfo (nolock)
                             where [IsDelete]=0 ";
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<SysLogisticsInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                string sql = string.Format(@"select top {0} [Id]
      ,[LogisticsSingle]
      ,[LogisticsDesc]
      ,[CreateDate]
      ,[IsDelete]
      ,[Status]
      ,[UpdateDate] from v_LogisticsInfo (nolock)
                             where [IsDelete]=0 and [Id] not in (select top {1} id from v_LogisticsInfo (nolock) where [IsDelete]=0 {2} ORDER BY {3} {4}) {2} ORDER BY {3} {4}", limit, offset, search, sort, order);
                return baseDao.SelectList<SysLogisticsInfo>(sql);
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
