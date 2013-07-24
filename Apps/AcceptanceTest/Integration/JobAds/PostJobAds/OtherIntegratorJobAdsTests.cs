using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class OtherIntegratorJobAdsTests
        : PostJobAdsTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        
        private const string ExternalReferenceIdFormat = "ABC00{0}";
        private const string TitleFormat = "The is the {0} title";

        [TestMethod]
        public void TestJobG8Integrator()
        {
            var integratorUser = CreateIntegratorUser(1);
            var employer1 = CreateEmployer(1);

            var jobG8IntegratorUser = _jobG8Query.GetIntegratorUser();
            var employer2 = CreateEmployer(2);

            // Create the job ad for employer 2 who belongs with JobG8.

            var externalReferenceId = CreateExternalReferenceId(0);
            var title = CreateTitle(0);
            var jobAd = CreateJobAd(0, j => { j.ExternalReferenceId = externalReferenceId; j.Title = title; });
            CreateJobAd(jobG8IntegratorUser, employer2.Id, jobAd);

            // Try to create with the other integrator which will not work because JobG8 has higher priority.

            PostJobAds(integratorUser, employer1, false, new[] { jobAd }, 0, 0, 0, 0);

            AssertJobAds(integratorUser, employer1.Id);
            AssertJobAds(jobG8IntegratorUser, employer2.Id, jobAd);
        }

        [TestMethod]
        public void TestCareerOneIntegrator()
        {
            var integratorUser = CreateIntegratorUser(1);
            var employer1 = CreateEmployer(1);

            var careerOneIntegrator = _careerOneQuery.GetIntegratorUser();
            var employer2 = CreateEmployer(2);

            // Create the job ad for employer 1 who belongs with JobG8.

            var externalReferenceId = CreateExternalReferenceId(0);
            var title = CreateTitle(0);
            var jobAd = CreateJobAd(0, j => { j.ExternalReferenceId = externalReferenceId; j.Title = title; });
            CreateJobAd(careerOneIntegrator, employer2.Id, jobAd);

            // Try to create with the other integrator which will not work because CareerOne has higher priority.

            PostJobAds(integratorUser, employer1, false, new[] { jobAd }, 0, 0, 0, 0);

            AssertJobAds(integratorUser, employer1.Id);
            AssertJobAds(careerOneIntegrator, employer2.Id, jobAd);
        }

        [TestMethod]
        public void TestOtherIntegrator()
        {
            var integratorUser = CreateIntegratorUser(1);
            var employer1 = CreateEmployer(1);

            var otherIntegrator = CreateIntegratorUser(2);
            var employer2 = CreateEmployer(2);

            // Create the job ad for employer 2 who belongs to the other integrator.

            var externalReferenceId = CreateExternalReferenceId(0);
            var title = CreateTitle(0);
            var jobAd = CreateJobAd(0, j => { j.ExternalReferenceId = externalReferenceId; j.Title = title; });
            CreateJobAd(otherIntegrator, employer2.Id, jobAd);

            // Try to create with the other integrator because it got in first.

            PostJobAds(integratorUser, employer1, false, new[] { jobAd }, 0, 0, 0, 0);

            AssertJobAds(integratorUser, employer1.Id);
            AssertJobAds(otherIntegrator, employer2.Id, jobAd);
        }

        [TestMethod]
        public void TestJobG8IntegratorHigherPriority()
        {
            var integratorUser = CreateIntegratorUser(1);
            var employer1 = CreateEmployer(1);

            var jobG8IntegratorUser = _jobG8Query.GetIntegratorUser();
            var employer2 = CreateEmployer(2);

            // Create the job ad for employer 1 who does not belong with JobG8.

            var externalReferenceId = CreateExternalReferenceId(0);
            var title = CreateTitle(0);
            var jobAd = CreateJobAd(0, j => { j.ExternalReferenceId = externalReferenceId; j.Title = title; });
            var jobAdId = CreateJobAd(integratorUser, employer1.Id, jobAd);

            // Not that JobG8 uses this interface but job ad should be created as JobG8 has higher priority.

            PostJobAds(jobG8IntegratorUser, employer2, false, new[] { jobAd }, 1, 0, 0, 0);

            // The other job should have been closed.

            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId).Status);

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, employer1.Id));
            Assert.AreEqual(1, jobAds.Count);
            AssertJobAd(integratorUser, JobAdStatus.Closed, jobAd, jobAds);

            AssertJobAds(jobG8IntegratorUser, employer2.Id, jobAd);
        }

        private void AssertJobAds(IntegratorUser integratorUser, Guid employerId, params JobAdElement[] expectedJobAds)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, employerId));
            Assert.AreEqual(expectedJobAds.Length, jobAds.Count);

            foreach (var expectedJobAd in expectedJobAds)
                AssertJobAd(integratorUser, JobAdStatus.Open, expectedJobAd, jobAds);
        }

        private void AssertJobAd(IntegratorUser integratorUser, JobAdStatus expectedStatus, JobAdElement expectedJobAd, IEnumerable<JobAd> jobAds)
        {
            AssertJobAd(expectedJobAd, AssertJobAd((from j in jobAds where j.Integration.ExternalReferenceId == expectedJobAd.ExternalReferenceId select j).Single(), integratorUser, expectedJobAd.ExternalReferenceId, expectedStatus));
        }

        private static string CreateExternalReferenceId(int index)
        {
            return string.Format(ExternalReferenceIdFormat, index);
        }

        private static string CreateTitle(int index)
        {
            return string.Format(TitleFormat, index);
        }

        private Guid CreateJobAd(IntegratorUser integratorUser, Guid employerId, JobAdElement jobAdElement)
        {
            var jobAd = jobAdElement.Map(_industriesQuery, _locationQuery);
            jobAd.PosterId = employerId;
            jobAd.Integration.IntegratorUserId = integratorUser.Id;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd.Id;
        }
    }
}
