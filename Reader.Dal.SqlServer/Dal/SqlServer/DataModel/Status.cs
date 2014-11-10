using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.DataModel
{
    public class Status
    {
        public virtual long Id { get; set; }

        public virtual long SubscriptionId { get; set; }

        public virtual long ArticleId { get; set; }

        public virtual bool Read { get; set; }

        public virtual DateTime ReadDate { get; set; }

        public virtual bool Starred { get; set; }

        public virtual DateTime StarredDate { get; set; }
    }
}
