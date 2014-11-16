using Reader.Dal.SqlServer.Configuration;
using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            if (feed.LastUpdate == DateTime.MinValue)
            {
                var lastArticle = articles.OrderByDescending(x => x.UpdateDate).FirstOrDefault();
                if (lastArticle == null)
                    feed.LastUpdate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                else
                    feed.LastUpdate = lastArticle.UpdateDate;
            }
            if (feed.LastUpdate == DateTime.MinValue)
            {
                feed.LastUpdate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            }

            feed.Subtitle = newFeedInfo.Subtitle;
            feed.Title = newFeedInfo.Title;

            var guids = articles.Select(x => x.Guid);

            // Tries to find existing articles with the guids we loaded
            var existingGuids = (from a in Context.Set<Article>()
                                 where guids.Contains(a.Guid)
                                      && a.FeedId == feedId
                                 select a.Guid).ToList();

            // removes from new articles the one we already have
            articles.RemoveAll(x => existingGuids.Contains(x.Guid));

            var dataArticles = articles.Select(x => ConvertArticle(feed, x));

            Context.Set<Article>().AddRange(dataArticles);

            Context.SaveChanges();
        }

        public Article ConvertArticle(Feed feed, Reader.Feeds.Article article)
        {
            var model = new Article()
            {
                Title = article.Title,
                Guid = article.Guid,
                Link = article.Link,
                PublicationDate = article.PublicationDate == DateTime.MinValue ? System.Data.SqlTypes.SqlDateTime.MinValue.Value : article.PublicationDate,
                UpdateDate = article.UpdateDate == DateTime.MinValue ? System.Data.SqlTypes.SqlDateTime.MinValue.Value : article.UpdateDate,
                Content = article.Description,

                Feed = feed
            };

            model.Authors = article.Authors.Select(x => new Author()
            {
                Name = x.Name,
                Email = x.Email,
                Article = model,
            }).ToList();

            model.Categories = article.Categories.Select(x => new Category()
            {
                Name = x.Name,
            }).ToList();

            return model;
        }



        public void UpdateFeeds()
        {
            // Update the 100 oldest feeds
            var feeds = (from f in Context.Set<Feed>()
                        orderby f.LastDownload ascending
                        select f.Id).Take(100).ToList();

            foreach (var feedId in feeds) 
            {
                UpdateFeed(feedId);
            }
        }
    }
}
