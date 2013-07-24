using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class RelatedTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestRelatedArticles()
        {
            var userId = Guid.NewGuid();

            var article1 = _resourcesQuery.GetArticles().OrderBy(a => a.CreatedTime).ToList()[1];
            var articles = _resourcesQuery.GetRelatedArticles(article1.SubcategoryId, userId, 2);
            Assert.AreEqual(2, articles.Count);
            articles = _resourcesQuery.GetRelatedArticles(article1.SubcategoryId, userId, 100);
            Assert.AreEqual(6, articles.Count);

            // View one so it now comes last.

            var articleId = articles[2].Id;
            _resourcesCommand.ViewArticle(userId, articleId);
            
            articles = _resourcesQuery.GetRelatedArticles(article1.SubcategoryId, userId, 100);
            Assert.AreEqual(6, articles.Count);
            Assert.AreEqual(articleId, articles[5].Id);

            // Another article.

            var article2 = _resourcesQuery.GetArticles().OrderBy(a => a.CreatedTime).ToList()[2];
            articles = _resourcesQuery.GetRelatedArticles(article2.SubcategoryId, userId, 2);
            Assert.AreEqual(2, articles.Count);
            articles = _resourcesQuery.GetRelatedArticles(article2.SubcategoryId, userId, 100);
            Assert.AreEqual(5, articles.Count);

            // View one so it now comes last.

            articleId = articles[1].Id;
            _resourcesCommand.ViewArticle(userId, articleId);

            articles = _resourcesQuery.GetRelatedArticles(article2.SubcategoryId, userId, 100);
            Assert.AreEqual(5, articles.Count);
            Assert.AreEqual(articleId, articles[4].Id);
        }

        [TestMethod]
        public void TestRelatedVideos()
        {
            var userId = Guid.NewGuid();

            var video1 = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime select v).ToList()[2];
            var videos = _resourcesQuery.GetRelatedVideos(video1.SubcategoryId, userId, 2);
            Assert.AreEqual(2, videos.Count);
            videos = _resourcesQuery.GetRelatedVideos(video1.SubcategoryId, userId, 100);
            Assert.AreEqual(4, videos.Count);

            // View one so it now comes last.

            var videoId = videos[2].Id;
            _resourcesCommand.ViewVideo(userId, videoId);

            videos = _resourcesQuery.GetRelatedVideos(video1.SubcategoryId, userId, 100);
            Assert.AreEqual(4, videos.Count);
            Assert.AreEqual(videoId, videos[3].Id);

            // Another video.

            var video2 = (from v in _resourcesQuery.GetVideos() orderby v.CreatedTime select v).ToList()[1];
            videos = _resourcesQuery.GetRelatedVideos(video2.SubcategoryId, userId, 2);
            Assert.AreEqual(2, videos.Count);
            videos = _resourcesQuery.GetRelatedVideos(video2.SubcategoryId, userId, 100);
            Assert.AreEqual(4, videos.Count);

            // View one so it now comes last.

            videoId = videos[0].Id;
            _resourcesCommand.ViewVideo(userId, videoId);

            videos = _resourcesQuery.GetRelatedVideos(video2.SubcategoryId, userId, 100);
            Assert.AreEqual(4, videos.Count);
            Assert.AreEqual(videoId, videos[3].Id);
        }

        [TestMethod]
        public void TestRelatedQnAs()
        {
            var userId = Guid.NewGuid();

            var qna1 = _resourcesQuery.GetQnAs().OrderBy(q => q.CreatedTime).ToList()[2];
            var qnas = _resourcesQuery.GetRelatedQnAs(qna1.SubcategoryId, userId, 2);
            Assert.AreEqual(2, qnas.Count);
            qnas = _resourcesQuery.GetRelatedQnAs(qna1.SubcategoryId, userId, 100);
            Assert.AreEqual(2, qnas.Count);

            // View one so it now comes last.

            var qnaId = qnas[0].Id;
            _resourcesCommand.ViewQnA(userId, qnaId);

            qnas = _resourcesQuery.GetRelatedQnAs(qna1.SubcategoryId, userId, 100);
            Assert.AreEqual(2, qnas.Count);
            Assert.AreEqual(qnaId, qnas[1].Id);

            // Another qna.

            var qna2 = _resourcesQuery.GetQnAs().OrderBy(q => q.CreatedTime).ToList()[4];
            qnas = _resourcesQuery.GetRelatedQnAs(qna2.SubcategoryId, userId, 2);
            Assert.AreEqual(2, qnas.Count);
            qnas = _resourcesQuery.GetRelatedQnAs(qna2.SubcategoryId, userId, 100);
            Assert.AreEqual(3, qnas.Count);

            // View one so it now comes last.

            qnaId = qnas[0].Id;
            _resourcesCommand.ViewQnA(userId, qnaId);

            qnas = _resourcesQuery.GetRelatedQnAs(qna2.SubcategoryId, userId, 100);
            Assert.AreEqual(3, qnas.Count);
            Assert.AreEqual(qnaId, qnas[2].Id);
        }
    }
}
