using System;

namespace LinkMe.Domain.Roles.Recruiters.Affiliations.Commands
{
    public interface IOrganisationAffiliationsCommand
    {
        void CreateEnquiry(Guid affiliateId, AffiliationEnquiry enquiry);
    }
}