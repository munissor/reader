using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds.Tests
{
    [TestClass]
    public class RssFeedParserTests
    {
        FeedParserFactory factory = new FeedParserFactory();

        [TestInitialize]
        public void Initialize()
        {
            factory = new FeedParserFactory();
        }

        
        private Feed GetFeed(string path) 
        {
            using (var fs = File.OpenRead(path))
            {
                var parser = factory.CreateParser(fs);
                return parser.ParseFeedInformation();
            }
        }

        private IEnumerable<Article> GetArticles(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var parser = factory.CreateParser(fs);
                return parser.ParseArticles();
            }
        }

        [TestMethod]
        public void TestFeedTitle()
        {
            var feed = GetFeed(@".\Data\Rss\PlanetPostgres.xml");
            Assert.AreEqual("Planet PostgreSQL", feed.Title);
        }

        [TestMethod]
        public void TestFeedDescription()
        {
            var feed = GetFeed(@".\Data\Rss\PlanetPostgres.xml");
            Assert.AreEqual("Planet PostgreSQL", feed.Subtitle);
        }

        [TestMethod]
        public void TestFeedEmptyDescription()
        {
            var feed = GetFeed(@".\Data\Rss\KalenDelaney.xml");
            Assert.AreEqual("", feed.Subtitle);
        }

        [TestMethod]
        public void TestFeedLanguage()
        {
            var feed = GetFeed(@".\Data\Rss\KalenDelaney.xml");
            Assert.AreEqual("en", feed.Language);
        }

        [TestMethod]
        public void TestFeedNullLanguage()
        {
            var feed = GetFeed(@".\Data\Rss\PlanetPostgres.xml");
            Assert.AreEqual(null, feed.Language);
        }

        [TestMethod]
        public void TestFeedLink()
        {
            var feed = GetFeed(@".\Data\Rss\PlanetPostgres.xml");
            Assert.AreEqual("http://planet.postgresql.org", feed.Link);
        }

        [TestMethod]
        public void TestFeedLastUpdate()
        {
            var feed = GetFeed(@".\Data\Rss\PlanetPostgres.xml");
            Assert.AreEqual(DateTime.Parse("14/11/2014 23:02:31"), feed.LastUpdate);
        }

        [TestMethod]
        public void TestFeedNullLastUpdate()
        {
            var feed = GetFeed(@".\Data\Rss\KalenDelaney.xml");
            Assert.AreEqual(DateTime.MinValue, feed.LastUpdate);
        }

        [TestMethod]
        public void TestArticleTitle()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();

            Assert.AreEqual("Did You Know? My PASS Demo Scripts are Up (and other news)!", article.Title);
        }

        [TestMethod]
        public void TestArticleGuid()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();

            Assert.AreEqual("21093a07-8b3d-42db-8cbf-3350fcbf5496:56387", article.Guid);
        }

        [TestMethod]
        public void TestArticleLink()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();

            Assert.AreEqual("http://www2.sqlblog.com/blogs/kalen_delaney/archive/2014/11/10/my-pass-demo-scripts-are-up-and-other-news.aspx", article.Link);
        }

        [TestMethod]
        public void TestArticlePublicationDate()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();
            
            Assert.AreEqual(DateTime.Parse("10/11/2014 22:48:00"), article.PublicationDate);
        }

        [TestMethod]
        public void TestArticleDescription()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();

            Assert.AreEqual(@"And so another PASS Summit passes into history. It was an awesome week, filled with old friends and new, and lots of superlative technical content! My Hekaton book was released just in time, and it was great seeing the excitement. Red Gate gave away all...(<a href=""http://www2.sqlblog.com/blogs/kalen_delaney/archive/2014/11/10/my-pass-demo-scripts-are-up-and-other-news.aspx"">read more</a>)<img src=""http://www2.sqlblog.com/aggbug.aspx?PostID=56387"" width=""1"" height=""1"">", article.Description);
        }

        [TestMethod]
        public void TestArticleAuthors()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();
            var author = article.Authors.First();

            Assert.AreEqual(@"Kalen Delaney", author.Name);
        }

        [TestMethod]
        public void TestArticleNullAuthors()
        {
            var articles = GetArticles(@".\Data\Rss\PlanetPostgres.xml");
            var article = articles.First();
            var author = article.Authors.FirstOrDefault();

            Assert.IsNull(author);
        }

        [TestMethod]
        public void TestArticleCategories()
        {
            var articles = GetArticles(@".\Data\Rss\KalenDelaney.xml");
            var article = articles.First();

            Assert.AreEqual(4, article.Categories.Count);
            Assert.IsTrue(article.Categories.Any(x => x.Name == "books"));
            Assert.IsTrue(article.Categories.Any(x => x.Name == "conference"));
            Assert.IsTrue(article.Categories.Any(x => x.Name == "Hekaton"));
            Assert.IsTrue(article.Categories.Any(x => x.Name == "raffle"));
        }

        [TestMethod]
        public void TestArticleNullCategories()
        {
            var articles = GetArticles(@".\Data\Rss\PlanetPostgres.xml");
            var article = articles.First();

            Assert.AreEqual(0, article.Categories.Count);
        }
    }
}
