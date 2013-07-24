using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskEmployerQueryTest
        : CampaignsTaskTests
    {
        private const string Query = "SELECT u.id FROM dbo.Employer AS e INNER JOIN dbo.RegisteredUser AS u ON u.id = e.id WHERE u.loginId LIKE '%1%'";

        [TestMethod]
        public void TestQuery()
        {
            var index = 0;

            // No employers.

            TestSearch(++index, Query, new Employer[0]);

            // Create some employers.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);

            // Default criteria should return everyone.

            TestSearch(++index, Query, new[]{employers[1]});
        }

        private void TestSearch(int index, string query, IEnumerable<Employer> expected)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Employer, query, CampaignStatus.Activated, out campaign, out template);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expected.Count());
            for (var employerIndex = 0; employerIndex < emails.Length; ++employerIndex)
                Assert.AreEqual(expected.ElementAt(employerIndex).EmailAddress.Address, emails[employerIndex].To[0].Address);
        }
    }
}