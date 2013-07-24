using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Campaigns.Emails
{
    public abstract class CampaignEmail
        : TemplateEmail
    {
        private readonly string _definition;
        private readonly string _category;

        protected CampaignEmail(Campaign campaign, string definition, string category, ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
            _definition = definition ?? "Campaign:" + campaign.Id;
            _category = category;
        }

        public override string Definition
        {
            get { return _definition; }
        }

        public override string Category
        {
            get { return _category; }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("IncludeUnsubscribe", true);
        }
    }
}