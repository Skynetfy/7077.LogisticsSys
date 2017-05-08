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
    [Table(Name = "SysReceiverInfo")]
    public class SysReceiverInfo : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "OrderId", ColumnType = DbType.Int64)]
        public long OrderId { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "ParcelSingle", ColumnType = DbType.AnsiString, Length = 100)]
        //public string ParcelSingle { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaCityId", ColumnType = DbType.Int64)]
        public long ChinaCityId { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "CityName", ColumnType = DbType.String, Length = 100)]
        public string CityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaAddress", ColumnType = DbType.String, Length = 500)]
        public string ChinaAddress { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ReceiverName", ColumnType = DbType.String, Length = 100)]
        public string ReceiverName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ReceiverPhone", ColumnType = DbType.AnsiString, Length = 50)]
        public string ReceiverPhone { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PackagingWay", ColumnType = DbType.Int32)]
        public int PackagingWay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ExpressWay", ColumnType = DbType.Int32)]
        public int ExpressWay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsDesc", ColumnType = DbType.String, Length = 2000)]
        public string GoodsDesc { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ParcelWeight", ColumnType = DbType.Decimal)]
        public decimal ParcelWeight { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "RealWeight", ColumnType = DbType.Decimal)]
        //public decimal RealWeight { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CourierFees", ColumnType = DbType.Decimal)]
        public decimal CourierFees { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CourierComId", ColumnType = DbType.Int64)]
        public long CourierComId { get; set; }
        public string CourierComName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaCourierNumber", ColumnType = DbType.AnsiString, Length = 50)]
        public string ChinaCourierNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Desc", ColumnType = DbType.String)]
        public string Desc { get; set; }
        // <summary>
        // </summary>
        [Column(Name = "PackagingCosts", ColumnType = DbType.Decimal)]
        public decimal PackagingCosts { get; set; }
}
}
