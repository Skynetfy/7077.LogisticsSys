using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL.Users
{
    [Flags]
    public enum MenusEnum
    {
        [Description("Home/index")]
        主页,
        [Description("Home/index")]
        订单管理,
        [Description("Home/index")]
        创建订单,
        [Description("Home/index")]
        订单列表,
        [Description("Home/index")]
        物流管理,
        [Description("Home/index")]
        城市管理,
        [Description("Home/index")]
        物流查询,
        [Description("Home/index")]
        订单处理,
        [Description("Home/index")]
        货物类型维护,
        [Description("Home/index")]
        个人中心
    }
}
