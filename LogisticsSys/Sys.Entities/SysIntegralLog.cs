using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data.Orm;

namespace Sys.Entities
{
    /// <summary>
    /// SysActionLog
    /// </summary>
    [Serializable]
    [Table(Name = "SysIntegralLog")]
    public class SysIntegralLog:BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "Uid", ColumnType = DbType.Int64)]
        public long Uid { get; set; }

        /// <summary>
        /// </summary>
        [Column(Name = "Type", ColumnType = DbType.Int32)]
        public int Type { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Value", ColumnType = DbType.Int32)]
        public int Value { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Desc", ColumnType = DbType.String,Length = 500)]
        public string Desc { get; set; }
    }
}
