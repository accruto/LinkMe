using System;

namespace LinkMe.Domain.Roles.Affiliations.Partners.Queries
{
    public interface IPartnersQuery
    {
        Partner GetPartner(Guid userId);
    }
}
