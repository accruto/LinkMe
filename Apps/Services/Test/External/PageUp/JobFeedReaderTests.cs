using System.Linq;
using System.ServiceModel.Syndication;
using LinkMe.Apps.Services.External.PageUp;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.PageUp
{
    [TestClass]
    public class JobFeedReaderTests
    {
        [TestMethod]
        public void ReadTest()
        {
            IJobFeedReader<SyndicationItem> reader = new JobFeedReader(FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\PageUp\SampleFeed.xml", RuntimeEnvironment.GetSourceFolder()));
            var posts = reader.GetPosts().ToArray();

            Assert.AreEqual(6, posts.Length);
        }
    }
}
