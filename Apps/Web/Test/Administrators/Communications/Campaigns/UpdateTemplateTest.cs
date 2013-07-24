using System.Web.Mvc;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Web.Areas.Administrators.Controllers.Campaigns;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateTemplateTest
        : CampaignsControllerTests
    {
        private const string UpdatedSubject = "My updated subject";
        private const string UpdatedBody = "My updated body";

        [TestMethod]
        public void GetTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaign = CreateCampaign(1, administrator);
            var controller = CreateController(administrator);

            // GET the page.

            var result = controller.EditTemplate(campaign.Id);

            // Assert.

            AssertView(string.Empty, result);
            AssertSummary(new CampaignSummaryModel {Campaign = campaign, Template = new Template(), CreatedBy = administrator, IsReadOnly = false}, (CampaignSummaryModel) ((ViewResult) result).ViewData.Model);
            AssertNoErrors(result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaign = CreateCampaign(1, administrator);
            var controller = CreateController(administrator);

            // POST an updated template.

            var template = new Template { Subject = UpdatedSubject, Body = UpdatedBody };
            var result = controller.PostEditTemplate(campaign.Id, template.Subject, template.Body);

            // Should be redirected back to the original page.

            AssertRedirectToRoute(typeof(CampaignsController).FullName + ".EditTemplate", new { id = campaign.Id }, result);

            // Campaign should be saved.

            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
            AssertTemplate(template, _campaignsQuery.GetTemplate(campaign.Id));
        }
    }
}
