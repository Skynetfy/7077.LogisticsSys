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
        [Description("Home/Contact")]
        主页,
        [Description("Order/index")]
        订单管理,
        [Description("Home/index")]
        创建订单,
        [Description("Home/index")]
        订单列表,
        [Description("Logistics/index")]
        物流管理,
        [Description("System/RussiaCity")]
        俄罗斯城市管理,
        [Description("System/ChinaCity")]
        国内城市管理,
        [Description("Home/index")]
        物流查询,
        [Description("Order/OrderManage")]
        订单处理,
        [Description("System/GoodsType")]
        货物类型维护,
        [Description("System/UnitPrice")]
        单价信息维护,
        [Description("Home/index")]
        发货管理,
        [Description("Home/index")]
        物流进度更新,
        [Description("System/RussiaCity")]
        系统维护,
        [Description("User/Profile")]
        个人中心,
        [Description("User/index")]
        用户管理,
        [Description("User/Profile")]
        个人信息,
        [Description("Home/index")]
        密码修改
    }
}
