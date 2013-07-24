using System;

namespace LinkMe.Domain.Roles.Affiliations.Partners
{
    public interface IPartnersRepository
    {
        void CreatePartner(Partner partner);
        void SetPartner(Guid userId, Guid? partnerId);
        Partner GetPartner(Guid userId);
    }
}
