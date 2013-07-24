using System;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public interface IMembersHandler
    {
        void OnMemberCreated(Guid memberId);
        void OnMemberDisabled(Guid memberId);
        void OnMemberActivated(Guid memberId);
        void OnMemberDeactivated(Guid memberId);
    }
}
