using System;

namespace LinkMe.Domain.Roles.Integration.LinkedIn
{
    public interface ILinkedInRepository
    {
        void UpdateProfile(LinkedInProfile profile);
        void DeleteProfile(Guid userId);
        LinkedInProfile GetProfile(string linkedInId);
        LinkedInProfile GetProfile(Guid userId);
    }
}
