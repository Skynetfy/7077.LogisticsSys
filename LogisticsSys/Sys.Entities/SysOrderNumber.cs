using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data.Orm;

namespace Sys.Entities
{
    [Serializable]
    [Table(Name = "SysOrderNumber")]
    public class SysOrderNumber:BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "OrderId", ColumnType = DbType.Int64)]
        public long OrderId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Number", ColumnType = DbType.AnsiString,Length = 50)]
        public string Number { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Boolean)]
        public bool Status { get; set; }
       
    }
}
