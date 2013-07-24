using System;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class CareerOneApplyTests
        : JobsTests
    {
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();
        private readonly IJobAdReportsQuery _jobAdReportsQuery = Resolve<IJobAdReportsQuery>();
        private readonly IMemberApplicationsQuery _memberApplicationsQuery = Resolve<IMemberApplicationsQuery>();

        [TestMethod]
        public void TestAnonymousApply()
        {
            var integratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, integratorUserId);

            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            // This simulates applying for the job ad.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            // Another anonymous application.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));
        }

        [TestMethod]
        public void TestMemberApply()
        {
            var integratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            var employer = CreateEmployer();
            var jobAd0 = CreateJobAd(employer, integratorUserId);
            var jobAd1 = CreateJobAd(employer, integratorUserId);

            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            AssertApplications(member0.Id);
            AssertApplications(member1.Id);

            LogIn(member0);

            // This simulates applying for the job ad.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd0.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member0.Id, jobAd0.Id);
            AssertApplications(member1.Id);

            // Another application by same member, not counted twice.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd0.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member0.Id, jobAd0.Id);
            AssertApplications(member1.Id);

            // Another job ad.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd1.Id))));
            Assert.AreEqual(2, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(2, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member0.Id, jobAd0.Id, jobAd1.Id);
            AssertApplications(member1.Id);
            
            // Another member.

            LogIn(member1);

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd1.Id))));
            Assert.AreEqual(3, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(3, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member0.Id, jobAd0.Id, jobAd1.Id);
            AssertApplications(member1.Id, jobAd1.Id);
        }

        [TestMethod]
        public void TestAnonymousAndMemberApply()
        {
            var integratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            var employer = CreateEmployer();
            var jobAd0 = CreateJobAd(employer, integratorUserId);
            var jobAd1 = CreateJobAd(employer, integratorUserId);

            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(0, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            var member1 = CreateMember(1);
            AssertApplications(member1.Id);

            // This simulates applying for the job ad.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd0.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member1.Id);

            // Another application by anonymous.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd0.Id))));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(1, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member1.Id);

            // Another job ad.

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd1.Id))));
            Assert.AreEqual(2, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(2, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member1.Id);

            // Member.

            LogIn(member1);

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetJobAdAppliedUrl(jobAd1.Id))));
            Assert.AreEqual(3, _jobAdReportsQuery.GetExternalApplications(new DayRange(DateTime.Now.Date)));
            Assert.AreEqual(3, _jobAdReportsQuery.GetExternalApplications(integratorUserId, new DayRange(DateTime.Now.Date)));

            AssertApplications(member1.Id, jobAd1.Id);
        }

        private void AssertApplications(Guid memberId, params Guid[] jobAdIds)
        {
            var applications = _memberApplicationsQuery.GetApplications(memberId);
            Assert.AreEqual(jobAdIds.Length, applications.Count);
            Assert.IsTrue(jobAdIds.All(j => (from a in applications where a.PositionId == j && a.ApplicantId == memberId select a).Any()));
        }

        private static ReadOnlyUrl GetJobAdAppliedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/api/" + jobAdId + "/externallyapplied");
        }

        private JobAd CreateJobAd(IEmployer employer, Guid integratorUserId)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.Integration.IntegratorUserId = integratorUserId;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }
    }
}
