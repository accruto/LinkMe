using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;

namespace LinkMe.Domain.Users.Employers.Queries
{
    public class EmployersQuery
        : EmployerRecruitersQuery, IEmployersQuery
    {
        private readonly IEmployersRepository _repository;
        private readonly IOrganisationsQuery _organisationsQuery;

        public EmployersQuery(IEmployersRepository repository, IRecruitersQuery recruitersQuery, IOrganisationsQuery organisationsQuery)
            : base(recruitersQuery)
        {
            _repository = repository;
            _organisationsQuery = organisationsQuery;
        }

        Employer IEmployersQuery.GetEmployer(Guid id)
        {
            var employer = _repository.GetEmployer(id);
            return employer != null
                ? GetEmployer(employer, _recruitersQuery.GetOrganisation(id))
                : employer;
        }

        IList<Employer> IEmployersQuery.GetEmployers(IEnumerable<Guid> ids)
        {
            return GetOrganisations(_repository.GetEmployers(ids));
        }

        IList<Employer> IEmployersQuery.GetEmployers(string emailAddress)
        {
            return GetOrganisations(_repository.GetEmployers(emailAddress));
        }

        IList<Guid> IEmployersQuery.GetEmployerIds()
        {
            return _repository.GetEmployerIds();
        }

        IList<Employer> IEmployersQuery.GetOrganisationEmployers(Guid organisationId)
        {
            // Get the organisation itself.

            var organisation = _organisationsQuery.GetOrganisation(organisationId);
            if (organisation == null)
                return new Employer[0].ToList();

            var recruiterIds = _recruitersQuery.GetRecruiters(organisationId);
            return (from e in _repository.GetEmployers(recruiterIds)
                    orderby e.FullName
                    select GetEmployer(e, organisation)).ToList();
        }

        IList<Employer> IEmployersQuery.GetOrganisationEmployers(IEnumerable<Guid> organisationIds)
        {
            // Get the organisations.

            var organisations = _organisationsQuery.GetOrganisations(organisationIds);
            if (organisations.Count == 0)
                return new Employer[0].ToList();

            // Find all recruiters and their organisation.

            IList<KeyValuePair<Guid, Organisation>> recruiters = new List<KeyValuePair<Guid, Organisation>>();
            foreach (var organisation in organisations)
                recruiters = recruiters.Concat(from r in _recruitersQuery.GetRecruiters(organisation.Id) select new KeyValuePair<Guid, Organisation>(r, organisation)).ToList();

            var allRecruiters = recruiters.ToDictionary(r => r.Key, r => r.Value);
            return (from e in _repository.GetEmployers(allRecruiters.Keys)
                    orderby e.FullName
                    select GetEmployer(e, allRecruiters)).ToList();
        }
    }
}