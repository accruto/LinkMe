using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds
{
    [TestClass]
    public class CriteriaEqualsTests
    {
        [TestMethod]
        public void TestAdTitle()
        {
            var criteria1 = new JobAdSearchCriteria { AdTitle = "Business Analyst" };
            var criteria2 = new JobAdSearchCriteria { AdTitle = "Business Analyst" };
            Assert.AreEqual(criteria1, criteria2);
            Assert.AreEqual(criteria1.GetHashCode(), criteria2.GetHashCode());
        }
    }
}
