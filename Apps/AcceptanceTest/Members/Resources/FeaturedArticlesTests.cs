using System;
using System.Linq;
using System.Web;
using LinkMe.Domain.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class FeaturedArticlesTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestSlideshow()
        {
            var featuredArticleIds = from a in _resourcesQuery.GetFeaturedArticles() where a.FeaturedResourceType == FeaturedResourceType.Slideshow select a.ResourceId;
            var articles = _resourcesQuery.GetArticles(featuredArticleIds);

            Get(_resourcesUrl);
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='slidesshow']//div[@class='text']");

            Assert.AreEqual(articles.Count, nodes.Count);

            foreach (var node in nodes)
            {
                // Find the article.

                var href = node.SelectSingleNode(".//a").Attributes["href"].Value;
                var id = new Guid(href.Substring(href.LastIndexOf('/') + 1));
                var article = (from a in articles where a.Id == id select a).Single();

                Assert.AreEqual(GetCategory(article.SubcategoryId).Name.ToUpper(), node.SelectSingleNode("./div[@class='nametitle']/span[@class='name']").InnerText);
                Assert.AreEqual(article.Title.ToUpper(), node.SelectSingleNode("./div[@class='nametitle']/span[@class='title']").InnerText);
                Assert.AreEqual(GetInnerText(article.Text), GetText(node.SelectSingleNode("./div[@class='content']").InnerText));
            }
        }

        [TestMethod]
        public void TestNew()
        {
            var featuredArticleIds = from a in _resourcesQuery.GetFeaturedArticles() where a.FeaturedResourceType == FeaturedResourceType.New select a.ResourceId;
            var articles = _resourcesQuery.GetArticles(featuredArticleIds);

            Get(_resourcesUrl);
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='newarticles']/div").Skip(1).ToList();

            Assert.AreEqual(articles.Count, nodes.Count);

            foreach (var node in nodes)
            {
                // Find the article.

                var href = node.SelectSingleNode(".//a").Attributes["href"].Value;
                var id = new Guid(href.Substring(href.LastIndexOf('/') + 1));
                var article = (from a in articles where a.Id == id select a).Single();

                Assert.AreEqual(article.Title, node.SelectSingleNode("./div[@class='title']").InnerText);
                Assert.AreEqual(GetInnerText(article.Text), GetText(node.SelectSingleNode("./div[@class='content']").InnerText));
            }
        }
    }
}
