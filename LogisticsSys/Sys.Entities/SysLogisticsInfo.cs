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
    [Table(Name = "SysLogisticsInfo")]
    public class SysLogisticsInfo:BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "LogisticsSingle", ColumnType = DbType.AnsiString, Length = 50)]
        public string LogisticsSingle { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LogisticsDesc", ColumnType = DbType.String, Length = 1000)]
        public string LogisticsDesc { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Boolean)]
        public bool Status { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UpdateDate", ColumnType = DbType.DateTime)]
        public DateTime UpdateDate { get; set; }

    }
}
