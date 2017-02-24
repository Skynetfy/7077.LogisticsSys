using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common
{
    public class BootstrapTableData<T> where T : class
    {
        public int total { get; set; }
        public IList<T> rows { get; set; }
    }
}
