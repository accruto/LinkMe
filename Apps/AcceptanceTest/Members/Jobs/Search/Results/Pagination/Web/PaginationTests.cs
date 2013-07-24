using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Pagination.Web
{
    [TestClass]
    public class PaginationTests
        : ResultsTests
    {
        private const string Title = "Archeologist";

        [TestMethod]
        public void TestPagination()
        {
            var employer = CreateEmployer(0);
            var jobAds = (from i in Enumerable.Range(0, 55) select PostJobAd(employer, i)).ToArray();

            TestResults(null, null, false, jobAds.Skip(0).Take(10).ToArray());

            TestResults(1, 10, false, jobAds.Skip(0).Take(10).ToArray());
            TestResults(2, 10, false, jobAds.Skip(10).Take(10).ToArray());
            TestResults(3, 10, false, jobAds.Skip(20).Take(10).ToArray());
            TestResults(4, 10, false, jobAds.Skip(30).Take(10).ToArray());
            TestResults(5, 10, false, jobAds.Skip(40).Take(10).ToArray());
            TestResults(6, 10, false, jobAds.Skip(50).Take(10).ToArray());
            TestResults(7, 10, false, jobAds.Skip(60).Take(10).ToArray());

            TestResults(1, 25, false, jobAds.Skip(0).Take(25).ToArray());
            TestResults(2, 25, false, jobAds.Skip(25).Take(25).ToArray());
            TestResults(3, 25, false, jobAds.Skip(50).Take(25).ToArray());
            TestResults(4, 25, false, jobAds.Skip(75).Take(25).ToArray());

            // Test bad page.

            TestResults(-1, null, false, jobAds.Skip(0).Take(10).ToArray());
        }

        [TestMethod]
        public void TestReversePagination()
        {
            var employer = CreateEmployer(0);
            var jobAds = (from i in Enumerable.Range(0, 55) select PostJobAd(employer, i)).ToArray();

            TestResults(null, null, true, jobAds.Reverse().Skip(0).Take(10).ToArray());

            TestResults(1, 10, true, jobAds.Reverse().Skip(0).Take(10).ToArray());
            TestResults(2, 10, true, jobAds.Reverse().Skip(10).Take(10).ToArray());
            TestResults(3, 10, true, jobAds.Reverse().Skip(20).Take(10).ToArray());
            TestResults(4, 10, true, jobAds.Reverse().Skip(30).Take(10).ToArray());
            TestResults(5, 10, true, jobAds.Reverse().Skip(40).Take(10).ToArray());
            TestResults(6, 10, true, jobAds.Reverse().Skip(50).Take(10).ToArray());
            TestResults(7, 10, true, jobAds.Reverse().Skip(60).Take(10).ToArray());

            TestResults(1, 25, true, jobAds.Reverse().Skip(0).Take(25).ToArray());
            TestResults(2, 25, true, jobAds.Reverse().Skip(25).Take(25).ToArray());
            TestResults(3, 25, true, jobAds.Reverse().Skip(50).Take(25).ToArray());
            TestResults(4, 25, true, jobAds.Reverse().Skip(75).Take(25).ToArray());
        }

        private JobAd PostJobAd(IEmployer employer, int index)
        {
            var jobAd = employer.CreateTestJobAd(Title);
            jobAd.CreatedTime = DateTime.Now.AddDays(-1 * index);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private void TestResults(int? page, int? items, bool reverseSortOrder, params JobAd[] expectedJobAds)
        {
            var criteria = new JobAdSearchCriteria { AdTitle = Title, SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = reverseSortOrder }};
            Get(GetSearchUrl(criteria, page, items));
            AssertResults(true, expectedJobAds);

            // Assert.

            var model = ApiSearch(criteria, page, items);
            AssertApiResults(model, true, expectedJobAds);
        }
    }
}
