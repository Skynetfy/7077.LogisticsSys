using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data.Orm;

namespace Sys.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "Id", ColumnType = DbType.Int64), ID, PK]
        public Int64 Id { get; set; }

        /// <summary>
        /// </summary>
        [Column(Name = "CreateDate", ColumnType = DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
