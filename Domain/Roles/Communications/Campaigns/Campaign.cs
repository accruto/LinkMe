using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Campaigns
{
    public enum CampaignStatus
    {
        Draft,
        Deleted,
        Activated,
        Running,
        Stopped
    }

    public enum CampaignCategory
    {
        Member,
        Employer,
    }

    public class Campaign
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required, StringLength(100), RemoveHtml]
        public string Name { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }

        [IsSet]
        public Guid CreatedBy { get; set; }

        public CampaignStatus Status { get; set; }
        public CampaignCategory Category { get; set; }
        public Guid? CommunicationDefinitionId { get; set; }
        public Guid? CommunicationCategoryId { get; set; }

        public string Query { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
