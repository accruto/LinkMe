using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Queries
{
    public class MembersQuery
        : IMembersQuery
    {
        private readonly IMembersRepository _repository;

        public MembersQuery(IMembersRepository repository)
        {
            _repository = repository;
        }

        Member IMembersQuery.GetMember(Guid id)
        {
            return _repository.GetMember(id);
        }

        Member IMembersQuery.GetMember(string emailAddress)
        {
            return _repository.GetMember(emailAddress);
        }

        IList<Member> IMembersQuery.GetMembers(IEnumerable<Guid> ids)
        {
            return _repository.GetMembers(ids);
        }

        IList<Member> IMembersQuery.GetMembers(IEnumerable<Guid> ids, Range range)
        {
            return _repository.GetMembers(ids, range);
        }

        IList<Member> IMembersQuery.GetMembers(string fullName)
        {
            return _repository.GetMembers(fullName);
        }

        IList<Member> IMembersQuery.GetMembers(IEnumerable<string> emailAddresses)
        {
            return _repository.GetMembers(emailAddresses);
        }

        IList<Guid> IMembersQuery.GetActiveMemberIds()
        {
            return _repository.GetActiveMemberIds();
        }

        IList<string> IMembersQuery.GetFullNames(IEnumerable<Guid> ids)
        {
            return _repository.GetFullNames(ids);
        }
    }
}