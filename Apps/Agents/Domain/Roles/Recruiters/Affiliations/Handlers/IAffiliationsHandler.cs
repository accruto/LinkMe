using System;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Apps.Agents.Domain.Roles.Recruiters.Affiliations.Handlers
{
    public interface IAffiliationsHandler
    {
        void OnEnquiryCreated(Guid affiliateId, AffiliationEnquiry enquiry);
    }
}