using Reader.Dal.SqlServer.Configuration;
using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class SubscriptionService : ServiceBase, ISubscriptionService
    {
        ITaskService taskService;

        public SubscriptionService(DataContext context, ITaskService taskService)
            :base(context)
        {
            this.taskService = taskService;
        }

        public IList<Model.Subscription> Get(string userId)
        {
            var res = from s in Context.Set<Subscription>()
                    join f in Context.Set<Feed>() on s.FeedId equals f.Id
                    //join a in context.Set<Article>() on f.Id equals a.FeedId
                    where s.UserId == userId
                    select new Model.Subscription() { 
                        Id = s.Id.ToString(),
                        FeedId = f.Id.ToString(),
                        UserId = s.UserId,
                        SubscriptionDate = s.SubscriptionDate,
                        Title = f.Title,
                        Url = f.Url
                    };

            return res.ToList();
        }


        public bool Post(string userId, Model.Subscription model)
        {
            // check if the user is already subscribed to the feed
            var subscribed = (from s in Context.Set<Subscription>()
                              join f in Context.Set<Feed>() on s.FeedId equals f.Id
                            where s.UserId == userId
                                && f.Url == model.Url
                            select s).Any();

            // if he is not
            if(!subscribed )
            {
                // see if the feed is already registered into the system
                var feed = Context.Set<Feed>().FirstOrDefault(x => x.Url == model.Url);
                
                // if not let's add it
                if (feed == null)
                {
                    feed = Context.Set<Feed>().Add(new Feed() { Url = model.Url, Title = string.Empty });
                    var subscription = new Subscription()
                    {
                        UserId = userId,
                        SubscriptionDate = DateTime.UtcNow,
                        Feed = feed
                    };

                    Context.Set<Subscription>().Add(subscription);

                    Context.SaveChanges();

                    taskService.UpdateFeed(feed.Id);
                }
                else 
                {
                    var subscription = new Subscription()
                    {
                        UserId = userId,
                        SubscriptionDate = DateTime.UtcNow,
                        Feed = feed
                    };

                    Context.Set<Subscription>().Add(subscription);

                    Context.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public bool Delete(string userId, string subscriptionId)
        {
            var sid = long.Parse(subscriptionId);
            var sub = Context.Set<Subscription>()
                .FirstOrDefault(x => x.UserId == userId && x.Id == sid);

            if (sub != null) 
            {
                Context.Set<Subscription>().Remove(sub);
                Context.SaveChanges();
                return true;
            }

            return false;
        } 
    }
}
