using System;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Test.Mvc;
using LinkMe.Apps.Web.Test.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Web.Areas.Administrators.Controllers.Campaigns;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using LinkMe.Web.Areas.Administrators.Routes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Administrators.Communications.Campaigns
{
    [TestClass]
    public abstract class CampaignsControllerTests
        : ControllerTest
    {
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICommunitiesQuery _communitiesQuery = Resolve<ICommunitiesQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected readonly ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();

        protected const string CampaignNameFormat = "My new campaign{0}";

        [TestInitialize]
        public void CampaignsControllerTestsInitialize()
        {
            ApplicationSetUp.SetCurrentApplication(WebSite.LinkMe);
            CampaignsRoutes.RegisterRoutes(new AreaRegistrationContext("Campaigns", new RouteCollection()));

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Campaign CreateCampaign(int index, Administrator administrator)
        {
            var campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = string.Format(CampaignNameFormat, index),
                CreatedTime = DateTime.Now,
                CreatedBy = administrator.Id,
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        protected CampaignsController CreateController(IRegisteredUser administrator)
        {
            var controller = new CampaignsController(
                Resolve<ICampaignsCommand>(),
                Resolve<ICampaignsQuery>(),
                null,
                null,
                Resolve<ISettingsQuery>(),
                _industriesQuery,
                _communitiesQuery,
                Resolve <IAdministratorsQuery>(),
                Resolve<ILoginCredentialsQuery>(),
                Resolve<IEmployersQuery>(),
                Resolve<IMembersQuery>(),
                _locationQuery);

            controller.MockContext(administrator);
            controller.ViewData["CurrentRegisteredUser"] = administrator;
            return controller;
        }

        protected void AssertSummary(CampaignSummaryModel expectedModel, CampaignSummaryModel model)
        {
            Assert.AreEqual(expectedModel.CreatedBy.Id, model.CreatedBy.Id);
            Assert.AreEqual(expectedModel.IsReadOnly, model.IsReadOnly);
            AssertCampaign(expectedModel.Campaign, model.Campaign);
            if (expectedModel.Template == null)
                Assert.IsNull(model.Template);
            else
                AssertTemplate(expectedModel.Template, model.Template);
        }

        protected void AssertCampaign(Campaign expectedCampaign, Campaign campaign)
        {
            Assert.AreEqual(expectedCampaign.Id, campaign.Id);
            Assert.AreEqual(expectedCampaign.Name, campaign.Name);
            Assert.AreEqual(expectedCampaign.Status, campaign.Status);
            Assert.AreEqual(expectedCampaign.Category, campaign.Category);
            Assert.AreEqual(expectedCampaign.CreatedBy, campaign.CreatedBy);
        }

        protected static void AssertTemplate(Template expectedTemplate, Template template)
        {
            Assert.AreEqual(expectedTemplate.Body, template.Body);
            Assert.AreEqual(expectedTemplate.Subject, template.Subject);
        }
    }
}
