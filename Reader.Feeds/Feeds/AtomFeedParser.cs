using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Feeds
{
    /// <summary>
    /// A parser for Atom feeds
    /// </summary>
    public class AtomFeedParser : XmlFeedParserBase, IFeedParser
    {
        private XmlNode root;

        /// <summary>
        /// Initializes a new instance of the <see cref="AtomFeedParser"/> class.
        /// </summary>
        /// <param name="stream">The feed stream.</param>
        internal AtomFeedParser(Stream stream)
            :base(stream)
        {
            InitAtomNameTable();
            this.root = feed.DocumentElement;
        }

        /// <summary>
        /// Initializes the atom name table.
        /// </summary>
        private void InitAtomNameTable()
        {
            namespaceManager.AddNamespace("a", "http://www.w3.org/2005/Atom");
            namespaceManager.AddNamespace("openSearch", "http://a9.com/-/spec/opensearchrss/1.0/");
            namespaceManager.AddNamespace("blogger", "http://schemas.google.com/blogger/2008");
            namespaceManager.AddNamespace("georss", "http://www.georss.org/georss");
            namespaceManager.AddNamespace("gd", "http://schemas.google.com/g/2005");
            namespaceManager.AddNamespace("thr", "http://purl.org/syndication/thread/1.0");
            namespaceManager.AddNamespace("geo", "http://www.w3.org/2003/01/geo/wgs84_pos#");
            namespaceManager.AddNamespace("feedburner", "http://rssnamespace.org/feedburner/ext/1.0");
        }

        /// <summary>
        /// Gets information about the feed.
        /// </summary>
        /// <returns>
        /// The feed information
        /// </returns>
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
            return GetNodeText(root, "./a:title");
        }

        private string ParseSubtitle()
        {
            return GetNodeText(root, "./a:subtitle");
        }

        private string ParseLanguage()
        {
            return null;//GetNodeText(root, "./dc:language");
        }

        private string ParseLink()
        {
            return GetNodeAttribute(root, "href", "./a:link[not(@rel) or @rel='alternate']");
        }

        private DateTime ParseLastUpdate()
        {
            return GetNodeUtcDate(root, "./a:updated");
        }


        /// <summary>
        /// Gets the articles in the feed.
        /// </summary>
        /// <returns>
        /// The list of articles
        /// </returns>
        public IEnumerable<Article> ParseArticles()
        {
            var items = root.SelectNodes("./a:entry", this.namespaceManager);
            foreach (XmlNode item in items)
            {
                yield return ParseArticle(item);
            }
        }

        private Article ParseArticle(XmlNode item)
        {
            return new Article() {
                Title = GetNodeText(item, "./a:title"),
                Guid = GetNodeText(item, "./a:id"),
                Link = GetNodeAttribute(item, "href", "./a:link[not(@rel) or (@rel='alternate' and (@type='text/html'))]"),
                PublicationDate = GetNodeUtcDate(item, "./a:published", "./a:updated"),
                UpdateDate = GetNodeUtcDate(item, "./a:updated"),
                Description = GetNodeText(item, "./a:content[not(@type) or @type='html']"),
                Authors = ParseAuthors(item),
                Categories = ParseCategories(item)
            };
        }

        private IList<Author> ParseAuthors(XmlNode item)
        {
            var authors = item.SelectNodes("./a:author", this.namespaceManager);
            var iq = Queryable.AsQueryable<XmlNode>(authors.OfType<XmlNode>());
            return iq.Select(x => new Author()
            {
                Name = GetNodeText(x, "./a:name"),
                Email = GetNodeText(x, "./a:email")
            }).ToList();
        }

        private IList<Category> ParseCategories(XmlNode item)
        {
            var categories = GetNodeAttributeList(item, "term", "./a:category");
            return categories.Select(x => new Category()
            {
                Name = x,
            }).ToList();
        }
    }
}
