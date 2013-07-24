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
    public class QnAsTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestQnAs()
        {
            var qnas = (from q in _resourcesQuery.GetQnAs() orderby q.CreatedTime descending select q).Take(ItemsPerPage).ToList();

            var articleCount = _resourcesQuery.GetArticles().Count;
            var videoCount = _resourcesQuery.GetVideos().Count;
            var qnaCount = _resourcesQuery.GetQnAs().Count;

            TestQnAs(false, _qnasUrl, _partialQnAsUrl, qnas, articleCount, videoCount, qnaCount);
        }

        [TestMethod]
        public void TestCurrent()
        {
            var qnas = (from q in _resourcesQuery.GetQnAs() orderby q.CreatedTime descending select q).Take(ItemsPerPage).ToList();
            TestCurrent(_qnasUrl, qnas);
        }

        [TestMethod]
        public void TestCategoryQnAs()
        {
            var category = (from c in _resourcesQuery.GetCategories()
                            where c.Name == "Australian job market"
                            select c).Single();

            var qnas = (from a in _resourcesQuery.GetQnAs()
                        where category.Subcategories.Any(s => s.Id == a.SubcategoryId)
                        orderby a.CreatedTime descending
                        select a).Take(ItemsPerPage).ToList();

            var articleCount = (from a in _resourcesQuery.GetArticles()
                                where category.Subcategories.Any(s => s.Id == a.SubcategoryId)
                                select a).Count();
            var videoCount = (from v in _resourcesQuery.GetVideos()
                              where category.Subcategories.Any(s => s.Id == v.SubcategoryId)
                              select v).Count();
            var qnaCount = (from q in _resourcesQuery.GetQnAs()
                            where category.Subcategories.Any(s => s.Id == q.SubcategoryId)
                            select q).Count();

            TestQnAs(
                false,
                GetCategoryQnAsUrl(category),
                GetPartialCategoryQnAsUrl(category),
                qnas,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryQnAsUrl(category)));
        }

        [TestMethod]
        public void TestSubcategoryQnAs()
        {
            var subcategory = (from s in _resourcesQuery.GetCategories().SelectMany(c => c.Subcategories)
                               where s.Name == "Find job in Australia"
                               select s).Single();

            var category = _resourcesQuery.GetCategories().GetCategoryBySubcategory(subcategory.Id);

            var videos = (from a in _resourcesQuery.GetQnAs()
                          where subcategory.Id == a.SubcategoryId
                          orderby a.CreatedTime descending
                          select a).Take(ItemsPerPage).ToList();

            var articleCount = (from a in _resourcesQuery.GetArticles()
                                where subcategory.Id == a.SubcategoryId
                                select a).Count();
            var videoCount = (from v in _resourcesQuery.GetVideos()
                              where subcategory.Id == v.SubcategoryId
                              select v).Count();
            var qnaCount = (from q in _resourcesQuery.GetQnAs()
                            where subcategory.Id == q.SubcategoryId
                            select q).Count();

            TestQnAs(
                false,
                GetSubcategoryQnAsUrl(category, subcategory),
                GetPartialSubcategoryQnAsUrl(subcategory),
                videos,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryQnAsUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryQnAsUrl(category, subcategory)));
        }

        [TestMethod]
        public void TestRecentlyViewed()
        {
            Get(_qnasUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();
            var qnas = GetRecentQnAs(anonymousId, null);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, qnas);

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, qnas);

            // Get the page.

            Get(_recentQnAsUrl);
            AssertUrl(_recentQnAsUrl);

            // Page specific.

            AssertQnAs(false, qnas, null, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Q &amp; A", _qnasUrl)
                });

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(_partialRecentQnAsUrl);
            AssertUrl(_partialRecentQnAsUrl);

            // Page specific.

            AssertQnAs(false, qnas, null, categories);
        }

        [TestMethod]
        public void TestSearch()
        {
            var articles = (from a in _resourcesQuery.GetArticles() where a.Title.ToLower().Contains(Keywords) || a.Text.ToLower().Contains(Keywords) select a).ToList();
            var videos = (from v in _resourcesQuery.GetVideos() where v.Title.ToLower().Contains(Keywords) || v.Text.ToLower().Contains(Keywords) select v).ToList();
            var qnas = (from q in _resourcesQuery.GetQnAs() where q.Title.ToLower().Contains(Keywords) || q.Text.ToLower().Contains(Keywords) select q).ToList();

            TestQnAs(
                true,
                GetSearchUrl(null, Keywords, ResourceType.QnA),
                GetPartialQnAsUrl(null, Keywords),
                qnas.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
                articles.Count,
                videos.Count,
                qnas.Count);
        }

        [TestMethod]
        public void TestCategorySearch()
        {
            var category = (from c in _resourcesQuery.GetCategories() where c.Name == "Resume writing" select c).Single();

            var articles = (from a in _resourcesQuery.GetArticles()
                            where (a.Title.Contains(Keywords) || a.Text.Contains(Keywords))
                            && category.Subcategories.Any(s => s.Id == a.SubcategoryId)
                            select a).ToList();
            var videos = (from v in _resourcesQuery.GetVideos()
                          where (v.Title.Contains(Keywords) || v.Text.Contains(Keywords))
                            && category.Subcategories.Any(s => s.Id == v.SubcategoryId)
                          select v).ToList();
            var qnas = (from q in _resourcesQuery.GetQnAs()
                        where (q.Title.Contains(Keywords) || q.Text.Contains(Keywords))
                        && category.Subcategories.Any(s => s.Id == q.SubcategoryId)
                        select q).ToList();

            TestQnAs(
                true,
                GetSearchUrl(category.Id, Keywords, ResourceType.QnA),
                GetPartialQnAsUrl(category.Id, Keywords),
                qnas.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
                articles.Count,
                videos.Count,
                qnas.Count,
                Tuple.Create(category.Name, GetCategoryQnAsUrl(category)));
        }

        [TestMethod]
        public void TestPaging()
        {
            var qnas = (from a in _resourcesQuery.GetQnAs() orderby a.CreatedTime descending select a).ToList();

            // Second page.

            var url = _qnasUrl.AsNonReadOnly();
            url.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);
            var partialUrl = _qnasUrl.AsNonReadOnly();
            partialUrl.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);

            TestPresentation(url, partialUrl, qnas.Skip(ItemsPerPage).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestSortOrder()
        {
            // Created time, descending is default.

            var qnas = _resourcesQuery.GetQnAs();

            var url = _qnasUrl.AsNonReadOnly();
            var partialUrl = _qnasUrl.AsNonReadOnly();

            TestPresentation(url, partialUrl, qnas.OrderByDescending(q => q.CreatedTime).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestBackTo()
        {
            var qna = _resourcesQuery.GetQnAs()[0];
            var categories = _resourcesQuery.GetCategories();
            var category = categories.GetCategoryBySubcategory(qna.SubcategoryId);
            var subcategory = categories.GetSubcategory(qna.SubcategoryId);

            // No previous.

            var url = GetQnAUrl(qna, categories);
            Get(url);
            AssertBackTo("Back to questions", GetSubcategoryQnAsUrl(category, subcategory));

            // Category.

            Get(GetCategoryQnAsUrl(category));
            Get(url);
            AssertBackTo("Back to questions", GetCategoryQnAsUrl(category));

            // Sub category.

            Get(GetSubcategoryQnAsUrl(category, subcategory));
            Get(url);
            AssertBackTo("Back to questions", GetSubcategoryQnAsUrl(category, subcategory));

            // Recently viewed.

            Get(_recentQnAsUrl);
            Get(url);
            AssertBackTo("Back to questions", GetSubcategoryQnAsUrl(category, subcategory));

            // Partial category.

            Get(GetPartialCategoryQnAsUrl(category));
            Get(url);
            AssertBackTo("Back to questions", GetCategoryQnAsUrl(category));

            // Partial sub category.

            Get(GetPartialSubcategoryQnAsUrl(subcategory));
            Get(url);
            AssertBackTo("Back to questions", GetSubcategoryQnAsUrl(category, subcategory));

            // Partial recently viewed.

            Get(_partialRecentQnAsUrl);
            Get(url);
            AssertBackTo("Back to questions", GetSubcategoryQnAsUrl(category, subcategory));
        }

        private void TestQnAs(bool isSearch, ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<QnA> qnas, int articleCount, int videoCount, int qnaCount, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_qnasUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewQnAs(qnas);
            var recentQnAs = GetRecentQnAs(anonymousId, qnas);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, null);

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, qnas.Concat(recentQnAs));

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertQnAs(isSearch, qnas, viewings, categories);
            AssertRecentQnAs(recentQnAs, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Q &amp; A", _qnasUrl)
                }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(articleCount, videoCount, qnaCount);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertQnAs(isSearch, qnas, viewings, categories);
            AssertRecentQnAs(recentQnAs, categories);
        }

        private void TestCurrent(ReadOnlyUrl url, IList<QnA> qnas)
        {
            Get(_qnasUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewQnAs(qnas);
            var recentQnAs = GetRecentQnAs(anonymousId, qnas);

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertQnAs(false, qnas, viewings, categories);

            // Get the partial page.

            Get(_partialCurrentUrl);
            AssertUrl(_partialCurrentUrl);

            // Page specific.

            AssertQnAs(false, qnas, viewings, categories);
            AssertRecentQnAs(recentQnAs, categories);
        }

        private void TestPresentation(ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<QnA> qnas)
        {
            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertQnAs(false, qnas, null, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertQnAs(false, qnas, null, categories);
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

        private void AssertQnAs(bool isSearch, IList<QnA> qnas, IDictionary<Guid, int> viewings, IList<Category> categories)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='answeredquestionlist']/div[@class='answeredquestionitem']");
            Assert.AreEqual(qnas.Count, nodes.Count);

            for (var index = 0; index < nodes.Count; ++index)
                AssertQnA(isSearch, qnas[index], viewings == null ? (int?)null : viewings[qnas[index].Id], categories, nodes[index]);
        }

        private static void AssertQnA(bool isSearch, Resource qna, int? viewings, IList<Category> categories, HtmlNode node)
        {
            // Link.

            var a = node.SelectSingleNode(".//div[@class='leftside']/a");
            AssertLink(qna.Title, GetQnAUrl(qna, categories), a);

            // Category.

            var categoryNode = node.SelectSingleNode(".//div[@class='category']");
            Assert.AreEqual(categories.GetCategoryBySubcategory(qna.SubcategoryId).Name, categoryNode.InnerText);
            var subcategoryNode = node.SelectSingleNode(".//div[@class='subcategory']");
            Assert.AreEqual(categories.GetSubcategory(qna.SubcategoryId).Name, subcategoryNode.InnerText);

            // Content.

            var contentNode = node.SelectSingleNode(".//div[@class='content']");
            var text = GetInnerText(qna.Text);
            Assert.AreEqual(text, GetTitleText(contentNode));
            if (!isSearch)
                Assert.AreEqual(text, GetText(contentNode.InnerText));

            // Viewings.

            if (viewings != null)
                AssertViewings(node, viewings.Value);
        }
    }
}
