using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class DeleteCampaignTests
        : CampaignsTests
    {
        private HtmlButtonTester _deleteButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _deleteButton = new HtmlButtonTester(Browser, "yes");
        }

        [TestMethod]
        public void DeleteTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign1 = CreateCampaign(1, administrator);
            var campaign2 = CreateCampaign(2, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to index page.

            Get(GetCampaignsUrl());
            AssertPageContains(campaign1.Name);
            AssertPageContains(campaign2.Name);

            // Go to the delete page.

            Get(GetDeleteCampaignUrl(campaign1.Id));
            AssertPageContains("Are you sure you want to delete the '" + campaign1.Name + "' campaign?");
            _deleteButton.Click();

            AssertUrl(GetCampaignsUrl());
            AssertPageDoesNotContain(campaign1.Name);
            AssertPageContains(campaign2.Name);
        }

        [TestMethod]
        public void DeleteFromPageTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            IList<Campaign> campaigns;
            IList<Template> templates;
            CreateCampaigns(0, MaxCampaignsPerPage + 2, CampaignCategory.Employer, out campaigns, out templates);

            // Login as administrator.

            LogIn(administrator);

            // Delete from the second page.

            Get(GetCampaignsUrl(2));
            AssertPageContains(campaigns.Last().Name);
            AssertPageContains(campaigns.Take(campaigns.Count - 1).Last().Name);

            // Delete the last campaign.

            var campaign = campaigns.Last();
            Get(GetDeleteCampaignUrl(campaign.Id, 2));
            AssertPageContains("Are you sure you want to delete the '" + campaign.Name + "' campaign?");
            _deleteButton.Click();

            AssertUrl(GetCampaignsUrl(2));
            AssertPageDoesNotContain(campaigns.Last().Name);
            AssertPageContains(campaigns.Take(campaigns.Count - 1).Last().Name);
        }

        [TestMethod]
        public void DeleteLastFromPageTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            IList<Campaign> campaigns;
            IList<Template> templates;
            CreateCampaigns(0, MaxCampaignsPerPage + 1, CampaignCategory.Employer, out campaigns, out templates);

            // Login as administrator.

            LogIn(administrator);

            // Delete from the second page.

            Get(GetCampaignsUrl(2));
            AssertPageContains(campaigns.Last().Name);
            AssertPageDoesNotContain(campaigns.Take(campaigns.Count - 1).Last().Name);

            // Delete the last campaign.

            var campaign = campaigns.Last();
            Get(GetDeleteCampaignUrl(campaign.Id, 2));
            AssertPageContains("Are you sure you want to delete the '" + campaign.Name + "' campaign?");
            _deleteButton.Click();

            AssertUrl(GetCampaignsUrl(1));
            AssertPageDoesNotContain(campaigns.Last().Name);
            AssertPageContains(campaigns.Take(campaigns.Count - 1).Last().Name);
        }
    }
}
