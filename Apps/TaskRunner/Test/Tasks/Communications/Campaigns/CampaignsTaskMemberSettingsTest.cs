using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskMemberSettingsTest
        : CampaignsTaskTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();

        [TestMethod]
        public void TestSettingOff()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 2, industry);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            // Turn off campaigns for one of the users.

            var category = _settingsQuery.GetCategory("Campaign");
            _settingsCommand.SetFrequency(members[0].Id, category.Id, Frequency.Never);

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(members.Skip(1).ToList(), emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestNoCommunicationCategory()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 2, industry);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            campaign.CommunicationCategoryId = null;
            _campaignsCommand.UpdateCampaign(campaign);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            // Turn off campaigns for one of the users.

            var category = _settingsQuery.GetCategory("Campaign");
            _settingsCommand.SetFrequency(members[0].Id, category.Id, Frequency.Never);

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(2);
            AssertEmails(members, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestDifferentDefinition()
        {
            // Create members.

            var industry = _industriesQuery.GetIndustries()[0];
            var members = CreateMembers(0, 2, industry);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry.Id } };
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            // Change definition to one the user has already been sent.

            var definition = _settingsQuery.GetDefinition("IosLaunchEmail");
            Assert.IsNotNull(definition);
            campaign.CommunicationDefinitionId = definition.Id;
            _campaignsCommand.UpdateCampaign(campaign);
            _settingsCommand.SetLastSentTime(members[0].Id, definition.Id, DateTime.Now.AddDays(-2));

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(members.Skip(1).ToList(), emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
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
