using System;

namespace LinkMe.Domain.Roles.Recruiters.Commands
{
    public interface IOrganisationsCommand
    {
        void CreateOrganisation(Organisation organisation);
        void UpdateOrganisation(Organisation organisation);
        VerifiedOrganisation VerifyOrganisation(Organisation organisation, Guid accountManagerId, Guid verifiedById);

        void SplitFullName(string fullName, out string parentFullName, out string name);
    }
}