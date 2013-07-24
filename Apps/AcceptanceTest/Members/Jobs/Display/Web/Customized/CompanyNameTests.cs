using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public class CompanyNameTests
        : CustomizedDisplayTests
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
            var organisation = new VerifiedOrganisation
            {
                Id = OrganisationId,
                Name = company,
            };
            _organisationsCommand.CreateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

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