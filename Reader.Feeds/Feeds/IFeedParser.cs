using Reader.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    /// <summary>
    /// A feed parser
    /// </summary>
    public interface IFeedParser
    {
        /// <summary>
        /// Gets information about the feed.
        /// </summary>
        /// <returns>The feed information</returns>
        Feed ParseFeedInformation();

        /// <summary>
        /// Gets the articles in the feed.
        /// </summary>
        /// <returns>The list of articles</returns>
        IEnumerable<Article> ParseArticles();
    }
}
