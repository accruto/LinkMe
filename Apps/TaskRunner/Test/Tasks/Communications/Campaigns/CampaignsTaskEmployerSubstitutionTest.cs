using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Employers;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskEmployerSubstitutionTest
        : CampaignsTaskTests
    {
        private const string Subject = "An email for <%= To.FirstName %>";
        private const string Body = "<%= To.FullName %>, here is an email for you.";

        [TestMethod]
        public void TestEmployerSubstitutions()
        {
            var index = 0;
            var criteria = new OrganisationEmployerSearchCriteria();

            // Create some employers.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);
            TestSearch(++index, criteria, employers);
        }

        private void TestSearch(int index, Criteria criteria, ICollection<Employer> expectedEmployers)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign, out template);
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);

            template.Subject = Subject;
            template.Body = Body;
            _campaignsCommand.UpdateTemplate(campaign.Id, template);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expectedEmployers.Count);
            for (var employerIndex = 0; employerIndex < emails.Length; ++employerIndex)
            {
                var email = emails[employerIndex];
                var expectedEmployer = (from e in expectedEmployers where e.EmailAddress.Address == email.To[0].Address select e).Single();
                Assert.AreEqual(expectedEmployer.EmailAddress.Address, email.To[0].Address);
                Assert.AreEqual(Subject.Replace("<%= To.FirstName %>", expectedEmployer.FirstName), email.Subject);
                Assert.IsTrue(email.GetHtmlView().Body.Contains(Body.Replace("<%= To.FullName %>", expectedEmployer.FullName)));
            }
        }
    }
}