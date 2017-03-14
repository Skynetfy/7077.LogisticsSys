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
    [Table(Name = "SysChinaCity")]
    public class SysChinaCity : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "CityName", ColumnType = DbType.String, Length = 100)]
        public string CityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UnitPrice", ColumnType = DbType.Decimal)]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ExpressBeavyPrice", ColumnType = DbType.Decimal)]
        public decimal ExpressBeavyPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "SfUnitPrice", ColumnType = DbType.Decimal)]
        public decimal SfUnitPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "SflogisticsBeavyPrice", ColumnType = DbType.Decimal)]
        public decimal SflogisticsBeavyPrice { get; set; }
    }
}
