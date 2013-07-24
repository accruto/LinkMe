using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns.IosLaunch
{
    [TestClass]
    public class IosLaunchTests
        : CampaignsTaskTests
    {
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();

        [TestMethod]
        public void TestEmails()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create employers.

            var employers = CreateEmployers(0, 2);

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(2);
            AssertEmails(employers, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestNoMembers()
        {
            var campaign = CreateCampaign();
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Create members.

            CreateMembers(0, 2);

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

            // Create employers.

            var employers = CreateEmployers(0, 2);

            // Turn off campaigns for one of the users.

            var category = _settingsQuery.GetCategory("Campaign");
            _settingsCommand.SetFrequency(employers[0].Id, category.Id, Frequency.Never);

            // Execute.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(employers.Skip(1).ToList(), emails);

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
                Id = new Guid("{83B9911D-0AE8-4550-B64A-F0D2A97B2380}"),
                Category = CampaignCategory.Employer,
                Name = "iOS Launch",
                Query = null,
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("IosLaunchEmail").Id,
                CreatedBy = new Guid("2E7D03B6-37E2-4D12-89F3-FFB36B939509"),
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        private static void AssertEmails(ICollection<Employer> employers, ICollection<MockEmail> emails)
        {
            Assert.AreEqual(employers.Count, emails.Count);
            foreach (var employer in employers)
                Assert.IsTrue((from e in emails where e.To[0].Address == employer.EmailAddress.Address select e).Any());
            foreach (var email in emails)
                Assert.IsTrue((from e in employers where e.EmailAddress.Address == email.To[0].Address select e).Any());
        }
    }
}