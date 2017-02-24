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
    [Table(Name = "SysAgentCityMapping")]
    public class SysAgentCityMapping : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "RCityId", ColumnType = DbType.Int32)]
        public int RCityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UserId", ColumnType = DbType.Int64)]
        public long UserId { get; set; }
    }
}
