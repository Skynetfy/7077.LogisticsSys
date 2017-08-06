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
    [Table(Name = "SysAddresserInfo")]
    public class SysAddresserInfo : BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "OrderId", ColumnType = DbType.Int64)]
        public long OrderId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LogisticsSingle", ColumnType = DbType.AnsiString,Length = 100)]
        public string LogisticsSingle { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaCityId", ColumnType = DbType.Int64)]
        public long RussiaCityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CityName", ColumnType = DbType.String, Length = 500)]
        public string CityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "RussiaAddress", ColumnType = DbType.String, Length = 500)]
        public string RussiaAddress { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "CargoNumber", ColumnType = DbType.Int32)]
        //public int CargoNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PickupDate", ColumnType = DbType.DateTime)]
        public DateTime PickupDate { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "PickupWay", ColumnType = DbType.Int32)]
        //public int PickupWay { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsType", ColumnType = DbType.Int32)]
        public int GoodsType { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "GoodsTypeName", ColumnType = DbType.String, Length = 500)]
        public string GoodsTypeName { get; set; }
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
        //[Column(Name = "GoodsVolumeWeight", ColumnType = DbType.Decimal)]
        //public decimal GoodsVolumeWeight { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "GoodsRealWeight", ColumnType = DbType.Decimal)]
        //public decimal GoodsRealWeight { get; set; }
        /// <summary>
        /// </summary>
        //[Column(Name = "AddressRealPrice", ColumnType = DbType.Decimal)]
        //public decimal AddressRealPrice { get; set; }
        ///// <summary>
        ///// </summary>
        //[Column(Name = "BoxLong", ColumnType = DbType.Decimal)]
        //public decimal BoxLong { get; set; }
        ///// <summary>
        ///// </summary>
        //[Column(Name = "BoxWidth", ColumnType = DbType.Decimal)]
        //public decimal BoxWidth { get; set; }
        ///// <summary>
        ///// </summary>
        //[Column(Name = "BoxHeight", ColumnType = DbType.Decimal)]
        //public decimal BoxHeight { get; set; }
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
        [Column(Name = "ArriveValueRuble", ColumnType = DbType.Decimal)]
        public decimal ArriveValueRuble { get; set; }
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
        [Column(Name = "InsuranceCost", ColumnType = DbType.Decimal)]
        public decimal InsuranceCost { get; set; }
    }
}
