using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sys.BLL.Users;

namespace Sys.WebUI.Models
{
    public static class MvcHtmlExtension
    {
        public static MvcHtmlString MvcIndexHtmlString(this HtmlHelper htmlhelper, string username)
        {
            var menus = new UserLoginProvider().GetMenusByUserName(username);
            StringBuilder sb = new StringBuilder();
            foreach (var d in menus)
            {
                var a = d.Key.Split('|');
                sb.AppendLine(" <li role=\"presentation\"><a href=\""+a[1]+"\">"+a[0]+"</a></li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}