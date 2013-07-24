using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Roles.Recruiters.Queries
{
    public class RecruitersQuery
        : IRecruitersQuery
    {
        private readonly IRecruitersRepository _repository;

        public RecruitersQuery(IRecruitersRepository repository)
        {
            _repository = repository;
        }

        Organisation IRecruitersQuery.GetOrganisation(Guid recruiterId)
        {
            return _repository.GetRecruiterOrganisation(recruiterId);
        }

        IDictionary<Guid, Organisation> IRecruitersQuery.GetOrganisations(IEnumerable<Guid> recruiterIds)
        {
            return _repository.GetRecruiterOrganisations(recruiterIds);
        }

        IDictionary<Guid, Organisation> IRecruitersQuery.GetOrganisations(IEnumerable<Guid> recruiterIds, IEnumerable<Guid> organisationIds)
        {
            return !organisationIds.Any()
                ? _repository.GetRecruiterOrganisations(recruiterIds)
                : _repository.GetRecruiterOrganisations(recruiterIds, organisationIds);
        }

        OrganisationHierarchyPath IRecruitersQuery.GetOrganisationHierarchyPath(Guid recruiterId)
        {
            return _repository.GetRecruiterOrganisationHierarchyPath(recruiterId);
        }

        IList<Guid> IRecruitersQuery.GetRecruiters(Guid organisationId)
        {
            return _repository.GetRecruiters(organisationId);
        }

        IList<Guid> IRecruitersQuery.GetRecruiters(OrganisationHierarchy organisationHierarchy)
        {
            return _repository.GetRecruiters(GetOrganisationIds(organisationHierarchy));
        }

        private static IEnumerable<Guid> GetOrganisationIds(OrganisationHierarchy organisationHierarchy)
        {
            var ids = new List<Guid>();
            GetOrganisationIds(ids, organisationHierarchy);
            return ids;
        }

        private static void GetOrganisationIds(ICollection<Guid> ids, OrganisationHierarchy organisationHierarchy)
        {
            ids.Add(organisationHierarchy.Organisation.Id);
            foreach (var child in organisationHierarchy.ChildOrganisationHierarchies)
                GetOrganisationIds(ids, child);
        }
    }
}