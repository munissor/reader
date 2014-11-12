using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.DataModel
{
    public class Subscription
    {
        public virtual long Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual long FeedId { get; set; }

        public virtual Feed Feed { get; set; }

        public virtual DateTime SubscriptionDate { get; set; }
    }
}
