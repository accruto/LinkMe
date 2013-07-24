using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.External.PageUp;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.PageUp
{
    [TestClass]
    public class JobFeedReaderTaskTests
        : TestClass
    {
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestLongSummary()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var task = CreateTask(integratorUser, jobPoster);
            task.ExecuteTask();

            // Check the job ad is created.

            var ids = _jobAdsQuery.GetJobAdIds(jobPoster.Id, JobAdStatus.Open);
            Assert.AreEqual(1, ids.Count);

            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(ids[0]);
            Assert.AreEqual(300, jobAd.Description.Summary.Length);
            Assert.AreEqual("This is a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very ve...", jobAd.Description.Summary);
        }

        private static JobFeedReaderTask CreateTask(IntegratorUser integratorUser, Employer jobPoster)
        {
            return new JobFeedReaderTask(
                Resolve<IJobAdsCommand>(),
                Resolve<IJobAdsQuery>(),
                Resolve<IJobAdIntegrationQuery>(),
                Resolve<IExternalJobAdsCommand>(),
                Resolve<IExternalJobAdsQuery>(),
                Resolve<IJobAdIntegrationReportsCommand>(),
                Resolve<IIntegrationQuery>(),
                Resolve<IIndustriesQuery>(),
                Resolve<ILocationQuery>())
                {
                    RemoteUrl = FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\PageUp\LongSummaryFeed.xml", RuntimeEnvironment.GetSourceFolder()),
                    IntegratorUserLoginId = integratorUser.LoginId,
                    JobPosterLoginId = jobPoster.GetLoginId(),
                };
        }
    }
}
