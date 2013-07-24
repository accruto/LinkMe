using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Recruiters.Queries
{
    public class OrganisationsQuery
        : IOrganisationsQuery
    {
        private readonly IRecruitersRepository _repository;

        public OrganisationsQuery(IRecruitersRepository repository)
        {
            _repository = repository;
        }

        Organisation IOrganisationsQuery.GetOrganisation(Guid id)
        {
            return _repository.GetOrganisation(id);
        }

        Organisation IOrganisationsQuery.GetOrganisation(Guid? parentId, string name)
        {
            return _repository.GetOrganisation(parentId, name);
        }

        VerifiedOrganisation IOrganisationsQuery.GetVerifiedOrganisation(string fullName)
        {
            return _repository.GetVerifiedOrganisation(fullName);
        }

        Organisation IOrganisationsQuery.GetRootOrganisation(Guid id)
        {
            return _repository.GetRootOrganisation(id);
        }

        IList<Organisation> IOrganisationsQuery.GetOrganisations(IEnumerable<Guid> ids)
        {
            return _repository.GetOrganisations(ids);
        }

        bool IOrganisationsQuery.IsDescendent(Guid parentId, Guid childId)
        {
            return _repository.IsDescendent(parentId, childId);
        }

        OrganisationHierarchy IOrganisationsQuery.GetOrganisationHierarchy(Guid organisationId)
        {
            return _repository.GetOrganisationHierarchy(organisationId);
        }

        OrganisationHierarchy IOrganisationsQuery.GetSubOrganisationHierarchy(Guid organisationId)
        {
            return _repository.GetSubOrganisationHierarchy(organisationId);
        }

        ContactDetails IOrganisationsQuery.GetEffectiveContactDetails(Guid organisationId)
        {
            return _repository.GetEffectiveContactDetails(organisationId);
        }
    }
}