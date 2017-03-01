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

        [Description("未付款")]
        UnPaing,

        [Description("已付款")]
        Paied,

        [Description("未发货")]
        Unfilled,

        [Description("已发货 ")]
        Filled,

        [Description("已完成 ")]
        Successed,

        [Description("已失败 ")]
        Failed
    }
}
