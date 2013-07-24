using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Query.Search.Recruiters.Data
{
    public class RecruitersRepository
        : Repository, IRecruitersRepository
    {
        private readonly ILocationQuery _locationQuery;

        private static readonly DataLoadOptions OrganisationLoadOptions;

        private static readonly Func<RecruitersDataContext, string, int, IQueryable<string>> GetOrganisationFullNames
            = CompiledQuery.Compile((RecruitersDataContext dc, string partialFullName, int count)
                                    => (from o in dc.OrganisationalUnitEntities
                                        where SqlMethods.Like(dc.GetOrganisationFullName(o.id, null), partialFullName + '%')
                                        select dc.GetOrganisationFullName(o.id, null)).Take(count));

        static RecruitersRepository()
        {
            OrganisationLoadOptions = new DataLoadOptions();
            OrganisationLoadOptions.LoadWith<OrganisationEntity>(o => o.OrganisationalUnitEntity);
            OrganisationLoadOptions.LoadWith<OrganisationEntity>(o => o.AddressEntity);
            OrganisationLoadOptions.LoadWith<AddressEntity>(a => a.LocationReferenceEntity);
            OrganisationLoadOptions.LoadWith<OrganisationalUnitEntity>(o => o.ContactDetailsEntity);
        }

        public RecruitersRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
        }

        IList<Organisation> IRecruitersRepository.Search(OrganisationSearchCriteria criteria)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = OrganisationLoadOptions;

                // Do two separate queries against each of the verified and unverified organisations.

                IQueryable<Organisation> organisations = null;

                if (criteria.VerifiedOrganisations)
                {
                    // Look for verified organisations.

                    var query = from o in dc.OrganisationEntities
                                where o.OrganisationalUnitEntity != null
                                select o;

                    if (criteria.AccountManagerId != null)
                        query = from o in query
                                where o.OrganisationalUnitEntity.accountManagerId == criteria.AccountManagerId.Value
                                select o;

                    // Match the full name.

                    if (!string.IsNullOrEmpty(criteria.FullName))
                    {
                        if (criteria.MatchFullNameExactly)
                            query = from o in query
                                    where dc.GetOrganisationFullName(o.id, null) == criteria.FullName
                                    select o;
                        else
                            query = from o in query
                                    where SqlMethods.Like(dc.GetOrganisationFullName(o.id, null), criteria.FullName + '%')
                                    select o;
                    }

                    organisations = from o in query
                                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), _locationQuery);
                }

                if (criteria.UnverifiedOrganisations)
                {
                    // If the account manager is set then don't return anything.

                    if (criteria.AccountManagerId == null)
                    {
                        // Look for unverified organisations.

                        var query = from o in dc.OrganisationEntities
                                    where o.OrganisationalUnitEntity == null
                                    select o;

                        // Match the name.

                        if (!string.IsNullOrEmpty(criteria.FullName))
                        {
                            if (criteria.MatchFullNameExactly)
                                query = from o in query
                                        where o.displayName == criteria.FullName
                                        select o;
                            else
                                query = from o in query
                                        where SqlMethods.Like(o.displayName, criteria.FullName + '%')
                                        select o;
                        }

                        var unverifiedOrganisations = from o in query
                                                      select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), _locationQuery);

                        organisations = organisations == null
                                            ? unverifiedOrganisations
                                            : organisations.Concat(unverifiedOrganisations);
                    }
                }

                // Order by full name after all results have been returned from the database.

                return organisations == null
                           ? new List<Organisation>()
                           : (from o in organisations.ToArray() orderby o.FullName select o).ToList();
            }
        }

        IList<string> IRecruitersRepository.GetOrganisationFullNames(string partialFullName, int count)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from n in GetOrganisationFullNames(dc, partialFullName, count)
                        orderby n
                        select n).ToList();
            }
        }

        private RecruitersDataContext CreateContext()
        {
            return CreateContext(c => new RecruitersDataContext(c));
        }
    }
}