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
    public class CampaignsTaskMemberStatusTest
        : CampaignsTaskTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestNoCampaigns()
        {
            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotActivatedCampaign()
        {
            // Create members.

            CreateMembers(0, 1);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Draft, out campaign, out template);
            CreateCampaign(2, CampaignCategory.Member, null, CampaignStatus.Deleted, out campaign, out template);
            CreateCampaign(3, CampaignCategory.Member, null, CampaignStatus.Running, out campaign, out template);
            CreateCampaign(4, CampaignCategory.Member, null, CampaignStatus.Stopped, out campaign, out template);

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestActivatedCampaign()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 1, industry);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(members, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestReactivatedCampaign()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 1, industry);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            // Email should be sent to first member.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(members, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Member should not get it again.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate and add new member.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            members = CreateMembers(1, 1, industry);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(members, emails);
        }

        [TestMethod]
        public void TestMultipleCampaigns()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members1 = CreateMembers(0, 1, industry);

            // Create campaigns.

            Campaign campaign1;
            Template template1;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign1, out template1);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign1.Id, criteria);

            Campaign campaign2;
            Template template2;
            CreateCampaign(2, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign2, out template2);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign2.Id, criteria);

            Campaign campaign3;
            Template template3;
            CreateCampaign(3, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign3, out template3);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign3.Id, criteria);

            Campaign campaign4;
            Template template4;
            CreateCampaign(4, CampaignCategory.Member, null, CampaignStatus.Draft, out campaign4, out template4);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign4.Id, criteria);

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(3);

            AssertEmails(members1, emails, new[] { campaign1, campaign2, campaign3 }, new []{template1, template2, template3});

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Add some more members.

            var members2 = CreateMembers(1, 2, industry);
            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate.

            _campaignsCommand.UpdateStatus(campaign1, CampaignStatus.Activated);
            _campaignsCommand.UpdateStatus(campaign2, CampaignStatus.Activated);
            _campaignsCommand.UpdateStatus(campaign3, CampaignStatus.Activated);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(6);
            AssertEmails(members2, emails, new[] { campaign1, campaign2, campaign3 }, new []{template1, template2, template3});

            // Add some more campaigns.

            Campaign campaign5;
            Template template5;
            CreateCampaign(5, CampaignCategory.Member, null, CampaignStatus.Draft, out campaign5, out template5);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign5.Id, criteria);

            Campaign campaign6;
            Template template6;
            CreateCampaign(6, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign6, out template6);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign6.Id, criteria);

            Campaign campaign7;
            Template template7;
            CreateCampaign(7, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign7, out template7);
            criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign7.Id, criteria);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(6);
            AssertEmails(members1.Concat(members2).ToList(), emails, new[] { campaign6, campaign7 }, new[] { template6, template7});

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private static void AssertEmails(ICollection<Member> members, ICollection<MockEmail> emails)
        {
            Assert.AreEqual(members.Count, emails.Count);
            foreach (var member in members)
                Assert.IsTrue((from e in emails where e.To[0].Address == member.GetBestEmailAddress().Address select e).Any());
            foreach (var email in emails)
                Assert.IsTrue((from e in members where e.GetBestEmailAddress().Address == email.To[0].Address select e).Any());
        }

        private static void AssertEmails(ICollection<Member> members, ICollection<MockEmail> emails, ICollection<Campaign> campaigns, IEnumerable<Template> templates)
        {
            // Each member should have received an email for each campaign.

            Assert.AreEqual(members.Count * campaigns.Count, emails.Count);
            foreach (var member in members)
            {
                foreach (var template in templates)
                    Assert.IsTrue((from e in emails where e.To[0].Address == member.GetBestEmailAddress().Address && e.GetHtmlView().Body.Contains(template.Body) select e).Any());
            }
            foreach (var email in emails)
            {
                Assert.IsTrue((from e in members where e.GetBestEmailAddress().Address == email.To[0].Address select e).Any()
                    && ((from t in templates where email.GetHtmlView().Body.Contains(t.Body) select t).Any()));
            }
        }
    }
}