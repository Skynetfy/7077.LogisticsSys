using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Sys.WebUI.Models;

namespace Sys.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender,EventArgs e)
        {
            // Extract the forms authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                // There is no authentication cookie.
                return;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                // Log exception details (omitted for simplicity)
                return;
            }

            if (null == authTicket)
            {
                // Cookie failed to decrypt.
                return;
            }

            // When the ticket was created, the UserData property was assigned a
            // pipe delimited string of role names.
            string[] roles = authTicket.UserData.Split('|');

            // This principal will flow throughout the request.
            var principal = new SysUserPrincipal<string[]>(authTicket, roles);
            // Attach the new principal object to the current HttpContext object
            Context.User = principal;
        }
    }
}
