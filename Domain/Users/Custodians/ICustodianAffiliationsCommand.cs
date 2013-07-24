using System;

namespace LinkMe.Domain.Users.Custodians
{
    public interface ICustodianAffiliationsCommand
    {
        void SetAffiliation(Guid custodianId, Guid? affiliateId);
        Guid? GetAffiliationId(Guid custodianId);
    }
}