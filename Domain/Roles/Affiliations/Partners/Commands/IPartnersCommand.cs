using System;

namespace LinkMe.Domain.Roles.Affiliations.Partners.Commands
{
    public interface IPartnersCommand
    {
        void CreatePartner(Partner partner);
        void SetPartner(Guid userId, Guid? partnerId);
    }
}
