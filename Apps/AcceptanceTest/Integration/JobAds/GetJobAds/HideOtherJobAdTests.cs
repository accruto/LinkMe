using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds
{
    [TestClass]
    public class HideOtherJobAdTests
        : HideJobAdTests
    {
        [TestMethod]
        public void TestNoHide()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAd(employer, integratorUser, CompanyName, ContactCompanyName, false, false);

            // Get.

            var response = JobAds(integratorUser, null, null);

            // Assert.

            AssertHide(null, new ContactDetails { FirstName = FirstName, LastName = LastName, CompanyName = employer.Organisation.Name, EmailAddress = EmailAddress, FaxNumber = FaxNumber, PhoneNumber = PhoneNumber, SecondaryEmailAddresses = SecondaryEmailAddresses }, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestNoHideNoContactCompanyName()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAd(employer, integratorUser, CompanyName, null, false, false);

            // Get.

            var response = JobAds(integratorUser, null, null);

            // Assert.

            AssertHide(null, new ContactDetails { FirstName = FirstName, LastName = LastName, CompanyName = employer.Organisation.Name, EmailAddress = EmailAddress, FaxNumber = FaxNumber, PhoneNumber = PhoneNumber, SecondaryEmailAddresses = SecondaryEmailAddresses }, GetJobAdFeed(response));
        }
    }
}
