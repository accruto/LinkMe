using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Anonymous.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class AnonymousTests
        : JobG8Tests
    {
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();

        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestMethod]
        public void TestAnonymously()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Try to apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(true));
            var applicationId = AssertApply(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, FirstName, LastName));

            // Assert application saved.

            var contact = _anonymousUsersQuery.GetContact(new AnonymousUser { Id = anonymousId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName });
            AssertView(jobAd.Id, anonymousId);
            AssertApplication(applicationId, jobAd, contact.Id, null);
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions(CoverLetterText);
            AssertAppliedUrl(jobAd, applicationId, EmailAddress);

            AssertView(jobAd.Id, anonymousId);
            var application = AssertApplication(applicationId, jobAd, contact.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(contact, jobAd, application, fileReference);
        }
    }
}