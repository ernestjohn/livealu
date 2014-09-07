using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KudevolveWeb.Startup))]
namespace KudevolveWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //string connectionString = "Endpoint=sb://kudevolvebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tI3+axjKEj2bedolWuzMc7mKEvTeqQppvso0vvxSRHA=";
            //GlobalHost.DependencyResolver.UseServiceBus(connectionString, "Kudevolve");

            app.MapSignalR();
        }
    }
}
