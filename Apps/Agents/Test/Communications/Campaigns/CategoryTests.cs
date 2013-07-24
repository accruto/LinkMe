using System;
using System.Net.Mime;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns
{
    [TestClass]
    public abstract class CategoryTests
        : TestClass
    {
        protected const string ReturnDisplayName = "LinkMe";
        protected const string ReturnEmailAddress = "do_not_reply@test.linkme.net.au";

        private const string CampaignName = "My campaign";
        protected IMockEmailServer _emailServer;
        protected ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();

        [TestInitialize]
        public void CategoryTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer = EmailHost.Start();
        }

        protected Campaign CreateTestCampaign()
        {
            var campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = CampaignName,
                CreatedBy = Guid.NewGuid(),
                Category = GetCampaignCategory(),
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        protected ICampaignEmailsCommand CreateCampaignEmailsCommand()
        {
            return new CampaignEmailsCommand(
                _campaignsQuery,
                Resolve<ISettingsQuery>(),
                Resolve<ISettingsCommand>(),
                Resolve<IEmailsCommand>(),
                ReturnEmailAddress, ReturnDisplayName);
        }

        protected void AssertCommunication(CampaignEmail email, string subject, string body, Communication communication)
        {
            // The text of the item should be the subject.

            Assert.AreEqual(subject, communication.Subject);

            // There should be one view item containing the body wrapped in the master.

            Assert.AreEqual(1, communication.Views.Count);
            Assert.AreEqual(MediaTypeNames.Text.Html, communication.Views[0].MimeType);
            Assert.AreEqual(GetBody(email, body), communication.Views[0].Body);
        }

        protected abstract string GetBody(CampaignEmail email, string content);
        protected abstract CampaignCategory GetCampaignCategory();
        protected abstract CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to);
    }
}