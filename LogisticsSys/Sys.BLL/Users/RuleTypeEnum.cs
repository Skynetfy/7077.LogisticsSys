using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL.Users
{
    //角色分类
    public enum RuleTypeEnum
    {
        //待定
        None = 0,

        //管理员
        Admin = 1,

        //客户
        Customer = 2,

        //代理商
        Agents = 3
    }
}
