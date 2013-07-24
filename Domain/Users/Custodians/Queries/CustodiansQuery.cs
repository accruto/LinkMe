using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Custodians.Queries
{
    public class CustodiansQuery
        : ICustodiansQuery
    {
        private readonly ICustodiansRepository _repository;

        public CustodiansQuery(ICustodiansRepository repository)
        {
            _repository = repository;
        }

        Custodian ICustodiansQuery.GetCustodian(Guid id)
        {
            return _repository.GetCustodian(id);
        }

        IList<Custodian> ICustodiansQuery.GetCustodians(IEnumerable<Guid> ids)
        {
            return _repository.GetCustodians(ids);
        }

        IList<Custodian> ICustodiansQuery.GetAffiliationCustodians(Guid affiliateId)
        {
            return (from c in _repository.GetAffiliationCustodians(affiliateId)
                    orderby c.FullName
                    select c).ToList();
        }
    }
}