using System;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class LoggedInProfileTests
        : ManagedExternallyTests
    {
        [TestMethod]
        public void TestApply()
        {
            var member = CreateMember();
            var resume = AddResume(member.Id);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            View(jobAd.Id, () => AssertLoggedInView(true, false, false));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, null));
            GetAppliedUrl(employer, jobAd);
            AssertStatus(jobAd, member.Id, true, true, null);

            // Check the application.

            AssertResumeApplication(applicationId, jobAd, member.Id, null, GetResumeId(member.Id));
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);
            _emailServer.AssertNoEmailSent();
            AssertRedirectToExternal(true, applicationId, jobAd);
        }

        [TestMethod]
        public void TestAlreadyApplied()
        {
            var member = CreateMember();
            AddResume(member.Id);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            AssertJsonSuccess(ApiApplyWithProfile(jobAd.Id, null));
            GetAppliedUrl(employer, jobAd);

            // Apply again.

            AssertStatus(jobAd, member.Id, true, true, null);
            AssertJsonError(ApiApplyWithProfile(jobAd.Id, null), null, "300", "This job has already been applied for.");
            GetAppliedUrl(employer, jobAd);
            AssertStatus(jobAd, member.Id, true, true, null);
        }

        [TestMethod]
        public void TestSubmitWithNoResume()
        {
            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, false, null);
            AssertJsonError(ApiApplyWithProfile(jobAd.Id, null), null, "300", "The resume cannot be found.");
            GetAppliedUrl(employer, jobAd);
            AssertStatus(jobAd, member.Id, false, false, null);
        }

        private Guid GetResumeId(Guid memberId)
        {
            return _candidatesQuery.GetCandidate(memberId).ResumeId.Value;
        }
    }
}