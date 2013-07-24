using System;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public CampaignStatus Status { get; set; }
        public int Page { get; set; }
    }
}