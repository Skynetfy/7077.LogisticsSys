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
    /// SysActionLog
    /// </summary>
    [Serializable]
    [Table(Name = "SysOrderPayInfo")]
    public class SysOrderPayInfo:BaseEntity
    {

        /// <summary>
        /// </summary>
        [Column(Name = "OrderId", ColumnType = DbType.Int64)]
        public long OrderId { get; set; }

        /// <summary>
        /// </summary>
        [Column(Name = "Type", ColumnType = DbType.Int32)]
        public int Type { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CardNumber", ColumnType = DbType.AnsiString,Length = 50)]
        public string CardNumber { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PayUserName", ColumnType = DbType.String, Length = 50)]
        public string PayUserName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "PayAmount", ColumnType = DbType.Decimal)]
        public decimal PayAmount { get; set; }
    }
}
