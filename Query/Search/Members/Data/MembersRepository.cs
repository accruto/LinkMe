using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.Members.Data
{
    public class MembersRepository
        : Repository, IMembersRepository
    {
        // Ignore these completely.

        private const string LegacyLikeCriteria = "LinkMe.Common.Managers.Search.LikeResumeSearchCriteria";

        private readonly ICriteriaPersister _criteriaPersister;

        private static readonly DataLoadOptions SearchLoadOptions = DataOptions.CreateLoadOptions<SavedResumeSearchEntity, ResumeSearchCriteriaSetEntity>(s => s.ResumeSearchCriteriaSetEntity, c => c.ResumeSearchCriteriaEntities);
        private static readonly DataLoadOptions ExecutionLoadOptions;

        private static readonly Func<MembersDataContext, Guid, SavedResumeSearchEntity> GetSavedResumeSearchEntityQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid id)
                => (from s in dc.SavedResumeSearchEntities
                    where s.id == id
                    select s).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, ICriteriaPersister, MemberSearch> GetMemberSearchQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid id, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    where s.id == id
                    select s.Map(criteriaPersister)).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, string, ICriteriaPersister, MemberSearch> GetMemberSearchByNameQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid ownerId, string name, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    where s.ownerId == ownerId
                    && s.name == name
                    select s.Map(criteriaPersister)).SingleOrDefault());

        private static readonly Func<MembersDataContext, string, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ids, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on s.id equals i.value
                    select s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, Guid, int> GetMemberSearchesByOwnerCount
            = CompiledQuery.Compile((MembersDataContext dc, Guid ownerId)
                => (from s in dc.SavedResumeSearchEntities
                    where s.ownerId == ownerId
                    select s).Count());

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesByOwnersSkipTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.ownerId equals i.value
                    select s).Skip(range.Skip).Take(range.Take.Value).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesByOwnersSkipQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.ownerId equals i.value
                    select s).Skip(range.Skip).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesByOwnersTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.ownerId equals i.value
                    select s).Take(range.Take.Value).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesByOwnersQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, ICriteriaPersister criteriaPersister)
                => from s in dc.SavedResumeSearchEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.ownerId equals i.value
                   select s.Map(criteriaPersister));

        private static readonly Func<MembersDataContext, Guid, ICriteriaPersister, IQueryable<MemberSearch>> GetMemberSearchesByOwnerQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid ownerId, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedResumeSearchEntities
                    where s.ownerId == ownerId
                    select s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, Guid, ICriteriaPersister, MemberSearchExecution> GetMemberSearchExecutionQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid id, ICriteriaPersister criteriaPersister)
                => (from s in dc.ResumeSearchEntities
                    where s.id == id
                    && s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                    select s.Map(criteriaPersister)).SingleOrDefault());

        private static readonly Func<MembersDataContext, Guid, ICriteriaPersister, IQueryable<MemberSearchExecution>> GetMemberSearchExecutionsQuery
            = CompiledQuery.Compile((MembersDataContext dc, Guid ownerId, ICriteriaPersister criteriaPersister)
                => from s in dc.ResumeSearchEntities
                   where s.searcherId == ownerId
                   && s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                   select s.Map(criteriaPersister));

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearchExecution>> GetMemberSearchExecutionsByOwnersSkipTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.ResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.searcherId equals i.value
                    where s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                    select s).Skip(range.Skip).Take(range.Take.Value).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearchExecution>> GetMemberSearchExecutionsByOwnersSkipQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.ResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.searcherId equals i.value
                    where s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                    select s).Skip(range.Skip).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, Range, ICriteriaPersister, IQueryable<MemberSearchExecution>> GetMemberSearchExecutionsByOwnersTakeQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, Range range, ICriteriaPersister criteriaPersister)
                => (from s in dc.ResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.searcherId equals i.value
                    where s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                    select s).Take(range.Take.Value).Select(s => s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, string, ICriteriaPersister, IQueryable<MemberSearchExecution>> GetMemberSearchExecutionsByOwnersQuery
            = CompiledQuery.Compile((MembersDataContext dc, string ownerIds, ICriteriaPersister criteriaPersister)
                => (from s in dc.ResumeSearchEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on s.searcherId equals i.value
                    where s.ResumeSearchCriteriaSetEntity.type != LegacyLikeCriteria
                    select s.Map(criteriaPersister)));

        private static readonly Func<MembersDataContext, Guid, Guid, bool> IsCriteriaOwnedByOtherSearch
            = CompiledQuery.Compile((MembersDataContext dc, Guid criteriaSetId, Guid notSavedSearchId)
                => (from c in dc.ResumeSearchCriteriaSetEntities
                    join s in dc.SavedResumeSearchEntities on c.id equals s.criteriaSetId
                    where c.id == criteriaSetId
                    && s.id != notSavedSearchId
                    select c).Any());

        private static readonly Func<MembersDataContext, Guid, bool> IsCriteriaOwnedByExecution
            = CompiledQuery.Compile((MembersDataContext dc, Guid criteriaSetId)
                => (from c in dc.ResumeSearchCriteriaSetEntities
                    join s in dc.ResumeSearchEntities on c.id equals s.criteriaSetId
                    where c.id == criteriaSetId
                    select c).Any());

        static MembersRepository()
        {
            ExecutionLoadOptions = new DataLoadOptions();
            ExecutionLoadOptions.LoadWith<ResumeSearchEntity>(s => s.ResumeSearchCriteriaSetEntity);
            ExecutionLoadOptions.LoadWith<ResumeSearchEntity>(s => s.ResumeSearchResultSetEntity);
            ExecutionLoadOptions.LoadWith<ResumeSearchCriteriaSetEntity>(s => s.ResumeSearchCriteriaEntities);
            ExecutionLoadOptions.LoadWith<ResumeSearchResultSetEntity>(s => s.ResumeSearchResultEntities);
        }

        public MembersRepository(IDataContextFactory dataContextFactory, ICriteriaPersister criteriaPersister)
            : base(dataContextFactory)
        {
            _criteriaPersister = criteriaPersister;
        }

        void IMembersRepository.CreateMemberSearch(MemberSearch search)
        {
            using (var dc = CreateContext())
            {
                dc.SavedResumeSearchEntities.InsertOnSubmit(search.Map(_criteriaPersister));

                try
                {
                    dc.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    CheckDuplicates(ex);
                    throw;
                }
            }
        }

        void IMembersRepository.UpdateMemberSearch(MemberSearch search)
        {
            using (var dc = CreateContext())
            {
                try
                {
                    var entity = GetSavedResumeSearchEntity(dc, search.Id);
                    if (entity != null)
                    {
                        // Delete any criteria that may already be there.

                        DeleteCriteria(dc, entity);
                        search.MapTo(entity, _criteriaPersister);
                        dc.SubmitChanges();
                    }
                }
                catch (SqlException ex)
                {
                    CheckDuplicates(ex);
                    throw;
                }
            }
        }

        void IMembersRepository.DeleteMemberSearch(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedResumeSearchEntity(dc, id);
                if (entity != null)
                {
                    DeleteCriteria(dc, entity);
                    dc.SavedResumeSearchEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        MemberSearch IMembersRepository.GetMemberSearch(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearch(dc, id);
            }
        }

        MemberSearch IMembersRepository.GetMemberSearch(Guid ownerId, string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearch(dc, ownerId, name);
            }
        }

        IList<MemberSearch> IMembersRepository.GetMemberSearches(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearches(dc, ids).ToList();
            }
        }

        IList<MemberSearch> IMembersRepository.GetMemberSearches(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearches(dc, ownerId).ToList();
            }
        }

        RangeResult<MemberSearch> IMembersRepository.GetMemberSearches(Guid ownerId, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Need to do this properly.

                return new RangeResult<MemberSearch>(
                    GetMemberSearchesByOwnerCount(dc, ownerId),
                    GetMemberSearches(dc, new[]{ownerId}, range).ToList());
            }
        }

        IList<MemberSearch> IMembersRepository.GetMemberSearches(IEnumerable<Guid> ownerIds, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearches(dc, ownerIds, range).ToList();
            }
        }

        void IMembersRepository.CreateMemberSearchExecution(MemberSearchExecution execution, int maxResults)
        {
            using (var dc = CreateContext())
            {
                dc.ResumeSearchEntities.InsertOnSubmit(execution.Map(maxResults, _criteriaPersister));
                dc.SubmitChanges();
            }
        }

        MemberSearchExecution IMembersRepository.GetMemberSearchExecution(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchExecution(dc, id);
            }
        }

        IList<MemberSearchExecution> IMembersRepository.GetMemberSearchExecutions(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchExecutions(dc, ownerId).ToList();
            }
        }

        IList<MemberSearchExecution> IMembersRepository.GetMemberSearchExecutions(IEnumerable<Guid> ownerIds, Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchExecutions(dc, ownerIds, range).ToList();
            }
        }

        private static SavedResumeSearchEntity GetSavedResumeSearchEntity(MembersDataContext dc, Guid id)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetSavedResumeSearchEntityQuery(dc, id);
        }

        private MemberSearch GetMemberSearch(MembersDataContext dc, Guid id)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetMemberSearchQuery(dc, id, _criteriaPersister);
        }

        private MemberSearch GetMemberSearch(MembersDataContext dc, Guid ownerId, string name)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetMemberSearchByNameQuery(dc, ownerId, name, _criteriaPersister);
        }

        private IEnumerable<MemberSearch> GetMemberSearches(MembersDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetMemberSearchesQuery(dc, new SplitList<Guid>(ids).ToString(), _criteriaPersister);
        }

        private IEnumerable<MemberSearch> GetMemberSearches(MembersDataContext dc, Guid ownerId)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetMemberSearchesByOwnerQuery(dc, ownerId, _criteriaPersister);
        }

        private IEnumerable<MemberSearch> GetMemberSearches(MembersDataContext dc, IEnumerable<Guid> ownerIds, Range range)
        {
            dc.LoadOptions = SearchLoadOptions;

            if (range.Skip != 0)
                return range.Take != null
                    ? GetMemberSearchesByOwnersSkipTakeQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister)
                    : GetMemberSearchesByOwnersSkipQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister);
            return range.Take != null
                ? GetMemberSearchesByOwnersTakeQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister)
                : GetMemberSearchesByOwnersQuery(dc, new SplitList<Guid>(ownerIds).ToString(), _criteriaPersister);
        }

        private static void DeleteCriteria(MembersDataContext dc, SavedResumeSearchEntity entity)
        {
            if (entity.ResumeSearchCriteriaSetEntity != null)
            {
                // Need to check that there are no other searches or executions that are using this criteria.

                if (!IsCriteriaOwnedByOtherSearch(dc, entity.ResumeSearchCriteriaSetEntity.id, entity.id) && !IsCriteriaOwnedByExecution(dc, entity.ResumeSearchCriteriaSetEntity.id))
                {
                    dc.ResumeSearchCriteriaEntities.DeleteAllOnSubmit(entity.ResumeSearchCriteriaSetEntity.ResumeSearchCriteriaEntities);
                    dc.ResumeSearchCriteriaSetEntities.DeleteOnSubmit(entity.ResumeSearchCriteriaSetEntity);
                }
            }
        }

        private MemberSearchExecution GetMemberSearchExecution(MembersDataContext dc, Guid id)
        {
            dc.LoadOptions = ExecutionLoadOptions;
            return GetMemberSearchExecutionQuery(dc, id, _criteriaPersister);
        }

        private IEnumerable<MemberSearchExecution> GetMemberSearchExecutions(MembersDataContext dc, Guid ownerId)
        {
            dc.LoadOptions = ExecutionLoadOptions;
            return GetMemberSearchExecutionsQuery(dc, ownerId, _criteriaPersister);
        }

        private IEnumerable<MemberSearchExecution> GetMemberSearchExecutions(MembersDataContext dc, IEnumerable<Guid> ownerIds, Range range)
        {
            dc.LoadOptions = ExecutionLoadOptions;

            if (range.Skip != 0)
                return range.Take != null
                    ? GetMemberSearchExecutionsByOwnersSkipTakeQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister)
                    : GetMemberSearchExecutionsByOwnersSkipQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister);
            return range.Take != null
                ? GetMemberSearchExecutionsByOwnersTakeQuery(dc, new SplitList<Guid>(ownerIds).ToString(), range, _criteriaPersister)
                : GetMemberSearchExecutionsByOwnersQuery(dc, new SplitList<Guid>(ownerIds).ToString(), _criteriaPersister);
        }

        private static void CheckDuplicates(SqlException ex)
        {
            if (ex.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.SavedResumeSearch' with unique index 'IX_SavedResumeSearch_ownerId_name'."))
                throw new ValidationErrorsException(new DuplicateValidationError("Name"));
        }

        private MembersDataContext CreateContext()
        {
            return CreateContext(c => new MembersDataContext(c));
        }
    }
}
