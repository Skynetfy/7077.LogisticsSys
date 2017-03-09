using System;
using System.Collections.Generic;
using Sys.Entities;

namespace Sys.Dal
{
        public partial interface ISysAgentInfoDao
        {

        /// <summary>
        ///  插入SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		long InsertSysAgentInfo(SysAgentInfo sysAgentInfo);
        /// <summary>
        /// 修改SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>状态代码</returns>
        int UpdateSysAgentInfo(SysAgentInfo sysAgentInfo);
        /// <summary>
        /// 删除SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象</param>
        /// <returns>状态代码</returns>
        int DeleteSysAgentInfo(SysAgentInfo sysAgentInfo);
        /// <summary>
        /// 根据主键获取SysAgentInfo信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysAgentInfo信息</returns>
        SysAgentInfo FindByPk(long id);
        /// <summary>
        /// 获取所有SysAgentInfo信息
        /// </summary>
        /// <returns>SysAgentInfo列表</returns>
        IList<SysAgentInfo> GetAll();
        /// <summary>
        /// 取得总记录数
        /// </summary>
        /// <returns>记录数</returns>
        long Count();
        /// <summary>
        ///  批量插入SysAgentInfo
        /// </summary>
        /// <param name="sysAgentInfo">SysAgentInfo实体对象列表</param>
        /// <returns>状态代码</returns>
        bool BulkInsertSysAgentInfo(IList<SysAgentInfo> sysAgentInfoList);
        /// <summary>
        ///  检索SysAgentInfo，带翻页
        /// </summary>
        /// <param name="obj">SysAgentInfo实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        IList<SysAgentInfo> GetListByPage(SysAgentInfo obj, int pagesize, int pageNo);


        }
}