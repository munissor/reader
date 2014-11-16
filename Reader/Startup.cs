using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using System;
using Microsoft.Practices.Unity;
using Reader.Services;


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

            // Update every 10 minutes
            RecurringJob.AddOrUpdate(() => Update(), "*/10 * * * *");
        }

        public void Update()
        {
            var tasks = UnityConfiguration.Container.Resolve<ITaskService>();
            tasks.UpdateFeeds();
        }
    }
}
