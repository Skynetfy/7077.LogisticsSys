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
        public string UserName { get; set; }

        public string DisplayName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "AgentCityId",ColumnType=DbType.Int64)]
        public long AgentCityId { get; set; }
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