using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using Sys.BLL;

[assembly: OwinStartupAttribute(typeof(Sys.WebUI.Startup))]
namespace Sys.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OrderSendEmailManager>().As<ISendEmailManager>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            ConfigureAuth(app);
        }
    }
}
