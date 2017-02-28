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
    public class SysOrderInfoRepository : ISysOrderInfoRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public int Insert(SysOrderInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysOrderInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo,
            SysReceiverInfo receiverinfo, ref int status)
        {
            var param = new StatementParameterCollection();
            param.AddInParameter("@OrderNo", DbType.AnsiString, orderinfo.OrderNo);
            param.AddInParameter("@ShipperName", DbType.String, orderinfo.ShipperName);
            param.AddInParameter("@ShipperPhone", DbType.AnsiString, orderinfo.ShipperPhone);
            param.AddInParameter("@UserName", DbType.AnsiString, username);
            param.AddInParameter("@PickupNumber", DbType.Int32, orderinfo.PickupNumber);
            param.AddInParameter("@Status", DbType.Int32, orderinfo.Status);
            param.AddInParameter("@LogisticsSingle", DbType.AnsiString, addresserinfo.LogisticsSingle);
            param.AddInParameter("@RussiaCityId", DbType.Int64, addresserinfo.RussiaCityId);
            param.AddInParameter("@RussiaAddress", DbType.String, addresserinfo.RussiaAddress);
            param.AddInParameter("@CargoNumber", DbType.Int32, addresserinfo.CargoNumber);
            param.AddInParameter("@PickupDate", DbType.DateTime, addresserinfo.PickupDate);
            param.AddInParameter("@PickupWay", DbType.Int32, addresserinfo.PickupWay);
            param.AddInParameter("@GoodsType", DbType.Int32, addresserinfo.GoodsType);
            param.AddInParameter("@TransportationWay", DbType.AnsiString, addresserinfo.TransportationWay);
            param.AddInParameter("@ProtectPrice", DbType.Decimal, addresserinfo.ProtectPrice);
            param.AddInParameter("@PolicyFee", DbType.Decimal, addresserinfo.PolicyFee);
            param.AddInParameter("@GoodsWeight", DbType.Decimal, addresserinfo.GoodsWeight);
            param.AddInParameter("@BoxLong", DbType.Decimal, addresserinfo.BoxLong);
            param.AddInParameter("@BoxWidth", DbType.Decimal, addresserinfo.BoxWidth);
            param.AddInParameter("@BoxHeight", DbType.Decimal, addresserinfo.BoxHeight);
            param.AddInParameter("@ParcelSingle", DbType.AnsiString, receiverinfo.ParcelSingle);
            param.AddInParameter("@ChinaCityId", DbType.Int64, receiverinfo.ChinaCityId);
            param.AddInParameter("@ChinaAddress", DbType.String, receiverinfo.ChinaAddress);
            param.AddInParameter("@ReceiverName", DbType.String, receiverinfo.ReceiverName);
            param.AddInParameter("@ReceiverPhone", DbType.AnsiString, receiverinfo.ReceiverPhone);
            param.AddInParameter("@PackagingWay", DbType.Int32, receiverinfo.PackagingWay);
            param.AddInParameter("@ExpressWay", DbType.Int32, receiverinfo.ExpressWay);
            param.AddInParameter("@GoodsDesc", DbType.String, receiverinfo.GoodsDesc);
            param.AddInParameter("@ParcelWeight", DbType.Decimal, receiverinfo.ParcelWeight);
            param.AddInParameter("@ChinaCourierNumber", DbType.AnsiString, receiverinfo.ChinaCourierNumber);
            param.AddInParameter("@Desc", DbType.String, receiverinfo.Desc);
            param.AddOutParameter("@result", DbType.String,100);
            param.AddParameter("@message", DbType.Int32, 0, 32, ParameterDirection.ReturnValue);

            var s = baseDao.VisitDataReaderBySp<object>("CreateOrder_Proc", param, x =>
            {
                return x;
            });
            status = Convert.ToInt32(param["@message"].Value);
            return param[31].Value.ToString();
        }

        public int Update(SysOrderInfo entity)
        {
            try
            {
                Object result = baseDao.Update<SysOrderInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public int Delete(SysOrderInfo entity)
        {
            try
            {
                Object result = baseDao.Delete<SysOrderInfo>(entity);
                int iReturn = Convert.ToInt32(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Delete时出错", ex);
            }
        }

        public SysOrderInfo FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysOrderInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问FindByPk时出错", ex);
            }
        }

        public IList<SysOrderInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysOrderInfo>();
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
                String sql = "SELECT count(1) from SysOrderInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }
        public bool BulkInsert(IList<SysOrderInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysOrderInfo>(list);
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

        public IList<SysOrderInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }

        public int GetOrderViewPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from [v_orderinfo]  with (nolock) where 1=1 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<OrderView> GetOrderViewPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from [v_orderinfo](nolock) where Id not in(
                  SELECT TOP {4} Id FROM [v_orderinfo](NOLOCK)
                  WHERE 1=1 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<OrderView>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }
    }
}
