using System.Collections.Specialized;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Web.Areas.Administrators.Controllers.Campaigns;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Administrators.Communications.Campaigns
{
    [TestClass]
    public class CreateCampaignTest
        : CampaignsControllerTests
    {
        private const string CampaignName = "My new campaign";

        [TestMethod]
        public void GetTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var controller = CreateController(administrator);

            // GET the page.

            var result = controller.New();

            // Assert.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel { Campaign = new Campaign(), CreatedBy = administrator, IsReadOnly = false }, (CampaignSummaryModel)((ViewResult)result).ViewData.Model);
            AssertNoErrors(result);
        }

        [TestMethod]
        public void CreateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var controller = CreateController(administrator);

            // POST a new campaign.

            var campaign = new Campaign { Name = CampaignName };
            var result = controller.New(campaign);

            // Should be redirected to edit the criteria.

            AssertRedirectToRoute(typeof(CampaignsController).FullName + ".EditCriteria", new { id = campaign.Id }, result);

            // Campaign should be saved.

            campaign.CreatedBy = administrator.Id;
            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }

        [TestMethod]
        public void NullNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            NameTest(administrator, null, "The name is required.");
        }

        [TestMethod]
        public void EmptyNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            NameTest(administrator, string.Empty, "The name is required.");
        }

        [TestMethod]
        public void LongNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            NameTest(administrator, new string('a', 200), "The name must be no more than 100 characters in length.");
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var controller = CreateController(administrator);

            // POST a new campaign.

            var campaign = new Campaign { Name = CampaignName };
            controller.New(campaign);

            // Create another.

            NameTest(administrator, CampaignName, "The name is already being used.");
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

        private void NameTest(IRegisteredUser administrator, string name, string expectedMessage)
        {
            var controller = CreateController(administrator);

            // POST a new campaign with no name.

            var campaign = new Campaign { Name = name, CreatedBy = administrator.Id };
            var result = controller.New(campaign);

            // Should get a view back with errors.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel { Campaign = campaign, CreatedBy = administrator, IsReadOnly = false }, (CampaignSummaryModel)((ViewResult)result).ViewData.Model);
            AssertErrors(new NameValueCollection { { "Name", expectedMessage } }, result);
        }

        private void NameHtmlTest(IRegisteredUser administrator, string withHtml, string withoutHtml)
        {
            var controller = CreateController(administrator);

            // POST a new campaign.

            var campaign = new Campaign { Name = withHtml };
            var result = controller.New(campaign);

            // Should be redirected to edit the criteria.

            AssertRedirectToRoute(typeof(CampaignsController).FullName + ".EditCriteria", new { id = campaign.Id }, result);

            // Campaign should be saved.

            campaign.Name = withoutHtml;
            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }
    }
}
