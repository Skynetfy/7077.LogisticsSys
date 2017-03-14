using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

namespace Sys.WebUI.Models
{
    public class SysUserPrincipal<TUserData> : IPrincipal
    {
        //当前用户实例
        public IIdentity Identity { get; private set; }
        //用户数据
        public TUserData UserData { get; private set; }
        public SysUserPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");

            Identity = new FormsIdentity(ticket);
            UserData = userData;
        }

        //角色验证
        public bool IsInRole(string role)
        {
            var userData = UserData as SysUserPrincipal<TUserData>;
            if (userData == null)
                throw new NotImplementedException();

            return userData.IsInRole(role);
        }

        //用户名验证
        public bool IsInUser(string user)
        {
            var userData = UserData as SysUserPrincipal<TUserData>;
            if (userData == null)
                throw new NotImplementedException();

            return userData.IsInUser(user);
        }


    }
}