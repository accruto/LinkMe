using System;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    public static class OrganisationsTestExtensions
    {
        private const string NameFormat = "Organisation{0}";

        public static Organisation CreateTestOrganisation(this IOrganisationsCommand organisationsCommand, int index)
        {
            return organisationsCommand.CreateTestOrganisation(string.Format(NameFormat, index));
        }

        public static Organisation CreateTestOrganisation(this IOrganisationsCommand organisationsCommand, string name)
        {
            return organisationsCommand.CreateTestOrganisation(name, null);
        }

        public static Organisation CreateTestOrganisation(this IOrganisationsCommand organisationsCommand, string name, LocationReference location)
        {
            var organisation = new Organisation
            {
                Name = name,
                Address = location == null ? null : new Address {Location = location},
            };

            organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, int index)
        {
            return organisationsCommand.CreateTestVerifiedOrganisation(index, null, Guid.NewGuid());
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, int index, Organisation parentOrganisation, Guid administratorId)
        {
            return organisationsCommand.CreateTestVerifiedOrganisation(index, parentOrganisation, administratorId, administratorId);
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, int index, Organisation parentOrganisation, Guid administratorId, Guid verifiedById)
        {
            return organisationsCommand.CreateTestVerifiedOrganisation(string.Format(NameFormat, index), parentOrganisation, administratorId, verifiedById);
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, string name)
        {
            return organisationsCommand.CreateTestVerifiedOrganisation(name, null, Guid.NewGuid());
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, string name, Organisation parentOrganisation, Guid accountManagerId)
        {
            return organisationsCommand.CreateTestVerifiedOrganisation(name, parentOrganisation, accountManagerId, accountManagerId);
        }

        public static VerifiedOrganisation CreateTestVerifiedOrganisation(this IOrganisationsCommand organisationsCommand, string name, Organisation parentOrganisation, Guid accountManagerId, Guid verifiedById)
        {
            var organisation = new VerifiedOrganisation
            {
                Name = name,
                VerifiedById = verifiedById,
                AccountManagerId = accountManagerId,
            };

            organisation.SetParent(parentOrganisation);
            organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }
    }
}