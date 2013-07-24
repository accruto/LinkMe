using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskErrorsTest
        : CampaignsTaskTests
    {
        private const string BadEmailAddress = "bad@test.linkme.net.au";
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestBadEmailAddress()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 10, industry);

            // Create a member with a bad email address.

            CreateMember(BadEmailAddress, industry);
            _emailServer.RegisterBadEmailAddress(BadEmailAddress);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            // Run.

            new CampaignsTask().ExecuteTask();

            // Even with a bad email address all other emails should be sent.

            var emails = _emailServer.AssertEmailsSent(10);
            AssertEmails(members, emails);
        }

        private static void AssertEmails(ICollection<Member> members, ICollection<MockEmail> emails)
        {
            Assert.AreEqual(members.Count, emails.Count);
            foreach (var member in members)
                Assert.IsTrue((from e in emails where e.To[0].Address == member.GetBestEmailAddress().Address select e).Any());
            foreach (var email in emails)
                Assert.IsTrue((from e in members where e.GetBestEmailAddress().Address == email.To[0].Address select e).Any());
        }
    }
}