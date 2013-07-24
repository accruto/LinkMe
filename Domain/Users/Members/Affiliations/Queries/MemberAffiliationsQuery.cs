using System;

namespace LinkMe.Domain.Users.Members.Affiliations.Queries
{
    public class MemberAffiliationsQuery
        : IMemberAffiliationsQuery
    {
        private readonly IMembersRepository _repository;

        public MemberAffiliationsQuery(IMembersRepository repository)
        {
            _repository = repository;
        }

        Guid? IMemberAffiliationsQuery.GetAffiliateId(Guid memberId)
        {
            return _repository.GetAffiliateId(memberId);
        }

        AffiliationItems IMemberAffiliationsQuery.GetItems(Guid memberId, Guid affiliateId)
        {
            return _repository.GetAffiliationItems(memberId, affiliateId);
        }
    }
}