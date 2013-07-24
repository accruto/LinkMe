using System;
using LinkMe.AcceptanceTest.Emails;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Reengagement
{
    [TestClass]
    public class EmailLinksTests
        : EmailsTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ICampaignEmailsCommand _campaignEmailsCommand = Resolve<ICampaignEmailsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestEmailLinks()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);
            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Send.

            var campaignEmail = _campaignEmailsCommand.CreateEmail(campaign, member);
            _campaignEmailsCommand.Send(new[] { campaignEmail }, campaign.Id, false);

            var email = _emailServer.AssertEmailSent();

            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(12, links.Count);

            const string definition = "ReengagementEmail";

            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/"), links[0]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/search/jobs"), links[1]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl(true, "~/login"), links[2]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[3]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[4]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/"), links[5]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/members/resources"), links[6]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[7]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[8]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/"), links[9]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/settings"), links[10]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl(true, "~/accounts/settings/unsubscribe", new ReadOnlyQueryString("userId", member.Id.ToString(), "category", "Campaign")), links[11]);

            links = email.GetPlainTextView().GetPlainTextLinks();
            Assert.AreEqual(9, links.Count);

            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/search/jobs"), links[0]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl(true, "~/login"), links[1]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[2]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[3]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl("~/members/resources"), links[4]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[5]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/profile"), links[6]);
            AssertActivationLink(member, true, definition, new ReadOnlyApplicationUrl(true, "~/members/settings"), links[7]);
            AssertActivationLink(member, false, definition, new ReadOnlyApplicationUrl(true, "~/accounts/settings/unsubscribe", new ReadOnlyQueryString("userId", member.Id.ToString(), "category", "Campaign")), links[8]);
        }

        [TestMethod]
        public void TestUnsubscribeEmailLink()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Send.

            var campaignEmail = _campaignEmailsCommand.CreateEmail(campaign, member);
            _campaignEmailsCommand.Send(new[] { campaignEmail }, campaign.Id, false);
            var email = _emailServer.AssertEmailSent();

            var category = _settingsQuery.GetCategory("Campaign");
            AssertUnsubscribeLink(member, category, email.GetHtmlView().GetLinks()[11]);
        }

        [TestMethod]
        public void TestUnsubscribePlainTextEmailLink()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Send.

            var campaignEmail = _campaignEmailsCommand.CreateEmail(campaign, member);
            _campaignEmailsCommand.Send(new[] { campaignEmail }, campaign.Id, false);
            var email = _emailServer.AssertEmailSent();

            var category = _settingsQuery.GetCategory("Campaign");
            AssertUnsubscribeLink(member, category, email.GetPlainTextView().GetPlainTextLinks()[8]);
        }

        protected Campaign CreateCampaign(Administrator administrator)
        {
            var campaign = new Campaign
            {
                Id = new Guid("{B3DAC669-5BEF-4C76-93F5-BD06F6C1AFB2}"),
                Category = CampaignCategory.Member,
                Name = "Member Reengagement",
                Query = null,
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("ReengagementEmail").Id,
                CreatedBy = administrator.Id,
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        private void AssertActivationLink(IRegisteredUser user, bool expectLogin, string definition, ReadOnlyUrl expectedUrl, ReadOnlyUrl link)
        {
            _userAccountsCommand.DeactivateUserAccount(user, Guid.NewGuid());
            Assert.IsFalse(_membersQuery.GetMember(user.Id).IsActivated);
            if (expectLogin)
                AssertLink(definition, user, expectedUrl, link);
            else
                AssertLink(definition, expectedUrl, link);
            Assert.IsTrue(_membersQuery.GetMember(user.Id).IsActivated);
        }
    }
}
