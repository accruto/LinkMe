using System;

namespace LinkMe.Domain.Roles.Affiliations.Partners.Queries
{
    public class PartnersQuery
        : IPartnersQuery
    {
        private readonly IPartnersRepository _repository;

        public PartnersQuery(IPartnersRepository repository)
        {
            _repository = repository;
        }

        Partner IPartnersQuery.GetPartner(Guid userId)
        {
            return _repository.GetPartner(userId);
        }
    }
}
