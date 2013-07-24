using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters.Commands
{
    public class OrganisationsCommand
        : IOrganisationsCommand
    {
        private readonly IRecruitersRepository _repository;

        public OrganisationsCommand(IRecruitersRepository repository)
        {
            _repository = repository;
        }

        void IOrganisationsCommand.CreateOrganisation(Organisation organisation)
        {
            organisation.Prepare();
            Validate(organisation);
            _repository.CreateOrganisation(organisation);
        }

        void IOrganisationsCommand.UpdateOrganisation(Organisation organisation)
        {
            Update(organisation);
        }

        VerifiedOrganisation IOrganisationsCommand.VerifyOrganisation(Organisation organisation, Guid accountManagerId, Guid verifiedById)
        {
            // Copy properties.

            var verifiedOrganisation = new VerifiedOrganisation
            {
                Id = organisation.Id,
                Name = organisation.Name,
                Address = organisation.Address == null ? null : organisation.Address.Clone(),
                AffiliateId = organisation.AffiliateId,
                ContactDetails = null,
                AccountManagerId = accountManagerId,
                VerifiedById = verifiedById
            };

            Update(verifiedOrganisation);
            return verifiedOrganisation;
        }

        void IOrganisationsCommand.SplitFullName(string fullName, out string parentFullName, out string name)
        {
            SplitFullName(fullName, out parentFullName, out name);
        }

        private void Update(Organisation organisation)
        {
            // The contact details may be newly added so set its defaults.

            if (organisation.IsVerified)
            {
                var verifiedOrganisation = (VerifiedOrganisation)organisation;
                if (verifiedOrganisation.ContactDetails != null)
                    verifiedOrganisation.ContactDetails.Prepare();
            }

            PrepareUpdate(organisation);
            Validate(organisation);
            _repository.UpdateOrganisation(organisation);
        }

        private static void PrepareUpdate(IOrganisation organisation)
        {
            // Make sure the location is set up properly, even on an update,
            // because the property may have been set to a new instance.

            if (organisation.Address != null)
                organisation.Address.Prepare();
        }

        private void Validate(Organisation organisation)
        {
            // Standard validations.

            organisation.Validate();

            // Check that the parent properties are consistent.

            if (organisation.ParentId == null && !string.IsNullOrEmpty(organisation.ParentFullName))
                throw new ValidationErrorsException(new InconsistentValidationError("ParentId", "ParentFullName"));

            // Cannot be its own parent.

            if (organisation.ParentId != null)
            {
                if (organisation.Id == organisation.ParentId.Value)
                    throw new ValidationErrorsException(new CircularValidationError("Parent"));

                // The parent cannot be a descendent.

                if (_repository.IsDescendent(organisation.Id, organisation.ParentId.Value))
                    throw new ValidationErrorsException(new CircularValidationError("Parent"));
            }

            // Unverified organisations can have the same name but verified organisations cannot.

            if (organisation.IsVerified)
            {
                var existingOrganisation = _repository.GetVerifiedOrganisation(organisation.FullName);
                if (existingOrganisation != null && existingOrganisation.Id != organisation.Id)
                    throw new ValidationErrorsException(new DuplicateValidationError("FullName"));
            }
        }

        private static void SplitFullName(string fullName, out string parentFullName, out string name)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("The full name must be specified.", "fullName");

            var index = fullName.LastIndexOf(Organisation.FullNameSeparator);
            if (index == 0 || index == fullName.Length - 1)
                throw new ArgumentException("The name separator was found at the start or end of the company name: " + fullName, "fullName");

            if (index == -1)
            {
                parentFullName = null;
                name = fullName;
            }
            else
            {
                parentFullName = fullName.Substring(0, index);
                name = fullName.Substring(index + 1);
            }
        }
    }
}