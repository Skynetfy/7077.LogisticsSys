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
    /// SysExportDataInfo
    /// </summary>
    [Serializable]
    [Table(Name = "SysExportDataInfo")]
    public partial class SysExportDataInfo : BaseEntity
    {
        /// <summary>
        /// T开头订单号
        /// </summary>
        [Column(Name = "TPARCELNO", ColumnType = DbType.String)]
        public string TPARCELNO { get; set; }
        /// <summary>
        /// 购物网站订单号
        /// </summary>
        [Column(Name = "PARCELNO", ColumnType = DbType.String)]
        public string PARCELNO { get; set; }
        /// <summary>
        /// 购买时实际价格
        /// </summary>
        [Column(Name = "COST", ColumnType = DbType.Decimal)]
        public decimal COST { get; set; }
        /// <summary>
        /// 货币单位
        /// </summary>
        [Column(Name = "CURRENCY", ColumnType = DbType.String)]
        public string CURRENCY { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column(Name = "QUANTITY", ColumnType = DbType.String)]
        public string QUANTITY { get; set; }
        /// <summary>
        /// 购买网址
        /// </summary>
        [Column(Name = "GOODSURL", ColumnType = DbType.String)]
        public String GOODSURL { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Column(Name = "ADDRESS", ColumnType = DbType.String)]
        public String ADDRESS { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Column(Name = "PHONE", ColumnType = DbType.String)]
        public String PHONE { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        [Column(Name = "recipient", ColumnType = DbType.String)]
        public String recipient { get; set; }

        [Column(Name = "FileName", ColumnType = DbType.String)]
        public string FileName { get; set; }
        /// <summary>
        /// 商品名称 规格（中文）
        /// </summary>
        [Column(Name = "ProductName", ColumnType = DbType.String)]
        public string ProductName { get; set; }
        /// <summary>
        /// 身份证号码（下单人或者收件人都行）
        /// </summary>
        [Column(Name = "IdNumber", ColumnType = DbType.String)]
        public string IdNumber { get; set; }
        /// <summary>
        /// 普通快递或者顺丰到付（二选一）
        /// </summary>
        [Column(Name = "Courier", ColumnType = DbType.String)]
        public string Courier { get; set; }
    }
}
