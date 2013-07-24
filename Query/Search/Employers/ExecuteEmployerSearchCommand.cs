using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Query.Search.Recruiters;

namespace LinkMe.Query.Search.Employers
{
    public class ExecuteEmployerSearchCommand
        : EmployerRecruitersQuery, IExecuteEmployerSearchCommand
    {
        private readonly IEmployersRepository _repository;
        private readonly IExecuteOrganisationSearchCommand _executeOrganisationSearchCommand;

        public ExecuteEmployerSearchCommand(IEmployersRepository repository, IRecruitersQuery recruitersQuery, IExecuteOrganisationSearchCommand executeOrganisationSearchCommand)
            : base(recruitersQuery)
        {
            _repository = repository;
            _executeOrganisationSearchCommand = executeOrganisationSearchCommand;
        }

        IList<Employer> IExecuteEmployerSearchCommand.Search(EmployerSearchCriteria criteria)
        {
            if (criteria is OrganisationEmployerSearchCriteria)
                return Search((OrganisationEmployerSearchCriteria)criteria);
            if (criteria is AdministrativeEmployerSearchCriteria)
                return Search((AdministrativeEmployerSearchCriteria) criteria);
            return new List<Employer>();
        }

        private IList<Employer> Search(OrganisationEmployerSearchCriteria criteria)
        {
            // Need to at least include employers or recruiters or both.

            if (!(criteria.Employers || criteria.Recruiters))
                return new List<Employer>();

            // Need to at least include verified or unverified organisations or both.

            if (!(criteria.VerifiedOrganisations || criteria.UnverifiedOrganisations))
                return new List<Employer>();

            return _repository.Search(criteria);
        }

        private IList<Employer> Search(AdministrativeEmployerSearchCriteria criteria)
        {
            // Get all organisations that match the criteria.

            IEnumerable<Employer> contacts;
            IDictionary<Guid, Organisation> contactOrganisations;

            if (!string.IsNullOrEmpty(criteria.OrganisationName))
            {
                // Get all the employers that match the other criteria.

                var originalCount = criteria.Count;
                criteria.Count = null;

                contacts = _repository.Search(criteria);

                // Search for the matching organisations.

                var organisationsCriteria = new OrganisationSearchCriteria
                                                {
                                                    FullName = criteria.OrganisationName,
                                                    MatchFullNameExactly = criteria.MatchOrganisationNameExactly,
                                                    UnverifiedOrganisations = true,
                                                    VerifiedOrganisations = true,
                                                };

                var organisations = _executeOrganisationSearchCommand.Search(organisationsCriteria);

                // Filter the contacts out by organisations.

                contactOrganisations = _recruitersQuery.GetOrganisations(from c in contacts select c.Id, from o in organisations select o.Id);
                contacts = from c in contacts
                           where contactOrganisations.ContainsKey(c.Id)
                           select c;

                if (originalCount != null)
                    contacts = contacts.Take(originalCount.Value);
            }
            else
            {
                // Pass it through to the repository.

                contacts = _repository.Search(criteria);
                contactOrganisations = _recruitersQuery.GetOrganisations(from c in contacts select c.Id);
            }

            // Set up the organisations.

            contacts = from c in contacts
                       select GetEmployer(c, contactOrganisations);

            // Order now.

            if (criteria.SortOrder == EmployerSortOrder.OrganisationNameLoginId)
                return (from c in contacts
                        orderby c.Organisation.Name, c.EmailAddress.Address // c.LoginId
                        select c).ToList();

            return (from c in contacts
                    orderby c.EmailAddress.Address // c.LoginId
                    select c).ToList();
        }
    }
}