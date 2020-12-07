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
    [Table(Name = "SysOrderTrade")]
    public class SysOrderTrade : BaseEntity
    {
        [Column(Name = "UserId", ColumnType = DbType.Int64)]
        public Int64 UserId { get; set; }

        [Column(Name = "OrderId", ColumnType = DbType.Int64)]
        public Int64 OrderId { get; set; }

        [Column(Name = "Amount", ColumnType = DbType.Decimal)]
        public decimal Amount { get; set; }

        [Column(Name = "RealAmount", ColumnType = DbType.Decimal)]
        public decimal RealAmount { get; set; }

        [Column(Name = "TradeNo", ColumnType = DbType.AnsiString)]
        public string TradeNo { get; set; }

        [Column(Name = "OutTradeNo", ColumnType = DbType.AnsiString)]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 0:交易中 1:成功 -1:失败
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Int32)]
        public int Status { get; set; }

        [Column(Name = "Rate", ColumnType = DbType.Double)]
        public double Rate { get; set; }

        [Column(Name = "TradeDate", ColumnType = DbType.DateTime)]
        public DateTime TradeDate { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Phone { get; set; }
    }
}
