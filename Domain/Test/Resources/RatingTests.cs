using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class RatingTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestRateArticle()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var articleId = _resourcesQuery.GetArticles()[0].Id;
            _resourcesCommand.RateArticle(userId1, articleId, 4);
            _resourcesCommand.RateArticle(userId2, articleId, 3);

            var ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { articleId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[articleId].RatingCount);
            Assert.AreEqual(3.5, ratings[articleId].AverageRating);
            Assert.AreEqual((byte)4, ratings[articleId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { articleId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[articleId].RatingCount);
            Assert.AreEqual(3.5, ratings[articleId].AverageRating);
            Assert.AreEqual((byte)3, ratings[articleId].UserRating);

            _resourcesCommand.RateArticle(userId2, articleId, 5);

            ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { articleId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[articleId].RatingCount);
            Assert.AreEqual(4.5, ratings[articleId].AverageRating);
            Assert.AreEqual((byte)4, ratings[articleId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { articleId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[articleId].RatingCount);
            Assert.AreEqual(4.5, ratings[articleId].AverageRating);
            Assert.AreEqual((byte)5, ratings[articleId].UserRating);
        }

        [TestMethod]
        public void TestRateVideo()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var videoId = _resourcesQuery.GetVideos()[0].Id;
            _resourcesCommand.RateVideo(userId1, videoId, 4);
            _resourcesCommand.RateVideo(userId2, videoId, 3);

            var ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { videoId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[videoId].RatingCount);
            Assert.AreEqual(3.5, ratings[videoId].AverageRating);
            Assert.AreEqual((byte)4, ratings[videoId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { videoId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[videoId].RatingCount);
            Assert.AreEqual(3.5, ratings[videoId].AverageRating);
            Assert.AreEqual((byte)3, ratings[videoId].UserRating);

            _resourcesCommand.RateVideo(userId2, videoId, 5);

            ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { videoId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[videoId].RatingCount);
            Assert.AreEqual(4.5, ratings[videoId].AverageRating);
            Assert.AreEqual((byte)4, ratings[videoId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { videoId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[videoId].RatingCount);
            Assert.AreEqual(4.5, ratings[videoId].AverageRating);
            Assert.AreEqual((byte)5, ratings[videoId].UserRating);
        }

        [TestMethod]
        public void TestRateQnA()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var qnaId = _resourcesQuery.GetQnAs()[0].Id;
            _resourcesCommand.RateQnA(userId1, qnaId, 4);
            _resourcesCommand.RateQnA(userId2, qnaId, 3);

            var ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { qnaId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[qnaId].RatingCount);
            Assert.AreEqual(3.5, ratings[qnaId].AverageRating);
            Assert.AreEqual((byte)4, ratings[qnaId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { qnaId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[qnaId].RatingCount);
            Assert.AreEqual(3.5, ratings[qnaId].AverageRating);
            Assert.AreEqual((byte)3, ratings[qnaId].UserRating);

            _resourcesCommand.RateQnA(userId2, qnaId, 5);

            ratings = _resourcesQuery.GetRatingSummaries(userId1, new[] { qnaId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[qnaId].RatingCount);
            Assert.AreEqual(4.5, ratings[qnaId].AverageRating);
            Assert.AreEqual((byte)4, ratings[qnaId].UserRating);

            ratings = _resourcesQuery.GetRatingSummaries(userId2, new[] { qnaId });
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual(2, ratings[qnaId].RatingCount);
            Assert.AreEqual(4.5, ratings[qnaId].AverageRating);
            Assert.AreEqual((byte)5, ratings[qnaId].UserRating);
        }

        [TestMethod]
        public void TestTopRatedArticle()
        {
            var userId = Guid.NewGuid();

            var articleId1 = _resourcesQuery.GetArticles()[0].Id;
            var articleId2 = _resourcesQuery.GetArticles()[1].Id;
            var articleId3 = _resourcesQuery.GetArticles()[2].Id;
            var articleId4 = _resourcesQuery.GetArticles()[3].Id;

            Assert.IsNull(_resourcesQuery.GetTopRatedArticle());

            _resourcesCommand.RateArticle(userId, articleId1, 1);
            Assert.AreEqual(articleId1, _resourcesQuery.GetTopRatedArticle().Id);

            _resourcesCommand.RateArticle(userId, articleId2, 2);

            Assert.AreEqual(articleId2, _resourcesQuery.GetTopRatedArticle().Id);

            _resourcesCommand.RateArticle(userId, articleId3, 3);
            _resourcesCommand.RateArticle(userId, articleId4, 4);

            Assert.AreEqual(articleId4, _resourcesQuery.GetTopRatedArticle().Id);

            _resourcesCommand.RateArticle(userId, articleId3, 5);
            _resourcesCommand.RateArticle(Guid.NewGuid(), articleId3, 5);

            Assert.AreEqual(articleId3, _resourcesQuery.GetTopRatedArticle().Id);
        }

        [TestMethod]
        public void TestTopRatedVideo()
        {
            var userId = Guid.NewGuid();

            var videoId1 = _resourcesQuery.GetVideos()[0].Id;
            var videoId2 = _resourcesQuery.GetVideos()[1].Id;
            var videoId3 = _resourcesQuery.GetVideos()[2].Id;
            var videoId4 = _resourcesQuery.GetVideos()[3].Id;

            Assert.IsNull(_resourcesQuery.GetTopRatedVideo());

            _resourcesCommand.RateVideo(userId, videoId1, 1);
            Assert.AreEqual(videoId1, _resourcesQuery.GetTopRatedVideo().Id);

            _resourcesCommand.RateVideo(userId, videoId2, 2);

            Assert.AreEqual(videoId2, _resourcesQuery.GetTopRatedVideo().Id);

            _resourcesCommand.RateVideo(userId, videoId3, 3);
            _resourcesCommand.RateVideo(userId, videoId4, 4);

            Assert.AreEqual(videoId4, _resourcesQuery.GetTopRatedVideo().Id);

            _resourcesCommand.RateVideo(userId, videoId3, 5);
            _resourcesCommand.RateVideo(Guid.NewGuid(), videoId3, 5);

            Assert.AreEqual(videoId3, _resourcesQuery.GetTopRatedVideo().Id);
        }

        [TestMethod]
        public void TestTopRatedQnA()
        {
            var userId = Guid.NewGuid();

            var qnaId1 = _resourcesQuery.GetQnAs()[0].Id;
            var qnaId2 = _resourcesQuery.GetQnAs()[1].Id;
            var qnaId3 = _resourcesQuery.GetQnAs()[2].Id;
            var qnaId4 = _resourcesQuery.GetQnAs()[3].Id;

            Assert.IsNull(_resourcesQuery.GetTopRatedQnA());

            _resourcesCommand.RateQnA(userId, qnaId1, 1);
            Assert.AreEqual(qnaId1, _resourcesQuery.GetTopRatedQnA().Id);

            _resourcesCommand.RateQnA(userId, qnaId2, 2);

            Assert.AreEqual(qnaId2, _resourcesQuery.GetTopRatedQnA().Id);

            _resourcesCommand.RateQnA(userId, qnaId3, 3);
            _resourcesCommand.RateQnA(userId, qnaId4, 4);

            Assert.AreEqual(qnaId4, _resourcesQuery.GetTopRatedQnA().Id);

            _resourcesCommand.RateQnA(userId, qnaId3, 5);
            _resourcesCommand.RateQnA(Guid.NewGuid(), qnaId3, 5);

            Assert.AreEqual(qnaId3, _resourcesQuery.GetTopRatedQnA().Id);
        }
    }
}
