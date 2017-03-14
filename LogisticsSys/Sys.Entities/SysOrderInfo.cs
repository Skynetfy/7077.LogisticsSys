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
    [Table(Name = "SysOrderInfo")]
    public class SysOrderInfo : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "OrderNo", ColumnType = DbType.AnsiString, Length = 50)]
        public string OrderNo { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ShipperName", ColumnType = DbType.String, Length = 50)]
        public string ShipperName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ShipperPhone", ColumnType = DbType.AnsiString, Length = 50)]
        public string ShipperPhone { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PickupNumber", ColumnType = DbType.Int32)]
        public int PickupNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Int32)]
        public int Status { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "OrderRealPrice", ColumnType = DbType.Decimal)]
        public decimal OrderRealPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PayStatus", ColumnType = DbType.Int32)]
        public int PayStatus { get; set; }
    }
}
