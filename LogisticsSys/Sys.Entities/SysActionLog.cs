using System;
using System.Data;
using Arch.Data.Orm;

namespace Sys.Entities
{
    /// <summary>
    /// SysActionLog
    /// </summary>
    [Serializable]
    [Table(Name = "SysActionLog")]
    public partial class SysActionLog
    {
        /// <summary>
        /// </summary>
        [Column(Name = "ActionDate",ColumnType=DbType.DateTime)]
        public DateTime ActionDate { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "ActionDesc",ColumnType=DbType.String,Length=4000)]
        public string ActionDesc { get; set; }
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
        [Column(Name = "LogType",ColumnType=DbType.Int32)]
        public int LogType { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "OrderId",ColumnType=DbType.Int64)]
        public long OrderId { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "UserId",ColumnType=DbType.Int64)]
        public long UserId { get; set; }
    }
}