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
    /// Base class for parsers that parse Xml formats.
    /// </summary>
    public class XmlFeedParserBase
    {
        protected readonly XmlDocument feed;
        protected readonly XmlNamespaceManager namespaceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFeedParserBase"/> class.
        /// </summary>
        /// <param name="stream">The feed stream.</param>
        protected XmlFeedParserBase(Stream stream)
        {
            this.feed = new XmlDocument();
            this.feed.Load(stream);
            this.namespaceManager = new XmlNamespaceManager(feed.NameTable);
        }

        /// <summary>
        /// Gets the content of a node as a UTC date.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The date</returns>
        protected DateTime GetNodeUtcDate(XmlNode parentNode, params string[] selectors)
        {
            var text = GetNodeText(parentNode, selectors);
            if (!string.IsNullOrWhiteSpace(text))
            {
                DateTime dt;
                if (DateTime.TryParse(text, out dt))
                    return dt.ToUniversalTime();
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets a the content of a node as text.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The text</returns>
        protected string GetNodeText(XmlNode parentNode, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var elem = parentNode.SelectSingleNode(selector, this.namespaceManager);
                if (elem != null)
                    return elem.InnerText;

            }
            return null;
        }

        /// <summary>
        /// Gets the content of a node attribute as text.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="attribute">The attribute name.</param>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The text.</returns>
        protected string GetNodeAttribute(XmlNode parentNode, string attribute, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var elem = parentNode.SelectSingleNode(selector, this.namespaceManager);
                if (elem != null)
                {
                    var attr = elem.Attributes[attribute];
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the content of a nodes attribute as a list of strings.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The list of strings</returns>
        protected IList<string> GetNodeTextList(XmlNode parentNode, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var elem = parentNode.SelectNodes(selector, this.namespaceManager);
                if (elem != null)
                {
                    var iq = Queryable.AsQueryable<XmlNode>(elem.OfType<XmlNode>());
                    return iq.Select(x => x.InnerText).ToList();
                }
            }
            return new List<string>();
        }

        /// <summary>
        /// Gets the node attribute list.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="selectors">The selectors.</param>
        /// <returns></returns>
        protected IList<string> GetNodeAttributeList(XmlNode parentNode, string attribute, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var elem = parentNode.SelectNodes(selector, this.namespaceManager);
                if (elem != null)
                {
                    var iq = Queryable.AsQueryable<XmlNode>(elem.OfType<XmlNode>());
                    return iq.Where(x => x.Attributes[attribute] != null).Select(x => x.Attributes[attribute].Value).ToList();
                }
            }
            return new List<string>();
        }
    }
}
