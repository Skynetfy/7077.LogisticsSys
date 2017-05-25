using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Arch.Data;
using Arch.Data.DbEngine;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    /// <summary>
    ///
    /// </summary>
    public partial class SysCustomerInfoDao : ISysCustomerInfoDao
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");
        
        //特别注意，如果是可空类型，建议以如下方式使用：
        // var data = reader["field"];
        // entity.stringData = data == null ? data : data.ToString();
        //如需要手工映射，请反注释如下代码，并注意转换类型
        /*
        /// <summary>
        /// 手工映射，建议使用1.2.0.5版本以上的VisitDataReader
        /// </summary>
        /// <returns>结果</returns>
        public SysCustomerInfo OrmByHand(string sql)
        {
            try
            {
                return baseDao.VisitDataReader<SysCustomerInfo>(sql, (reader) =>
                {
                    SysCustomerInfo entity = new SysCustomerInfo();
					if(reader.Read())
					{
                        entity.Address = reader["Address"];
                        entity.CityId = reader["CityId"];
                        entity.CreateDate = reader["CreateDate"];
                        entity.CustomerID = reader["CustomerID"];
                        entity.Id = reader["Id"];
                        entity.IsDelete = reader["IsDelete"];
                        entity.Phone = reader["Phone"];
                        entity.QQNumber = reader["QQNumber"];
                        entity.UserId = reader["UserId"];
                        entity.WebChatNo = reader["WebChatNo"];
                    }
                    return entity;
                });

                //SysCustomerInfo entity = new SysCustomerInfo();
                //using(var reader = baseDao.SelectDataReader(sql))
                //{
					//if(reader.Read())
					//{
                        //entity.Address = reader["Address"];
                        //entity.CityId = reader["CityId"];
                        //entity.CreateDate = reader["CreateDate"];
                        //entity.CustomerID = reader["CustomerID"];
                        //entity.Id = reader["Id"];
                        //entity.IsDelete = reader["IsDelete"];
                        //entity.Phone = reader["Phone"];
                        //entity.QQNumber = reader["QQNumber"];
                        //entity.UserId = reader["UserId"];
                        //entity.WebChatNo = reader["WebChatNo"];
	                //}
                //}
                //return entity;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问OrmByHand时出错", ex);
            }
        }
        */
        /// <summary>
        ///  插入SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		public long Insert(SysCustomerInfo sysCustomerInfo)
        {
            try
            {
                Object result = baseDao.Insert<SysCustomerInfo>(sysCustomerInfo);
		        long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfo时，访问Insert时出错", ex);
            }
        }
        /// <summary>
        /// 修改SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>状态代码</returns>
        public int Update(SysCustomerInfo sysCustomerInfo)
        {
            try
            {
                Object result = baseDao.Update<SysCustomerInfo>(sysCustomerInfo);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfo时，访问Update时出错", ex);
            }
        }
        /// <summary>
        /// 删除SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>状态代码</returns>
        public int Delete(SysCustomerInfo sysCustomerInfo)
        {
            try
            {
                Object result = baseDao.Delete<SysCustomerInfo>(sysCustomerInfo);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfo时，访问Delete时出错", ex);
            }
        }
        /// <summary>
        /// 根据主键获取SysCustomerInfo信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysCustomerInfo信息</returns>
        public SysCustomerInfo FindByPk(long id )
        {
            try
            {
                return baseDao.GetByKey<SysCustomerInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问FindByPk时出错", ex);
            }
        }

        public SysCustomerInfo FindByUid(long id)
        {
            try
            {
                string sql = @"SELECT TOP 1  [Id]
      ,[UserId]
      ,[CustomerID]
      ,[CityId]
      ,[Phone]
      ,[Address]
      ,[WebChatNo]
      ,[QQNumber]
      ,[CreateDate]
      ,[IsDelete]
      ,[Integral]
                  FROM [dbo].[SysCustomerInfo] (NOLOCK) where [UserId]=@Uid";
                StatementParameterCollection parameters=new StatementParameterCollection();
                parameters.AddInParameter("@Uid",DbType.Int64, id);
                return baseDao.SelectFirst<SysCustomerInfo>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问FindByPk时出错", ex);
            }
        }
        /// <summary>
        /// 获取所有SysCustomerInfo信息
        /// </summary>
        /// <returns>SysCustomerInfo列表</returns>
        public IList<SysCustomerInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysCustomerInfo>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问GetAll时出错", ex);
            }
        }
        /// <summary>
        /// 取得总记录数
        /// </summary>
        /// <returns>记录数</returns>
        public long Count()
        {
            try
            {
                String sql = "SELECT count(1) from SysCustomerInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问Count时出错", ex);
            }
        }
        /// <summary>
        ///  检索SysCustomerInfo，带翻页
        /// </summary>
        /// <param name="obj">SysCustomerInfo实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        public IList<SysCustomerInfo> GetListByPage(SysCustomerInfo obj, int pagesize, int pageNo)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder(200);

                sbSql.Append(@"select Address, CityId, CreateDate, CustomerID, Id, IsDelete, Phone, QQNumber, UserId, WebChatNo from SysCustomerInfo (nolock) ");
                sbSql.Append(" order by Id desc ");
                sbSql.Append(string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (pageNo - 1) * pagesize, pagesize));
                IList<SysCustomerInfo> list = baseDao.SelectList<SysCustomerInfo>(sbSql.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问GetListByPage时出错", ex);
            }
        }

       /// <summary>
       ///  批量插入SysCustomerInfo
       /// </summary>
       /// <param name="sysCustomerInfo">SysCustomerInfo实体对象列表</param>
       /// <returns>状态代码</returns>
        public bool BulkInsertSysCustomerInfo(IList<SysCustomerInfo> sysCustomerInfoList)
       	{
            try
            {
                return baseDao.BulkInsert<SysCustomerInfo>(sysCustomerInfoList);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysCustomerInfoDao时，访问BulkInsert时出错", ex);
            }
        }


        public bool BulkInsert(IList<SysCustomerInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysCustomerInfo>(list);
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

        public IList<SysCustomerInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }

    }
}