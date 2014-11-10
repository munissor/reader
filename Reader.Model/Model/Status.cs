using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Model
{
    public class Status
    {
        public string SubscriptionId { get; set; }

        public string ArticleId { get; set; }

        public bool Read { get; set; }

        public DateTime ReadDate { get; set; }

        public bool Starred { get; set; }

        public DateTime StarredDate { get; set; }
    }
}
