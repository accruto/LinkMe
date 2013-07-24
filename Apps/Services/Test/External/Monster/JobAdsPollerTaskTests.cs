using System.IO;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.Monster;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    [TestClass]
    public class JobAdsPollerTaskTests
        : TestClass
    {
        private const string LoginId = "CareerOne-jobs";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCanProcessProductionFeed()
        {
            CreateEmployer();
            ExecuteTask(true, "SampleFeed.xml");
        }

        [TestMethod]
        public void TestJob()
        {
            var employer = CreateEmployer();
            ExecuteTask(false, "SampleJob.xml");

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds(employer.Id));
            Assert.AreEqual(1, jobAds.Count);

            Assert.AreEqual("118204981", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual("200116851", jobAds[0].Integration.ExternalReferenceId);
            Assert.AreEqual("Change Analyst / Process Analyst", jobAds[0].Title);
        }

        [TestMethod]
        public void TestBadEmailAddress()
        {
            var employer = CreateEmployer();
            ExecuteTask(false, "SampleBadEmailAddress.xml");

            // The email address will be ignored.

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds(employer.Id));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual("118203455", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual("DYNAMIC SALES CONSULTANTS - Full-Time", jobAds[0].Title);
            Assert.IsNull(jobAds[0].ContactDetails.EmailAddress);
            Assert.IsNull(jobAds[0].ContactDetails.SecondaryEmailAddresses);
        }

        [TestMethod]
        public void TestPhoneNumberTooLong()
        {
            var employer = CreateEmployer();
            ExecuteTask(false, "SamplePhoneNumberTooLong.xml");

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds(employer.Id));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual("118254914", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual("3298-5533", jobAds[0].ContactDetails.PhoneNumber);
        }

        [TestMethod]
        public void TestBadPhoneNumber()
        {
            var employer = CreateEmployer();
            ExecuteTask(false, "SampleBadPhoneNumber.xml");

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds(employer.Id));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual("118932977", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual(null, jobAds[0].ContactDetails.PhoneNumber);
        }

        private static JobAdsPollerTask CreateTask(bool useMockRepository, string file)
        {
            var jobAdsCommand = useMockRepository
                ? new JobAdsCommand(new MockJobAdsRepository(), 14, 30)
                : Resolve<IJobAdsCommand>();

            file = FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\Monster\" + file, RuntimeEnvironment.GetSourceFolder());

            return new JobAdsPollerTask(
                jobAdsCommand,
                Resolve<IJobAdsQuery>(),
                Resolve<IJobAdIntegrationQuery>(),
                Resolve<IExternalJobAdsCommand>(),
                Resolve<IExternalJobAdsQuery>(),
                Resolve<IJobAdIntegrationReportsCommand>(),
                Resolve<ILocationQuery>(),
                Resolve<IIndustriesQuery>(),
                Resolve<ICareerOneQuery>())
                {
                    RemoteBaseUrl = Path.GetDirectoryName(file),
                    RemoteUsername = string.Empty,
                    RemotePassword = string.Empty
                };
        }

        private static void ExecuteTask(bool useMockRepository, string file)
        {
            var task = CreateTask(useMockRepository, file);
            task.ExecuteTask(new[] { "*", "*", "*", Path.GetFileName(file) });
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(LoginId, _organisationsCommand.CreateTestOrganisation(0));
        }
    }
}