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
    public class SysOrderNumberRepository : ISysOrderNumberRepository
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
        public long Insert(SysOrderNumber sysActionLog)
        {
            try
            {
                Object result = baseDao.Insert<SysOrderNumber>(sysActionLog);
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
        public int Update(SysOrderNumber sysActionLog)
        {
            try
            {
                Object result = baseDao.Update<SysOrderNumber>(sysActionLog);
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
        public int Delete(SysOrderNumber sysActionLog)
        {
            try
            {
                Object result = baseDao.Delete<SysOrderNumber>(sysActionLog);
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
        public SysOrderNumber FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysOrderNumber>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问FindByPk时出错", ex);
            }
        }

        public SysOrderNumber FindBySingle(string single)
        {
            try
            {
                String sql = "SELECT top 1 * from SysOrderNumber  with (nolock) where [Number]=@Number  ";
                StatementParameterCollection parameters = new StatementParameterCollection();
                parameters.AddInParameter("@Number", DbType.AnsiString, single);
                return baseDao.SelectFirst<SysOrderNumber>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问Count时出错", ex);
            }
        }

        /// <summary>
        /// 获取所有SysActionLog信息
        /// </summary>
        /// <returns>SysActionLog列表</returns>
        public IList<SysOrderNumber> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysOrderNumber>();
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
                String sql = "SELECT count(1) from SysOrderNumber  with (nolock)  ";
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
        public IList<SysOrderNumber> GetListByPage(SysOrderNumber obj, int pagesize, int pageNo)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder(200);

                sbSql.Append(@"select ActionDate, ActionDesc, CreateDate, Id, IsDelete, LogType, OrderId, UserId from SysActionLog (nolock) ");
                sbSql.Append(" order by Id desc ");
                sbSql.Append(string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (pageNo - 1) * pagesize, pagesize));
                IList<SysOrderNumber> list = baseDao.SelectList<SysOrderNumber>(sbSql.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问GetListByPage时出错", ex);
            }
        }

        public bool BulkInsert(IList<SysOrderNumber> list)
        {
            try
            {
                return baseDao.BulkInsert<SysOrderNumber>(list);
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

        public IList<SysOrderNumber> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }

    }
}
