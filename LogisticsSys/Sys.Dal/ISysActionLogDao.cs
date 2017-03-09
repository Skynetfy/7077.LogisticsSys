using System;
using System.Collections.Generic;
using Sys.Entities;

namespace Sys.Dal
{
        public partial interface ISysActionLogDao
        {

        /// <summary>
        ///  插入SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		long InsertSysActionLog(SysActionLog sysActionLog);
        /// <summary>
        /// 修改SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>状态代码</returns>
        int UpdateSysActionLog(SysActionLog sysActionLog);
        /// <summary>
        /// 删除SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象</param>
        /// <returns>状态代码</returns>
        int DeleteSysActionLog(SysActionLog sysActionLog);
        /// <summary>
        /// 根据主键获取SysActionLog信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysActionLog信息</returns>
        SysActionLog FindByPk(long id);
        /// <summary>
        /// 获取所有SysActionLog信息
        /// </summary>
        /// <returns>SysActionLog列表</returns>
        IList<SysActionLog> GetAll();
        /// <summary>
        /// 取得总记录数
        /// </summary>
        /// <returns>记录数</returns>
        long Count();
        /// <summary>
        ///  批量插入SysActionLog
        /// </summary>
        /// <param name="sysActionLog">SysActionLog实体对象列表</param>
        /// <returns>状态代码</returns>
        bool BulkInsertSysActionLog(IList<SysActionLog> sysActionLogList);
        /// <summary>
        ///  检索SysActionLog，带翻页
        /// </summary>
        /// <param name="obj">SysActionLog实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        IList<SysActionLog> GetListByPage(SysActionLog obj, int pagesize, int pageNo);


        }
}