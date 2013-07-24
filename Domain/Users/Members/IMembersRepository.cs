using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members
{
    public interface IMembersRepository
    {
        void CreateMember(Member member);
        void UpdateMember(Member member);

        Member GetMember(Guid id);
        Member GetMember(string emailAddress);
        IList<Member> GetMembers(IEnumerable<Guid> ids);
        IList<Member> GetMembers(string fullName);
        IList<Member> GetMembers(IEnumerable<Guid> ids, Range range);
        IList<Member> GetMembers(IEnumerable<string> emailAddresses);
        IList<Guid> GetActiveMemberIds();
        IList<string> GetFullNames(IEnumerable<Guid> ids);

        void SetAffiliation(Guid memberId, Guid? affiliateId);
        Guid? GetAffiliateId(Guid memberId);

        void SetAffiliationItems(Guid memberId, Guid affiliateId, AffiliationItems items);
        AffiliationItems GetAffiliationItems(Guid memberId, Guid affiliateId);
    }
}
