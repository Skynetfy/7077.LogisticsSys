using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sys.WebUI.Startup))]
namespace Sys.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
