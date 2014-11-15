using Reader.Dal.SqlServer.Configuration;
using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFeedParserFactory = Reader.Feeds.IFeedParserFactory;

namespace Reader.Services
{
    public class TaskService : ServiceBase, ITaskService
    {
        IFeedParserFactory parserFactory;

        public TaskService(DataContext context, IFeedParserFactory parserFactory)
            :base(context)
        {
            this.parserFactory = parserFactory;
        }

        public void UpdateFeed(long feedId)
        {
            var feed = Context.Set<Feed>().FirstOrDefault(x => x.Id == feedId);

            var parser = parserFactory.CreateParserFromUrl(feed.Url);

            var newFeedInfo = parser.ParseFeedInformation();
            var articles = parser.ParseArticles().ToList();

            // Updates the feed with the last information
            feed.LastDownload = DateTime.UtcNow;
            feed.LastUpdate = newFeedInfo.LastUpdate;
            feed.Subtitle = newFeedInfo.Subtitle;
            feed.Title = newFeedInfo.Title;

            var guids = articles.Select(x => x.Guid);

            // Tries to find existing articles with the guids we loaded
            var existingGuids = from a in Context.Set<Article>()
                                   where guids.Contains(a.Guid)
                                        && a.FeedId == feedId
                                   select a.Guid;

            // removes from new articles the one we already have
            articles.RemoveAll(x => existingGuids.Contains(x.Guid));

            var dataArticles = articles.Select(x => new Article()
            {
                
            });

            Context.Set<Article>().AddRange(dataArticles);
            
            Context.SaveChanges();
        }



        public void UpdateFeeds()
        {
            throw new NotImplementedException();
        }
    }
}
