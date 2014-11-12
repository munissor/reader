using Reader.Dal.SqlServer.Configuration;
using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private DataContext context;

        public SubscriptionService(DataContext context)
        {
            this.context = context;
        }

        public IList<Model.Subscription> Get(string userId)
        {
            var res = from s in context.Set<Subscription>()
                    join f in context.Set<Feed>() on s.FeedId equals f.Id
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


        public void Post(string userId, Model.Subscription model)
        {
            // check if the user is already subscribed to the feed
            var subscribed = (from s in context.Set<Subscription>()
                            join f in context.Set<Feed>() on s.FeedId equals f.Id
                            where s.UserId == userId
                            select s).Any();

            // if he is not
            if(!subscribed )
            {
                // see if the feed is already registered into the system
                var feed = context.Set<Feed>().FirstOrDefault( x=> x.Url == model.Url);
                
                // if not let's add it
                if (feed == null) {
                    feed = context.Set<Feed>().Add(new Feed() { Url = model.Url, Title = string.Empty });
                    var subscription = new Subscription()
                    {
                        UserId = userId,
                        SubscriptionDate = DateTime.UtcNow,
                        Feed = feed
                    };

                    context.Set<Subscription>().Add(subscription);

                    context.SaveChanges();

                    // TODO: trigger reading of the feed here
                }
            }
        }
    }
}
