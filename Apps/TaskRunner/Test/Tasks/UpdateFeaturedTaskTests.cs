using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Featured.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;
using LinkMe.TaskRunner.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks
{
    [TestClass]
    public class UpdateFeaturedTaskTests
        : TaskTests
    {
        private readonly IFeaturedCommand _featuredCommand = Resolve<IFeaturedCommand>();
        private readonly IFeaturedQuery _featuredQuery = Resolve<IFeaturedQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobAdReportsQuery _jobAdReportsQuery = Resolve<IJobAdReportsQuery>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery = Resolve<IEmployerMemberAccessReportsQuery>();
        private readonly IMemberSearchReportsQuery _memberSearchReportsQuery = Resolve<IMemberSearchReportsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUpdateStatistics()
        {
            var statistics = new FeaturedStatistics();
            _featuredCommand.UpdateFeaturedStatistics(statistics);
            AssertStatistics(0, 0, 0, 0, _featuredQuery.GetFeaturedStatistics());

            _memberAccountsCommand.CreateTestMember(0);
            _memberAccountsCommand.CreateTestMember(1);
            _memberAccountsCommand.CreateTestMember(2);

            new UpdateFeaturedTask(_featuredCommand, _jobAdsQuery, _jobAdReportsQuery, _accountReportsQuery, _employerMemberAccessReportsQuery, _memberSearchReportsQuery).ExecuteTask(new[] { "7", "100" });
            AssertStatistics(0, 3, 0, 0, _featuredQuery.GetFeaturedStatistics());
        }

        [TestMethod]
        public void TestUpdateJobAds()
        {
            new UpdateFeaturedTask(_featuredCommand, _jobAdsQuery, _jobAdReportsQuery, _accountReportsQuery, _employerMemberAccessReportsQuery, _memberSearchReportsQuery).ExecuteTask(new[] { "7", "100" });
            AssertJobAds(new FeaturedItem[0], _featuredQuery.GetFeaturedJobAds());

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            new UpdateFeaturedTask(_featuredCommand, _jobAdsQuery, _jobAdReportsQuery, _accountReportsQuery, _employerMemberAccessReportsQuery, _memberSearchReportsQuery).ExecuteTask(new[] { "7", "100" });

            AssertJobAds(new[] { CreateFeaturedJobAd(jobAd) }, _featuredQuery.GetFeaturedJobAds());
        }

        private static FeaturedItem CreateFeaturedJobAd(JobAd jobAd)
        {
            return new FeaturedItem
            {
                Id = jobAd.Id,
                Title = jobAd.Title + " (" + jobAd.Description.Location + ")",
                Url = "~/jobs/" + jobAd.GetLocationDisplayText().ToLower().Replace(' ', '-') + "/" + jobAd.Description.Industries[0].UrlName + "/" + jobAd.Title.ToLower() + "/" + jobAd.Id
            };
        }

        private static void AssertStatistics(int expectedCreatedJobAds, int expectedMembers, int expectedMemberSearches, int expectedMemberAccesses, FeaturedStatistics statistics)
        {
            Assert.AreEqual(expectedCreatedJobAds, statistics.CreatedJobAds);
            Assert.AreEqual(expectedMembers, statistics.Members);
            Assert.AreEqual(expectedMemberSearches, statistics.MemberSearches);
            Assert.AreEqual(expectedMemberAccesses, statistics.MemberAccesses);
        }

        private static void AssertJobAds(ICollection<FeaturedItem> expectedJobAds, ICollection<FeaturedItem> jobAds)
        {
            Assert.AreEqual(expectedJobAds.Count, jobAds.Count);
            foreach (var expectedJobAd in expectedJobAds)
                AssertJobAd(expectedJobAd, (from j in jobAds where j.Id == expectedJobAd.Id select j).Single());
        }

        private static void AssertJobAd(FeaturedItem expectedJobAd, FeaturedItem jobAd)
        {
            Assert.AreEqual(expectedJobAd.Title, jobAd.Title);
            Assert.AreEqual(expectedJobAd.Url, jobAd.Url);
        }
    }
}
