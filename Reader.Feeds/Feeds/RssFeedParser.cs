
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Feeds
{
    public class RssFeedParser : XmlFeedParserBase, IFeedParser
    {
        private XmlNode channel;

        public RssFeedParser(Stream stream)
            :base(stream)
        {
            InitRssNameTable();
            this.channel = feed.SelectSingleNode("/rss/channel");
        }

        private void InitRssNameTable()
        {
            namespaceManager.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            namespaceManager.AddNamespace("slash", "http://purl.org/rss/1.0/modules/slash/");
            namespaceManager.AddNamespace("wfw", "http://wellformedweb.org/CommentAPI/");
        }

        public Feed ParseFeedInformation()
        {
            return new Feed(){
                Title = ParseTitle(),
                Subtitle = ParseSubtitle(),
                Language = ParseLanguage(),
                Link = ParseLink(),
                LastUpdate = ParseLastUpdate()
            };
        }

        private string ParseTitle()
        {
            return GetNodeText(channel, "./title");
        }

        private string ParseSubtitle()
        {
            return GetNodeText(channel, "./description");
        }

        private string ParseLanguage()
        {
            return GetNodeText(channel, "./dc:language");
        }

        private string ParseLink()
        {
            return GetNodeText(channel, "./link");
        }

        private DateTime ParseLastUpdate()
        {
            return GetNodeUtcDate(channel, "./lastBuildDate");
        }
               

        public IEnumerable<Article> ParseArticles()
        {
            var items = channel.SelectNodes("./item");
            foreach (XmlNode item in items)
            {
                yield return ParseArticle(item);
            }
        }

        private Article ParseArticle(XmlNode item)
        {
            return new Article() {
                Title = GetNodeText(item, "./title"),
                Guid = GetNodeText(item, "./guid[not(@isPermalink) or @isPermalink=false]"),
                Link = GetNodeText(item, "./link"),
                PublicationDate = GetNodeUtcDate(item, "./pubDate"),
                Description = GetNodeText(item, "./description"),
                Authors = ParseAuthors(item),
                Categories = ParseCategories(item)
            };
        }

        private IList<Author> ParseAuthors(XmlNode item)
        {
            var authors = GetNodeTextList(item, "./dc:creator");
            return authors.Select(x => new Author()
            {
                Name = x,
            }).ToList();
        }

        private IList<Category> ParseCategories(XmlNode item)
        {
            var categories = GetNodeTextList(item, "./category");
            return categories.Select(x => new Category()
            {
                Name = x,
            }).ToList();
        }
    }
}
