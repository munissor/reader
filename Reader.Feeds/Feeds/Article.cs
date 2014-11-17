using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    /// <summary>
    /// An article.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the url
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the date when the article was published
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the article was updates.
        /// </summary>
        /// <remarks>
        /// If the article was never updated, the UpdateDate will be the same as the PublicationDate.
        /// </remarks>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the list of authors.
        /// </summary>
        public IList<Author> Authors{get; set;}

        /// <summary>
        /// Gets or sets the content of the article.
        /// </summary>
        public string Description{get; set;}

        /// <summary>
        /// Gets or sets the list of categories.
        /// </summary>
        public IList<Category> Categories {get; set;}
    }
}
