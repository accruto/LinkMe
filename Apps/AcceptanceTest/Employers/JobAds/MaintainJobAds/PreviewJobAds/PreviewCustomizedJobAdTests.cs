using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public class PreviewCustomizedJobAdTests
        : PreviewJobAdTests
    {
        private static readonly Guid OrganisationId = new Guid("703317d4-da51-49e8-a553-b9c94af70156");
        private const string OrganisationName = "Database Consultants Australia (VIC)";

        [TestMethod]
        public void TestCustomizedJobAd()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, JobAdStatus.Open, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            // Specific to CustomizedJobAd.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='JobAdHeading']");
            Assert.IsNotNull(node);

            // Style sheet should be included in the page.

            var stylesheetUrl = new ReadOnlyApplicationUrl("~/Content/Organisations/Css/JobAds/Database Consultants Australia (VIC) - 703317d4-da51-49e8-a553-b9c94af70156.css");
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//link");
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Any(n => string.Equals(n.Attributes["href"].Value, stylesheetUrl.Path, StringComparison.InvariantCultureIgnoreCase)));
        }

        protected Employer CreateEmployer()
        {
            var organisation = new VerifiedOrganisation
            {
                Id = OrganisationId,
                Name = OrganisationName,
            };
            _organisationsCommand.CreateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            return employer;
        }
    }
}
