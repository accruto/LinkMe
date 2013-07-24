using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Mobile
{
    [TestClass]
    public class CompanyNameTests
        : MobileDisplayTests
    {
        private const string Company = "Acme";

        [TestMethod]
        public void TestCompanyName()
        {
            TestCompanyName(false, Company);
        }

        [TestMethod]
        public void TestHiddenCompanyName()
        {
            TestCompanyName(true, Company);
        }

        private void TestCompanyName(bool hideCompany, string company)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(company));
            var jobAd = PostJobAd(
                employer,
                j =>
                {
                    j.Visibility.HideContactDetails = true;
                    j.Visibility.HideCompany = hideCompany;
                    j.Description.CompanyName = company;
                });

            Get(GetJobUrl(jobAd.Id));

            // Whether hidden or not the company name does not appear in this page.

            AssertPageContains(jobAd.Title);
            AssertPageDoesNotContain(company);
        }
    }
}