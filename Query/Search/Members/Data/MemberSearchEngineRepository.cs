using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Data
{
    public class MemberSearchEngineRepository
        : Repository, IMemberSearchEngineRepository
    {
        #region Queries

        private static readonly Func<MembersDataContext, IQueryable<Guid>> GetAllMemberIds
            = CompiledQuery.Compile((MembersDataContext dc)
                => from m in dc.MemberEntities
                   where (m.employerAccess & 1) == 1 && (m.RegisteredUserEntity.flags & 0x04) == 0x00
                   select m.id);

        private static readonly Func<MembersDataContext, DateTime, IQueryable<Guid>> GetModifiedMemberIds
            = CompiledQuery.Compile((MembersDataContext dc, DateTime modifiedSince)
                => from m in dc.MemberIndexingEntities
                   where m.modifiedTime >= modifiedSince
                   select m.memberId);

        #endregion

        public MemberSearchEngineRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        #region Implementation of ISearchRepository

        IList<Guid> IMemberSearchEngineRepository.GetModified(DateTime? modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return modifiedSince.HasValue
                    ? GetModifiedMemberIds(dc, modifiedSince.Value).ToList()
                    : GetAllMemberIds(dc).ToList();
            }
        }

        void IMemberSearchEngineRepository.SetModified(Guid id)
        {
            using (var dc = CreateContext())
            {
                dc.MemberSetModified(id);
            }
        }

        #endregion

        private MembersDataContext CreateContext()
        {
            return CreateContext(c => new MembersDataContext(c));
        }
    }
}