using System;
using System.Collections.Generic;
using System.Net.Mime;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;
using Template=LinkMe.Domain.Roles.Communications.Campaigns.Template;

namespace LinkMe.Apps.Agents.Communications.Campaigns.Emails
{
    public class CampaignEmailsCommand
        : ICampaignEmailsCommand
    {
        private readonly ICommunicationUser _from;

        private readonly ICampaignsQuery _campaignsQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly IEmailsCommand _emailsCommand;

        private const string DefaultCommunicationCategory = "Campaign";

        public CampaignEmailsCommand(ICampaignsQuery campaignsQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand, IEmailsCommand emailsCommand, string returnAddress, string returnDisplayName)
        {
            _campaignsQuery = campaignsQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;
            _emailsCommand = emailsCommand;
            _from = new EmailUser(returnAddress, returnDisplayName, null);
        }

        CampaignEmail ICampaignEmailsCommand.CreateEmail(Campaign campaign, ICommunicationUser to)
        {
            if (campaign.Category == CampaignCategory.Employer)
                return new EmployerCampaignEmail(campaign, GetCommunicationDefinition(campaign.CommunicationDefinitionId), GetCommunicationCategory(campaign.CommunicationCategoryId, UserType.Employer), to, _from);
            return new MemberCampaignEmail(campaign, GetCommunicationDefinition(campaign.CommunicationDefinitionId), GetCommunicationCategory(campaign.CommunicationCategoryId, UserType.Member), to, _from);
        }

        Communication ICampaignEmailsCommand.GeneratePreview(CampaignEmail email, Guid campaignId)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(campaignId);
            if (campaign == null)
                throw new ApplicationException("Cannot find the campaign with id '" + campaignId + "'.");

            email.Properties.Add("Category", campaign.CommunicationCategoryId == null ? "Campaign" : _settingsQuery.GetCategory(campaign.CommunicationCategoryId.Value).Name);
            email.Properties.Add("IncludeUnsubscribe", true);

            var template = _campaignsQuery.GetTemplate(campaign.Id);
            if (template != null)
            {
                // Create an item template based on it.

                var templateContentItem = CreateTemplateContentItem(campaign, template);
                return _emailsCommand.GeneratePreview(email, templateContentItem);
            }
            
            return _emailsCommand.GeneratePreview(email);
        }

        int ICampaignEmailsCommand.Send(IEnumerable<CampaignEmail> emails, Guid campaignId, bool isTest)
        {
            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(campaignId);
            if (campaign == null)
                throw new ApplicationException("Cannot find the campaign with id '" + campaignId + "'.");
            var template = _campaignsQuery.GetTemplate(campaign.Id) ?? new Template();

            // Make sure a definition exists.

            CreateDefinition(campaignId);

            // Create an item template based on it.

            var templateContentItem = CreateTemplateContentItem(campaign, template);

            // If it is a test then ignore all checks.
            // Otherwise, don't send it twice to the same person.
            
            return isTest
                ? _emailsCommand.TrySend(emails, templateContentItem, true)
                : _emailsCommand.TrySend(emails, templateContentItem, DateTime.MinValue);
        }

        private void CreateDefinition(Guid campaignId)
        {
            // check whether it exists.

            var name = "Campaign:" + campaignId;
            var definition = _settingsQuery.GetDefinition(name);
            if (definition != null)
                return;

            var category = _settingsQuery.GetCategory("Campaign");
            _settingsCommand.CreateDefinition(new Definition { Name = name, CategoryId = category.Id });
        }

        private string GetCommunicationDefinition(Guid? communicationDefinitionId)
        {
            // An empty string means do not use any definition.

            if (communicationDefinitionId == null)
                return null;
            var definition = _settingsQuery.GetDefinition(communicationDefinitionId.Value);
            return definition == null ? null : definition.Name;
        }

        private string GetCommunicationCategory(Guid? communicationCategoryId, UserType userType)
        {
            // An empty string means do not use any category.

            if (communicationCategoryId == null)
                return string.Empty;

            var category = _settingsQuery.GetCategory(communicationCategoryId.Value);
            if (category == null)
                return DefaultCommunicationCategory;

            return !category.UserTypes.IsFlagSet(userType)
                ? DefaultCommunicationCategory
                : category.Name;
        }

        private static TemplateContentItem CreateTemplateContentItem(Campaign campaign, Template template)
        {
            // If the campaign has a definition then it will be generated independently.

            if (campaign.CommunicationDefinitionId != null)
                return null;

            // Choose the template corresponding to the category.

            return new TemplateContentItem
            {
                Master = campaign.Category + "CampaignEmail",
                Subject = template.Subject,
                Views = new List<ViewContentItem>
                {
                    new ViewContentItem
                    {
                        MimeType = MediaTypeNames.Text.Html,
                        Parts = new List<ViewPartContentItem>
                        {
                            new ViewPartContentItem
                            {
                                Name = "Body",
                                Text = template.Body
                            }
                        },
                    }
                }
            };
        }
    }
}