using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    [TestClass]
    public class DeserializationTests
        : MonsterFeedTests
    {
        [TestMethod]
        public void TestCanDeserializeFeed()
        {
            var file = FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\Monster\SampleFeed.xml", RuntimeEnvironment.GetSourceFolder());
            var jobs = GetJobFeed(file);
            Assert.AreEqual(1377, jobs.Count);
        }
    }
}