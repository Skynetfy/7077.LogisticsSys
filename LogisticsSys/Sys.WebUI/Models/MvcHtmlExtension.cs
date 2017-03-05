using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sys.BLL.Users;
using Sys.Common;

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

        public static MvcHtmlString BindingSelectHtmlString(this HtmlHelper html, List<SelectBinding> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var option in data)
            {
                var str = "";
                if(option.Selected)
                    str = " selected='selected' ";
                sb.AppendLine(string.Format("<option value='{0}' {2}>{1}</option>",option.Value,option.Text, str));
            }
            return MvcHtmlString.Create(sb.ToString());
        }


        public static MvcHtmlString BindingCheckboxHtmlString(this HtmlHelper html, List<CheckBoxBinding> data,string name)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var option in data)
            {
                var str = "";
                if (option.Checked)
                    str = " Checked ";
                sb.AppendLine(string.Format("<input type='checkbox' value='{0}' name='{1}' {2}/>{3}", option.Value, name, str,option.Text));
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}