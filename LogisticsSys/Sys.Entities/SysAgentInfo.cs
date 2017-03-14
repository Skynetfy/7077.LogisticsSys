using System;
using System.Data;
using Arch.Data.Orm;

namespace Sys.Entities
{
    /// <summary>
    /// SysAgentInfo
    /// </summary>
    [Serializable]
    [Table(Name = "SysAgentInfo")]
    public partial class SysAgentInfo
    {
        /// <summary>
        /// </summary>
        [Column(Name = "UserName", ColumnType = DbType.AnsiString)]
        public string UserName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "DisplayName", ColumnType = DbType.String)]
        public string DisplayName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Password", ColumnType = DbType.AnsiString)]
        public string Password { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Email", ColumnType = DbType.AnsiString)]
        public string Email { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Int32)]
        public int? Status { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Phone", ColumnType = DbType.AnsiString, Length = 20)]
        public string Phone { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "AgentCityId",ColumnType=DbType.Int64)]
        public long AgentCityId { get; set; }
        [Column(Name = "CityName", ColumnType = DbType.String)]
        public string CityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CreateDate",ColumnType=DbType.DateTime)]
        public DateTime CreateDate { get; set; }
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
        [Column(Name = "QQNumber",ColumnType=DbType.AnsiString,Length=20)]
        public string QQNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UserId",ColumnType=DbType.Int64)]
        public long UserId { get; set; }
    }
}