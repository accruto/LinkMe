using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Data.Linq;
using Mappings = LinkMe.Domain.Users.Employers.Data.Mappings;

namespace LinkMe.Query.Search.Employers.Data
{
    public class EmployersRepository
        : Repository, IEmployersRepository
    {
        private readonly IIndustriesQuery _industriesQuery;

        public EmployersRepository(IDataContextFactory dataContextFactory, IIndustriesQuery industriesQuery)
            : base(dataContextFactory)
        {
            _industriesQuery = industriesQuery;
        }

        IList<Employer> IEmployersRepository.Search(OrganisationEmployerSearchCriteria criteria)
        {
            using (var dc = CreateContext())
            {
                // Only include enabled users.

                var employers = from e in dc.EmployerEntities
                                join u in dc.RegisteredUserEntities on e.id equals u.id
                                where (u.flags & (short) UserFlags.Disabled) == 0
                                select new {e, u};

                // Employers, recruiters?

                if (criteria.Employers && !criteria.Recruiters)
                    employers = from q in employers
                                where q.e.subRole == (byte)EmployerSubRole.Employer
                                select q;
                else if (!criteria.Employers && criteria.Recruiters)
                    employers = from q in employers
                                where q.e.subRole == (byte)EmployerSubRole.Recruiter
                                select q;

                // Industries.

                if (criteria.IndustryIds != null && criteria.IndustryIds.Count > 0)
                {
                    var industries = criteria.IndustryIds.ToArray();
                    employers = from q in employers
                                join ei in dc.EmployerIndustryEntities on q.e.id equals ei.employerId
                                where industries.Contains(ei.industryId)
                                select q;
                }

                // Organisation.

                if (criteria.VerifiedOrganisations && !criteria.UnverifiedOrganisations)
                    employers = from q in employers
                                join ou in dc.OrganisationalUnitEntities on q.e.organisationId equals ou.id
                                select q;
                else if (!criteria.VerifiedOrganisations && criteria.UnverifiedOrganisations)
                    employers = from q in employers
                                join o in dc.OrganisationEntities on q.e.organisationId equals o.id
                                where !(from ou in dc.OrganisationalUnitEntities select ou.id).Contains(o.id)
                                select q;

                // Logins.

                if (criteria.MinimumLogins != null)
                    employers = from q in employers
                                where (from e in dc.EmployerEntities where e.organisationId == q.e.organisationId select e).Count() >= criteria.MinimumLogins.Value
                                select q;
                if (criteria.MaximumLogins != null)
                    employers = from q in employers
                                where (from e in dc.EmployerEntities where e.organisationId == q.e.organisationId select e).Count() <= criteria.MaximumLogins.Value
                                select q;

                // Do the query.

                return (from e in employers.Distinct().ToList()
                        orderby e.u.emailAddress
                        select Mappings.Map(e.e, e.u, _industriesQuery)).ToList();
            }
        }

        IList<Employer> IEmployersRepository.Search(AdministrativeEmployerSearchCriteria criteria)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Need to build up the query.

                var employers = from e in dc.EmployerEntities
                                join u in dc.RegisteredUserEntities on e.id equals u.id
                                select new { e, u };

                // Email address.

                if (!string.IsNullOrEmpty(criteria.EmailAddress))
                    employers = from x in employers
                                where SqlMethods.Like(x.u.emailAddress, "%" + criteria.EmailAddress + "%")
                                select x;

                // First name.

                if (!string.IsNullOrEmpty(criteria.FirstName))
                    employers = from x in employers
                                where SqlMethods.Like(x.u.firstName, "%" + criteria.FirstName + "%")
                                select x;

                // Last name.

                if (!string.IsNullOrEmpty(criteria.LastName))
                    employers = from x in employers
                                where SqlMethods.Like(x.u.lastName, "%" + criteria.LastName + "%")
                                select x;

                // LoginId.

                if (!string.IsNullOrEmpty(criteria.LoginId))
                    employers = from x in employers
                                where SqlMethods.Like(x.u.loginId, "%" + criteria.LoginId + "%")
                                select x;

                // Enabled.

                if (!criteria.IsEnabled && !criteria.IsDisabled)
                    employers = from x in employers
                                where 1 == 0
                                select x;
                else if (criteria.IsEnabled && !criteria.IsDisabled)
                    employers = from x in employers
                                where (x.u.flags & (int)UserFlags.Disabled) == 0
                                select x;
                else if (!criteria.IsEnabled && criteria.IsDisabled)
                    employers = from x in employers
                                where (x.u.flags & (int)UserFlags.Disabled) != 0
                                select x;

                // Always order by login id.

                employers = from x in employers
                            orderby x.u.loginId
                            select x;

                // Count.

                if (criteria.Count != null)
                    employers = employers.Take(criteria.Count.Value);

                return (from x in employers
                        select Mappings.Map(x.e, x.u, _industriesQuery)).ToList();
            }
        }

        private EmployersDataContext CreateContext()
        {
            return CreateContext(c => new EmployersDataContext(c));
        }
    }
}