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
    /// ActivityUser
    /// </summary>
    [Serializable]
    [Table(Name = "SysKuaiDiCom")]
    public class SysKuaiDiCom:BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "ComSn", ColumnType = DbType.AnsiString, Length = 50)]
        public string ComSn { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ComName", ColumnType = DbType.String, Length = 100)]
        public string ComName { get; set; }
    }
}
