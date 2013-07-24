using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class ViewingTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestArticleViewingCount()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var article1 = _resourcesQuery.GetArticles()[12];
            var article2 = _resourcesQuery.GetArticles()[14];
            var article3 = _resourcesQuery.GetArticles()[16];

            _resourcesCommand.ViewArticle(userId1, article1.Id);
            _resourcesCommand.ViewArticle(userId1, article2.Id);
            _resourcesCommand.ViewArticle(userId2, article1.Id);

            Assert.AreEqual(2, _resourcesQuery.GetViewingCount(article1.Id));
            Assert.AreEqual(1, _resourcesQuery.GetViewingCount(article2.Id));
            Assert.AreEqual(0, _resourcesQuery.GetViewingCount(article3.Id));

            var counts = _resourcesQuery.GetViewingCounts(new[] { article1.Id, article2.Id, article3.Id });
            Assert.AreEqual(3, counts.Count);
            Assert.AreEqual(2, counts[article1.Id]);
            Assert.AreEqual(1, counts[article2.Id]);
            Assert.AreEqual(0, counts[article3.Id]);
        }

        [TestMethod]
        public void TestVideoViewingCount()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var video1 = _resourcesQuery.GetVideos()[6];
            var video2 = _resourcesQuery.GetVideos()[7];
            var video3 = _resourcesQuery.GetVideos()[8];

            _resourcesCommand.ViewVideo(userId1, video1.Id);
            _resourcesCommand.ViewVideo(userId1, video2.Id);
            _resourcesCommand.ViewVideo(userId2, video1.Id);

            Assert.AreEqual(2, _resourcesQuery.GetViewingCount(video1.Id));
            Assert.AreEqual(1, _resourcesQuery.GetViewingCount(video2.Id));
            Assert.AreEqual(0, _resourcesQuery.GetViewingCount(video3.Id));

            var counts = _resourcesQuery.GetViewingCounts(new[] { video1.Id, video2.Id, video3.Id });
            Assert.AreEqual(3, counts.Count);
            Assert.AreEqual(2, counts[video1.Id]);
            Assert.AreEqual(1, counts[video2.Id]);
            Assert.AreEqual(0, counts[video3.Id]);
        }

        [TestMethod]
        public void TestQnAViewingCount()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var qna1 = _resourcesQuery.GetQnAs()[2];
            var qna2 = _resourcesQuery.GetQnAs()[4];
            var qna3 = _resourcesQuery.GetQnAs()[6];

            _resourcesCommand.ViewQnA(userId1, qna1.Id);
            _resourcesCommand.ViewQnA(userId1, qna2.Id);
            _resourcesCommand.ViewQnA(userId2, qna1.Id);

            Assert.AreEqual(2, _resourcesQuery.GetViewingCount(qna1.Id));
            Assert.AreEqual(1, _resourcesQuery.GetViewingCount(qna2.Id));
            Assert.AreEqual(0, _resourcesQuery.GetViewingCount(qna3.Id));

            var counts = _resourcesQuery.GetViewingCounts(new[] { qna1.Id, qna2.Id, qna3.Id });
            Assert.AreEqual(3, counts.Count);
            Assert.AreEqual(2, counts[qna1.Id]);
            Assert.AreEqual(1, counts[qna2.Id]);
            Assert.AreEqual(0, counts[qna3.Id]);
        }

        [TestMethod]
        public void TestRecentlyViewedArticles()
        {
            var userId = Guid.NewGuid();
            var articles = _resourcesQuery.GetArticles();

            var recentlyViewedArticles = _resourcesQuery.GetRecentlyViewedArticles(userId, 5);
            Assert.AreEqual(0, recentlyViewedArticles.Count);

            var article1 = articles[3];
            _resourcesCommand.ViewArticle(userId, article1.Id);

            recentlyViewedArticles = _resourcesQuery.GetRecentlyViewedArticles(userId, 5);
            Assert.AreEqual(1, recentlyViewedArticles.Count);

            SleepForDifferentSqlTimestamp();

            var article2 = articles[5];
            _resourcesCommand.ViewArticle(userId, article2.Id);

            SleepForDifferentSqlTimestamp();

            var article3 = articles[6];
            _resourcesCommand.ViewArticle(userId, article3.Id);

            // Make sure only the last 2 of the three viewings are returned.

            recentlyViewedArticles = _resourcesQuery.GetRecentlyViewedArticles(userId, 2);
            Assert.AreEqual(2, recentlyViewedArticles.Count);
            Assert.AreEqual(article3.Id, recentlyViewedArticles[0].Id);
            Assert.AreEqual(article2.Id, recentlyViewedArticles[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewArticle(userId, article1.Id);

            // Results should be different from previous

            recentlyViewedArticles = _resourcesQuery.GetRecentlyViewedArticles(userId, 2);
            Assert.AreEqual(2, recentlyViewedArticles.Count);
            Assert.AreEqual(article1.Id, recentlyViewedArticles[0].Id);
            Assert.AreEqual(article3.Id, recentlyViewedArticles[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 2 by a different user.

            _resourcesCommand.ViewArticle(Guid.NewGuid(), article2.Id);

            // Results should be the same as previous

            recentlyViewedArticles = _resourcesQuery.GetRecentlyViewedArticles(userId, 2);
            Assert.AreEqual(2, recentlyViewedArticles.Count);
            Assert.AreEqual(article1.Id, recentlyViewedArticles[0].Id);
            Assert.AreEqual(article3.Id, recentlyViewedArticles[1].Id);
        }

        [TestMethod]
        public void TestRecentlyViewedVideos()
        {
            var userId = Guid.NewGuid();
            var videos = _resourcesQuery.GetVideos();

            var recentlyViewedVideos = _resourcesQuery.GetRecentlyViewedVideos(userId, 5);
            Assert.AreEqual(0, recentlyViewedVideos.Count);

            var video1 = videos[3];
            _resourcesCommand.ViewVideo(userId, video1.Id);

            recentlyViewedVideos = _resourcesQuery.GetRecentlyViewedVideos(userId, 5);
            Assert.AreEqual(1, recentlyViewedVideos.Count);

            SleepForDifferentSqlTimestamp();

            var video2 = videos[5];
            _resourcesCommand.ViewVideo(userId, video2.Id);

            SleepForDifferentSqlTimestamp();

            var video3 = videos[6];
            _resourcesCommand.ViewVideo(userId, video3.Id);

            // Make sure only the last 2 of the three viewings are returned.

            recentlyViewedVideos = _resourcesQuery.GetRecentlyViewedVideos(userId, 2);
            Assert.AreEqual(2, recentlyViewedVideos.Count);
            Assert.AreEqual(video3.Id, recentlyViewedVideos[0].Id);
            Assert.AreEqual(video2.Id, recentlyViewedVideos[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewVideo(userId, video1.Id);

            // Results should be different from previous

            recentlyViewedVideos = _resourcesQuery.GetRecentlyViewedVideos(userId, 2);
            Assert.AreEqual(2, recentlyViewedVideos.Count);
            Assert.AreEqual(video1.Id, recentlyViewedVideos[0].Id);
            Assert.AreEqual(video3.Id, recentlyViewedVideos[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 2 by a different user.

            _resourcesCommand.ViewVideo(Guid.NewGuid(), video2.Id);

            // Results should be the same as previous

            recentlyViewedVideos = _resourcesQuery.GetRecentlyViewedVideos(userId, 2);
            Assert.AreEqual(2, recentlyViewedVideos.Count);
            Assert.AreEqual(video1.Id, recentlyViewedVideos[0].Id);
            Assert.AreEqual(video3.Id, recentlyViewedVideos[1].Id);
        }

        [TestMethod]
        public void TestRecentlyViewedQnAs()
        {
            var userId = Guid.NewGuid();
            var qnas = _resourcesQuery.GetQnAs();

            var recentlyViewedQnAs = _resourcesQuery.GetRecentlyViewedQnAs(userId, 5);
            Assert.AreEqual(0, recentlyViewedQnAs.Count);

            var qna1 = qnas[3];
            _resourcesCommand.ViewQnA(userId, qna1.Id);

            recentlyViewedQnAs = _resourcesQuery.GetRecentlyViewedQnAs(userId, 5);
            Assert.AreEqual(1, recentlyViewedQnAs.Count);

            SleepForDifferentSqlTimestamp();

            var qna2 = qnas[5];
            _resourcesCommand.ViewQnA(userId, qna2.Id);

            SleepForDifferentSqlTimestamp();

            var qna3 = qnas[6];
            _resourcesCommand.ViewQnA(userId, qna3.Id);

            // Make sure only the last 2 of the three viewings are returned.

            recentlyViewedQnAs = _resourcesQuery.GetRecentlyViewedQnAs(userId, 2);
            Assert.AreEqual(2, recentlyViewedQnAs.Count);
            Assert.AreEqual(qna3.Id, recentlyViewedQnAs[0].Id);
            Assert.AreEqual(qna2.Id, recentlyViewedQnAs[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewQnA(userId, qna1.Id);

            // Results should be different from previous

            recentlyViewedQnAs = _resourcesQuery.GetRecentlyViewedQnAs(userId, 2);
            Assert.AreEqual(2, recentlyViewedQnAs.Count);
            Assert.AreEqual(qna1.Id, recentlyViewedQnAs[0].Id);
            Assert.AreEqual(qna3.Id, recentlyViewedQnAs[1].Id);

            SleepForDifferentSqlTimestamp();

            // View 2 by a different user.

            _resourcesCommand.ViewQnA(Guid.NewGuid(), qna2.Id);

            // Results should be the same as previous

            recentlyViewedQnAs = _resourcesQuery.GetRecentlyViewedQnAs(userId, 2);
            Assert.AreEqual(2, recentlyViewedQnAs.Count);
            Assert.AreEqual(qna1.Id, recentlyViewedQnAs[0].Id);
            Assert.AreEqual(qna3.Id, recentlyViewedQnAs[1].Id);
        }

        [TestMethod]
        public void TestTopViewedArticles()
        {
            var userId = Guid.NewGuid();
            var articles = _resourcesQuery.GetArticles();

            var article = _resourcesQuery.GetTopViewedArticle();
            Assert.IsNull(article);

            var article1 = articles[3];
            _resourcesCommand.ViewArticle(userId, article1.Id);

            article = _resourcesQuery.GetTopViewedArticle();
            Assert.AreEqual(articles[3].Id, article.Id);

            SleepForDifferentSqlTimestamp();

            var article2 = articles[5];
            _resourcesCommand.ViewArticle(userId, article2.Id);

            SleepForDifferentSqlTimestamp();

            _resourcesCommand.ViewArticle(userId, article2.Id);

            // Make sure only the last 2 of the three viewings are returned.

            article = _resourcesQuery.GetTopViewedArticle();
            Assert.AreEqual(article2.Id, article.Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewArticle(userId, article1.Id);
            _resourcesCommand.ViewArticle(userId, article1.Id);

            // Results should be different from previous

            article = _resourcesQuery.GetTopViewedArticle();
            Assert.AreEqual(article1.Id, article.Id);
        }

        [TestMethod]
        public void TestTopViewedVideos()
        {
            var userId = Guid.NewGuid();
            var videos = _resourcesQuery.GetVideos();

            var video = _resourcesQuery.GetTopViewedVideo();
            Assert.IsNull(video);

            var video1 = videos[3];
            _resourcesCommand.ViewVideo(userId, video1.Id);

            video = _resourcesQuery.GetTopViewedVideo();
            Assert.AreEqual(videos[3].Id, video.Id);

            SleepForDifferentSqlTimestamp();

            var video2 = videos[5];
            _resourcesCommand.ViewVideo(userId, video2.Id);

            SleepForDifferentSqlTimestamp();

            _resourcesCommand.ViewVideo(userId, video2.Id);

            // Make sure only the last 2 of the three viewings are returned.

            video = _resourcesQuery.GetTopViewedVideo();
            Assert.AreEqual(video2.Id, video.Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewVideo(userId, video1.Id);
            _resourcesCommand.ViewVideo(userId, video1.Id);

            // Results should be different from previous

            video = _resourcesQuery.GetTopViewedVideo();
            Assert.AreEqual(video1.Id, video.Id);
        }

        [TestMethod]
        public void TestTopViewedQnAs()
        {
            var userId = Guid.NewGuid();
            var qnas = _resourcesQuery.GetQnAs();

            var qna = _resourcesQuery.GetTopViewedQnA();
            Assert.IsNull(qna);

            var qna1 = qnas[3];
            _resourcesCommand.ViewQnA(userId, qna1.Id);

            qna = _resourcesQuery.GetTopViewedQnA();
            Assert.AreEqual(qnas[3].Id, qna.Id);

            SleepForDifferentSqlTimestamp();

            var qna2 = qnas[5];
            _resourcesCommand.ViewQnA(userId, qna2.Id);

            SleepForDifferentSqlTimestamp();

            _resourcesCommand.ViewQnA(userId, qna2.Id);

            // Make sure only the last 2 of the three viewings are returned.

            qna = _resourcesQuery.GetTopViewedQnA();
            Assert.AreEqual(qna2.Id, qna.Id);

            SleepForDifferentSqlTimestamp();

            // View 1 again.

            _resourcesCommand.ViewQnA(userId, qna1.Id);
            _resourcesCommand.ViewQnA(userId, qna1.Id);

            // Results should be different from previous

            qna = _resourcesQuery.GetTopViewedQnA();
            Assert.AreEqual(qna1.Id, qna.Id);
        }

        private static void SleepForDifferentSqlTimestamp()
        {
            Thread.Sleep(20);
        }
    }
}
