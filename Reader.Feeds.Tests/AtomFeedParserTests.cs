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
    public class AtomFeedParserTests
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
            var feed = GetFeed(@".\Data\Atom\MartinFowler.xml");
            Assert.AreEqual("Martin Fowler", feed.Title);
        }

        [TestMethod]
        public void TestFeedSubtitle()
        {
            var feed = GetFeed(@".\Data\Atom\MartinFowler.xml");
            Assert.AreEqual("Master feed of news and updates from martinfowler.com", feed.Subtitle);
        }

        [TestMethod]
        public void TestFeedLink()
        {
            var feed = GetFeed(@".\Data\Atom\MartinFowler.xml");
            Assert.AreEqual("http://martinfowler.com", feed.Link);
        }

        [TestMethod]
        public void TestFeedAlternateLink()
        {
            var feed = GetFeed(@".\Data\Atom\TroyHunt.xml");
            Assert.AreEqual("http://www.troyhunt.com/", feed.Link);
        }

        [TestMethod]
        public void TestFeedLastUpdate()
        {
            var feed = GetFeed(@".\Data\Atom\MartinFowler.xml");
            Assert.AreEqual(DateTime.Parse("2014-11-04T08:49:00-05:00"), feed.LastUpdate);
        }

        [TestMethod]
        public void TestArticleTitle()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual("Updates to Collection Pipelines", article.Title);
        }

        [TestMethod]
        public void TestArticleGuid()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual("tag:martinfowler.com,2014-11-04:Updates-to-Collection-Pipelines", article.Guid);
        }

        [TestMethod]
        public void TestArticleLink()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual("http://martinfowler.com/articles/collection-pipeline", article.Link);
        }

        [TestMethod]
        public void TestArticleRelAlternateHtmlLink()
        {
            var articles = GetArticles(@".\Data\Atom\TroyHunt.xml");
            var article = articles.First();

            Assert.AreEqual("http://feedproxy.google.com/~r/TroyHunt/~3/OUZRm4NuUOc/success-by-thousand-cuts-visual-studio.html", article.Link);
        }

        [TestMethod]
        public void TestArticlePublicationDateNotUpdated()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual(DateTime.Parse("2014-11-04T08:49:00-05:00"), article.PublicationDate);
        }

        [TestMethod]
        public void TestArticleUpdateDateNotUpdated()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual(DateTime.Parse("2014-11-04T08:49:00-05:00"), article.UpdateDate);
            Assert.AreEqual(article.PublicationDate, article.UpdateDate);
        }

        [TestMethod]
        public void TestArticlePublicationDateUpdated()
        {
            var articles = GetArticles(@".\Data\Atom\TroyHunt.xml");
            var article = articles.First();

            Assert.AreEqual(DateTime.Parse("2014-11-13T21:32:00.001+11:00"), article.PublicationDate);
        }

        [TestMethod]
        public void TestArticleUpdateDateUpdated()
        {
            var articles = GetArticles(@".\Data\Atom\TroyHunt.xml");
            var article = articles.First();

            Assert.AreEqual(DateTime.Parse("2014-11-13T21:33:23.325+11:00"), article.UpdateDate);
            Assert.AreNotEqual(article.PublicationDate, article.UpdateDate);
        }

        [TestMethod]
        public void TestArticleDescriptionHtml()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual("<div class = 'img'><a href = 'http://martinfowler.com/articles/collection-pipeline'><img src = 'http://martinfowler.com/articles/collection-pipeline/collection-pipeline/sketch.png' width = '150px' height = '' alt = '' title = ''/></a></div><p>Over the last few weeks I’ve been quietly making a bunch of small updates to <a href=\"http://martinfowler.com/articles/collection-pipeline\">my article on collection pipelines</a>. To the main text I’ve added a subsection contrasting them with <a href=\"http://martinfowler.com/articles/collection-pipeline/#NestedOperatorExpressions\">Nested Operator Expressions</a>. I’ve also added several operators to the <a href=\"http://martinfowler.com/articles/collection-pipeline/#op-catalog\">operation catalog</a>, including slice and various set operations.</p>", article.Description.Trim());
        }

        [TestMethod]
        public void TestArticleCategories()
        {
            var articles = GetArticles(@".\Data\Atom\TroyHunt.xml");
            var article = articles.First();
            
            Assert.AreEqual(2, article.Categories.Count);
            Assert.IsTrue(article.Categories.Any(x => x.Name == "Visual Studio"));
            Assert.IsTrue(article.Categories.Any(x => x.Name == "Azure"));
        }

        [TestMethod]
        public void TestArticleCategoriesNull()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual(0, article.Categories.Count);
        }

        [TestMethod]
        public void TestArticleAuthors()
        {
            var articles = GetArticles(@".\Data\Atom\TroyHunt.xml");
            var article = articles.First();

            Assert.AreEqual(1, article.Authors.Count);
            Assert.IsTrue(article.Authors.Any(x => x.Name == "Troy Hunt"));
            Assert.IsTrue(article.Authors.Any(x => x.Email == "noreply@blogger.com"));
        }

        [TestMethod]
        public void TestArticleAuthorsNull()
        {
            var articles = GetArticles(@".\Data\Atom\MartinFowler.xml");
            var article = articles.First();

            Assert.AreEqual(0, article.Authors.Count);
        }
        
    }
}

