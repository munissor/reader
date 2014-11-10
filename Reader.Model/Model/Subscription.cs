using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Model
{
    public class Subscription
    {
        public string UserId { get; set; }

        public string FeedId { get; set; }

        public DateTime SubscriptionDate { get; set; }
    }
}
