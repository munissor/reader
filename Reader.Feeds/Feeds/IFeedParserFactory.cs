using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    /// <summary>
    /// Creates feed parsers.
    /// </summary>
    public interface IFeedParserFactory
    {
        /// <summary>
        /// Creates a parser from a url
        /// </summary>
        /// <param name="url">The url to get the feed content from.</param>
        /// <returns>The parser instance.</returns>
        IFeedParser CreateParserFromUrl(string url);

        /// <summary>
        /// Creates a parser from a stream.
        /// </summary>
        /// <param name="feed">The feed stream.</param>
        /// <returns>The parser instance.</returns>
        IFeedParser CreateParser(Stream feed);
    }
}
