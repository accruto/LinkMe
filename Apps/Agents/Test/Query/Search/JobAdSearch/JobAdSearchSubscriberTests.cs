using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAdSearch
{
    [TestClass]
    public class JobAdSearchSubscriberTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private Country _australia;

        // Punctuation characters listed in order from least to most likely to be inserted.

        private JobAd _one, _two;

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSearchHost.ClearIndex();

            var employer = _employerAccountsCommand.CreateTestEmployer("jobposter", _organisationsCommand.CreateTestOrganisation("The Job Advertiser"));

            _one = employer.CreateTestJobAd("Title one", "The content for the first ad");
            _one.CreatedTime = DateTime.Now.AddDays(-1); // Just to make sure it's before "two"; we only index days.
            _one.Description.Salary = new Salary { LowerBound = 50000, UpperBound = 60000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _jobAdsCommand.CreateJobAd(_one);
            _jobAdsCommand.OpenJobAd(_one);

            _two = employer.CreateTestJobAd("Title two", "Different content for the second ad");
            _locationQuery.ResolvePostalSuburb(_two.Description.Location, _australia, "2000");
            _two.Description.CompanyName = "Really Bad Employers";
            _two.Description.JobTypes = JobTypes.Contract;
            _two.Description.Industries = new List<Industry> { _industriesQuery.GetIndustry("Other") };
            _jobAdsCommand.CreateJobAd(_two);
            _jobAdsCommand.OpenJobAd(_two);
        }

        [TestMethod]
        public void TestSimpleSearch()
        {
            // Title only

            var criteria = new JobAdSearchCriteria { AdTitle = "ONE" };
            TestSearch(criteria, _one);

            // Title and content

            criteria = new JobAdSearchCriteria { AdTitle = "title", SortCriteria = new JobAdSearchSortCriteria {SortOrder = JobAdSortOrder.CreatedTime} };
            criteria.SetKeywords("content");
            TestSearch(criteria, _two, _one);

            // Content and Locality

            GeographicalArea area = _locationQuery.GetPostalCode(_australia, "3143").Locality;
            criteria = new JobAdSearchCriteria { Location = new LocationReference(area) };
            criteria.SetKeywords("ad AND content");
            TestSearch(criteria, _one);

            // State

            area = _locationQuery.GetCountrySubdivision(_australia, "NSW");
            criteria = new JobAdSearchCriteria { Location = new LocationReference(area) };
            TestSearch(criteria, _two);

            // No results

            criteria = new JobAdSearchCriteria { AdTitle = "one" };
            criteria.SetKeywords("second");
            TestSearch(criteria);
        }

        [TestMethod]
        public void TestAdvancedSearch()
        {
            // Title only

            var criteria = new JobAdSearchCriteria { AdTitle = "ONE" };
            TestSearch(criteria, _one);

            // Job poster name.

            criteria = new JobAdSearchCriteria { AdvertiserName = "advertiser", SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            TestSearch(criteria, _two, _one);

            // Employer name.

            criteria = new JobAdSearchCriteria { AdvertiserName = "great", SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            TestSearch(criteria, _one);

            // Exact phrase

            criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            criteria.SetKeywords(null, "content for the", null, null);
            TestSearch(criteria, _two, _one);

            // Any words and without words

            criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            criteria.SetKeywords(null, null, "content", "different");
            TestSearch(criteria, _one);

            // Distance

            var area = _locationQuery.GetPostalCode(_australia, "3143").Locality;
            criteria = new JobAdSearchCriteria { Location = new LocationReference(area), Distance = 5, SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            TestSearch(criteria);

            criteria = new JobAdSearchCriteria { Location = new LocationReference(area), Distance = 25, SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            TestSearch(criteria, _one);

            // Salary

            var salary = new Salary { LowerBound = 60000, UpperBound = 70000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            criteria = new JobAdSearchCriteria { Salary = salary, SortCriteria = new JobAdSearchSortCriteria{SortOrder = JobAdSortOrder.CreatedTime} };
            TestSearch(criteria, _one);

            // Job types

            criteria = new JobAdSearchCriteria { JobTypes = JobTypes.PartTime | JobTypes.Contract };
            TestSearch(criteria, _two);

            // Industry

            var industryIds = new[] { Resolve<IIndustriesQuery>().GetIndustry("Other").Id };
            criteria = new JobAdSearchCriteria { IndustryIds = industryIds };
            TestSearch(criteria, _two);
        }

        private void TestSearch(JobAdSearchCriteria criteria, params JobAd[] expectedResults)
        {
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(expectedResults.Length, execution.Results.TotalMatches);
            Assert.AreEqual(expectedResults.Length, execution.Results.JobAdIds.Count);
            for (var index = 0; index < expectedResults.Length; index++)
                Assert.AreEqual(expectedResults[index].Id, execution.Results.JobAdIds[index], string.Format("Result {0} did not match", index));
        }
    }
}