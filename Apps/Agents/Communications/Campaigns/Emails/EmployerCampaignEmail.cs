using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Apps.Agents.Communications.Campaigns.Emails
{
    public class EmployerCampaignEmail
        : CampaignEmail
    {
        public EmployerCampaignEmail(Campaign campaign, string definition, string category, ICommunicationUser to, ICommunicationUser from)
            : base(campaign, definition, category, to, from)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}