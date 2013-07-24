using System;

namespace LinkMe.Domain.Users.Custodians
{
    public class CustodianAffiliationsCommand
        : ICustodianAffiliationsCommand
    {
        private readonly ICustodiansRepository _repository;

        public CustodianAffiliationsCommand(ICustodiansRepository repository)
        {
            _repository = repository;
        }

        void ICustodianAffiliationsCommand.SetAffiliation(Guid custodianId, Guid? affiliateId)
        {
            _repository.SetAffiliation(custodianId, affiliateId);
        }

        Guid? ICustodianAffiliationsCommand.GetAffiliationId(Guid custodianId)
        {
            return _repository.GetAffiliationId(custodianId);
        }
    }
}