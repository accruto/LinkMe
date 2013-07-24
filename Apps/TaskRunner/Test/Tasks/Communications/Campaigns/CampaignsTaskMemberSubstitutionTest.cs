using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskMemberSubstitutionTest
        : CampaignsTaskTests
    {
        private const string Subject = "An email for <%= To.FirstName %>";
        private const string Body = "<%= To.FullName %>, here is an email for you.";

        [TestMethod]
        public void TestMemberSubstitutions()
        {
            var index = 0;
            var criteria = new MemberSearchCriteria();

            // Create some members.

            var members = CreateMembers(0, 6);
            TestSearch(++index, criteria, members);
        }

        private void TestSearch(int index, Criteria criteria, ICollection<Member> expectedMembers)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);

            template.Subject = Subject;
            template.Body = Body;
            _campaignsCommand.UpdateTemplate(campaign.Id, template);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expectedMembers.Count);
            foreach (var email in emails)
            {
                var expectedMember = (from m in expectedMembers where m.GetBestEmailAddress().Address == email.To[0].Address select m).Single();
                Assert.AreEqual(Subject.Replace("<%= To.FirstName %>", expectedMember.FirstName), email.Subject);
                Assert.IsTrue(email.GetHtmlView().Body.Contains(Body.Replace("<%= To.FullName %>", expectedMember.FullName)));
            }
        }
    }
}