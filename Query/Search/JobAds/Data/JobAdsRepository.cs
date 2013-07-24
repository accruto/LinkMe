using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.JobAds.Data
{
    public class JobAdsRepository
        : Repository, IJobAdsRepository
    {
        private readonly ICriteriaPersister _criteriaPersister;

        private static readonly DataLoadOptions SearchLoadOptions = DataOptions.CreateLoadOptions<SavedJobSearchEntity, JobSearchCriteriaSetEntity>(s => s.JobSearchCriteriaSetEntity, c => c.JobSearchCriteriaEntities);
        private static readonly DataLoadOptions ExecutionLoadOptions;

        private static readonly Func<JobAdsDataContext, Guid, SavedJobSearchEntity> GetSavedJobSearchEntityQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from s in dc.SavedJobSearchEntities
                    where s.id == id
                    select s).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, ICriteriaPersister, JobAdSearch> GetJobAdSearchQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedJobSearchEntities
                    where s.id == id
                    select s.Map(criteriaPersister)).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, ICriteriaPersister, IQueryable<JobAdSearch>> GetJobAdSearchesQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, ICriteriaPersister criteriaPersister)
                => (from s in dc.SavedJobSearchEntities
                    where s.ownerId == ownerId
                    select s.Map(criteriaPersister)));

        private static readonly Func<JobAdsDataContext, Guid, ICriteriaPersister, JobAdSearchExecution> GetJobAdSearchExecutionQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id, ICriteriaPersister criteriaPersister)
                => (from s in dc.JobSearchEntities
                    where s.id == id
                    select s.Map(criteriaPersister)).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, DateTimeRange, ICriteriaPersister, IQueryable<JobAdSearchExecution>> GetJobAdSearchExecutionsQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, DateTimeRange range, ICriteriaPersister criteriaPersister)
                => from s in dc.JobSearchEntities
                   where s.searcherId == ownerId
                   && s.startTime >= range.Start.Value && s.startTime < range.End.Value
                   orderby s.startTime descending
                   select s.Map(criteriaPersister));

        private static readonly Func<JobAdsDataContext, Guid, int, ICriteriaPersister, IQueryable<JobAdSearchExecution>> GetMaxJobAdSearchExecutionsQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, int maxResults, ICriteriaPersister criteriaPersister)
                => (from s in dc.JobSearchEntities
                    where s.searcherId == ownerId
                    orderby s.startTime descending
                    select s.Map(criteriaPersister)).Take(maxResults));

        private static readonly Func<JobAdsDataContext, Guid, DateTimeRange, int> GetJobAdSearchExecutionCount
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid ownerId, DateTimeRange dateTimeRange)
                => (from s in dc.JobSearchEntities
                    where s.searcherId == ownerId
                    && s.startTime >= dateTimeRange.Start && s.startTime < dateTimeRange.End
                    select s).Count());

        private static readonly Func<JobAdsDataContext, Guid, Guid, bool> IsCriteriaOwnedByOtherSearch
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid criteriaSetId, Guid notSavedSearchId)
                => (from c in dc.JobSearchCriteriaSetEntities
                    join s in dc.SavedJobSearchEntities on c.id equals s.criteriaSetId
                    where c.id == criteriaSetId
                    && s.id != notSavedSearchId
                    select c).Any());

        private static readonly Func<JobAdsDataContext, Guid, bool> IsCriteriaOwnedByExecution
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid criteriaSetId)
                => (from c in dc.JobSearchCriteriaSetEntities
                    join s in dc.JobSearchEntities on c.id equals s.criteriaSetId
                    where c.id == criteriaSetId
                    select c).Any());

        static JobAdsRepository()
        {
            ExecutionLoadOptions = new DataLoadOptions();
            ExecutionLoadOptions.LoadWith<JobSearchEntity>(s => s.JobSearchCriteriaSetEntity);
            ExecutionLoadOptions.LoadWith<JobSearchEntity>(s => s.JobSearchResultSetEntity);
            ExecutionLoadOptions.LoadWith<JobSearchCriteriaSetEntity>(s => s.JobSearchCriteriaEntities);
            ExecutionLoadOptions.LoadWith<JobSearchResultSetEntity>(s => s.JobSearchResultEntities);
        }

        public JobAdsRepository(IDataContextFactory dataContextFactory, ICriteriaPersister criteriaPersister)
            : base(dataContextFactory)
        {
            _criteriaPersister = criteriaPersister;
        }

        void IJobAdsRepository.CreateJobAdSearch(JobAdSearch search)
        {
            using (var dc = CreateContext())
            {
                dc.SavedJobSearchEntities.InsertOnSubmit(search.Map(_criteriaPersister));

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

        void IJobAdsRepository.DeleteJobAdSearch(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedJobSearchEntity(dc, id);
                if (entity == null)
                    return;
                
                DeleteCriteria(dc, entity);
                dc.SavedJobSearchEntities.DeleteOnSubmit(entity);
                dc.SubmitChanges();
            }
        }

        void IJobAdsRepository.UpdateJobAdSearch(JobAdSearch search)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedJobSearchEntity(dc, search.Id);
                if (entity == null)
                    return;
                
                // Need to delete all existing criteria.

                if (entity.JobSearchCriteriaSetEntity != null)
                {
                    if (entity.JobSearchCriteriaSetEntity.JobSearchCriteriaEntities != null && entity.JobSearchCriteriaSetEntity.JobSearchCriteriaEntities.Count > 0)
                        dc.JobSearchCriteriaEntities.DeleteAllOnSubmit(entity.JobSearchCriteriaSetEntity.JobSearchCriteriaEntities);
                    dc.JobSearchCriteriaSetEntities.DeleteOnSubmit(entity.JobSearchCriteriaSetEntity);
                }

                search.MapTo(entity, _criteriaPersister);

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

        void IJobAdsRepository.RemoveAlertFromSearch(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedJobSearchEntity(dc, id);
                if (entity == null)
                    return;

                entity.alertId = null;
                dc.SubmitChanges();
            }
        }

        JobAdSearch IJobAdsRepository.GetJobAdSearch(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearch(dc, id);
            }
        }

        IList<JobAdSearch> IJobAdsRepository.GetJobAdSearches(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearches(dc, ownerId).ToList();
            }
        }

        void IJobAdsRepository.CreateJobAdSearchExecution(JobAdSearchExecution execution, int maxResults)
        {
            using (var dc = CreateContext())
            {
                dc.JobSearchEntities.InsertOnSubmit(execution.Map(maxResults, _criteriaPersister));
                dc.SubmitChanges();
            }
        }

        JobAdSearchExecution IJobAdsRepository.GetJobAdSearchExecution(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchExecution(dc, id);
            }
        }

        IList<JobAdSearchExecution> IJobAdsRepository.GetJobAdSearchExecutions(Guid ownerId, DateTimeRange range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchExecutions(dc, ownerId, range).ToList();
            }
        }

        IList<JobAdSearchExecution> IJobAdsRepository.GetJobAdSearchExecutions(Guid ownerId, int maxCount)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchExecutions(dc, ownerId, maxCount).ToList();
            }
        }

        int IJobAdsRepository.GetJobAdSearchExecutionCount(Guid ownerId, DateTimeRange dateTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchExecutionCount(dc, ownerId, dateTimeRange);
            }
        }

        private static SavedJobSearchEntity GetSavedJobSearchEntity(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetSavedJobSearchEntityQuery(dc, id);
        }

        private JobAdSearch GetJobAdSearch(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetJobAdSearchQuery(dc, id, _criteriaPersister);
        }

        private IEnumerable<JobAdSearch> GetJobAdSearches(JobAdsDataContext dc, Guid ownerId)
        {
            dc.LoadOptions = SearchLoadOptions;
            return GetJobAdSearchesQuery(dc, ownerId, _criteriaPersister);
        }

        private static void DeleteCriteria(JobAdsDataContext dc, SavedJobSearchEntity entity)
        {
            if (entity.JobSearchCriteriaSetEntity != null)
            {
                // Need to check that there are no other searches or executions that are using this criteria.

                if (!IsCriteriaOwnedByOtherSearch(dc, entity.JobSearchCriteriaSetEntity.id, entity.id) && !IsCriteriaOwnedByExecution(dc, entity.JobSearchCriteriaSetEntity.id))
                {
                    dc.JobSearchCriteriaEntities.DeleteAllOnSubmit(entity.JobSearchCriteriaSetEntity.JobSearchCriteriaEntities);
                    dc.JobSearchCriteriaSetEntities.DeleteOnSubmit(entity.JobSearchCriteriaSetEntity);
                }
            }
        }

        private JobAdSearchExecution GetJobAdSearchExecution(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = ExecutionLoadOptions;
            return GetJobAdSearchExecutionQuery(dc, id, _criteriaPersister);
        }

        private IEnumerable<JobAdSearchExecution> GetJobAdSearchExecutions(JobAdsDataContext dc, Guid ownerId, DateTimeRange range)
        {
            dc.LoadOptions = ExecutionLoadOptions;
            return GetJobAdSearchExecutionsQuery(dc, ownerId, range, _criteriaPersister);
        }

        private IEnumerable<JobAdSearchExecution> GetJobAdSearchExecutions(JobAdsDataContext dc, Guid ownerId, int maxResults)
        {
            dc.LoadOptions = ExecutionLoadOptions;
            return GetMaxJobAdSearchExecutionsQuery(dc, ownerId, maxResults, _criteriaPersister);
        }

        private static void CheckDuplicates(SqlException ex)
        {
            if (ex.Message.StartsWith("The INSERT statement conflicted with the CHECK constraint \"CK_SavedJobSearch_duplicatenames\".")
                || ex.Message.StartsWith("The UPDATE statement conflicted with the CHECK constraint \"CK_SavedJobSearch_duplicatenames\"."))
                throw new ValidationErrorsException(new DuplicateValidationError("Name"));
        }

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}