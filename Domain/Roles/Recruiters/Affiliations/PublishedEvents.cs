using System;

namespace LinkMe.Domain.Roles.Recruiters.Affiliations
{
    public static class PublishedEvents
    {
        public const string EnquiryCreated = "LinkMe.Domain.Roles.Recruiters.Affiliations.EnquiryCreated";
    }

    public class EnquiryEventArgs
        : EventArgs
    {
        public readonly Guid AffiliateId;
        public readonly AffiliationEnquiry Enquiry;

        public EnquiryEventArgs(Guid affiliateId, AffiliationEnquiry enquiry)
        {
            AffiliateId = affiliateId;
            Enquiry = enquiry;
        }
    }
}