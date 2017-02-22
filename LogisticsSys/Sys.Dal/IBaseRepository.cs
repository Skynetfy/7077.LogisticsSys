using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Entities;

namespace Sys.Dal
{
    public partial interface IBaseRepository<T> where T : new()
    {
        int Insert(T entity);

        int Update(T entity);

        int Delete(T entity);

        T FindByPk(long id);

        IList<T> GetAll();
        long Count();

        bool BulkInsert(IList<T> list);
        
        //IList<T> GetPagerData(string sort, string order, int limit, int offset, string search);
    }
}
