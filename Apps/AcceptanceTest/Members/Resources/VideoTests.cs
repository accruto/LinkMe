using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class VideoTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestVideo()
        {
            var video = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime select v).ToList()[6];
            var categories = _resourcesQuery.GetCategories();

            var category = categories.GetCategoryBySubcategory(video.SubcategoryId);
            var subcategory = categories.GetSubcategory(video.SubcategoryId);

            TestVideo(
                GetVideoUrl(video, categories),
                GetPartialVideoUrl(video),
                video,
                Tuple.Create(category.Name, GetCategoryVideosUrl(category)),
                Tuple.Create(subcategory.Name, GetSubcategoryVideosUrl(category, subcategory)),
                Tuple.Create(video.Title, GetVideoUrl(video, categories)));
        }

        private void TestVideo(ReadOnlyUrl url, ReadOnlyUrl partialUrl, Resource video, params Tuple<string, ReadOnlyUrl>[] extraBreadcrumbs)
        {
            Get(_videosUrl);
            var anonymousId = GetAnonymousId();

            // Set up other data.

            var categories = _resourcesQuery.GetCategories();

            const int viewings = 3;
            ViewVideo(video.Id, viewings);

            const int relatedResourceCount = 5;
            var relatedResources = GetRelatedResources(anonymousId, video, relatedResourceCount);
            var recentVideos = GetRecentVideos(anonymousId, new[] { video }.Concat(relatedResources));

            const int topArticleRating = 5;
            var topRatedArticle = GetTopRatedArticle(topArticleRating, new[] { video }.Concat(relatedResources).Concat(recentVideos));

            const int topQnAViewings = 6;
            var topViewedQnA = GetTopViewedQnA(topQnAViewings, new[] { video }.Concat(relatedResources).Concat(recentVideos));

            // Get the page.

            Get(url);
            AssertUrl(url);

            // Page specific.

            AssertVideo(video, viewings);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentVideos(recentVideos, categories);

            // Page general.

            AssertBreadcrumbs(
                new[]
                {
                    Tuple.Create("Resources", _resourcesUrl),
                    Tuple.Create("Videos", _videosUrl)
                }.Concat(extraBreadcrumbs).ToArray());

            AssertSideNav(categories);
            AssertTabs(null, null, null);
            AssertTopRatedArticle(topRatedArticle, topArticleRating, categories);
            AssertTopViewedQnA(topViewedQnA, topQnAViewings, categories);

            // Get the partial page.

            Get(partialUrl);
            AssertUrl(partialUrl);

            // Page specific.

            AssertVideo(video, viewings);
            AssertRelatedResources(relatedResources.Take(relatedResourceCount).ToList(), categories);
            AssertRecentVideos(recentVideos, categories);
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

        private void AssertVideo(Resource video, int? viewings)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(".//div[@class='video']");

            // Title.

            var titleNode = node.SelectSingleNode("./div[@class='videoarea']/div[@class='bg']//div[@class='title']");
            Assert.AreEqual(video.Title, titleNode.Attributes["title"].Value);
            Assert.AreEqual(video.Title, titleNode.InnerText);

            // Content.

            var contentNode = node.SelectSingleNode("./div[@class='transcriptarea']/div[@class='bg']//div[@class='content']");
            Assert.AreEqual(GetInnerText(video.Text), GetContentText(contentNode));

            // Viewings.

            if (viewings != null)
                Assert.AreEqual(viewings.Value.ToString(CultureInfo.InvariantCulture), node.SelectSingleNode("./div[@class='socialarea']/div[@class='bg']/div[@class='viewed']/div[@class='number']").InnerText);
        }
    }
}
