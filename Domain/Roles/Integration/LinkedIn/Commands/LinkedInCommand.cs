using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Commands
{
    public class LinkedInCommand
        : ILinkedInCommand
    {
        private readonly ILinkedInRepository _repository;

        public LinkedInCommand(ILinkedInRepository repository)
        {
            _repository = repository;
        }

        void ILinkedInCommand.UpdateProfile(LinkedInProfile profile)
        {
            Prepare(profile);
            profile.Validate();
            _repository.UpdateProfile(profile);
        }

        void ILinkedInCommand.DeleteProfile(Guid userId)
        {
            _repository.DeleteProfile(userId);
        }

        private static void Prepare(LinkedInProfile profile)
        {
            profile.Prepare();
            profile.LastUpdatedTime = DateTime.Now;
            if (profile.Location != null)
                profile.Location.Prepare();
        }
    }
}
