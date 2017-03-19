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

        public DataTable createtable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.Int64"));
            dt.Columns.Add("ParcelSingle", Type.GetType("System.String"));
            var dr = dt.NewRow();
            dr["Id"] = 10;
            dr["ParcelSingle"] = "ParcelSingle";
            dt.Rows.Add(dr);
            return dt;
        }

        public DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                if(!entityProperties[i].Name.Equals("IsDelete")&& !entityProperties[i].Name.Equals("CreateDate"))
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                //dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length-2];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    if (!entityProperties[i].Name.Equals("IsDelete") && !entityProperties[i].Name.Equals("CreateDate"))
                        entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            
            return dt;
        }
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

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo, List<SysReceiverInfo> receiverInfos, ref int status)
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
            //param.AddInParameter("@ParcelSingle", DbType.AnsiString, receiverinfo.ParcelSingle);
            //param.AddInParameter("@ChinaCityId", DbType.Int64, receiverinfo.ChinaCityId);
            //param.AddInParameter("@ChinaAddress", DbType.String, receiverinfo.ChinaAddress);
            //param.AddInParameter("@ReceiverName", DbType.String, receiverinfo.ReceiverName);
            //param.AddInParameter("@ReceiverPhone", DbType.AnsiString, receiverinfo.ReceiverPhone);
            //param.AddInParameter("@PackagingWay", DbType.Int32, receiverinfo.PackagingWay);
            //param.AddInParameter("@ExpressWay", DbType.Int32, receiverinfo.ExpressWay);
            //param.AddInParameter("@GoodsDesc", DbType.String, receiverinfo.GoodsDesc);
            //param.AddInParameter("@ParcelWeight", DbType.Decimal, receiverinfo.ParcelWeight);
            //param.AddInParameter("@ChinaCourierNumber", DbType.AnsiString, receiverinfo.ChinaCourierNumber);
            //param.AddInParameter("@Desc", DbType.String, receiverinfo.Desc);
            var dt1 = ListToDataTable(receiverInfos);
            param.AddInParameter("@Reciviertable", DbType.Xml, dt1);
            //param.AddOutParameter("@result", DbType.String, 100);
            //param.AddParameter("@message", DbType.Int32, 0, 32, ParameterDirection.ReturnValue);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConStr"].ConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CreateOrder_Proc";
                cmd.Parameters.AddWithValue("@OrderNo", orderinfo.OrderNo);
                cmd.Parameters.AddWithValue("@ShipperName", orderinfo.ShipperName);
                cmd.Parameters.AddWithValue("@ShipperPhone", orderinfo.ShipperPhone);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@PickupNumber", orderinfo.PickupNumber);
                cmd.Parameters.AddWithValue("@Status", orderinfo.Status);
                cmd.Parameters.AddWithValue("@LogisticsSingle", addresserinfo.LogisticsSingle);
                cmd.Parameters.AddWithValue("@RussiaCityId", addresserinfo.RussiaCityId);
                cmd.Parameters.AddWithValue("@RussiaAddress", addresserinfo.RussiaAddress);
                cmd.Parameters.AddWithValue("@CargoNumber", addresserinfo.CargoNumber);
                cmd.Parameters.AddWithValue("@PickupDate", addresserinfo.PickupDate);
                cmd.Parameters.AddWithValue("@PickupWay", addresserinfo.PickupWay);
                cmd.Parameters.AddWithValue("@GoodsType", addresserinfo.GoodsType);
                cmd.Parameters.AddWithValue("@TransportationWay", addresserinfo.TransportationWay);
                cmd.Parameters.AddWithValue("@ProtectPrice", addresserinfo.ProtectPrice);
                cmd.Parameters.AddWithValue("@PolicyFee", addresserinfo.PolicyFee);
                cmd.Parameters.AddWithValue("@GoodsWeight", addresserinfo.GoodsWeight);
                cmd.Parameters.AddWithValue("@BoxLong", addresserinfo.BoxLong);
                cmd.Parameters.AddWithValue("@BoxWidth", addresserinfo.BoxWidth);
                cmd.Parameters.AddWithValue("@BoxHeight", addresserinfo.BoxHeight);
                cmd.Parameters.AddWithValue("@ReciverList", dt1);
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@result",
                    Value = 100,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                });
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@message",
                    Value = 0,
                    SqlDbType = SqlDbType.Int,
                    Size = 32,
                    Direction = ParameterDirection.ReturnValue
                });
                conn.Open();
                var cs = cmd.ExecuteNonQuery();
            }
            //baseDao.ExecSp("CreateOrder_Proc", param);
            //status = Convert.ToInt32(param["@message"].Value);
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
