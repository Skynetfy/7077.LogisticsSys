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

        public int UpdateReciverInfo(SysReceiverInfo entity)
        {
            try
            {
                string sql = @"Update [dbo].[SysReceiverInfo] set
                               [RealWeight]=@RealWeight,
                               [RealPrice]=@RealPrice
                              where Id=@id";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@id", DbType.Int64, entity.Id);
                //parameters.AddInParameter("@RealWeight", DbType.Decimal, entity.RealWeight);
                //parameters.AddInParameter("@RealPrice", DbType.Decimal, entity.RealPrice);
                Object result = baseDao.ExecNonQuery(sql, parameters);
                int iReturn = Convert.ToInt32(result);
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

        public SysReceiverInfo GetByCourierNo(string no)
        {
            try
            {
                string sql = @"Select Top 1 * from [SysReceiverInfo](nolock)
                             where [ChinaCourierNumber]=@no and [IsDelete]=0";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@no", DbType.AnsiString, no);
                var dt = baseDao.SelectDataTable(sql, parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new SysReceiverInfo()
                    {
                        Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                        OrderId = Convert.ToInt64(dt.Rows[0]["OrderId"])
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysReceiverInfo> GetByOrderId(long id)
        {
            var list = new List<SysReceiverInfo>();
            try
            {
                string sql = @"select A.*,B.[CityName],C.[ComName] from [dbo].[SysReceiverInfo](nolock) A
                              left join [dbo].[SysChinaCity](nolock) B on A.ChinaCityId=B.Id
                              left join [dbo].[SysKuaiDiCom] C on A.CourierComId=C.Id
                            where A.[IsDelete]=0 and A.[OrderId]=@orderId";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@orderId", DbType.Int64, id);
                var dt = baseDao.SelectDataTable(sql, parameters);
                return dt.AsEnumerable().Select(x => new SysReceiverInfo()
                {
                    Id = x.Field<Int64>("Id"),
                    OrderId = x.Field<Int64>("OrderId"),
                    ParcelSingle = x.Field<string>("ParcelSingle"),
                    ChinaCityId = x.Field<long>("ChinaCityId"),
                    CityName = x.Field<string>("CityName"),
                    ChinaAddress = x.Field<string>("ChinaAddress"),
                    ReceiverName = x.Field<string>("ReceiverName"),
                    ReceiverPhone = x.Field<string>("ReceiverPhone"),
                    PackagingWay = x.Field<int>("PackagingWay"),
                    ExpressWay = x.Field<int>("ExpressWay"),
                    GoodsDesc = x.Field<string>("GoodsDesc"),
                    ParcelWeight = x.Field<decimal>("ParcelWeight"),
                    //RealWeight = x.Field<decimal>("RealWeight"),
                    //RealPrice = x.Field<decimal>("RealPrice"),
                    //CourierComId = x.Field<long>("CourierComId"),
                    //CourierComName = x.Field<string>("ComName"),
                    //ChinaCourierNumber = x.Field<string>("ChinaCourierNumber"),
                    //Desc = x.Field<string>("Desc"),
                    //BudgetPrice = x.Field<decimal>("BudgetPrice")
                }).ToList();
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
