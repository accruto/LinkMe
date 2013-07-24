using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignSummaryModel
    {
        public Campaign Campaign { get; set; }
        public Template Template { get; set; }
        public IRegisteredUser CreatedBy { get; set; }
        public bool IsReadOnly { get; set; }
        public IList<Definition> CommunicationDefinitions { get; set; }
        public IList<Category> CommunicationCategories { get; set; }
    }
}