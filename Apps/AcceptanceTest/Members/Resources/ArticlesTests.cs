using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class ArticlesTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestArticles()
        {
            var articles = (from a in _resourcesQuery.GetArticles() orderby a.CreatedTime descending select a).Take(ItemsPerPage).ToList();

            var articleCount = _resourcesQuery.GetArticles().Count;
            var videoCount = _resourcesQuery.GetVideos().Count;
            var qnaCount = _resourcesQuery.GetQnAs().Count;

            TestArticles(false, _articlesUrl, _partialArticlesUrl, articles, articleCount, videoCount, qnaCount);
        }

        [TestMethod]
        public void TestCurrent()
        {
            var articles = (from a in _resourcesQuery.GetArticles() orderby a.CreatedTime descending select a).Take(ItemsPerPage).ToList();
            TestCurrent(_articlesUrl, articles);
        }

        [TestMethod]
        public void TestCategoryArticles()
        {
            var category = (from c in _resourcesQuery.GetCategories()
                            where c.Name == "Australian job market"
                            select c).Single();

            var articles = (from a in _resourcesQuery.GetArticles()
                            where category.Subcategories.Any(s => s.Id == a.SubcategoryId)
                            orderby a.CreatedTime descending select a).Take(ItemsPerPage).ToList();

            var articleCount = (from a in _resourcesQuery.GetArticles()
                                where category.Subcategories.Any(s => s.Id == a.SubcategoryId)
                                select a).Count();
            var videoCount = (from v in _resourcesQuery.GetVideos()
                              where category.Subcategories.Any(s => s.Id == v.SubcategoryId)
                              select v).Count();
            var qnaCount = (from q in _resourcesQuery.GetQnAs()
                            where category.Subcategories.Any(s => s.Id == q.SubcategoryId)
                            select q).Count();

            TestArticles(
                false,
                GetCategoryArticlesUrl(category),
                GetPartialCategoryArticlesUrl(category),
                articles,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryArticlesUrl(category)));
        }

        [TestMethod]
        public void TestSubcategoryArticles()
        {
            var subcategory = (from s in _resourcesQuery.GetCategories().SelectMany(c => c.Subcategories)
                               where s.Name == "Find job in Australia"
                               select s).Single();

            var category = _resourcesQuery.GetCategories().GetCategoryBySubcategory(subcategory.Id);

            var articles = (from a in _resourcesQuery.GetArticles()
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

            TestArticles(
                false, 
                GetSubcategoryArticlesUrl(category, subcategory),
                GetPartialSubcategoryArticlesUrl(subcategory),
                articles,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryArticlesUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryArticlesUrl(category, subcategory)));
        }

        [TestMethod]
        public void TestSearch()
        {
            var articles = (from a in _resourcesQuery.GetArticles() where a.Title.ToLower().Contains(Keywords) || a.Text.ToLower().Contains(Keywords) select a).ToList();
            var videos = (from v in _resourcesQuery.GetVideos() where v.Title.ToLower().Contains(Keywords) || v.Text.ToLower().Contains(Keywords) select v).ToList();
            var qnas = (from q in _resourcesQuery.GetQnAs() where q.Title.ToLower().Contains(Keywords) || q.Text.ToLower().Contains(Keywords) select q).ToList();

            TestArticles(
                true,
                GetSearchUrl(null, Keywords, ResourceType.Article),
                GetPartialArticlesUrl(null, Keywords),
                articles.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
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

            TestArticles(
                true,
                GetSearchUrl(category.Id, Keywords, ResourceType.Article),
                GetPartialArticlesUrl(category.Id, Keywords),
                articles.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
                articles.Count,
                videos.Count,
                qnas.Count,
                Tuple.Create(category.Name, GetCategoryArticlesUrl(category)));
        }

        [TestMethod]
        public void TestPaging()
        {
            var articles = (from a in _resourcesQuery.GetArticles() orderby a.CreatedTime descending select a).ToList();

            // Second page.

            var url = _articlesUrl.AsNonReadOnly();
            url.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);

            var partialUrl = _partialArticlesUrl.AsNonReadOnly();
            partialUrl.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);

            TestPresentation(url, partialUrl, articles.Skip(ItemsPerPage).Take(ItemsPerPage).ToList());

            // Third page.

            url.QueryString["Page"] = 3.ToString(CultureInfo.InvariantCulture);
            partialUrl.QueryString["Page"] = 3.ToString(CultureInfo.InvariantCulture);

            TestPresentation(url, partialUrl, articles.Skip(2 * ItemsPerPage).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestSortOrder()
        {
            // Created time, descending is default.

            var articles = _resourcesQuery.GetArticles();

            var url = _articlesUrl.AsNonReadOnly();
            var partialUrl = _partialArticlesUrl.AsNonReadOnly();

            TestPresentation(url, partialUrl, articles.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestTitle()
        {
            var articles = _resourcesQuery.GetArticles();

            // Choose an article which requires encoding.
            // The created time of the articles are such that the "What a spelling..." article, which requires encoding, appears first.

            articles = articles.OrderByDescending(a => a.CreatedTime).ToList();

            var url = _articlesUrl.AsNonReadOnly();
            var partialUrl = _partialArticlesUrl.AsNonReadOnly();

            TestPresentation(url, partialUrl, articles.Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestRecentlyViewed()
        {
            Get(_articlesUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();
            var articles = GetRecentArticles(anonymousId, null);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, articles);

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, articles);

            // Get the page.

            Get(_recentArticlesUrl);
            AssertUrl(_recentArticlesUrl);

            // Page specific.

            AssertArticles(false, articles, null, null, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Articles", _articlesUrl)
                });

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(_partialRecentArticlesUrl);
            AssertUrl(_partialRecentArticlesUrl);

            // Page specific.

            AssertArticles(false, articles, null, null, categories);
        }

        [TestMethod]
        public void TestBackTo()
        {
            var article = _resourcesQuery.GetArticles()[0];
            var categories = _resourcesQuery.GetCategories();
            var category = categories.GetCategoryBySubcategory(article.SubcategoryId);
            var subcategory = categories.GetSubcategory(article.SubcategoryId);

            // No previous.

            var url = GetArticleUrl(article, categories);
            Get(url);
            AssertBackTo("Back to articles", GetSubcategoryArticlesUrl(category, subcategory));

            // Category.

            Get(GetCategoryArticlesUrl(category));
            Get(url);
            AssertBackTo("Back to articles", GetCategoryArticlesUrl(category));

            // Sub category.

            Get(GetSubcategoryArticlesUrl(category, subcategory));
            Get(url);
            AssertBackTo("Back to articles", GetSubcategoryArticlesUrl(category, subcategory));

            // Recently viewed.

            Get(_recentArticlesUrl);
            Get(url);
            AssertBackTo("Back to articles", GetSubcategoryArticlesUrl(category, subcategory));

            // Partial category.

            Get(GetPartialCategoryArticlesUrl(category));
            Get(url);
            AssertBackTo("Back to articles", GetCategoryArticlesUrl(category));

            // Partial sub category.

            Get(GetPartialSubcategoryArticlesUrl(subcategory));
            Get(url);
            AssertBackTo("Back to articles", GetSubcategoryArticlesUrl(category, subcategory));

            // Partial recently viewed.

            Get(_partialRecentArticlesUrl);
            Get(url);
            AssertBackTo("Back to articles", GetSubcategoryArticlesUrl(category, subcategory));
        }

        private void TestArticles(bool isSearch, ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<Article> articles, int articleCount, int videoCount, int qnaCount, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_articlesUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewArticles(articles);
            var ratings = RateArticles(articles);

            var recentArticles = GetRecentArticles(anonymousId, articles);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, articles.Concat(recentArticles));

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, articles.Concat(recentArticles));

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertArticles(isSearch, articles, viewings, ratings, categories);
            AssertRecentArticles(recentArticles, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                    {
                        Tuple.Create("Resources", _resourcesUrl),
                        Tuple.Create("Articles", _articlesUrl)
                    }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(articleCount, videoCount, qnaCount);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertArticles(isSearch, articles, viewings, ratings, categories);
            AssertRecentArticles(recentArticles, categories);
        }

        private void TestCurrent(ReadOnlyUrl url, IList<Article> articles)
        {
            Get(_articlesUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewArticles(articles);
            var ratings = RateArticles(articles);

            var recentArticles = GetRecentArticles(anonymousId, articles);

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertArticles(false, articles, viewings, ratings, categories);

            // Get the partial page.

            Get(_partialCurrentUrl);
            AssertUrl(_partialCurrentUrl);

            // Page specific.

            AssertArticles(false, articles, viewings, ratings, categories);
            AssertRecentArticles(recentArticles, categories);
        }

        private void TestPresentation(ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<Article> articles)
        {
            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertArticles(false, articles, null, null, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertArticles(false, articles, null, null, categories);
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

        private void AssertArticles(bool isSearch, IList<Article> articles, IDictionary<Guid, int> viewings, IDictionary<Guid, int> ratings, IList<Category> categories)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='articlelist']/div[@class='articleitem']");
            Assert.AreEqual(articles.Count, nodes.Count);

            for (var index = 0; index < nodes.Count; ++index)
                AssertArticle(isSearch, articles[index], viewings == null ? (int?) null : viewings[articles[index].Id], ratings == null ? (int?) null : ratings[articles[index].Id], categories, nodes[index]);
        }

        private static void AssertArticle(bool isSearch, Resource article, int? viewings, int? rating, IList<Category> categories, HtmlNode node)
        {
            // Link.

            var a = node.SelectSingleNode(".//div[@class='toparea']/a");
            AssertLink(article.Title, GetArticleUrl(article, categories), a);

            // Category.

            var categoryNode = node.SelectSingleNode(".//div[@class='category']");
            Assert.AreEqual(categories.GetCategoryBySubcategory(article.SubcategoryId).Name, categoryNode.InnerText);
            var subcategoryNode = node.SelectSingleNode(".//div[@class='subcategory']");
            Assert.AreEqual(categories.GetSubcategory(article.SubcategoryId).Name, subcategoryNode.InnerText);

            // Content.

            var contentNode = node.SelectSingleNode(".//div[@class='content']");
            var text = GetInnerText(article.Text);
            Assert.AreEqual(text, GetTitleText(contentNode));
            if (!isSearch)
                Assert.AreEqual(text, GetContentText(contentNode));

            // Viewings.

            if (viewings != null)
                AssertViewings(node, viewings.Value);
            if (rating != null)
                AssertRating(node, rating.Value);
        }

        private static void AssertRating(HtmlNode node, int rating)
        {
            var divs = node.SelectNodes(".//div[@class='rightside']/div[@class='rating']/div");
            Assert.AreEqual(6, divs.Count);
            AssertRating(divs, rating);
            Assert.AreEqual("ratedcount", divs[5].Attributes["class"].Value);
            Assert.AreEqual("(" + (rating == 0 ? 0 : 1) + ")", divs[5].InnerText);
        }
    }
}
