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
    public class SysReceiverInfoRepository : ISysReceiverInfoRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public long Insert(SysReceiverInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysReceiverInfo>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysReceiverInfo entity)
        {
            try
            {
                Object result = baseDao.Update<SysReceiverInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysReceiverInfo entity)
        {
            try
            {
                Object result = baseDao.Delete<SysReceiverInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysReceiverInfo FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysReceiverInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysReceiverInfo> GetByOrderId(long id)
        {
            try
            {
                string sql = @"select A.*,B.[CityName] from [dbo].[SysReceiverInfo](nolock) A
                              left join [dbo].[SysChinaCity](nolock) B on A.ChinaCityId=B.Id
                            where A.[IsDelete]=0 and A.[OrderId]=@orderId";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@orderId", DbType.Int64, id);
                return baseDao.SelectList<SysReceiverInfo>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysReceiverInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysReceiverInfo>();
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
                String sql = "SELECT count(1) from SysReceiverInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysReceiverInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysReceiverInfo>(list);
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

        public IList<SysReceiverInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }
    }
}
