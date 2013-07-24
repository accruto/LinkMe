using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Mobile
{
    [TestClass]
    public class LoggedInProfileTests
        : ManagedInternallyTests
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

            View(jobAd.Id, () => AssertView(jobAd.Id));
            var applicationId = AssertApply(Apply(employer, jobAd, true, null));

            AssertStatus(jobAd, member.Id, true, true, null);

            // Check the application.

            AssertApplication(applicationId, jobAd, member.Id, null, GetResumeId(member.Id));
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);
            AssertEmail(member, employer, null, GetProfileResumeFileName(member));
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

            View(jobAd.Id, () => AssertView(jobAd.Id));
            AssertApply(Apply(employer, jobAd, true, CoverLetterText));

            // Apply again.

            AssertStatus(jobAd, member.Id, true, true, null);

            View(jobAd.Id, () => AssertView(jobAd.Id));
            AssertJsonError(Apply(employer, jobAd, true, CoverLetterText), null, "300", "This job has already been applied for.");

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

            View(jobAd.Id, () => AssertView(jobAd.Id));

            Get(GetApplyUrl(jobAd.Id));
            var div = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='button apply']");
            Assert.IsNull(div);
            AssertPageContains("You currently do not have any resume");

            AssertJsonError(ApiApplyWithProfile(jobAd.Id, null), null, "300", "The resume cannot be found.");
            AssertStatus(jobAd, member.Id, false, false, null);
        }

        private Guid GetResumeId(Guid memberId)
        {
            return _candidatesQuery.GetCandidate(memberId).ResumeId.Value;
        }

        private void AssertApplication(Guid applicationId, JobAdEntry jobAd, Guid applicantId, string coverLetterText, Guid resumeId)
        {
            var application = AssertApplication(applicationId, jobAd, applicantId, coverLetterText);
            Assert.AreEqual(resumeId, application.ResumeId);
            Assert.IsNull(application.ResumeFileId);
        }
    }
}