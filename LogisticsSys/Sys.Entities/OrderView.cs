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
        [Column(Name = "UserId", ColumnType = DbType.Int64)]
        public long UserId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Int32)]
        public int Status { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UserName", ColumnType = DbType.AnsiString, Length = 50)]
        public string UserName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "DisplayName", ColumnType = DbType.String, Length = 150)]
        public string DisplayName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "OrderRealPrice", ColumnType = DbType.Decimal)]
        public decimal OrderRealPrice { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Bid", ColumnType = DbType.Int64)]
        public long Bid { get; set; }
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
        public string RussiaCityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaAddress", ColumnType = DbType.String, Length = 500)]
        public string RussiaAddress { get; set; }
       
        /// <summary>
        /// </summary>
        [Column(Name = "PickupDate", ColumnType = DbType.DateTime)]
        public DateTime PickupDate { get; set; }
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
        [Column(Name = "OrderFrees", ColumnType = DbType.Decimal)]
        public decimal OrderFrees { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "IsArrivePay", ColumnType = DbType.Boolean)]
        public bool IsArrivePay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ArrivePayValue", ColumnType = DbType.Decimal)]
        public decimal ArrivePayValue { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "IsOutPhoto", ColumnType = DbType.Boolean)]
        public bool IsOutPhoto { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ExchangeRate", ColumnType = DbType.Decimal)]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "WebUrl", ColumnType = DbType.AnsiString,Length = 200)]
        public string WebUrl { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Cid", ColumnType = DbType.Int64)]
        public long Cid { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaCityId", ColumnType = DbType.Int64)]
        public long ChinaCityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ChinaCityName", ColumnType = DbType.String, Length = 100)]
        public string ChinaCityName { get; set; }
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
        /// <summary>
        /// </summary>
        [Column(Name = "CourierFees", ColumnType = DbType.Decimal)]
        public decimal CourierFees { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CourierComId", ColumnType = DbType.Int64)]
        public long CourierComId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PayStatus", ColumnType = DbType.Int32)]
        public int PayStatus { get; set; }
    }
}
