using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class QnATests
        : ResourcesTests
    {
        [TestMethod]
        public void TestQnA()
        {
            var qna = (from q in _resourcesQuery.GetQnAs() orderby q.CreatedTime select q).ToList()[0];
            var categories = _resourcesQuery.GetCategories();

            var category = categories.GetCategoryBySubcategory(qna.SubcategoryId);
            var subcategory = categories.GetSubcategory(qna.SubcategoryId);

            TestQnA(
                GetQnAUrl(qna, categories),
                GetPartialQnAUrl(qna),
                qna,
                Tuple.Create(category.Name, GetCategoryQnAsUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryQnAsUrl(category, subcategory)),
                Tuple.Create(qna.Title, GetQnAUrl(qna, categories)));
        }

        private void TestQnA(ReadOnlyUrl url, ReadOnlyUrl partialUrl, Resource qna, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_qnasUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            const int viewings = 3;
            ViewQnA(qna.Id, viewings);

            const int relatedResourceCount = 5;
            var relatedResources = GetRelatedResources(anonymousId, qna, relatedResourceCount);
            var recentQnAs = GetRecentQnAs(anonymousId, new[] { qna }.Concat(relatedResources));

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, new[] { qna }.Concat(relatedResources).Concat(recentQnAs));

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, new[] { qna }.Concat(relatedResources).Concat(recentQnAs));

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertQnA(qna, viewings);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentQnAs(recentQnAs, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Q &amp; A", _qnasUrl)
                }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertQnA(qna, viewings);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentQnAs(recentQnAs, categories);
        }

        private void AssertRecentQnAs(IList<QnA> recentQnAs, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='recentlist']");
            var itemNodes = node.SelectNodes(".//div[@class='item']");

            Assert.AreEqual(recentQnAs.Count, itemNodes.Count);
            for (var index = 0; index < recentQnAs.Count; ++index)
            {
                // Link.

                var a = itemNodes[index].SelectSingleNode("a");
                AssertLink(recentQnAs[index].Title, GetQnAUrl(recentQnAs[index], categories), a);

                // Date.

                Assert.AreEqual(index == 0 ? "Viewed 1 day ago" : "Viewed " + (index + 1) + " days ago", itemNodes[index].SelectSingleNode("./div[@class='date']").InnerText);
            }

            var morea = node.SelectSingleNode(".//div[@class='viewmore']//a");
            AssertLink("View more", _recentQnAsUrl, morea);
        }

        private void AssertQnA(Resource article, int? viewings)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(".//div[@class='answeredquestion']");

            // Title.

            var titleNode = node.SelectSingleNode("./div[@class='answeredquestionarea']/div[@class='bg']//div[@class='title']");
            Assert.AreEqual(article.Title, titleNode.Attributes["title"].Value);
            Assert.AreEqual(article.Title, titleNode.InnerText);

            // Content.

            var contentNode = node.SelectSingleNode("./div[@class='answeredquestionarea']/div[@class='bg']//div[@class='content']");
            Assert.AreEqual(GetInnerText(article.Text), GetContentText(contentNode));

            // Viewings.

            if (viewings != null)
                Assert.AreEqual(viewings.Value.ToString(CultureInfo.InvariantCulture), node.SelectSingleNode("./div[@class='socialarea']/div[@class='bg']/div[@class='viewed']/div[@class='number']").InnerText);
        }
    }
}
