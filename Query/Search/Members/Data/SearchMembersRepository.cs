using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain.Data;

namespace LinkMe.Query.Search.Members.Data
{
    public class SearchMembersRepository
        : Repository, ISearchMembersRepository
    {
        public SearchMembersRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Guid> ISearchMembersRepository.Search(AdministrativeMemberSearchCriteria criteria)
        {
            using (var dc = CreateContext())
            {
                var query = from m in dc.MemberEntities
                            join u in dc.RegisteredUserEntities on m.id equals u.id
                            join c in dc.CommunityMemberEntities on m.id equals c.id into tCommunityMemberEntities
                            from tc in tCommunityMemberEntities.DefaultIfEmpty()
                            select new {m, u, tc};

                if (!string.IsNullOrEmpty(criteria.FirstName))
                    query = from m in query
                            where SqlMethods.Like(m.u.firstName, criteria.FirstName + '%')
                            select m;

                if (!string.IsNullOrEmpty(criteria.LastName))
                    query = from m in query
                            where SqlMethods.Like(m.u.lastName, criteria.LastName + '%')
                            select m;

                if (!string.IsNullOrEmpty(criteria.EmailAddress))
                    query = from m in query
                            where SqlMethods.Like(m.u.emailAddress, criteria.EmailAddress + '%')
                            select m;

                query = from m in query
                        orderby m.u.firstName, m.u.lastName
                        select m;

                if (criteria.Count != null)
                    query = query.Take(criteria.Count.Value);

                return (from m in query
                        select m.m.id).ToList();
            }
        }

        private MembersDataContext CreateContext()
        {
            return CreateContext(c => new MembersDataContext(c));
        }
    }
}
