using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Queries
{
    public interface IMembersQuery
    {
        Member GetMember(Guid id);
        Member GetMember(string emailAddress);
        IList<Member> GetMembers(IEnumerable<Guid> ids);
        IList<Member> GetMembers(IEnumerable<Guid> ids, Range range);
        IList<Member> GetMembers(string fullName);
        IList<Member> GetMembers(IEnumerable<string> emailAddresses);
        IList<Guid> GetActiveMemberIds();
        IList<string> GetFullNames(IEnumerable<Guid> ids);
    }
}