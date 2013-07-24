using System;

namespace LinkMe.Query.Search.Members.Commands
{
    public interface IUpdateMemberSearchCommand
    {
        void ClearAll();
        void AddMember(Guid memberId);
        void RemoveMember(Guid memberId);
    }
}