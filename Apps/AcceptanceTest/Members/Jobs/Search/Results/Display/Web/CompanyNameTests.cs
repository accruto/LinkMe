using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class CompanyNameTests
        : DisplayTests
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
            var jobAd = CreateJobAd(hideCompany, company);
            Search(Keywords);
            var node = GetResult(jobAd.Id);
            Assert.IsNotNull(node);

            // Whether hidden or not the company name does not appear in this page.

            AssertPageDoesNotContain(company);
        }

        private JobAd CreateJobAd(bool hideCompany, string company)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(company));
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Visibility.HideContactDetails = true;
            jobAd.Visibility.HideCompany = hideCompany;
            jobAd.Description.CompanyName = company;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
