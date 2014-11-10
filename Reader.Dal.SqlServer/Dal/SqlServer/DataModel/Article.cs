using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.DataModel
{
    /// <summary>
    /// Model for an article
    /// </summary>
    public class Article
    {
        public virtual long Id { get; set; }

        public virtual long FeedId { get; set; }

        public virtual Feed Feed { get; set; }

        public virtual string Guid { get; set; }

        public virtual string Title { get; set; }

        public virtual string Content { get; set; }

        public virtual string Link { get; set; }

        public virtual DateTime PublicationDate { get; set; }

        public virtual DateTime UpdateDate { get; set; }

        public virtual IList<Author> Authors { get; set; }

        public virtual IList<Category> Categories { get; set; }
    }
}
