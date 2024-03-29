﻿using Microsoft.AspNet.SignalR;
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
            string connectionString = "Endpoint=sb://kudevolvelive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hIJkHB97djSnVuprYzMZk2+UXgw517MyWkBd+FdwiqU=";
            GlobalHost.DependencyResolver.UseServiceBus(connectionString, "Kudevolve");

            app.MapSignalR();
        }
    }
}
