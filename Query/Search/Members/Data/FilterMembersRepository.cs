using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Data
{
    public class FilterMembersRepository
        : Repository, IFilterMembersRepository
    {
        // EmailAddress matches exactly.

        private static readonly Func<MembersDataContext, string, DateTime, IQueryable<Guid>> FilterSortedByLastEditedTime
            = CompiledQuery.Compile((MembersDataContext dc, string memberIds, DateTime modifiedSince)
                => (from r in (from r in dc.ResumeEntities
                               join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberIds) on r.id equals i.value
                               where r.lastEditedTime > modifiedSince
                               select new { r.id, r.lastEditedTime })
                    orderby r.lastEditedTime descending
                    select r.id).Distinct());

        private static readonly Func<MembersDataContext, string, DateTime, IQueryable<Guid>> FilterByLastEditedTime
            = CompiledQuery.Compile((MembersDataContext dc, string memberIds, DateTime modifiedSince)
                => (from r in dc.ResumeEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberIds) on r.id equals i.value
                    where r.lastEditedTime > modifiedSince
                    select r.id).Distinct());

        public FilterMembersRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Guid> IFilterMembersRepository.Filter(IEnumerable<Guid> memberIds, MemberSortOrder sortOrder, DateTime? modifiedSince)
        {
            return modifiedSince == null
                ? memberIds.ToList()
                : Filter(memberIds, sortOrder, modifiedSince.Value);
        }

        private IList<Guid> Filter(IEnumerable<Guid> memberIds, MemberSortOrder sortOrder, DateTime modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                switch (sortOrder)
                {
                    case MemberSortOrder.DateUpdated:
                        return FilterSortedByLastEditedTime(dc, new SplitList<Guid>(memberIds).ToString(), modifiedSince).ToList();

                    default:
                        return FilterByLastEditedTime(dc, new SplitList<Guid>(memberIds).ToString(), modifiedSince).ToList();
                }
            }
        }

        private MembersDataContext CreateContext()
        {
            return CreateContext(c => new MembersDataContext(c));
        }
    }
}