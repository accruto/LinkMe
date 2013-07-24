using System;

namespace LinkMe.Apps.Agents.Profiles.Queries
{
    public class ProfilesQuery
        : IProfilesQuery
    {
        private readonly IProfilesRepository _repository;

        public ProfilesQuery(IProfilesRepository repository)
        {
            _repository = repository;
        }

        EmployerProfile IProfilesQuery.GetEmployerProfile(Guid employerId)
        {
            return _repository.GetEmployerProfile(employerId);
        }

        MemberProfile IProfilesQuery.GetMemberProfile(Guid memberId)
        {
            return _repository.GetMemberProfile(memberId);
        }
    }
}