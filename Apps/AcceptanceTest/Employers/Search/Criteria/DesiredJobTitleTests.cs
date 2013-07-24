using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class DesiredJobTitleTests
        : CriteriaTests
    {
        private const string JobTitle = "Archeologist";
        private const string QuotesJobTitle = "\"compliance officer\" OR \"compliance manager\"";

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.DesiredJobTitle = null;
            TestDisplay(criteria);

            criteria.DesiredJobTitle = JobTitle;
            TestDisplay(criteria);

            criteria.DesiredJobTitle = QuotesJobTitle;
            TestDisplay(criteria);
        }
    }
}