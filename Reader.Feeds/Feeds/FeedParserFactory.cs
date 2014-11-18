using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Feeds
{

    public class FeedParserFactory : IFeedParserFactory
    {
        public IFeedParser CreateParserFromUrl(string url)
        {
            using (HttpClient cli = new HttpClient())
            {
                var stream = cli.GetStreamAsync(url).Result;
                return CreateParser(stream);
            }
        }

        public IFeedParser CreateParser(System.IO.Stream feed)
        {
            if (feed == null)
                throw new ArgumentNullException("feed");

            if (!feed.CanRead)
                throw new ArgumentException("feed should be readable", "feed");

            // Open a memory stream so we can move back and forward with the in memory feed
            using(var mem = new MemoryStream())
            {
                feed.CopyTo(mem);

                // reset the Stream at the start
                mem.Seek(0, SeekOrigin.Begin);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(mem);
                var name = xmlDoc.DocumentElement.LocalName;
                
                // reset the Stream at the start
                mem.Seek(0, SeekOrigin.Begin);

                if (name == "feed")
                    return new AtomFeedParser(mem);
                else if (name == "rss")
                    return new RssFeedParser(mem);

                throw new Exception("Unrecognized feed format");
            }

        }
    }
}
