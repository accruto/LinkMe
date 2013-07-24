using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns
{
    [TestClass]
    public class CreateCampaignTest
        : CampaignsCommandTests
    {
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        [TestMethod]
        public void CreateTest()
        {
            // Create it.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);

            // Get it.

            AssertCampaign(campaign, template);
        }

        [TestMethod]
        public void CreateMultipleTest()
        {
            // Create them.

            const int count = 6;
            var campaigns = new Campaign[count];
            var templates = new Template[count];
            for (var index = 0; index < count; ++index)
            {
                Campaign campaign;
                Template template;
                CreateTestCampaign(index, out campaign, out template);
                campaigns[count - index - 1] = campaign;
                templates[count - index - 1] = template;
            }

            // Get them.

            AssertCampaigns(campaigns, templates);
        }

        [TestMethod]
        public void NoCreatedByTest()
        {
            try
            {
                const string name = "My campaign";
                var campaign = new Campaign { Name = name };
                _campaignsCommand.CreateCampaign(campaign);
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationErrorsException ex)
            {
                AssertErrors(ex, new IsSetValidationError("CreatedBy"));
            }
        }

        [TestMethod]
        public void NoNameTest()
        {
            NameTest(null, new RequiredValidationError("Name"));
        }

        [TestMethod]
        public void LongNameTest()
        {
            NameTest(new string('a', 200), new MaximumLengthValidationError("Name", 100));
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            const string name = "My campaign";

            // Create first campaign.

            var campaign = new Campaign { Name = name, CreatedBy = Guid.NewGuid() };
            _campaignsCommand.CreateCampaign(campaign);

            // Try to create duplicate.

            NameTest(name, new DuplicateValidationError("Name"));
        }

        [TestMethod]
        public void NameHtmlTest()
        {
            // Put some html in the name which should be stripped out.

            NameHtmlTest("<div>Some html1</div>", "Some html1");
            NameHtmlTest("<div>Some html2", "Some html2");
            NameHtmlTest("<script>Some html3</script>", "Some html3");
            NameHtmlTest("<script>Some </script>html4", "Some html4");
            NameHtmlTest("<script>Some</script>html5", "Somehtml5");
        }

        [TestMethod]
        public void CommunicationCategoryTest()
        {
            // Create campaign.

            var campaign = new Campaign
            {
                Name = string.Format(CampaignNameFormat, 0),
                CreatedBy = Guid.NewGuid(),
                CommunicationCategoryId = _settingsQuery.GetCategories(UserType.Member)[1].Id
            };
            _campaignsCommand.CreateCampaign(campaign);

            var template = new Template
            {
                Subject = string.Format(TemplateSubjectFormat, 0),
                Body = string.Format(TemplateBodyFormat, 0)
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);

            AssertCampaign(campaign, template);
        }

        [TestMethod]
        public void CommunicationDefinitionTest()
        {
            // Create campaign.

            var campaign = new Campaign
            {
                Name = string.Format(CampaignNameFormat, 0),
                CreatedBy = Guid.NewGuid(),
                CommunicationDefinitionId = _settingsQuery.GetDefinitions(UserType.Member)[1].Id
            };
            _campaignsCommand.CreateCampaign(campaign);

            var template = new Template
            {
                Subject = string.Format(TemplateSubjectFormat, 0),
                Body = string.Format(TemplateBodyFormat, 0)
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);

            AssertCampaign(campaign, template);
        }

        private void NameTest(string name, ValidationError expectedError)
        {
            try
            {
                var campaign = new Campaign { Name = name, CreatedBy = Guid.NewGuid() };
                _campaignsCommand.CreateCampaign(campaign);
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationErrorsException ex)
            {
                AssertErrors(ex, expectedError);
            }
        }

        private void NameHtmlTest(string withHtml, string withoutHtml)
        {
            var campaign = new Campaign { Name = withHtml, CreatedBy = Guid.NewGuid() };
            _campaignsCommand.CreateCampaign(campaign);

            // It should have been stripped out.

            campaign.Name = withoutHtml;
            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }
    }
}