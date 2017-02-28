using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Data.Orm;

namespace Sys.Entities
{
    [Serializable]
    [Table(Name = "v_orderinfo")]
    public class OrderView : BaseEntity
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
        [Column(Name = "UserName", ColumnType = DbType.AnsiString, Length = 50)]
        public string UserName { get; set; }
        [Column(Name = "DisplayName", ColumnType = DbType.String, Length = 150)]
        public string DisplayName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LogisticsSingle", ColumnType = DbType.AnsiString, Length = 100)]
        public string LogisticsSingle { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaCityId", ColumnType = DbType.Int64)]
        public long RussiaCityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaCityName", ColumnType = DbType.String, Length = 100)]
        public long? RussiaCityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaAddress", ColumnType = DbType.String, Length = 500)]
        public string RussiaAddress { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CargoNumber", ColumnType = DbType.Int32)]
        public int CargoNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PickupDate", ColumnType = DbType.DateTime)]
        public DateTime PickupDate { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PickupWay", ColumnType = DbType.Int32)]
        public int PickupWay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsType", ColumnType = DbType.Int32)]
        public int GoodsType { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "TransportationWay", ColumnType = DbType.Int32)]
        public int TransportationWay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ProtectPrice", ColumnType = DbType.Decimal)]
        public decimal ProtectPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PolicyFee", ColumnType = DbType.Decimal)]
        public decimal PolicyFee { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsWeight", ColumnType = DbType.Decimal)]
        public decimal GoodsWeight { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "BoxLong", ColumnType = DbType.Decimal)]
        public decimal BoxLong { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "BoxWidth", ColumnType = DbType.Decimal)]
        public decimal BoxWidth { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "BoxHeight", ColumnType = DbType.Decimal)]
        public decimal BoxHeight { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ParcelSingle", ColumnType = DbType.AnsiString, Length = 100)]
        public string ParcelSingle { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaCityId", ColumnType = DbType.Int64)]
        public long ChinaCityId { get; set; }
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
        [Column(Name = "ChinaCourierNumber", ColumnType = DbType.AnsiString, Length = 50)]
        public string ChinaCourierNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Desc", ColumnType = DbType.String)]
        public string Desc { get; set; }
    }
}
