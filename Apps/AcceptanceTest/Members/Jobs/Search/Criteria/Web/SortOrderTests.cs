using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class SortOrderTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.SortCriteria = null;
            TestDisplay(false, criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            TestDisplay(false, criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.Salary };
            TestDisplay(false, criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            TestDisplay(false, criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.Salary, ReverseSortOrder = true };
            TestDisplay(false, criteria);
        }

        [TestMethod]
        public void TestCreatedTimeDescending()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // The option used to be called CreatedTimeDescending which some external sources seem to still send through.

            var url = GetSearchUrl(criteria).AsNonReadOnly();
            url.QueryString["SortOrder"] = "CreatedTimeDescending";
            url.QueryString["SortOrderDirection"] = "SortOrderIsDescending";

            Get(url);

            // Should be treated as:

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            AssertCriteria(criteria);
        }
    }
}