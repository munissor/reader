using Microsoft.Practices.Unity;
using Reader.Dal.SqlServer.Configuration;
using Reader.Services;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Reader.Feeds;

namespace Reader
{
    public static class UnityConfiguration
    {
        public static UnityContainer Container { get; private set; }

        public static void Initialize(HttpConfiguration config)
        {
            Container = new UnityContainer();

            Container.RegisterType<DataContext>(new TransientLifetimeManager());

            Container.RegisterType<IFeedParserFactory, FeedParserFactory>();

            Container.RegisterType<ITaskService, TaskService>();
            Container.RegisterType<ISubscriptionService, SubscriptionService>();
            Container.RegisterType<IArticleService, ArticleService>();
            
            DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(Container));
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(Container);
        }
    }
}