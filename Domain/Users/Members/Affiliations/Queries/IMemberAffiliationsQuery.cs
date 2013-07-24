using System;

namespace LinkMe.Domain.Users.Members.Affiliations.Queries
{
    public interface IMemberAffiliationsQuery
    {
        Guid? GetAffiliateId(Guid memberId);
        AffiliationItems GetItems(Guid memberId, Guid affiliateId);
    }
}