﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    /// <summary>
    /// A feed
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Gets or sets the title of the feed
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle of the feed
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the language of the feed
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the link of the feed
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the last time the feed was update from the author
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
