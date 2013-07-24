using LinkMe.Apps.Agents.Test.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class VecciTests
        : SpecificCommunityTests
    {
        private const string HomeContent = "LinkMe will help you find jobs in two ways";
        private const string NonVecciHomeContent = "1000s of employers search LinkMe to fill everything from engineering jobs to health jobs to IT jobs.";

        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Vecci;
        }

        [TestMethod]
        public void TestMemberHomePage()
        {
            var data = TestCommunity.Vecci.GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Hit the landing page.

            var url = GetCommunityPathUrl(community, "");
            Get(url);

            // Vecci should get the standard home page, and not the shared community home page.

            AssertUrl(HomeUrl);
            AssertPageContains(HomeContent);
            AssertPageDoesNotContain(NonVecciHomeContent);
        }
    }
}
