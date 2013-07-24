using System;

namespace LinkMe.Apps.Agents.Profiles.Commands
{
    public interface IProfilesCommand
    {
        void UpdateEmployerProfile(Guid employerId, EmployerProfile profile);
        void UpdateMemberProfile(Guid memberId, MemberProfile profile);
    }
}