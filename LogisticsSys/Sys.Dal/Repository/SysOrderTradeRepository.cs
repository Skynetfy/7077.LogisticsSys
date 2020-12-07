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
    public partial class SysOrderTradeRepository: ISysOrderTradeRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");
        public long Insert(SysOrderTrade entity)
        {
            return Convert.ToInt64(baseDao.Insert(entity));
        }

        public int Update(SysOrderTrade entity)
        {
            return baseDao.Update(entity);
        }

        public int Delete(SysOrderTrade entity)
        {
            return baseDao.Delete(entity);
        }

        public SysOrderTrade FindByPk(long id)
        {
            return baseDao.GetByKey<SysOrderTrade>(id);
        }

        public SysOrderTrade FindByOutTradeNo(string outTradeNo)
        {
            String sql = string.Format(@"SELECT TOP 1 * from SysOrderTrade  with (nolock) where [OutTradeNo]=@OutTradeNo");
            StatementParameterCollection parameters = new StatementParameterCollection();
            parameters.AddInParameter("@OutTradeNo", DbType.AnsiString, outTradeNo);
            return baseDao.SelectFirst<SysOrderTrade>(sql, parameters);
        }
        public IList<SysOrderTrade> GetAll()
        {
            String sql = string.Format(@"SELECT * from SysOrderTrade  with (nolock) where [IsDelete]=0");
            return baseDao.SelectList<SysOrderTrade>(sql);
        }

        public long Count()
        {
            String sql = "SELECT count(1) from SysOrderTrade  with (nolock)  ";
            object obj = baseDao.ExecScalar(sql);
            return Convert.ToInt64(obj);
        }

        public bool BulkInsert(IList<SysOrderTrade> list)
        {
            throw new NotImplementedException();
        }

        public int GetPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from SysOrderTrade a with (nolock)
                 LEFT JOIN [dbo].[SysUser] b on a.userId=b.Id
                 LEFT JOIN [SysAddresserInfo] c on a.orderId = c.[OrderId]
                where 0=0 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysOrderTrade> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} a.*,b.[UserName],b.[DisplayName],b.[Phone] from SysOrderTrade a (nolock)
                  LEFT JOIN [dbo].[SysUser] b on a.userId=b.Id
                  LEFT JOIN [SysAddresserInfo] c on a.orderId = c.[OrderId]
                  where a.Id not in(
                  SELECT TOP {4} Id FROM SysOrderTrade(NOLOCK)
                  WHERE 0=0 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysOrderTrade>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }
    }
}
