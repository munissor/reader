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
                           select a;

            if (!string.IsNullOrWhiteSpace(subscriptionId))
            {
                long sid = long.Parse(subscriptionId);
                long fid = (from s in Context.Set<Subscription>()
                            where s.Id == sid
                            select s.FeedId).First();

                articles = articles.Where(x => x.FeedId == fid);
            }

            if (!string.IsNullOrWhiteSpace(lastArticleId))
            {
                long aid = long.Parse(lastArticleId);
                articles = articles.Where(x => x.Id > aid);
            }

            var data = articles.Take(count).ToList();
            var res = data.Select(x => new Model.Article()
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Link = x.Link,
                PublicationDate = x.PublicationDate,
                Categories = x.Categories.Select(c => new Model.Category() { Name = c.Name }).ToArray(),
                Authors = x.Authors.Select(a => new Model.Author() { Name = a.Name, Email = a.Email }).ToArray()
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
    }
}
