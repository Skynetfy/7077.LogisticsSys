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
    [Table(Name = "SysExchange")]
    public partial class SysExchange : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "ExchangeValue", ColumnType = DbType.Single)]
        public double ExchangeValue { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CurrentDate", ColumnType = DbType.DateTime)]
        public DateTime CurrentDate { get; set; }
    }
}
