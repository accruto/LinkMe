using System;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Commands
{
    public interface ILinkedInCommand
    {
        void UpdateProfile(LinkedInProfile profile);
        void DeleteProfile(Guid userId);
    }
}
