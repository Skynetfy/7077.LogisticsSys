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
    [Table(Name = "SysUser")]
    public class SysUser: BaseEntity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "UserName", ColumnType = DbType.AnsiString, Length = 50)]
        public string UserName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Password", ColumnType = DbType.AnsiString, Length = 50)]
        public string Password { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Email", ColumnType = DbType.AnsiString, Length = 100)]
        public string Email { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Status", ColumnType = DbType.Int32)]
        public int Status { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "Phone", ColumnType = DbType.AnsiString, Length = 50)]
        public string Phone { get; set; }
    }
}
