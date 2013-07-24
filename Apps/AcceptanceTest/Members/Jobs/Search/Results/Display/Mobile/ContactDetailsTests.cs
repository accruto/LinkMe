using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
{
    [TestClass]
    public class ContactDetailsTests
        : DisplayTests
    {
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string Company = "Acme";
        private const string PhoneNumber = "99999999";

        [TestMethod]
        public void TestContactDetails()
        {
            TestContactDetails(false, false, FirstName, LastName, Company, PhoneNumber, FirstName + " " + LastName + " at " + Company + ", " + PhoneNumber);
        }

        [TestMethod]
        public void TestSomeContactDetails()
        {
            TestContactDetails(false, false, FirstName, LastName, Company, null, FirstName + " " + LastName + " at " + Company);
        }

        [TestMethod]
        public void TestHiddenContactDetails()
        {
            TestContactDetails(true, false, FirstName, LastName, Company, PhoneNumber, "");
        }

        [TestMethod]
        public void TestHiddenCompanyDetails()
        {
            TestContactDetails(false, true, FirstName, LastName, Company, PhoneNumber, FirstName + " " + LastName + ", " + PhoneNumber);
        }

        private void TestContactDetails(bool hideContactDetails, bool hideCompany, string firstName, string lastName, string company, string phoneNumber, string expectedContactDetails)
        {
            var jobAd = CreateJobAd(hideContactDetails, hideCompany, firstName, lastName, company, phoneNumber);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='company']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedContactDetails, node.InnerText);

            // Make sure the details don't appear anywhere if hidden.

            if (hideContactDetails)
            {
                AssertPageDoesNotContain(firstName);
                AssertPageDoesNotContain(lastName);
                AssertPageDoesNotContain(company);
                AssertPageDoesNotContain(phoneNumber);
            }

            if (hideCompany)
            {
                AssertPageDoesNotContain(company);
            }
        }

        private JobAd CreateJobAd(bool hideContactDetails, bool hideCompany, string firstName, string lastName, string company, string phoneNumber)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(company));
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Visibility.HideContactDetails = hideContactDetails;
            jobAd.Visibility.HideCompany = hideCompany;
            jobAd.ContactDetails = new ContactDetails { FirstName = firstName, LastName = lastName, CompanyName = company, PhoneNumber = phoneNumber };
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
