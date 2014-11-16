using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    public class Article
    {
        public string Title { get; set; }

        public string Guid { get; set; }

        public string Link { get; set; }

        public DateTime PublicationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public IList<Author> Authors{get; set;}

        public string Description{get; set;}

        public IList<Category> Categories {get; set;}
    }
}
