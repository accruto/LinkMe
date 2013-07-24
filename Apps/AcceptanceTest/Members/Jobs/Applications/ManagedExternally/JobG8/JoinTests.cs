using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class JoinTests
        : JobG8Tests
    {
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string ExternalEmailAddress = "fjhsdfahsdjkf@gmail.com";
        private const string BadEmailAddress = "afsdfsdf@sdfsafdlksdflskdaf.com";
        private const string Password = "password";

        [TestMethod]
        public void TestApply()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(true));
            AssertJsonSuccess(ApiJoin(FirstName, LastName, EmailAddress, Password));
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, null));

            var member = _membersQuery.GetMember(EmailAddress);
            AssertApplication(applicationId, jobAd, member.Id, null, false);
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions(CoverLetterText);
            AssertAppliedUrl(jobAd, applicationId, EmailAddress);

            // The job was viewed anonymously.

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the new member.

            AssertNoApplication(anonymousId, jobAd.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(member, jobAd, application, fileReference);
        }

        [TestMethod]
        public void TestApplyWithExternalEmailAddress()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(true));
            AssertJsonSuccess(ApiJoin(FirstName, LastName, ExternalEmailAddress, Password));
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, null));

            var member = _membersQuery.GetMember(ExternalEmailAddress);
            AssertApplication(applicationId, jobAd, member.Id, null, false);
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions(CoverLetterText);
            AssertAppliedUrl(jobAd, applicationId, ExternalEmailAddress);

            // The job was viewed anonymously.

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the new member.

            AssertNoApplication(anonymousId, jobAd.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(member, jobAd, application, fileReference);
        }

        [TestMethod]
        public void TestApplyWithBadEmailAddress()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(true));
            AssertJsonError(ApiJoin(FirstName, LastName, BadEmailAddress, Password), "EmailAddress", "The email address host name is not recognised.");
            AssertJsonSuccess(ApiJoin(FirstName, LastName, EmailAddress, Password));
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, null));

            var member = _membersQuery.GetMember(EmailAddress);
            AssertApplication(applicationId, jobAd, member.Id, null, false);
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions(CoverLetterText);
            AssertAppliedUrl(jobAd, applicationId, EmailAddress);

            // The job was viewed anonymously.

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the new member.

            AssertNoApplication(anonymousId, jobAd.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(member, jobAd, application, fileReference);
        }
    }
}