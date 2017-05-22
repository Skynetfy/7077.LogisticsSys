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
    [Table(Name = "SysDbConfig")]
    public class SysDbConfig : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "Key", ColumnType = DbType.AnsiString,Length = 20)]
        public string Key { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Value", ColumnType = DbType.String, Length = 200)]
        public string Value { get; set; }
    }
}
