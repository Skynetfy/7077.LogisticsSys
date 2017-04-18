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
    public partial class SysAgentInfoDao : ISysAgentInfoDao
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
        public SysAgentInfo OrmByHand(string sql)
        {
            try
            {
                return baseDao.VisitDataReader<SysAgentInfo>(sql, (reader) =>
                {
                    SysAgentInfo entity = new SysAgentInfo();
					if(reader.Read())
					{
                        entity.AgentCityId = reader["AgentCityId"];
                        entity.CreateDate = reader["CreateDate"];
                        entity.Id = reader["Id"];
                        entity.IsDelete = reader["IsDelete"];
                        entity.QQNumber = reader["QQNumber"];
                        entity.UserId = reader["UserId"];
                    }
                    return entity;
                });

                //SysAgentInfo entity = new SysAgentInfo();
                //using(var reader = baseDao.SelectDataReader(sql))
                //{
					//if(reader.Read())
					//{
                        //entity.AgentCityId = reader["AgentCityId"];
                        //entity.CreateDate = reader["CreateDate"];
                        //entity.Id = reader["Id"];
                        //entity.IsDelete = reader["IsDelete"];
                        //entity.QQNumber = reader["QQNumber"];
                        //entity.UserId = reader["UserId"];
	                //}
                //}
                //return entity;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问OrmByHand时出错", ex);
            }
        }
        */
        /// <summary>
        ///  插入SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		public long Insert(SysAgentInfo sysAgentInfo)
        {
            try
            {
                Object result = baseDao.Insert<SysAgentInfo>(sysAgentInfo);
		        long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfo时，访问Insert时出错", ex);
            }
        }
        /// <summary>
        /// 修改SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>状态代码</returns>
        public int Update(SysAgentInfo sysAgentInfo)
        {
            try
            {
                Object result = baseDao.Update<SysAgentInfo>(sysAgentInfo);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfo时，访问Update时出错", ex);
            }
        }
        /// <summary>
        /// 删除SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>状态代码</returns>
        public int Delete(SysAgentInfo sysAgentInfo)
        {
            try
            {
                string sql = @"UPdate [SysUser] set [isdelete]=1 where Id=@uid;
                                Update [SysAgentInfo] set [isdelete]=1 where Id=@id";
                StatementParameterCollection parameters=new StatementParameterCollection();
                parameters.AddInParameter("@uid",DbType.Int64, sysAgentInfo.UserId);
                parameters.AddInParameter("@id", DbType.Int64, sysAgentInfo.Id);
                Object result = baseDao.ExecNonQuery(sql, parameters);
                int iReturn = Convert.ToInt32(result);

                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfo时，访问Delete时出错", ex);
            }
        }
        /// <summary>
        /// 根据主键获取SysAgentInfo信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysAgentInfo信息</returns>
        public SysAgentInfo FindByPk(long id )
        {
            try
            {
                return baseDao.GetByKey<SysAgentInfo>(id);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问FindByPk时出错", ex);
            }
        }
        /// <summary>
        /// 获取所有SysAgentInfo信息
        /// </summary>
        /// <returns>SysAgentInfo列表</returns>
        public IList<SysAgentInfo> GetAll()
        {
            try
            {
                String sql = string.Format(@"SELECT * from SysAgentInfo  with (nolock) where [IsDelete]=0");
                return baseDao.SelectList<SysAgentInfo>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问GetAll时出错", ex);
            }
        }

        public IList<SysAgentInfo> GetAgentInfoView()
        {
            try
            {
                String sql = string.Format(@"SELECT * from v_AgentInfo  with (nolock) where [IsDelete]=0");
                return baseDao.SelectList<SysAgentInfo>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问GetAll时出错", ex);
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
                String sql = "SELECT count(1) from SysAgentInfo  with (nolock)  ";
                object obj = baseDao.ExecScalar(sql);
                long ret = Convert.ToInt64(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问Count时出错", ex);
            }
        }
        /// <summary>
        ///  检索SysAgentInfo，带翻页
        /// </summary>
        /// <param name="obj">SysAgentInfo实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        public IList<SysAgentInfo> GetListByPage(SysAgentInfo obj, int pagesize, int pageNo)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder(200);

                sbSql.Append(@"select AgentCityId, CreateDate, Id, IsDelete, QQNumber, UserId from SysAgentInfo (nolock) ");
                sbSql.Append(" order by Id desc ");
                sbSql.Append(string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (pageNo - 1) * pagesize, pagesize));
                IList<SysAgentInfo> list = baseDao.SelectList<SysAgentInfo>(sbSql.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysAgentInfoDao时，访问GetListByPage时出错", ex);
            }
        }


        public bool BulkInsert(IList<SysAgentInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysAgentInfo>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }
        public int GetPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from v_AgentInfo  with (nolock) where 0=0 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysAgentInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from v_AgentInfo(nolock) where Id not in(
                  SELECT TOP {4} Id FROM v_AgentInfo(NOLOCK)
                  WHERE 0=0 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysAgentInfo>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }


    }
}