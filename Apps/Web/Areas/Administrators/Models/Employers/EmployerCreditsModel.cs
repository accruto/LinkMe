using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Models.Credits;

namespace LinkMe.Web.Areas.Administrators.Models.Employers
{
    public class EmployerCreditsModel
        : CreditsModel
    {
        public IEmployer Employer { get; set; }
        public Guid CreditId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}