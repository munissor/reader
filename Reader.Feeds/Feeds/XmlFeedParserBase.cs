using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reader.Feeds
{
    public class XmlFeedParserBase
    {
        protected readonly XmlDocument feed;
        protected readonly XmlNamespaceManager namespaceManager;

        protected XmlFeedParserBase(Stream stream)
        {
            this.feed = new XmlDocument();
            this.feed.Load(stream);
            this.namespaceManager = new XmlNamespaceManager(feed.NameTable);
        }

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
