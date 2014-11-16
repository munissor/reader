﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Feeds.Tests
{
    [TestClass]
    public class FeedParserFactoryTests
    {
        FeedParserFactory factory = new FeedParserFactory();

        [TestInitialize]
        public void Initialize() 
        {
            factory = new FeedParserFactory();
        }

        [TestMethod]
        public void CreateRssFeed()
        {
            using (var fs = File.OpenRead(@".\Data\Rss\PlanetPostgres.xml"))
            {
                var parser = factory.CreateParser(fs);
                Assert.IsInstanceOfType(parser, typeof(RssFeedParser));
            }
        }

        [TestMethod]
        public void CreateAtomFeed()
        {
            using (var fs = File.OpenRead(@".\Data\Atom\MartinFowler.xml"))
            {
                var parser = factory.CreateParser(fs);
                Assert.IsInstanceOfType(parser, typeof(AtomFeedParser));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateUnknownFeed()
        {
            using (var fs = File.OpenRead(@".\Data\unknown.xml"))
            {
                var parser = factory.CreateParser(fs);
            }
        }
    }
}
