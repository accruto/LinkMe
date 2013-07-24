using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns.Reengagement
{
    [TestClass]
    public class ReengagementTests
        : CampaignsTaskTests
    {
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();

        [TestMethod]
        public void TestEmails()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create members.

            var members = CreateMembers(0, 2);

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
        public void TestDeactivated()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create members.

            var member = CreateMembers(0, 1)[0];
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(new[] { member }, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestNoEmployers()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create employers.

            CreateEmployers(0, 2);

            // Execute.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestCommunicationCategoryOff()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create members.

            var members = CreateMembers(0, 2);

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

        private Campaign CreateCampaign()
        {
            var campaign = new Campaign
            {
                Id = new Guid("{B3DAC669-5BEF-4C76-93F5-BD06F6C1AFB2}"),
                Category = CampaignCategory.Member,
                Name = "Member Reengagement",
                Query = "SELECT id FROM dbo.Member",
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("ReengagementEmail").Id,
                CreatedBy = new Guid("2E7D03B6-37E2-4D12-89F3-FFB36B939509"),
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        private static void AssertEmails(ICollection<Member> members, ICollection<MockEmail> emails)
        {
            Assert.AreEqual(members.Count, emails.Count);
            foreach (var member in members)
                Assert.IsTrue((from e in emails where e.To[0].Address == member.EmailAddresses[0].Address select e).Any());
            foreach (var email in emails)
            {
                Assert.IsTrue((from e in members where e.EmailAddresses[0].Address == email.To[0].Address select e).Any());

                // Make sure the HTML email comes last.

                Assert.AreEqual(2, email.AlternateViews.Count);
                Assert.AreEqual(MediaTypeNames.Text.Plain, email.AlternateViews[0].MediaType);
                Assert.AreEqual(MediaTypeNames.Text.Html, email.AlternateViews[1].MediaType);
            }
        }
    }
}