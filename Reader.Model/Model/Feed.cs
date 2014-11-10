using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Model
{
    /// <summary>
    /// Model for a feed
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Gets or sets Id of the feed
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Url of the feed
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Title of the feed
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Subtitle of the feed
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the last time the feed was update from the author
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the last time the feed was downloaded from the system
        /// </summary>
        public DateTime LastDownload { get; set; }

        /// <summary>
        /// Gets or sets the last error that was generated when downloading the feed
        /// </summary>
        public string LastDownloadError { get; set; }
    }
}
