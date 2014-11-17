using Reader.Dal.SqlServer.Configuration;
using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class ArticleService : ServiceBase, IArticleService
    {
        public ArticleService(DataContext context)
            : base(context)
        {
        }

        public IList<Model.Article> Get(string userId, string subscriptionId, string lastArticleId, int count)
        {
            var articles = from a in Context.Set<Article>()
                           join su in Context.Set<Subscription>() on a.FeedId equals su.FeedId
                           join s in Context.Set<Status>() on a.Id equals s.ArticleId into status
                           orderby a.PublicationDate ascending, a.Id ascending
                           where su.UserId == userId &&
                            (status.FirstOrDefault() == null || status.FirstOrDefault().Read == false)
                           select new { Article = a, Status = status.FirstOrDefault() };

            if (!string.IsNullOrWhiteSpace(subscriptionId))
            {
                long sid = long.Parse(subscriptionId);
                long fid = (from s in Context.Set<Subscription>()
                            where s.Id == sid
                            select s.FeedId).First();

                articles = articles.Where(x => x.Article.FeedId == fid);
            }

            if (!string.IsNullOrWhiteSpace(lastArticleId))
            {
                long aid = long.Parse(lastArticleId);
                articles = articles.Where(x => x.Article.Id > aid);
            }

            var data = articles.Take(count).ToList();

            var res = data.Select(x => new Model.Article()
            {
                Id = x.Article.Id.ToString(),
                Title = x.Article.Title,
                Link = x.Article.Link,
                PublicationDate = x.Article.PublicationDate,
                Categories = x.Article.Categories.Select(c => new Model.Category() { Name = c.Name }).ToArray(),
                Authors = x.Article.Authors.Select(a => new Model.Author() { Name = a.Name, Email = a.Email }).ToArray(),
                Read = x.Status == null ? false : x.Status.Read,
                Starred = x.Status == null ? false : x.Status.Starred
            }).ToList();

            return res;
        }

        public Model.ArticleDetail Get(string articleId)
        {
            var aid = long.Parse(articleId);
            var a = Context.Set<Article>().FirstOrDefault(x => x.Id == aid);
            Model.ArticleDetail res = null;
            if (a != null)
            {
                res = new Model.ArticleDetail()
                {
                    Content = a.Content
                };
            }
            return res;
        }


        public void Post(string userId, Model.Article article)
        {
            var aid = long.Parse(article.Id);
            // we get the actual article from the DB, don't trust data coming from outside
            var realArticle = Context.Set<Article>().FirstOrDefault(x => x.Id == aid);
            if (realArticle != null)
            {
                // we need to check that the user is subscribed to the feed
                var subscription = Context.Set<Subscription>().FirstOrDefault(x => x.FeedId == realArticle.FeedId && x.UserId == userId);

                if (subscription != null)
                {
                    var status = Context.Set<Status>().FirstOrDefault(x => x.SubscriptionId == subscription.Id && x.ArticleId == realArticle.Id);
                    if (status == null)
                    {
                        status = new Status()
                        {
                            SubscriptionId = subscription.Id,
                            ArticleId = realArticle.Id,
                            Read = article.Read,
                            ReadDate = article.Read ? DateTime.UtcNow : MinSqlDate,
                            Starred = article.Starred,
                            StarredDate = article.Starred ? DateTime.UtcNow : MinSqlDate
                        };
                        Context.Set<Status>().Add(status);
                    }
                    else
                    {
                        status.Read = article.Read;
                        status.ReadDate = article.Read ? DateTime.UtcNow : MinSqlDate;
                        status.Starred = article.Starred;
                        status.StarredDate = article.Starred ? DateTime.UtcNow : MinSqlDate;
                    }

                    Context.SaveChanges();
                }
            }
        }
    }
}
