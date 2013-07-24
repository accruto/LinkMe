using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Profiles.Commands
{
    public class ProfilesCommand
        : IProfilesCommand
    {
        private readonly IProfilesRepository _repository;

        public ProfilesCommand(IProfilesRepository repository)
        {
            _repository = repository;
        }

        void IProfilesCommand.UpdateEmployerProfile(Guid employerId, EmployerProfile profile)
        {
            profile.Prepare();
            profile.Validate();
            _repository.UpdateEmployerProfile(employerId, profile);
        }

        void IProfilesCommand.UpdateMemberProfile(Guid memberId, MemberProfile profile)
        {
            profile.Prepare();
            profile.Validate();
            _repository.UpdateMemberProfile(memberId, profile);
        }
    }
}
