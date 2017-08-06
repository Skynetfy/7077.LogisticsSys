using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        public long Insert(SysOrderInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysOrderInfo>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public SysOrderInfo GetOrderByNumber(string ordernumber)
        {
            try
            {
                string sql = @"Select TOP 1 *  from SysOrderInfo  with (nolock) where Isdelete=0 and OrderNo=@OrderNo ";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@OrderNo", DbType.AnsiString, ordernumber);
                return baseDao.SelectFirst<SysOrderInfo>(sql, parameters);

            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRules时，访问Update时出错", ex);
            }
        }

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo, SysReceiverInfo receiverInfo, ref long status)
        {
            var param = new StatementParameterCollection();
            param.AddInParameter("@OrderNo", DbType.AnsiString, orderinfo.OrderNo);
            param.AddInParameter("@ShipperName", DbType.String, orderinfo.ShipperName);
            param.AddInParameter("@ShipperPhone", DbType.AnsiString, orderinfo.ShipperPhone);
            param.AddInParameter("@UserName", DbType.AnsiString, username);
            //param.AddInParameter("@PickupNumber", DbType.Int32, orderinfo.PickupNumber);
            param.AddInParameter("@Status", DbType.Int32, orderinfo.Status);
            param.AddInParameter("@OrderRealPrice",DbType.Decimal, orderinfo.OrderRealPrice);
            param.AddInParameter("@LogisticsSingle", DbType.AnsiString, addresserinfo.LogisticsSingle);
            param.AddInParameter("@RussiaCityId", DbType.Int64, addresserinfo.RussiaCityId);
            param.AddInParameter("@RussiaAddress", DbType.String, addresserinfo.RussiaAddress);
            //param.AddInParameter("@CargoNumber", DbType.Int32, addresserinfo.CargoNumber);
            param.AddInParameter("@PickupDate", DbType.DateTime, addresserinfo.PickupDate);
            param.AddInParameter("@WebUrl", DbType.AnsiString, addresserinfo.WebUrl);
            param.AddInParameter("@GoodsType", DbType.Int32, addresserinfo.GoodsType);
            param.AddInParameter("@TransportationWay", DbType.AnsiString, addresserinfo.TransportationWay);
            param.AddInParameter("@ProtectPrice", DbType.Decimal, addresserinfo.ProtectPrice);
            param.AddInParameter("@PolicyFee", DbType.Decimal, addresserinfo.PolicyFee);
            param.AddInParameter("@GoodsWeight", DbType.Decimal, addresserinfo.GoodsWeight);
            param.AddInParameter("@OrderFrees", DbType.Decimal, addresserinfo.OrderFrees);
            param.AddInParameter("@IsArrivePay", DbType.Boolean, addresserinfo.IsArrivePay);
            param.AddInParameter("@ArrivePayValue", DbType.Decimal, addresserinfo.ArrivePayValue);
            param.AddInParameter("@IsOutPhoto", DbType.Boolean, addresserinfo.IsOutPhoto);
            param.AddInParameter("@ExchangeRate", DbType.Decimal, addresserinfo.ExchangeRate);
            param.AddInParameter("@ArriveValueRuble",DbType.Decimal,addresserinfo.ArriveValueRuble);
            //param.AddInParameter("@ChinaCityId", DbType.Int64, receiverInfo.ChinaCityId);
            param.AddInParameter("@ChinaCityId", DbType.Int64, receiverInfo.ChinaCityId);
            param.AddInParameter("@ChinaAddress", DbType.String, receiverInfo.ChinaAddress);
            param.AddInParameter("@ReceiverName", DbType.String, receiverInfo.ReceiverName);
            param.AddInParameter("@ReceiverPhone", DbType.AnsiString, receiverInfo.ReceiverPhone);
            param.AddInParameter("@PackagingWay", DbType.Int32, receiverInfo.PackagingWay);
            param.AddInParameter("@ExpressWay", DbType.Int32, receiverInfo.ExpressWay);
            param.AddInParameter("@GoodsDesc", DbType.String, receiverInfo.GoodsDesc);
            param.AddInParameter("@ParcelWeight", DbType.Decimal, receiverInfo.ParcelWeight);
            param.AddInParameter("@ChinaCourierNumber", DbType.AnsiString, receiverInfo.ChinaCourierNumber);
            param.AddInParameter("@Desc", DbType.String, receiverInfo.Desc);
            param.AddInParameter("@CourierFees", DbType.Decimal, receiverInfo.CourierFees);
            param.AddInParameter("@CourierComId", DbType.Int64, receiverInfo.CourierComId);
            param.AddOutParameter("@result", DbType.String, 100);
            param.AddParameter("@message", DbType.Int32, 0, 32, ParameterDirection.ReturnValue);
           
            baseDao.ExecSp("CreateOrder_Proc", param);
            status = Convert.ToInt64(param["@message"].Value);
            return param[32].Value.ToString();
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

        public OrderView GetOrderViewById(long id)
        {
            try
            {
                return baseDao.GetByKey<OrderView>(id);
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
                String sql = "SELECT count(1) from SysOrderInfo  with (nolock) where Isdelete=0 ";
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
