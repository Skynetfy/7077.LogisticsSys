using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public class SysDbConfigRepository: ISysDbConfigRepository
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
        public SysDbConfig OrmByHand(string sql)
        {
            try
            {
                return baseDao.VisitDataReader<SysDbConfig>(sql, (reader) =>
                {
                    SysDbConfig entity = new SysDbConfig();
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

                //SysDbConfig entity = new SysDbConfig();
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
                throw new DalException("调用SysDbConfigDao时，访问OrmByHand时出错", ex);
            }
        }
        */
        /// <summary>
        ///  插入SysDbConfig
        /// </summary>
        /// <param name="SysDbConfig">SysDbConfig实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
        public long Insert(SysDbConfig SysDbConfig)
        {
            try
            {
                Object result = baseDao.Insert<SysDbConfig>(SysDbConfig);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfig时，访问Insert时出错", ex);
            }
        }
        /// <summary>
        /// 修改SysDbConfig
        /// </summary>
        /// <param name="SysDbConfig">SysDbConfig实体对象</param>
        /// <returns>状态代码</returns>
        public int Update(SysDbConfig SysDbConfig)
        {
            try
            {
                Object result = baseDao.Update<SysDbConfig>(SysDbConfig);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfig时，访问Update时出错", ex);
            }
        }
        /// <summary>
        /// 删除SysDbConfig
        /// </summary>
        /// <param name="SysDbConfig">SysDbConfig实体对象</param>
        /// <returns>状态代码</returns>
        public int Delete(SysDbConfig SysDbConfig)
        {
            try
            {
                Object result = baseDao.Delete<SysDbConfig>(SysDbConfig);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfig时，访问Delete时出错", ex);
            }
        }
        /// <summary>
        /// 根据主键获取SysDbConfig信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysDbConfig信息</returns>
        public SysDbConfig FindByPk(long id)
        {
            try
            {
                return baseDao.GetByKey<SysDbConfig>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfigDao时，访问FindByPk时出错", ex);
            }
        }
        /// <summary>
        /// 获取所有SysDbConfig信息
        /// </summary>
        /// <returns>SysDbConfig列表</returns>
        public IList<SysDbConfig> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysDbConfig>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfigDao时，访问GetAll时出错", ex);
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
                String sql = "SELECT count(1) from SysDbConfig  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfigDao时，访问Count时出错", ex);
            }
        }
        /// <summary>
        ///  检索SysDbConfig，带翻页
        /// </summary>
        /// <param name="obj">SysDbConfig实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        public IList<SysDbConfig> GetListByPage(SysDbConfig obj, int pagesize, int pageNo)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder(200);

                sbSql.Append(@"select ActionDate, ActionDesc, CreateDate, Id, IsDelete, LogType, OrderId, UserId from SysDbConfig (nolock) ");
                sbSql.Append(" order by Id desc ");
                sbSql.Append(string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (pageNo - 1) * pagesize, pagesize));
                IList<SysDbConfig> list = baseDao.SelectList<SysDbConfig>(sbSql.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysDbConfigDao时，访问GetListByPage时出错", ex);
            }
        }

        public bool BulkInsert(IList<SysDbConfig> list)
        {
            try
            {
                return baseDao.BulkInsert<SysDbConfig>(list);
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

        public IList<SysDbConfig> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            return null;
        }
    }
}
