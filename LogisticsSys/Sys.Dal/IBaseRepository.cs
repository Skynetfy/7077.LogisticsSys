using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface IBaseRepository<T> where T : new()
    {
        long Insert(T entity);

        int Update(T entity);

        int Delete(T entity);

        T FindByPk(long id);

        IList<T> GetAll();
        long Count();

        bool BulkInsert(IList<T> list);
        
        int GetPagerCount(string search);

        IList<T> GetPagerList(string search, int offset, int limit, string order, string sort);
    }
}
