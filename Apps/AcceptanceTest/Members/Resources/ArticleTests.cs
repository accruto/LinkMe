using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class ArticleTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestArticle()
        {
            var article = (from a in _resourcesQuery.GetArticles() orderby a.CreatedTime select a).ToList()[0];
            var categories = _resourcesQuery.GetCategories();

            var category = categories.GetCategoryBySubcategory(article.SubcategoryId);
            var subcategory = categories.GetSubcategory(article.SubcategoryId);

            TestArticle(
                GetArticleUrl(article, categories),
                GetPartialArticleUrl(article),
                article,
                Tuple.Create(category.Name, GetCategoryArticlesUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryArticlesUrl(category, subcategory)),
                Tuple.Create(article.Title, GetArticleUrl(article, categories)));
        }

        private void TestArticle(ReadOnlyUrl url, ReadOnlyUrl partialUrl, Resource article, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_articlesUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            const int viewings = 3;
            ViewArticle(article.Id, viewings);
            const int rating = 4;
            RateArticle(article.Id, rating);

            const int relatedResourceCount = 5;
            var relatedResources = GetRelatedResources(anonymousId, article, relatedResourceCount);
            var recentArticles = GetRecentArticles(anonymousId, new[] {article}.Concat(relatedResources));

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, new[] {article}.Concat(relatedResources).Concat(recentArticles));

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, new[] {article}.Concat(relatedResources).Concat(recentArticles));

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertArticle(article, viewings, rating);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentArticles(recentArticles, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Articles", _articlesUrl)
                }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertArticle(article, viewings, rating);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentArticles(recentArticles, categories);
        }

        private void AssertRecentArticles(IList<Article> recentArticles, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='recentlist']");
            var itemNodes = node.SelectNodes(".//div[@class='item']");
            
            Assert.AreEqual(recentArticles.Count, itemNodes.Count);
            for (var index = 0; index < recentArticles.Count; ++index)
            {
                // Link.

                var a = itemNodes[index].SelectSingleNode("a");
                AssertLink(recentArticles[index].Title, GetArticleUrl(recentArticles[index], categories), a);

                // Rating.

                var ratingNodes = itemNodes[index].SelectNodes("./div[@class='rating']/div");
                Assert.AreEqual(5, ratingNodes.Count);
                AssertRating(ratingNodes, 0);

                // Date.

                Assert.AreEqual(index == 0 ? "Viewed 1 day ago" : "Viewed " + (index + 1) + " days ago", itemNodes[index].SelectSingleNode("./div[@class='date']").InnerText);
            }

            var morea = node.SelectSingleNode(".//div[@class='viewmore']//a");
            AssertLink("View more", _recentArticlesUrl, morea);
        }

        private void AssertArticle(Resource article, int? viewings, int? rating)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(".//div[@class='article']");

            // Title.

            var titleNode = node.SelectSingleNode("./div[@class='articlearea']/div[@class='bg']//div[@class='title']");
            Assert.AreEqual(article.Title, titleNode.Attributes["title"].Value);
            Assert.AreEqual(article.Title, titleNode.InnerText);

            // Content.

            var contentNode = node.SelectSingleNode("./div[@class='articlearea']/div[@class='bg']//div[@class='content']");
            Assert.AreEqual(GetInnerText(article.Text), GetContentText(contentNode));

            // Viewings.
            
            if (viewings != null)
                Assert.AreEqual(viewings.Value.ToString(CultureInfo.InvariantCulture), node.SelectSingleNode("./div[@class='ratingarea']/div[@class='bg']/div[@class='viewed']/div[@class='number']").InnerText);
            if (rating != null)
                AssertRating(node, rating.Value);
        }

        private static void AssertRating(HtmlNode node, int rating)
        {
            var divs = node.SelectNodes("./div[@class='articlearea']/div[@class='bg']//div[@class='rating']/div");
            Assert.AreEqual(6, divs.Count);
            AssertRating(divs, rating);
            Assert.AreEqual("ratedcount", divs[5].Attributes["class"].Value);
            Assert.AreEqual("(" + (rating == 0 ? 0 : 1) + ")", divs[5].InnerText);
        }
    }
}
