using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data;
using Sys.Entities;

namespace Sys.Dal.Repository
{
    public class SysExportDataInfoRepository: ISysExportDataInfoRepository
    {
        readonly BaseDao baseDao = BaseDaoFactory.CreateBaseDao("DefaultConStr");

        public bool BulkInsert(IList<SysExportDataInfo> list)
        {
            try
            {
                return baseDao.BulkInsert<SysExportDataInfo>(list);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问BulkInsert时出错", ex);
            }
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(SysExportDataInfo entity)
        {
            throw new NotImplementedException();
        }

        public SysExportDataInfo FindByPk(long id)
        {
            throw new NotImplementedException();
        }

        public IList<SysExportDataInfo> GetAll()
        {
            try
            {
                return baseDao.GetAll<SysExportDataInfo>();
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLogDao时，访问GetAll时出错", ex);
            }
        }

        public int GetPagerCount(string search)
        {
            try
            {
                String sql = string.Format(@"SELECT count(1) from [SysExportDataInfo]  with (nolock) where 1=1 {0} ", search);
                object obj = baseDao.ExecScalar(sql);
                int ret = Convert.ToInt32(obj);
                return ret;
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问Count时出错", ex);
            }
        }

        public IList<SysExportDataInfo> GetPagerList(string search, int offset, int limit, string order, string sort)
        {
            try
            {
                String sql = string.Format(@"SELECT TOP {1} * from [SysExportDataInfo](nolock) where Id not in(
                  SELECT TOP {4} Id FROM [SysExportDataInfo](NOLOCK)
                  WHERE 1=1 {0} ORDER BY {2} {3}) {0} ORDER BY {2} {3} ", search, limit, sort, order, offset);
                return baseDao.SelectList<SysExportDataInfo>(sql);
            }
            catch (Exception ex)
            {
                throw new DalException("调用ActivityDirectRulesDao时，访问GetAll时出错", ex);
            }
        }

        public long Insert(SysExportDataInfo entity)
        {
            try
            {
                Object result = baseDao.Insert<SysExportDataInfo>(entity);
                long iReturn = Convert.ToInt64(result);
                return iReturn;
            }
            catch (Exception ex)
            {
                throw new DalException("调用SysActionLog时，访问Insert时出错", ex);
            }
        }

        public int Update(SysExportDataInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
