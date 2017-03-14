using System;
using System.Data;
using Arch.Data.Orm;

namespace Sys.Entities
{
    /// <summary>
    /// SysCustomerInfo
    /// </summary>
    [Serializable]
    [Table(Name = "SysCustomerInfo")]
    public partial class SysCustomerInfo
    {
        /// <summary>
        /// </summary>
        [Column(Name = "Address",ColumnType=DbType.String,Length=1000)]
        public string Address { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CityId",ColumnType=DbType.Int64)]
        public long CityId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CreateDate",ColumnType=DbType.DateTime)]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CustomerID",ColumnType=DbType.AnsiString,Length=50)]
        public string CustomerID { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Id",ColumnType=DbType.Int64),ID,PK]
        public long Id { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "IsDelete",ColumnType=DbType.Boolean)]
        public bool IsDelete { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Phone",ColumnType=DbType.AnsiString,Length=50)]
        public string Phone { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "QQNumber",ColumnType=DbType.AnsiString,Length=20)]
        public string QQNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UserId",ColumnType=DbType.Int64)]
        public long UserId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "WebChatNo",ColumnType=DbType.AnsiString,Length=50)]
        public string WebChatNo { get; set; }
    }
}