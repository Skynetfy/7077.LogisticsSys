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
    public class SysAddresserInfoRepository : ISysAddresserInfoRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public long Insert(SysAddresserInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysAddresserInfo>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Update(SysAddresserInfo entity)
        {
            try
            {
                Object result = baseDao.Update<SysAddresserInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }
        public int UpdateAddresser(SysAddresserInfo entity)
        {
            try
            {
                string sql = @"Update [SysAddresserInfo]
                             set [AddressRealPrice]=@AddressRealPrice,
                             [GoodsVolumeWeight]=@GoodsVolumeWeight,
                             [GoodsRealWeight]=@GoodsRealWeight
                             where Id=@id";
                StatementParameterCollection parameters=new StatementParameterCollection();
                parameters.AddInParameter("@id",DbType.Int64,entity.Id);
                parameters.AddInParameter("@AddressRealPrice", DbType.Decimal, entity.AddressRealPrice);
                parameters.AddInParameter("@GoodsVolumeWeight", DbType.Decimal, entity.GoodsVolumeWeight);
                parameters.AddInParameter("@GoodsRealWeight", DbType.Decimal, entity.GoodsRealWeight);
                Object result = baseDao.ExecNonQuery(sql, parameters);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }
        public int Delete(SysAddresserInfo entity)
        {
            try
            {
                Object result = baseDao.Delete<SysAddresserInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysAddresserInfo FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysAddresserInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }
        public SysAddresserInfo GetByOrderId(long id)
        {
            try
            {
                string sql = @"select top 1 A.*,B.[CityName],C.[GoodsType] as GoodsTypeName from [SysAddresserInfo](nolock) A
                            left join [dbo].[SysRussiaCity](nolock) B on A.[RussiaCityId]=B.Id
                            left join [dbo].[SysGoodsType](nolock) C on A.[GoodsType]=C.Id
                            where A.[IsDelete]=0 and A.[OrderId]=@orderId";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@orderId", DbType.Int64, id);
                return baseDao.SelectFirst<SysAddresserInfo>(sql, parameters);
            } 
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }
        public IList<SysAddresserInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysAddresserInfo>();
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
                String sql = "SELECT count(1) from SysAddresserInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysAddresserInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysAddresserInfo>(list);
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

        public IList<SysAddresserInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }
    }
}
