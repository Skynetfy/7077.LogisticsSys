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
        [Description("已删除")]
        Deleted = -1,

        [Description("未提交")]
        UnSubmit = 0,

        [Description("待处理")]
        Processing,

        [Description("已处理")]
        Processed,

        [Description("未发货")]
        Unfilled,

        [Description("已发货")]
        Filled,

        [Description("未付款")]
        UnPaing,

        [Description("已付款")]
        Paied,

        [Description("未收款")]
        UnReceiving,

        [Description("已收款")]
        Received,

        //[Description("未发货")]
        //Unfilled,

        //[Description("已发货 ")]
        //Filled,

        [Description("已完成 ")]
        Successed,

        [Description("已失败 ")]
        Failed
    }
}
