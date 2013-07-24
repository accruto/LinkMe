using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class VideosTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestVideos()
        {
            var videos = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime descending select v).Take(ItemsPerPage).ToList();

            var articleCount = _resourcesQuery.GetArticles().Count;
            var videoCount = _resourcesQuery.GetVideos().Count;
            var qnaCount = _resourcesQuery.GetQnAs().Count;

            TestVideos(false, _videosUrl, _partialVideosUrl, videos, articleCount, videoCount, qnaCount);
        }

        [TestMethod]
        public void TestCurrent()
        {
            var videos = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime descending select v).Take(ItemsPerPage).ToList();
            TestCurrent(_videosUrl, videos);
        }

        [TestMethod]
        public void TestCategoryVideos()
        {
            var category = (from c in _resourcesQuery.GetCategories()
                            where c.Name == "Australian job market"
                            select c).Single();

            var videos = (from a in _resourcesQuery.GetVideos()
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

            TestVideos(
                false,
                GetCategoryVideosUrl(category),
                GetPartialCategoryVideosUrl(category),
                videos,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryVideosUrl(category)));
        }

        [TestMethod]
        public void TestSubcategoryVideos()
        {
            var subcategory = (from s in _resourcesQuery.GetCategories().SelectMany(c => c.Subcategories)
                               where s.Name == "Find job in Australia"
                               select s).Single();

            var category = _resourcesQuery.GetCategories().GetCategoryBySubcategory(subcategory.Id);

            var videos = (from a in _resourcesQuery.GetVideos()
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

            TestVideos(
                false,
                GetSubcategoryVideosUrl(category, subcategory),
                GetPartialSubcategoryVideosUrl(subcategory),
                videos,
                articleCount,
                videoCount,
                qnaCount,
                Tuple.Create(category.Name, GetCategoryVideosUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryVideosUrl(category, subcategory)));
        }

        [TestMethod]
        public void TestRecentlyViewed()
        {
            Get(_videosUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();
            var videos = GetRecentVideos(anonymousId, null);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, videos);

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, videos);

            // Get the page.

            Get(_recentVideosUrl);
            AssertUrl(_recentVideosUrl);

            // Page specific.

            AssertVideos(false, videos, null, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Videos", _videosUrl)
                });

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(_partialRecentVideosUrl);
            AssertUrl(_partialRecentVideosUrl);

            // Page specific.

            AssertVideos(false, videos, null, categories);
        }

        [TestMethod]
        public void TestSearch()
        {
            var articles = (from a in _resourcesQuery.GetArticles() where a.Title.ToLower().Contains(Keywords) || a.Text.ToLower().Contains(Keywords) select a).ToList();
            var videos = (from v in _resourcesQuery.GetVideos() where v.Title.ToLower().Contains(Keywords) || v.Text.ToLower().Contains(Keywords) select v).ToList();
            var qnas = (from q in _resourcesQuery.GetQnAs() where q.Title.ToLower().Contains(Keywords) || q.Text.ToLower().Contains(Keywords) select q).ToList();

            TestVideos(
                true,
                GetSearchUrl(null, Keywords, ResourceType.Video),
                GetPartialVideosUrl(null, Keywords),
                videos.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
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

            TestVideos(
                true,
                GetSearchUrl(category.Id, Keywords, ResourceType.Video),
                GetPartialVideosUrl(category.Id, Keywords),
                videos.OrderByDescending(a => a.CreatedTime).Take(ItemsPerPage).ToList(),
                articles.Count,
                videos.Count,
                qnas.Count,
                Tuple.Create(category.Name, GetCategoryVideosUrl(category)));
        }

        [TestMethod]
        public void TestPaging()
        {
            var videos = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime descending select v).ToList();

            // Second page.

            var url = _videosUrl.AsNonReadOnly();
            url.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);
            var partialUrl = _videosUrl.AsNonReadOnly();
            partialUrl.QueryString["Page"] = 2.ToString(CultureInfo.InvariantCulture);

            TestPresentation(url, partialUrl, videos.Skip(ItemsPerPage).Take(ItemsPerPage).ToList());

            // Third page.

            url.QueryString["Page"] = 3.ToString(CultureInfo.InvariantCulture);
            partialUrl.QueryString["Page"] = 3.ToString(CultureInfo.InvariantCulture);

            TestPresentation(url, partialUrl, videos.Skip(2 * ItemsPerPage).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestSortOrder()
        {
            // Created time, descending is default.

            var videos = _resourcesQuery.GetVideos();

            var url = _videosUrl.AsNonReadOnly();
            var partialUrl = _partialVideosUrl.AsNonReadOnly();

            TestPresentation(url, partialUrl, videos.OrderByDescending(v => v.CreatedTime).Take(ItemsPerPage).ToList());
        }

        [TestMethod]
        public void TestBackTo()
        {
            var video = _resourcesQuery.GetVideos()[0];
            var categories = _resourcesQuery.GetCategories();
            var category = categories.GetCategoryBySubcategory(video.SubcategoryId);
            var subcategory = categories.GetSubcategory(video.SubcategoryId);

            // No previous.

            var url = GetVideoUrl(video, categories);
            Get(url);
            AssertBackTo("Back to videos", GetSubcategoryVideosUrl(category, subcategory));

            // Category.

            Get(GetCategoryVideosUrl(category));
            Get(url);
            AssertBackTo("Back to videos", GetCategoryVideosUrl(category));

            // Sub category.

            Get(GetSubcategoryVideosUrl(category, subcategory));
            Get(url);
            AssertBackTo("Back to videos", GetSubcategoryVideosUrl(category, subcategory));

            // Recently viewed.

            Get(_recentVideosUrl);
            Get(url);
            AssertBackTo("Back to videos", GetSubcategoryVideosUrl(category, subcategory));

            // Partial category.

            Get(GetPartialCategoryVideosUrl(category));
            Get(url);
            AssertBackTo("Back to videos", GetCategoryVideosUrl(category));

            // Partial sub category.

            Get(GetPartialSubcategoryVideosUrl(subcategory));
            Get(url);
            AssertBackTo("Back to videos", GetSubcategoryVideosUrl(category, subcategory));

            // Partial recently viewed.

            Get(_partialRecentVideosUrl);
            Get(url);
            AssertBackTo("Back to videos", GetSubcategoryVideosUrl(category, subcategory));
        }

        private void TestVideos(bool isSearch, ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<Video> videos, int articleCount, int videoCount, int qnaCount, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_videosUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewVideos(videos);

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, null);
            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, null);

            var recentVideos = GetRecentVideos(anonymousId, videos);

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertVideos(isSearch, videos, viewings, categories);
            AssertRecentVideos(recentVideos, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Videos", _videosUrl)
                }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(articleCount, videoCount, qnaCount);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertVideos(isSearch, videos, viewings, categories);
            AssertRecentVideos(recentVideos, categories);
        }

        private void TestCurrent(ReadOnlyUrl url, IList<Video> videos)
        {
            Get(_videosUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            var viewings = ViewVideos(videos);
            var recentVideos = GetRecentVideos(anonymousId, videos);

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertVideos(false, videos, viewings, categories);

            // Get the partial page.

            Get(_partialCurrentUrl);
            AssertUrl(_partialCurrentUrl);

            // Page specific.

            AssertVideos(false, videos, viewings, categories);
            AssertRecentVideos(recentVideos, categories);
        }

        private void TestPresentation(ReadOnlyUrl url, ReadOnlyUrl partialUrl, IList<Video> videos)
        {
            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertVideos(false, videos, null, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertVideos(false, videos, null, categories);
        }

        private void AssertRecentVideos(IList<Video> recentVideos, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='recentlist']");

            var itemNodes = node.SelectNodes(".//div[@class='item']");
            Assert.AreEqual(recentVideos.Count, itemNodes.Count);
            for (var index = 0; index < recentVideos.Count; ++index)
            {
                // Link.

                var a = itemNodes[index].SelectSingleNode("a");
                AssertLink(recentVideos[index].Title, GetVideoUrl(recentVideos[index], categories), a);

                // Date.

                Assert.AreEqual(index == 0 ? "Viewed 1 day ago" : "Viewed " + (index + 1) + " days ago", itemNodes[index].SelectSingleNode("./div[@class='date']").InnerText);
            }

            var morea = node.SelectSingleNode(".//div[@class='viewmore']//a");
            AssertLink("View more", _recentVideosUrl, morea);
        }

        private void AssertVideos(bool isSearch, IList<Video> videos, IDictionary<Guid, int> viewings, IList<Category> categories)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='videolist']/div[@class='videoitem']");
            Assert.AreEqual(videos.Count, nodes.Count);

            for (var index = 0; index < nodes.Count; ++index)
                AssertVideo(isSearch, videos[index], viewings == null ? (int?)null : viewings[videos[index].Id], categories, nodes[index]);
        }

        private static void AssertVideo(bool isSearch, Resource video, int? viewings, IList<Category> categories, HtmlNode node)
        {
            // Link.

            var a = node.SelectSingleNode(".//div[@class='leftside']/a");
            AssertLink(video.Title, GetVideoUrl(video, categories), a);

            // Content.

            var transcriptNode = node.SelectSingleNode(".//div[@class='transcript']");
            var text = GetInnerText(video.Text);
            Assert.AreEqual(HttpUtility.HtmlEncode(text), GetTitleText(transcriptNode));
            if (!isSearch)
                Assert.AreEqual(text, GetText(transcriptNode.InnerText));

            // Viewings.

            if (viewings != null)
                AssertViewings(node, viewings.Value);
        }
    }
}
