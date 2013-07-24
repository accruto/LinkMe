using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAdSort
{
    [TestClass]
    public class JobAdSortSubscriberTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IExecuteJobAdSortCommand _executeJobAdSortCommand = Resolve<IExecuteJobAdSortCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        private Country _australia;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSortHost.ClearIndex();
        }

        [TestMethod]
        public void TestSimpleSort()
        {
            _australia = _locationQuery.GetCountry("Australia");

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var one = employer.CreateTestJobAd("Title one", "The content for the first ad");
            one.CreatedTime = DateTime.Now.AddDays(-1); // Just to make sure it's before "two"; we only index days.
            one.Description.Salary = new Salary { LowerBound = 50000, UpperBound = 60000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _jobAdsCommand.CreateJobAd(one);
            _jobAdsCommand.OpenJobAd(one);

            var two = employer.CreateTestJobAd("Title two", "Different content for the second ad");
            _locationQuery.ResolvePostalSuburb(two.Description.Location, _australia, "2000");
            two.Description.CompanyName = "Really Bad Employers";
            two.Description.JobTypes = JobTypes.Contract;
            two.Description.Industries = new List<Industry> { _industriesQuery.GetIndustry("Other") };
            _jobAdsCommand.CreateJobAd(two);
            _jobAdsCommand.OpenJobAd(two);

            var member = new Member { Id = Guid.NewGuid() };
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, one.Id);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, two.Id);
            
            // Title only

            var criteria = new JobAdSearchSortCriteria {SortOrder = JobAdSortOrder.JobType};
            TestSort(member, criteria, one, two);

            // Title and content

            criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            TestSort(member, criteria, two, one);

            // No results

            //criteria = new JobAdSortCriteria { AdTitle = "one", Keywords = "second" };
            //TestSort(criteria);
        }

        private void TestSort(IMember member, JobAdSearchSortCriteria criteria, params JobAd[] expectedResults)
        {
            var execution = _executeJobAdSortCommand.SortFlagged(member, criteria, new Range(0, expectedResults.Length + 1));

            Assert.AreEqual(expectedResults.Length, execution.Results.TotalMatches);
            Assert.AreEqual(expectedResults.Length, execution.Results.JobAdIds.Count);
            for (var index = 0; index < expectedResults.Length; index++)
                Assert.AreEqual(expectedResults[index].Id, execution.Results.JobAdIds[index], string.Format("Result {0} did not match", index));
        }
    }
}