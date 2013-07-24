using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Presentation;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public abstract class ResourcesTests
        : WebTestClass
    {
        protected readonly IResourcesCommand _resourcesCommand = Resolve<IResourcesCommand>();
        protected readonly IResourcesQuery _resourcesQuery = Resolve<IResourcesQuery>();
        protected readonly IResourcesRepository _resourcesRepository = Resolve<IResourcesRepository>();

        protected ReadOnlyUrl _resourcesUrl;

        protected ReadOnlyUrl _articlesUrl;
        protected ReadOnlyUrl _partialArticlesUrl;
        protected ReadOnlyUrl _recentArticlesUrl;
        protected ReadOnlyUrl _partialRecentArticlesUrl;

        protected ReadOnlyUrl _videosUrl;
        protected ReadOnlyUrl _partialVideosUrl;
        protected ReadOnlyUrl _recentVideosUrl;
        protected ReadOnlyUrl _partialRecentVideosUrl;

        protected ReadOnlyUrl _qnasUrl;
        protected ReadOnlyUrl _partialQnAsUrl;
        protected ReadOnlyUrl _recentQnAsUrl;
        protected ReadOnlyUrl _partialRecentQnAsUrl;

        protected ReadOnlyUrl _partialCurrentUrl;

        protected const int ItemsPerPage = 5;
        protected const string Keywords = "job";

        [TestInitialize]
        public void ResourcesTestInitialize()
        {
            _resourcesUrl = new ReadOnlyApplicationUrl("~/members/resources");

            _articlesUrl = new ReadOnlyApplicationUrl("~/members/resources/articles");
            _partialArticlesUrl = new ReadOnlyApplicationUrl("~/members/resources/articles/partial");
            _recentArticlesUrl = new ReadOnlyApplicationUrl("~/members/resources/articles/recent");
            _partialRecentArticlesUrl = new ReadOnlyApplicationUrl("~/members/resources/articles/recent/partial");

            _videosUrl = new ReadOnlyApplicationUrl("~/members/resources/videos");
            _partialVideosUrl = new ReadOnlyApplicationUrl("~/members/resources/videos/partial");
            _recentVideosUrl = new ReadOnlyApplicationUrl("~/members/resources/videos/recent");
            _partialRecentVideosUrl = new ReadOnlyApplicationUrl("~/members/resources/videos/recent/partial");

            _qnasUrl = new ReadOnlyApplicationUrl("~/members/resources/qnas");
            _partialQnAsUrl = new ReadOnlyApplicationUrl("~/members/resources/qnas/partial");
            _recentQnAsUrl = new ReadOnlyApplicationUrl("~/members/resources/qnas/recent");
            _partialRecentQnAsUrl = new ReadOnlyApplicationUrl("~/members/resources/qnas/recent/partial");

            _partialCurrentUrl = new ReadOnlyApplicationUrl("~/members/resources/current/partial");
        }

        protected static ReadOnlyUrl GetArticleUrl(Resource article, IList<Category> categories)
        {
            return GetResourceUrl(article, categories, "articles");
        }

        protected static ReadOnlyUrl GetPartialArticleUrl(Resource article)
        {
            return GetPartialResourceUrl(article.Id, "articles");
        }

        protected static ReadOnlyUrl GetVideoUrl(Resource video, IList<Category> categories)
        {
            return GetResourceUrl(video, categories, "videos");
        }

        protected static ReadOnlyUrl GetPartialVideoUrl(Resource video)
        {
            return GetPartialResourceUrl(video.Id, "videos");
        }

        protected static ReadOnlyUrl GetQnAUrl(Resource qna, IList<Category> categories)
        {
            return GetResourceUrl(qna, categories, "qnas");
        }

        protected static ReadOnlyUrl GetPartialQnAUrl(Resource qna)
        {
            return GetPartialResourceUrl(qna.Id, "qnas");
        }

        private static ReadOnlyUrl GetResourceUrl(Resource resource, IList<Category> categories, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/" + type + "/"
                + categories.GetCategoryBySubcategory(resource.SubcategoryId).Name.EncodeUrlSegment()
                + "-"
                + categories.GetSubcategory(resource.SubcategoryId).Name.EncodeUrlSegment()
                + "-"
                + GetUrlSegmentTitle(resource.Title).EncodeUrlSegment()
                + "/"
                + resource.Id);
        }

        private static ReadOnlyUrl GetPartialResourceUrl(Guid resourceId, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/"
                + type
                + "/"
                + resourceId
                + "/partial");
        }

        protected static ReadOnlyUrl GetCategoryArticlesUrl(Category category)
        {
            return GetCategoryResourcesUrl(category, "articles");
        }

        protected static ReadOnlyUrl GetPartialCategoryArticlesUrl(Category category)
        {
            return GetPartialCategoryResourcesUrl(category, "articles");
        }

        protected static ReadOnlyUrl GetCategoryVideosUrl(Category category)
        {
            return GetCategoryResourcesUrl(category, "videos");
        }

        protected static ReadOnlyUrl GetPartialCategoryVideosUrl(Category category)
        {
            return GetPartialCategoryResourcesUrl(category, "videos");
        }

        protected static ReadOnlyUrl GetCategoryQnAsUrl(Category category)
        {
            return GetCategoryResourcesUrl(category, "qnas");
        }

        protected static ReadOnlyUrl GetPartialCategoryQnAsUrl(Category category)
        {
            return GetPartialCategoryResourcesUrl(category, "qnas");
        }

        private static ReadOnlyUrl GetCategoryResourcesUrl(Category category, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/" + type + "/"
                + category.Name.EncodeUrlSegment(),
                new ReadOnlyQueryString("categoryId", category.Id.ToString()));
        }

        private static ReadOnlyUrl GetPartialCategoryResourcesUrl(Category category, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/" + type + "/partial",
                new ReadOnlyQueryString("categoryId", category.Id.ToString()));
        }

        protected static ReadOnlyUrl GetSubcategoryArticlesUrl(Category category, Subcategory subcategory)
        {
            return GetSubcategoryResourcesUrl(category, subcategory, "articles");
        }

        protected static ReadOnlyUrl GetPartialSubcategoryArticlesUrl(Subcategory subcategory)
        {
            return GetPartialSubcategoryResourcesUrl(subcategory, "articles");
        }

        protected static ReadOnlyUrl GetSubcategoryVideosUrl(Category category, Subcategory subcategory)
        {
            return GetSubcategoryResourcesUrl(category, subcategory, "videos");
        }

        protected static ReadOnlyUrl GetPartialSubcategoryVideosUrl(Subcategory subcategory)
        {
            return GetPartialSubcategoryResourcesUrl(subcategory, "videos");
        }

        protected static ReadOnlyUrl GetSubcategoryQnAsUrl(Category category, Subcategory subcategory)
        {
            return GetSubcategoryResourcesUrl(category, subcategory, "qnas");
        }

        protected static ReadOnlyUrl GetPartialSubcategoryQnAsUrl(Subcategory subcategory)
        {
            return GetPartialSubcategoryResourcesUrl(subcategory, "qnas");
        }

        private static ReadOnlyUrl GetSubcategoryResourcesUrl(Category category, Subcategory subcategory, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/" + type + "/"
                + category.Name.EncodeUrlSegment()
                + "-"
                + subcategory.Name.EncodeUrlSegment(),
                new ReadOnlyQueryString("subcategoryId", subcategory.Id.ToString()));
        }

        private static ReadOnlyUrl GetPartialSubcategoryResourcesUrl(Subcategory subcategory, string type)
        {
            return new ReadOnlyApplicationUrl(
                "~/members/resources/" + type + "/partial",
                new ReadOnlyQueryString("subcategoryId", subcategory.Id.ToString()));
        }

        protected ReadOnlyUrl GetSearchUrl(Guid? categoryId, string keywords, ResourceType? resourceType)
        {
            var url = new ApplicationUrl("~/members/resources/search");
            if (categoryId != null)
                url.QueryString["CategoryId"] = categoryId.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["Keywords"] = keywords;
            if (resourceType != null)
                url.QueryString["ResourceType"] = resourceType.Value.ToString();
            return url;
        }

        protected ReadOnlyUrl GetPartialArticlesUrl(Guid? categoryId, string keywords)
        {
            var url = _partialArticlesUrl.AsNonReadOnly();
            if (categoryId != null)
                url.QueryString["CategoryId"] = categoryId.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["Keywords"] = keywords;
            return url;
        }

        protected ReadOnlyUrl GetPartialVideosUrl(Guid? categoryId, string keywords)
        {
            var url = _partialVideosUrl.AsNonReadOnly();
            if (categoryId != null)
                url.QueryString["CategoryId"] = categoryId.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["Keywords"] = keywords;
            return url;
        }

        protected ReadOnlyUrl GetPartialQnAsUrl(Guid? categoryId, string keywords)
        {
            var url = _partialQnAsUrl.AsNonReadOnly();
            if (categoryId != null)
                url.QueryString["CategoryId"] = categoryId.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["Keywords"] = keywords;
            return url;
        }

        protected Category GetCategory(Guid subcategoryId)
        {
            return (from c in _resourcesQuery.GetCategories()
                    where c.Subcategories.Any(s => s.Id.Equals(subcategoryId))
                    select c).SingleOrDefault();
        }

        private static string GetUrlSegmentTitle(string title)
        {
            // To avoid extra long segments cut off at end of first statement.

            var fullstop = title.IndexOf('.');
            var question = title.IndexOf('?');
            if (fullstop == -1 && question == -1)
                return title;

            var index = fullstop == -1
                ? question
                : question == -1
                    ? fullstop
                    : Math.Min(fullstop, question);

            return title.Substring(0, index);
        }

        protected static string GetInnerText(string text)
        {
            var document = new HtmlDocument();
            document.LoadHtml(text);
            return GetText(document.DocumentNode.InnerText);
        }

        protected static string GetTitleText(HtmlNode node)
        {
            return GetText(node.Attributes["title"].Value);
        }

        protected static string GetContentText(HtmlNode node)
        {
            return GetText(node.InnerText);
        }

        protected static string GetText(string text)
        {
            return text.Trim().Replace("\r\n", "\n").Replace("\n", "").Replace(" ", "");
        }

        protected IDictionary<Guid, int> ViewArticles(IEnumerable<Article> articles)
        {
            return ViewResources(articles.ToList(), ViewArticle);
        }

        protected void ViewArticle(Guid articleId, int count)
        {
            ViewResource(articleId, count, (userId, resourceId) => _resourcesCommand.ViewArticle(userId, resourceId));
        }

        protected IDictionary<Guid, int> ViewVideos(IEnumerable<Video> videos)
        {
            return ViewResources(videos.ToList(), ViewVideo);
        }

        protected void ViewVideo(Guid videoId, int count)
        {
            ViewResource(videoId, count, (userId, resourceId) => _resourcesCommand.ViewVideo(userId, resourceId));
        }

        protected IDictionary<Guid, int> ViewQnAs(IEnumerable<QnA> qnas)
        {
            return ViewResources(qnas.ToList(), ViewQnA);
        }

        protected void ViewQnA(Guid qnaId, int count)
        {
            ViewResource(qnaId, count, (userId, resourceId) => _resourcesCommand.ViewQnA(userId, resourceId));
        }

        private static IDictionary<Guid, int> ViewResources<TResource>(IList<TResource> resources, Action<Guid, int> viewResource)
            where TResource : Resource
        {
            var viewings = new Dictionary<Guid, int>();
            for (var index = 0; index < resources.Count; ++index)
            {
                viewResource(resources[index].Id, index);
                viewings[resources[index].Id] = index;
            }

            return viewings;
        }

        private static void ViewResource(Guid resourceId, int count, Action<Guid, Guid> viewResource)
        {
            for (var index = 0; index < count; ++index)
                viewResource(Guid.NewGuid(), resourceId);
        }

        protected IDictionary<Guid, int> RateArticles(IEnumerable<Article> articles)
        {
            return RateResources(articles.ToList(), RateArticle);
        }

        protected void RateArticle(Guid articleId, int rating)
        {
            RateResource(articleId, rating, (userId, resourceId, resourceRating) => _resourcesCommand.RateArticle(userId, resourceId, resourceRating));
        }

        protected IDictionary<Guid, int> RateVideos(IEnumerable<Video> videos)
        {
            return RateResources(videos.ToList(), RateVideo);
        }

        protected void RateVideo(Guid videoId, int rating)
        {
            RateResource(videoId, rating, (userId, resourceId, resourceRating) => _resourcesCommand.RateArticle(userId, resourceId, resourceRating));
        }

        protected IDictionary<Guid, int> RateQnAs(IEnumerable<QnA> qnas)
        {
            return RateResources(qnas.ToList(), RateQnA);
        }

        protected void RateQnA(Guid qnaId, int rating)
        {
            RateResource(qnaId, rating, (userId, resourceId, resourceRating) => _resourcesCommand.RateArticle(userId, resourceId, resourceRating));
        }

        private static IDictionary<Guid, int> RateResources<TResource>(IList<TResource> resources, Action<Guid, int> rateResource)
            where TResource : Resource
        {
            var ratings = new Dictionary<Guid, int>();
            for (var index = 0; index < resources.Count; ++index)
            {
                var rating = index > 0 && index < 5 ? index : 0;
                if (rating > 0)
                    rateResource(resources[index].Id, rating);
                ratings[resources[index].Id] = rating;
            }

            return ratings;
        }

        private static void RateResource(Guid resourceId, int rating, Action<Guid, Guid, byte> rateResource)
        {
            rateResource(Guid.NewGuid(), resourceId, (byte)rating);
        }

        protected IList<Article> GetRecentArticles(Guid userId, IEnumerable<Resource> excludeResources)
        {
            return GetRecentResources(userId, _resourcesQuery.GetArticles(), excludeResources);
        }

        protected IList<Video> GetRecentVideos(Guid userId, IEnumerable<Resource> excludeResources)
        {
            return GetRecentResources(userId, _resourcesQuery.GetVideos(), excludeResources);
        }

        protected IList<QnA> GetRecentQnAs(Guid userId, IEnumerable<Resource> excludeResources)
        {
            return GetRecentResources(userId, _resourcesQuery.GetQnAs(), excludeResources);
        }

        private IList<TResource> GetRecentResources<TResource>(Guid userId, IEnumerable<TResource> resources, IEnumerable<Resource> excludeResources)
            where TResource : Resource
        {
            var recentResources = excludeResources == null
                ? resources.Take(2).ToList()
                : (from r in resources
                   where excludeResources.All(x => x.Id != r.Id)
                   select r).Take(2).ToList();

            for (var index = 0; index < recentResources.Count; ++index)
            {
                var viewing = new ResourceViewing
                {
                    ResourceType = typeof(TResource) == typeof(Article)
                        ? ResourceType.Article
                        : typeof(TResource) == typeof(Video)
                            ? ResourceType.Video
                            : ResourceType.QnA,
                    UserId = userId,
                    ResourceId = recentResources[index].Id,
                    Id = Guid.NewGuid(),
                    Time = DateTime.Now.AddDays(-1 * (index + 1)),
                };
                _resourcesRepository.CreateResourceViewing(viewing);
            }

            return recentResources;
        }

        protected Article GetTopRatedArticle(int rating, IEnumerable<Resource> excludeResources)
        {
            var article = excludeResources == null
                ? _resourcesQuery.GetArticles().First()
                : (from a in _resourcesQuery.GetArticles()
                   where excludeResources.All(x => x.Id != a.Id)
                   select a).First();

            _resourcesCommand.RateArticle(Guid.NewGuid(), article.Id, (byte) rating);
            return article;
        }

        protected Resource GetTopViewedQnA(int viewings, IEnumerable<Resource> excludeResources)
        {
            var qna = excludeResources == null
                ? _resourcesQuery.GetQnAs().First()
                : (from q in _resourcesQuery.GetQnAs()
                   where excludeResources.All(x => x.Id != q.Id)
                   select q).First();

            for (var index = 0; index < viewings; ++index)
                _resourcesCommand.ViewQnA(Guid.NewGuid(), qna.Id);
            return qna;
        }

        protected IList<Resource> GetRelatedResources(Guid userId, Resource resource, int count)
        {
            var resources = (from a in _resourcesQuery.GetArticles()
                             where a.Id != resource.Id
                             && a.SubcategoryId == resource.SubcategoryId
                             select (Resource)a)
                             .Concat(from v in _resourcesQuery.GetVideos()
                                     where v.Id != resource.Id
                                     && v.SubcategoryId == resource.SubcategoryId
                                     select v)
                            .Concat(from q in _resourcesQuery.GetQnAs()
                                    where q.Id != resource.Id
                                    && q.SubcategoryId == resource.SubcategoryId
                                    select q).Randomise().ToList();

            // View more than are needed to push them down the list, but not resources of the same type.

            resources = resources.Where(r => r.GetType() == resource.GetType()).Concat(resources.Where(r => r.GetType() != resource.GetType())).ToList();

            for (var index = count; index < resources.Count; ++index)
            {
                var relatedResource = resources[index];
                var viewing = new ResourceViewing
                {
                    ResourceType = relatedResource is Article
                        ? ResourceType.Article
                        : resource is Video
                            ? ResourceType.Video
                            : ResourceType.QnA,
                    UserId = userId,
                    ResourceId = relatedResource.Id,
                    Id = Guid.NewGuid(),
                    Time = DateTime.Now.AddDays(-1 * (index + 1))
                };
                _resourcesRepository.CreateResourceViewing(viewing);
            }

            return resources.ToList();
        }

        protected void AssertBackTo(string text, ReadOnlyUrl url)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(".//div[@class='toparea']/div[@class='back']/a");
            AssertLink(text, url, node);
        }

        protected static void AssertRating(IList<HtmlNode> nodes, int rating)
        {
            for (var index = 0; index < 5; ++index)
                Assert.AreEqual(index >= rating ? "star empty" : "star", nodes[index].Attributes["class"].Value.Trim());
        }
        protected void AssertBreadcrumbs(params Tuple<string, ReadOnlyUrl>[] links)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//ul[@class='breadcrumbs']/li/a");
            Assert.AreEqual(links.Length, nodes.Count);
            for (var index = 0; index < links.Length; ++index)
            {
                if (links[index].Item2 != null)
                    AssertLink(links[index].Item1, links[index].Item2, nodes[index]);
            }
        }

        protected void AssertSideNav(ICollection<Category> categories)
        {
            var sideNavNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='side-nav']");

            // Articles.

            AssertSideNavCategories(sideNavNode, "Article", "Articles", _articlesUrl, GetCategoryArticlesUrl, GetSubcategoryArticlesUrl, categories);
            AssertSideNavCategories(sideNavNode, "Video", "Videos", _videosUrl, GetCategoryVideosUrl, GetSubcategoryVideosUrl, categories);
            AssertSideNavCategories(sideNavNode, "QnA", "Q &amp; A", _qnasUrl, GetCategoryQnAsUrl, GetSubcategoryQnAsUrl, categories);
        }

        private static void AssertSideNavCategories(HtmlNode sideNavNode, string type, string innerText, ReadOnlyUrl url, Func<Category, ReadOnlyUrl> getCategoryUrl, Func<Category, Subcategory, ReadOnlyUrl> getSubcategoryUrl, ICollection<Category> categories)
        {
            var resourcesNode = sideNavNode.SelectSingleNode("./div[@type='" + type + "']");

            var a = resourcesNode.SelectSingleNode(".//div[@class='bg']/a");
            AssertLink(innerText, url, a);

            var categoryNodes = resourcesNode.SelectNodes(".//div[@class='category']");
            Assert.AreEqual(categories.Count, categoryNodes.Count);

            foreach (var category in categories)
            {
                var categoryNode = resourcesNode.SelectSingleNode(".//div[@class='categories']/div[@categoryid='" + category.Id + "']");
                a = categoryNode.SelectSingleNode(".//div[@class='bg']/a");
                AssertLink(category.Name, getCategoryUrl(category), a);

                var subcategoryNodes = categoryNode.SelectNodes(".//div[@class='subcategory']");
                Assert.AreEqual(category.Subcategories.Count, subcategoryNodes.Count);

                foreach (var subcategory in category.Subcategories)
                {
                    var subcategoryNode = categoryNode.SelectSingleNode(".//div[@class='subcategories']/div[@subcategoryid='" + subcategory.Id + "']");
                    a = subcategoryNode.SelectSingleNode(".//div[@class='bg']/a");
                    AssertLink(subcategory.Name, getSubcategoryUrl(category, subcategory), a);
                }
            }
        }

        protected void AssertTopViewedQnA(Resource qna, int viewings, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='mostpopular']");
            Assert.AreEqual(qna.Title, node.SelectSingleNode("./div[@class='bg']/div[@class='title']").InnerText);
            Assert.AreEqual(GetInnerText(qna.Text), GetText(node.SelectSingleNode("./div[@class='bg']/div[@class='content']").InnerText));
            Assert.AreEqual(viewings.ToString(CultureInfo.InvariantCulture), node.SelectSingleNode("./div[@class='bg']/div[@class='viewed']/div[@class='number']").InnerText);

            var a = node.SelectSingleNode("./div[@class='bg']/a");
            AssertLink("Read full", GetQnAUrl(qna, categories), a);
        }

        protected void AssertTopRatedArticle(Resource article, int rating, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='toprated']");
            Assert.AreEqual(article.Title, node.SelectSingleNode("./div[@class='bg']/div[@class='title']").InnerText);
            Assert.AreEqual(GetInnerText(article.Text), GetText(node.SelectSingleNode("./div[@class='bg']/div[@class='content']").InnerText));

            var starNodes = node.SelectNodes("./div[@class='bg']/div[@class='rating']/div");
            Assert.AreEqual(5, starNodes.Count);
            for (var index = 0; index < 5; ++index)
                Assert.AreEqual(index >= rating ? "star empty" : "star", starNodes[index].Attributes["class"].Value.Trim());

            var a = node.SelectSingleNode("./div[@class='bg']/a");
            AssertLink("Read full", GetArticleUrl(article, categories), a);
        }

        protected void AssertRelatedResources(ICollection<Resource> relatedResources, IList<Category> categories)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='relatedlist']");
            var itemNodes = node.SelectNodes(".//div[@class='item']");

            Assert.AreEqual(relatedResources.Count, itemNodes.Count);
            foreach (var relatedResource in relatedResources)
            {
                // Link.

                var url = relatedResource is Article
                    ? GetArticleUrl(relatedResource, categories)
                    : relatedResource is Video
                        ? GetVideoUrl(relatedResource, categories)
                        : GetQnAUrl(relatedResource, categories);

                var a = node.SelectSingleNode(".//a[text()='" + relatedResource.Title + "']");

                AssertLink(relatedResource.Title, url, a);
            }
        }

        protected static void AssertViewings(HtmlNode node, int viewings)
        {
            Assert.AreEqual(viewings.ToString(CultureInfo.InvariantCulture), node.SelectSingleNode(".//div[@class='rightside']/div[@class='viewed']/div[@class='number']").InnerText);
        }

        protected void AssertTabs(int? articles, int? videos, int? qnas)
        {
            var tabNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='tabs']/div");
            Assert.AreEqual(3, tabNodes.Count);

            var tabNumbersNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='tabnumbers']/div");
            Assert.AreEqual(3, tabNumbersNodes.Count);

            AssertTab(tabNodes[0], tabNumbersNodes[0], "Article", "ARTICLES", articles);
            AssertTab(tabNodes[1], tabNumbersNodes[1], "Video", "VIDEOS", videos);
            AssertTab(tabNodes[2], tabNumbersNodes[2], "QnA", "Q & A", qnas);
        }

        private static void AssertTab(HtmlNode tabNode, HtmlNode tabNumbersNode, string type, string title, int? count)
        {
            Assert.AreEqual(type, tabNode.Attributes["itemtype"].Value);
            Assert.AreEqual(title, tabNode.SelectSingleNode("./div[@class='bg']/div[@class='title']").InnerText);
            Assert.AreEqual("", tabNode.SelectSingleNode("./div[@class='bg']/div[@class='count']").InnerText);

            Assert.AreEqual(type, tabNumbersNode.Attributes["class"].Value);
            Assert.AreEqual(count == null ? "" : "(" + count + ")", tabNumbersNode.InnerText);
        }

        protected static void AssertLink(string text, ReadOnlyUrl url, HtmlNode node)
        {
            Assert.AreEqual(text, node.InnerText);
            Assert.AreEqual(url.PathAndQuery.ToLower(), node.Attributes["href"].Value.ToLower());
        }
    }
}
