using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class JobTitleTests
        : CriteriaTests
    {
        private const string JobTitle = "Archeologist";
        private const string QuotesJobTitle = "\"compliance officer\" OR \"compliance manager\"";

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.JobTitle = null;
            TestDisplay(criteria);

            criteria.JobTitle = JobTitle;
            TestDisplay(criteria);

            criteria.JobTitlesToSearch = JobsToSearch.AllJobs;
            TestDisplay(criteria);

            criteria.JobTitlesToSearch = JobsToSearch.RecentJobs;
            TestDisplay(criteria);

            criteria.JobTitle = QuotesJobTitle;
            TestDisplay(criteria);
        }
    }
}