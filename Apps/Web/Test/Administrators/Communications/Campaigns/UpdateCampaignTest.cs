using System.Collections.Specialized;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Areas.Administrators.Controllers.Campaigns;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateCampaignTest
        : CampaignsControllerTests
    {
        private const string UpdatedCampaignName = "My updated campaign";

        [TestMethod]
        public void GetTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);

            // Create the controller and command.

            var controller = CreateController(administrator);

            var campaign = CreateCampaign(1, administrator);

            // GET the page.

            var result = controller.Edit(campaign.Id);

            // Assert.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel { Campaign = campaign, CreatedBy = administrator, IsReadOnly = false }, (CampaignSummaryModel)((ViewResult)result).ViewData.Model);
            AssertNoErrors(result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaign = CreateCampaign(1, administrator);
            var controller = CreateController(administrator);

            // POST an updated campaign.

            campaign.Name = UpdatedCampaignName;
//            controller.ValueProvider = new FormCollection { { "Name", campaign.Name } }.ToValueProvider();
            var result = controller.PostEdit(campaign.Id, campaign.Name, campaign.Category, campaign.CommunicationDefinitionId, campaign.CommunicationCategoryId, campaign.Query);

            // Should be redirected back to the original page.

            AssertRedirectToRoute(typeof(CampaignsController).FullName + ".Edit", new { id = campaign.Id }, result);

            // Campaign should be saved.

            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }

        [TestMethod]
        public void NullNameTest()
        {
            NameTest(null, "The name is required.");
        }

        [TestMethod]
        public void EmptyNameTest()
        {
            NameTest(string.Empty, "The name is required.");
        }

        [TestMethod]
        public void LongNameTest()
        {
            NameTest(new string('a', 200), "The name must be no more than 100 characters in length.");
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaign1 = CreateCampaign(1, administrator);
            var campaign2 = CreateCampaign(2, administrator);

            // POST an updated campaign with no name.

            campaign1.Name = campaign2.Name;
            var controller = CreateController(administrator);
//            controller.ValueProvider = new FormCollection { { "Name", campaign1.Name } }.ToValueProvider();
            var result = controller.PostEdit(campaign1.Id, campaign1.Name, campaign1.Category, campaign1.CommunicationDefinitionId, campaign1.CommunicationCategoryId, campaign1.Query);

            // Should get a view back with errors.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel { Campaign = campaign1, CreatedBy = administrator, IsReadOnly = false }, (CampaignSummaryModel)((ViewResult)result).ViewData.Model);
            AssertErrors(new NameValueCollection { { "Name", "The name is already being used." } }, result);
        }

        [TestMethod]
        public void NameHtmlTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);

            // Put some html in the name which should be stripped out.

            NameHtmlTest(administrator, "<div>Some html1</div>", "Some html1");
            NameHtmlTest(administrator, "<div>Some html2", "Some html2");
            NameHtmlTest(administrator, "<script>Some html3</script>", "Some html3");
            NameHtmlTest(administrator, "<script>Some </script>html4", "Some html4");
            NameHtmlTest(administrator, "<script>Some</script>html5", "Somehtml5");
        }

        private void NameTest(string name, string expectedMessage)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaign = CreateCampaign(1, administrator);

            // POST an updated campaign with no name.

            campaign.Name = name;
            var controller = CreateController(administrator);
            var result = controller.PostEdit(campaign.Id, campaign.Name, campaign.Category, campaign.CommunicationDefinitionId, campaign.CommunicationCategoryId, campaign.Query);

            // Should get a view back with errors.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel { Campaign = campaign, CreatedBy = administrator, IsReadOnly = false }, (CampaignSummaryModel)((ViewResult)result).ViewData.Model);
            AssertErrors(new NameValueCollection { { "Name", expectedMessage } }, result);
        }

        private void NameHtmlTest(Administrator administrator,  string withHtml, string withoutHtml)
        {
            var campaign = CreateCampaign(1, administrator);

            // POST an updated campaign.

            campaign.Name = withHtml;
            var controller = CreateController(administrator);
            var result = controller.PostEdit(campaign.Id, campaign.Name, campaign.Category, campaign.CommunicationDefinitionId, campaign.CommunicationCategoryId, campaign.Query);

            // Should be redirected back to the original page.

            AssertRedirectToRoute(typeof(CampaignsController).FullName + ".Edit", new { id = campaign.Id }, result);

            // Campaign should be saved.

            campaign.Name = withoutHtml;
            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }
    }
}
