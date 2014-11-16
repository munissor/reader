using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds
{
    public interface IFeedParserFactory
    {
        IFeedParser CreateParserFromUrl(string url);

        IFeedParser CreateParser(Stream feed);
    }
}
