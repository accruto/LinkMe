using System;

namespace LinkMe.Apps.Agents.Profiles
{
    public interface IProfilesRepository
    {
        void UpdateEmployerProfile(Guid employerId, EmployerProfile state);
        EmployerProfile GetEmployerProfile(Guid employerId);

        void UpdateMemberProfile(Guid memberId, MemberProfile state);
        MemberProfile GetMemberProfile(Guid memberId);
    }
}