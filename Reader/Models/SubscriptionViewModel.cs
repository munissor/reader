using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reader.Models
{
    public class SubscriptionViewModel
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