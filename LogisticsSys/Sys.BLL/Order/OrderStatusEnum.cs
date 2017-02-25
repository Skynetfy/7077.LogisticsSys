using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL.Order
{
    public enum OrderStatusEnum
    {
        [Description("待处理")]
        Processing = 0,

        [Description("已处理")]
        Processed = 1,
    }
}
