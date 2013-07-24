using System;

namespace LinkMe.Domain.Users.Members.Affiliations.Commands
{
    public interface IMemberAffiliationsCommand
    {
        void SetAffiliation(Guid memberId, Guid? affiliateId);
        void SetItems(Guid memberId, Guid affiliateId, AffiliationItems items);
    }
}