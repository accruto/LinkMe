using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Mobile
{
    [TestClass]
    public class MembersJobsTests
        : JobsTests
    {
        private ReadOnlyApplicationUrl _jobsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
            _jobsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs");
        }

        [TestMethod]
        public void TestLinks()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_jobsUrl);
            AssertUrl(_jobsUrl);

            AssertLink("//a[@class='button jobsappliedfor']", new ReadOnlyApplicationUrl("~/members/jobs/applications"), "Jobs I&#39;ve applied for");
            AssertLink("//a[@class='button suggestedjobs']", new ReadOnlyApplicationUrl("~/members/jobs/suggested"), "Suggested jobs");
            AssertLink("//a[@class='button savedjobs']", new ReadOnlyApplicationUrl("~/members/jobs/folders/mobile"), "Saved jobs");
        }
    }
}
