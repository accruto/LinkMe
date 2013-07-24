using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Anonymous.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class AnonymousTests
        : ManagedExternallyTests
    {
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();

        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestMethod]
        public void TestAnonymously()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Try to apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(false));
            var applicationId = AssertApply(ApplyAnonymously(employer, jobAd, fileReference.Id, EmailAddress, FirstName, LastName));

            // Assert application saved.

            var contact = _anonymousUsersQuery.GetContact(new AnonymousUser { Id = anonymousId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName });
            AssertView(jobAd.Id, anonymousId);
            AssertApplication(applicationId, jobAd, contact.Id, null);
            _emailServer.AssertNoEmailSent();
            AssertRedirectToExternal(false, applicationId, jobAd);
        }

        [TestMethod]
        public void TestWithProfile()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Try to apply.

            View(jobAd.Id, () => AssertNotLoggedInView(false));
            AssertJsonError(ApiApplyWithProfile(jobAd.Id, null), null, "100", "The user is not logged in.");

            // Assert no application saved.

            AssertView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);
            _emailServer.AssertNoEmailSent();
        }
    }
}