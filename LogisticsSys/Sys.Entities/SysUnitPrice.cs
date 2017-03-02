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
    [Table(Name = "SysUnitPrice")]
    public class SysUnitPrice : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "RCityId", ColumnType = DbType.Int32)]
        public int RCityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsTypeId", ColumnType = DbType.Int64)]
        public long GoodsTypeId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LandPrice1", ColumnType = DbType.Decimal)]
        public decimal LandPrice1 { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LandPrice2", ColumnType = DbType.Decimal)]
        public decimal LandPrice2 { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "AirPrice1", ColumnType = DbType.Decimal)]
        public decimal AirPrice1 { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "AirPrice2", ColumnType = DbType.Decimal)]
        public decimal AirPrice2 { get; set; }
    }
}
