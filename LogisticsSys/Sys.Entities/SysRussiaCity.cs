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
    [Table(Name = "SysRussiaCity")]
    public class SysRussiaCity
    {
        /// <summary>
        /// </summary>
        [Column(Name = "CityName", ColumnType = DbType.String, Length = 100)]
        public string CityName { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "CityDesc", ColumnType = DbType.String, Length = 1000)]
        public string CityDesc { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "LandTransportTime", ColumnType = DbType.String, Length = 50)]
        public string LandTransportTime { get; set; }
        /// <summary>
        /// </summary>
        [Column(Name = "AirTransportTime", ColumnType = DbType.String, Length = 50)]
        public string AirTransportTime { get; set; }
    }
}
