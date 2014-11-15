using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using System;


[assembly: OwinStartupAttribute(typeof(Reader.Startup))]
namespace Reader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage("DefaultConnection");
                config.UseServer();
            });
        }
    }
}
