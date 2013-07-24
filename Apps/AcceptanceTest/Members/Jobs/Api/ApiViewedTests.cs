using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Api
{
    [TestClass]
    public class ApiViewedTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdViewsQuery _jobAdViewsQuery = Resolve<IJobAdViewsQuery>();

        [TestMethod]
        public void TestAnonymous()
        {
            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            Assert.IsTrue(_jobAdViewsQuery.GetViewedJobAdIds(anonymousId, new[] { jobAd.Id }).CollectionEqual(new Guid[0]));
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedCount(jobAd.Id));

            // View.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetViewedUrl(jobAd.Id))));
            Assert.IsTrue(_jobAdViewsQuery.GetViewedJobAdIds(anonymousId, new[] { jobAd.Id }).CollectionEqual(new[] { jobAd.Id }));
            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(anonymousId, jobAd.Id));
        }

        [TestMethod]
        public void TestMember()
        {
            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);

            var member = CreateMember();
            LogIn(member);
            Assert.IsTrue(_jobAdViewsQuery.GetViewedJobAdIds(member.Id, new Guid[0]).CollectionEqual(new Guid[0]));
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedCount(jobAd.Id));

            // View.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetViewedUrl(jobAd.Id))));
            Assert.IsTrue(_jobAdViewsQuery.GetViewedJobAdIds(member.Id, new[] { jobAd.Id }).CollectionEqual(new[] { jobAd.Id }));
            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(member.Id, jobAd.Id));
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private static ReadOnlyUrl GetViewedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/api/" + jobAdId + "/viewed");
        }
    }
}
