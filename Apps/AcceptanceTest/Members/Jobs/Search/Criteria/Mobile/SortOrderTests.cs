using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Mobile
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
            TestDisplay(criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            TestDisplay(criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.Salary };
            TestDisplay(criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            TestDisplay(criteria);

            criteria.SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.Salary, ReverseSortOrder = true };
            TestDisplay(criteria);
        }
    }
}