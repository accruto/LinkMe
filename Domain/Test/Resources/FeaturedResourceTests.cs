using System.Linq;
using LinkMe.Domain.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class FeaturedResourceTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestFeaturedArticles()
        {
            var articles = _resourcesQuery.GetFeaturedArticles();
            Assert.AreEqual(7, articles.Count);

            Assert.AreEqual(2, articles.Count(a => a.FeaturedResourceType == FeaturedResourceType.New));
            Assert.AreEqual(5, articles.Count(a => a.FeaturedResourceType == FeaturedResourceType.Slideshow));
        }

        [TestMethod]
        public void TestFeaturedVideos()
        {
            var videos = _resourcesQuery.GetFeaturedVideos();
            Assert.AreEqual(1, videos.Count);
        }

        [TestMethod]
        public void TestFeaturedQnAs()
        {
            var qnas = _resourcesQuery.GetFeaturedQnAs();
            Assert.AreEqual(1, qnas.Count);
        }
    }
}
