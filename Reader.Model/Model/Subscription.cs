using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Model
{
    public class Subscription
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FeedId { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public long UnreadCount { get; set; }
    }
}
