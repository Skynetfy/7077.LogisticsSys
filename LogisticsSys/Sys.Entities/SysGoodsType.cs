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
    [Table(Name = "SysGoodsType")]
    public class SysGoodsType : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsType", ColumnType = DbType.String, Length = 200)]
        public string GoodsType { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsTypeDesc", ColumnType = DbType.String, Length = 1000)]
        public string GoodsTypeDesc { get; set; }

        /// <summary>
        /// </summary>
        [Column(Name = "MinWeight", ColumnType = DbType.Decimal)]
        public decimal MinWeight { get; set; }

        /// <summary>
        /// </summary>
        [Column(Name = "PremiumAmount", ColumnType = DbType.Decimal)]
        public decimal PremiumAmount { get; set; }
    }
}
