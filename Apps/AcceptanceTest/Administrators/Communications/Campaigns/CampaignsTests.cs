using System;
using System.Collections.Generic;
using System.Globalization;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public abstract class CampaignsTests
        : WebTestClass
    {
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICampaignsRepository _campaignsRepository = Resolve<ICampaignsRepository>();
        protected readonly ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected readonly ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();
        protected readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        private const string CampaignNameFormat = "My new campaign {0}";
        private const string TemplateSubjectFormat = "My new campaign {0} subject";
        private const string TemplateBodyFormat = "My new campaign {0} body";
        protected const int MaxCampaignsPerPage = 20;

        private ReadOnlyUrl _campaignsUrl;
        protected ReadOnlyUrl _newCampaignUrl;
        private ReadOnlyUrl _campaignUrlBase;

        protected HtmlTextBoxTester _nameTextBox;
        protected HtmlDropDownListTester _categoryDropDownList;
        protected HtmlDropDownListTester _communicationDefinitionDropDownList;
        protected HtmlDropDownListTester _communicationCategoryDropDownList;
        protected HtmlTextAreaTester _queryTextArea;
        protected HtmlButtonTester _saveButton;

        [TestInitialize]
        public void CampaignsTestsInitialize()
        {
            _campaignsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/communications/campaigns");
            _newCampaignUrl = new ReadOnlyApplicationUrl(true, "~/administrators/communications/campaigns/campaign/new");
            _campaignUrlBase = new ReadOnlyApplicationUrl(true, "~/administrators/communications/campaigns/campaign/");

            _nameTextBox = new HtmlTextBoxTester(Browser, "Name");
            _categoryDropDownList = new HtmlDropDownListTester(Browser, "Category");
            _communicationDefinitionDropDownList = new HtmlDropDownListTester(Browser, "CommunicationDefinitionId");
            _communicationCategoryDropDownList = new HtmlDropDownListTester(Browser, "CommunicationCategoryId");
            _queryTextArea = new HtmlTextAreaTester(Browser, "Query");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }

        protected Campaign CreateCampaign(int index, Administrator administrator)
        {
            var campaign = new Campaign { Name = string.Format(CampaignNameFormat, index), CreatedBy = administrator.Id };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        protected Campaign CreateCampaign(int index, CampaignCategory category, Administrator administrator)
        {
            var campaign = new Campaign { Name = string.Format(CampaignNameFormat, index), CreatedBy = administrator.Id, Category = category };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        protected void CreateCampaigns(int start, int count, CampaignCategory category, out IList<Campaign> campaigns, out IList<Template> templates)
        {
            campaigns = new List<Campaign>();
            templates = new List<Template>();
            var createdStart = DateTime.Now.AddDays(-2);

            for (var index = start; index < start + count; ++index)
            {
                var name = string.Format(CampaignNameFormat, index < 10 ? "00" + index : (index < 100 ? "0" + index : index.ToString(CultureInfo.InvariantCulture)));
                var campaign = new Campaign
                {
                    Id = Guid.NewGuid(),
                    CreatedTime = createdStart.AddSeconds(index),
                    Name = name,
                    Category = category,
                };

                var template = new Template
                {
                    Subject = string.Format(TemplateSubjectFormat, index < 10 ? "00" + index : (index < 100 ? "0" + index : index.ToString(CultureInfo.InvariantCulture))),
                    Body = string.Format(TemplateBodyFormat, index < 10 ? "00" + index : (index < 100 ? "0" + index : index.ToString(CultureInfo.InvariantCulture)))
                };

                _campaignsRepository.CreateCampaign(campaign);
                _campaignsRepository.CreateTemplate(campaign.Id, template);

                // Newest campaigns at start.

                campaigns.Insert(0, campaign);
                templates.Insert(0, template);
            }
        }

        protected ReadOnlyUrl GetCampaignsUrl()
        {
            return _campaignsUrl;
        }

        protected Guid GetCurrentCampaignId()
        {
            var path = Browser.CurrentUrl.AbsolutePath;
            var pos = path.IndexOf("/campaign/");
            if (pos == -1)
                throw new ApplicationException("Cannot find the current campaign id in the url '" + Browser.CurrentUrl.AbsoluteUri + "'.");

            var start = pos + "/campaign/".Length;
            var end = path.IndexOf("/", start, StringComparison.Ordinal);

            var id = end == -1 ? path.Substring(start) : path.Substring(start, end - start);
            return new Guid(id);
        }

        protected ReadOnlyUrl GetCampaignsUrl(CampaignCategory? category)
        {
            return category == null ? GetCampaignsUrl() : new ReadOnlyApplicationUrl((_campaignsUrl.AbsoluteUri + "/").AddUrlSegments(category.Value.ToString().ToLower()));
        }

        protected ReadOnlyUrl GetCampaignsUrl(int page)
        {
            return new ReadOnlyApplicationUrl((_campaignsUrl.AbsoluteUri + "/").AddUrlSegments(page.ToString(CultureInfo.InvariantCulture)));
        }

        protected ReadOnlyUrl GetCampaignsUrl(CampaignCategory? category, int page)
        {
            return category == null ? GetCampaignsUrl(page) : new ReadOnlyApplicationUrl((_campaignsUrl.AbsoluteUri + "/").AddUrlSegments(category.Value.ToString().ToLower().AddUrlSegments(page.ToString(CultureInfo.InvariantCulture))));
        }

        protected ReadOnlyUrl GetEditCampaignUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n"));
        }

        protected ReadOnlyUrl GetEditTemplateUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("template"));
        }

        protected ReadOnlyUrl GetEditCriteriaUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("criteria"));
        }

        protected ReadOnlyUrl GetPreviewUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("preview"));
        }

        protected ReadOnlyUrl GetPreviewUrl(Guid id, Guid userId)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("preview"), new ReadOnlyQueryString("userId", userId.ToString()));
        }

        protected ReadOnlyUrl GetDeleteCampaignUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("delete"));
        }

        protected ReadOnlyUrl GetDeleteCampaignUrl(Guid id, int page)
        {
            return new ReadOnlyApplicationUrl(_campaignUrlBase, id.ToString("n").AddUrlSegments("delete"), new QueryString("page", page.ToString(CultureInfo.InvariantCulture)));
        }

        protected static int GetTotalPages(int totalItems, int maxPerPage)
        {
            if ((totalItems % maxPerPage) == 0)
                return (totalItems / maxPerPage);
            return (totalItems / maxPerPage) + 1;
        }
    }
}
