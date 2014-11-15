using Reader.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    public interface IFeedParser
    {
        Feed ParseFeedInformation();

        IEnumerable<Article> ParseArticles();
    }
}
