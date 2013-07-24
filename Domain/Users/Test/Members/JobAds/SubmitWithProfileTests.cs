using System;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Members.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds
{
    [TestClass]
    public class SubmitWithProfileTests
        : SubmitTests
    {
        [TestMethod]
        public void TestSubmit()
        {
            var member = CreateMember();
            var resume = AddResume(member.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var applicationId = _internalApplicationsCommand.SubmitApplication(member, jobAd, null);

            // Check the application.

            var applications = _memberApplicationsQuery.GetApplications(member.Id);
            Assert.AreEqual(1, applications.Count);
            Assert.AreEqual(applicationId, applications[0].Id);
            AssertApplication(member.Id, jobAd.Id, GetResumeId(member.Id), applications[0]);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);
        }

        [TestMethod, ExpectedException(typeof(AlreadyAppliedException))]
        public void TestAlreadyApplied()
        {
            var member = CreateMember();
            AddResume(member.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplication(member, jobAd, null);

            // Submit again.

            _internalApplicationsCommand.SubmitApplication(member, jobAd, null);
        }

        [TestMethod, ExpectedException(typeof(NoResumeException))]
        public void TestSubmitWithNoResume()
        {
            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplication(member, jobAd, null);
        }

        private Guid GetResumeId(Guid memberId)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            return candidate.ResumeId.Value;
        }

        private void AssertApplication(Guid applicantId, Guid jobAdId, Guid resumeId, Application application)
        {
            Assert.IsInstanceOfType(application, typeof(InternalApplication));
            AssertApplication(applicantId, jobAdId, application);
            Assert.AreEqual(resumeId, ((InternalApplication)application).ResumeId);
            Assert.IsNull(((InternalApplication)application).ResumeFileId);
        }
    }
}
