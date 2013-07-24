using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Search.Members.Commands
{
    public interface IMemberSearchesCommand
    {
        void CreateMemberSearch(IUser owner, MemberSearch search);
        void UpdateMemberSearch(IUser owner, MemberSearch search);
        void DeleteMemberSearch(IUser owner, Guid id);

        void CreateMemberSearchExecution(MemberSearchExecution execution);
    }
}