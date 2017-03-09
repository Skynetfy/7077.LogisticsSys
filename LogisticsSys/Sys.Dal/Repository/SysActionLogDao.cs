using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Arch.Data;
using Sys.Dal;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    /// <summary>
    ///
    /// </summary>
    public partial class SysActionLogDao : ISysActionLogDao
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
        public SysActionLog OrmByHand(string sql)
        {
            try
            {
                return baseDao.VisitDataReader<SysActionLog>(sql, (reader) =>
                {
                    SysActionLog entity = new SysActionLog();
					if(reader.Read())
					{
                        entity.ActionDate = reader["ActionDate"];
                        entity.ActionDesc = reader["ActionDesc"];
                        entity.CreateDate = reader["CreateDate"];
                        entity.Id = reader["Id"];
                        entity.IsDelete = reader["IsDelete"];
                        entity.LogType = reader["LogType"];
                        entity.OrderId = reader["OrderId"];
                        entity.UserId = reader["UserId"];
                    }
                    return entity;
                });

                //SysActionLog entity = new SysActionLog();
                //using(var reader = baseDao.SelectDataReader(sql))
                //{
					//if(reader.Read())
					//{
                        //entity.ActionDate = reader["ActionDate"];
                        //entity.ActionDesc = reader["ActionDesc"];
                        //entity.CreateDate = reader["CreateDate"];
                        //entity.Id = reader["Id"];
                        //entity.IsDelete = reader["IsDelete"];
                        //entity.LogType = reader["LogType"];
                        //entity.OrderId = reader["OrderId"];
                        //entity.UserId = reader["UserId"];
	                //}
                //}
                //return entity;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问OrmByHand时出错", ex);
            }
        }
        */
        /// <summary>
        ///  插入SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		public long InsertSysActionLog(SysActionLog sysActionLog)
        {
            try
            {
                Object result = baseDao.Insert<SysActionLog>(sysActionLog);
		        long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLog时，访问Insert时出错", ex);
            }
        }
        /// <summary>
        /// 修改SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>状态代码</returns>
        public int UpdateSysActionLog(SysActionLog sysActionLog)
        {
            try
            {
                Object result = baseDao.Update<SysActionLog>(sysActionLog);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLog时，访问Update时出错", ex);
            }
        }
        /// <summary>
        /// 删除SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>状态代码</returns>
        public int DeleteSysActionLog(SysActionLog sysActionLog)
        {
            try
            {
                Object result = baseDao.Delete<SysActionLog>(sysActionLog);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLog时，访问Delete时出错", ex);
            }
        }
        /// <summary>
        /// 根据主键获取SysActionLog信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysActionLog信息</returns>
        public SysActionLog FindByPk(long id )
        {
            try
            {
                return baseDao.GetByKey<SysActionLog>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问FindByPk时出错", ex);
            }
        }
        /// <summary>
        /// 获取所有SysActionLog信息
        /// </summary>
        /// <returns>SysActionLog列表</returns>
        public IList<SysActionLog> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysActionLog>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问GetAll时出错", ex);
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
                String sql = "SELECT count(1) from SysActionLog  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问Count时出错", ex);
            }
        }
        /// <summary>
        ///  检索SysActionLog，带翻页
        /// </summary>
        /// <param name="obj">SysActionLog实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        public IList<SysActionLog> GetListByPage(SysActionLog obj, int pagesize, int pageNo)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder(200);

                sbSql.Append(@"select ActionDate, ActionDesc, CreateDate, Id, IsDelete, LogType, OrderId, UserId from SysActionLog (nolock) ");
                sbSql.Append(" order by Id desc ");
                sbSql.Append(string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (pageNo - 1) * pagesize, pagesize));
                IList<SysActionLog> list = baseDao.SelectList<SysActionLog>(sbSql.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问GetListByPage时出错", ex);
            }
        }

       /// <summary>
       ///  批量插入SysActionLog
       /// </summary>
       /// <param name="sysActionLog">SysActionLog实体对象列表</param>
       /// <returns>状态代码</returns>
        public bool BulkInsertSysActionLog(IList<SysActionLog> sysActionLogList)
       	{
            try
            {
                return baseDao.BulkInsert<SysActionLog>(sysActionLogList);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问BulkInsert时出错", ex);
            }
        }

        
    }
}