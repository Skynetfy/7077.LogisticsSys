using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sys.BLL;
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
                MenusEnum menu = (MenusEnum)Enum.Parse(typeof(MenusEnum), d.Key);
                var url = menu.GetDescription();
                var userprovider = new UserLoginProvider();
                var _user = userprovider.GetUser(HttpContext.Current.User.Identity.Name);
                if (_user.RuleType.Equals(RuleTypeEnum.Customer.ToString()))
                {
                    if (d.Key.Equals(MenusEnum.订单管理.ToString()))
                        sb.AppendLine(" <li role=\"presentation\"><a href=\"order/index\">" + d.Key + "</a></li>");
                    else
                        sb.AppendLine(" <li role=\"presentation\"><a href=\"" + url + "\">" + d.Key + "</a></li>");
                }
                else
                    sb.AppendLine(" <li role=\"presentation\"><a href=\"" + url + "\">" + d.Key + "</a></li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString LeftMeunHtmlString(this HtmlHelper htmlhelper, string parent, string current, string username)
        {
            var menus = new UserLoginProvider().GetMenusByUserName(username);
            StringBuilder sb = new StringBuilder();
            var _menus = menus.FirstOrDefault(x => x.Key.Equals(parent, StringComparison.OrdinalIgnoreCase));
            if (_menus.Key != null)
                foreach (var d in _menus.Value)
                {
                    MenusEnum menu = (MenusEnum)Enum.Parse(typeof(MenusEnum), d.Key);
                    var url = menu.GetDescription();
                    if (current.Equals(d.Key, StringComparison.OrdinalIgnoreCase))
                        sb.AppendLine(" <li><a href=\"" + url + "\" class=\"current\">" + d.Key + "</a></li>");
                    else
                        sb.AppendLine(" <li><a href=\"" + url + "\">" + d.Key + "</a></li>");
                }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString BindingSelectHtmlString(this HtmlHelper html, List<SelectBinding> data)
        {
            StringBuilder sb = new StringBuilder();
            if (data != null)
                foreach (var option in data)
                {
                    var str = "";
                    if (option.Selected)
                        str = " selected='selected' ";
                    sb.AppendLine(string.Format("<option value='{0}' {2}>{1}</option>", option.Value, option.Text, str));
                }
            return MvcHtmlString.Create(sb.ToString());
        }


        public static MvcHtmlString BindingCheckboxHtmlString(this HtmlHelper html, List<CheckBoxBinding> data, string name)
        {
            StringBuilder sb = new StringBuilder();
            if (data != null)
                foreach (var option in data)
                {
                    var str = "";
                    if (option.Checked)
                        str = " Checked ";
                    sb.AppendLine(string.Format("<input type='checkbox' value='{0}' name='{1}' {2}/>{3}", option.Value, name, str, option.Text));
                }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString BindingOrderStatusSelectHtmlString(this HtmlHelper html)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<option value='' selected>全部</option>");
            sb.AppendLine("<option value='1'>待处理</option>");
            sb.AppendLine("<option value='2'>已处理</option>");
            sb.AppendLine("<option value='3'>未付款</option>");
            sb.AppendLine("<option value='4'>已付款</option>");
            sb.AppendLine("<option value='5'>未收款</option>");
            sb.AppendLine("<option value='6'>已收款</option>");
            sb.AppendLine("<option value='7'>未发货</option>");
            sb.AppendLine("<option value='8'>已发货</option>");
            sb.AppendLine("<option value='9'>已完成</option>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString LetuLinkHtmlString(this HtmlHelper html, string userName)
        {
            var userProvider = new UserLoginProvider();
            var _user = userProvider.GetUser(userName);
            var cusmer = UserService.GetCustomerByUid(_user.Id);
            var email = _user.Email;
            var firstName = _user.DisplayName?.Substring(0, 1);
            var lastName = cusmer?.CustomerID;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" <a href=\"https://www.letu.ru/register?email={email}&firstname={firstName}&lastname={lastName}\" target=\"_blank\">");
            sb.AppendLine("<img src=\"/images/letu.png\" style=\"width:100%;margin-top:10px;\"/>");
            sb.AppendLine("</a>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}