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
    public class CampaignsTaskMemberQueryTest
        : CampaignsTaskTests
    {
        private const string Query = "SELECT u.id FROM dbo.Member AS m INNER JOIN dbo.RegisteredUser AS u ON u.id = m.id WHERE u.loginId LIKE '%1%'";

        [TestMethod]
        public void TestQuery()
        {
            var index = 0;

            // No members.

            TestSearch(++index, Query, new Member[0]);

            // Create some members.

            var members = CreateMembers(0, 6);

            // Default criteria should return everyone.

            TestSearch(++index, Query, new[] { members[1] });
        }

        private void TestSearch(int index, string query, IEnumerable<Member> expected)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Member, query, CampaignStatus.Activated, out campaign, out template);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expected.Count()).OrderBy(e => e.To[0].Address).ToArray();
            for (var memberIndex = 0; memberIndex < emails.Length; ++memberIndex)
                Assert.AreEqual(expected.ElementAt(memberIndex).GetBestEmailAddress().Address, emails[memberIndex].To[0].Address);
        }
    }
}