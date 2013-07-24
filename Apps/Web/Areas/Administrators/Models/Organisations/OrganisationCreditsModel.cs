using System;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Web.Models.Credits;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class OrganisationCreditsModel
        : CreditsModel
    {
        public Organisation Organisation { get; set; }
        public Guid CreditId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}