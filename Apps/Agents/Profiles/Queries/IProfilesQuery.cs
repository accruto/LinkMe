using System;

namespace LinkMe.Apps.Agents.Profiles.Queries
{
    public interface IProfilesQuery
    {
        EmployerProfile GetEmployerProfile(Guid employerId);
        MemberProfile GetMemberProfile(Guid memberId);
    }
}