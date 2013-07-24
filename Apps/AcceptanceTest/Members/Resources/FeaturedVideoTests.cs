using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class FeaturedVideoTests
        : ResourcesTests
    {
        [TestMethod]
        public void TestNew()
        {
            var featuredVideo = _resourcesQuery.GetFeaturedVideos().Single();
            var video = _resourcesQuery.GetVideo(featuredVideo.ResourceId);

            Get(_resourcesUrl);
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='featuredvideo']/div[@class='bg']");
            Assert.AreEqual(1, nodes.Count);
            var node = nodes[0];

            var a = node.SelectSingleNode("a");
            var href = a.Attributes["href"].Value;
            var id = new Guid(href.Substring(href.LastIndexOf('/') + 1));
            var img = node.SelectSingleNode("img");

            Assert.AreEqual(video.Id, id);
            Assert.AreEqual(video.Title, a.InnerText);
            Assert.AreEqual("http://i.ytimg.com/vi/" + video.ExternalVideoId + "/0.jpg", img.Attributes["src"].Value);
        }
    }
}
