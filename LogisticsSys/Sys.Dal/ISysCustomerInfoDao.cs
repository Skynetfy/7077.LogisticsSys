using System;
using System.Collections.Generic;
using Sys.Entities;

namespace Sys.Dal
{
        public partial interface ISysCustomerInfoDao
        {

        /// <summary>
        ///  插入SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>新增的主键,如果有多个主键则返回第一个主键</returns>
		long InsertSysCustomerInfo(SysCustomerInfo sysCustomerInfo);
        /// <summary>
        /// 修改SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>状态代码</returns>
        int UpdateSysCustomerInfo(SysCustomerInfo sysCustomerInfo);
        /// <summary>
        /// 删除SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象</param>
        /// <returns>状态代码</returns>
        int DeleteSysCustomerInfo(SysCustomerInfo sysCustomerInfo);
        /// <summary>
        /// 根据主键获取SysCustomerInfo信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SysCustomerInfo信息</returns>
        SysCustomerInfo FindByPk(long id);
        /// <summary>
        /// 获取所有SysCustomerInfo信息
        /// </summary>
        /// <returns>SysCustomerInfo列表</returns>
        IList<SysCustomerInfo> GetAll();
        /// <summary>
        /// 取得总记录数
        /// </summary>
        /// <returns>记录数</returns>
        long Count();
        /// <summary>
        ///  批量插入SysCustomerInfo
        /// </summary>
        /// <param name="sysCustomerInfo">SysCustomerInfo实体对象列表</param>
        /// <returns>状态代码</returns>
        bool BulkInsertSysCustomerInfo(IList<SysCustomerInfo> sysCustomerInfoList);
        /// <summary>
        ///  检索SysCustomerInfo，带翻页
        /// </summary>
        /// <param name="obj">SysCustomerInfo实体对象检索条件</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>检索结果</returns>
        IList<SysCustomerInfo> GetListByPage(SysCustomerInfo obj, int pagesize, int pageNo);


        }
}