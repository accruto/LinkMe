using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Criterias.Data;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds.Data
{
    internal partial class JobSearchCriteriaEntity
        : ICriteriaEntity
    {
        public Guid id
        {
            get { return setId; }
            set { setId = value; }
        }

        object ICriteriaEntity.value
        {
            get { return value; }
            set { this.value = value == null ? null : value.ToString(); }
        }
    }

    internal partial class JobSearchCriteriaSetEntity
        : ICriteriaSetEntity<JobSearchCriteriaEntity>
    {
        public EntitySet<JobSearchCriteriaEntity> Entities
        {
            get { return JobSearchCriteriaEntities; }
            set { JobSearchCriteriaEntities = value; }
        }
    }

    internal static class Mappings
    {
        public static SavedJobSearchEntity Map(this JobAdSearch search, ICriteriaPersister criteriaPersister)
        {
            var entity = new SavedJobSearchEntity { id = search.Id };
            search.MapTo(entity, criteriaPersister);
            return entity;
        }

        public static void MapTo(this JobAdSearch search, SavedJobSearchEntity entity, ICriteriaPersister criteriaPersister)
        {
            entity.ownerId = search.OwnerId;
            entity.displayText = search.Name;
            entity.JobSearchCriteriaSetEntity = search.Criteria.MapTo<JobSearchCriteriaSetEntity, JobSearchCriteriaEntity, JobAdSearchCriteria>(criteriaPersister, true);
        }

        public static JobAdSearch Map(this SavedJobSearchEntity search, ICriteriaPersister criteriaPersister)
        {
            // At one stage if no name was supplied then a HTML snippet was generated based off the criteria and
            // stored in the "displayText" field.  Not ideal.  The generated HTML snippet is now generated
            // as needed in the UI.
            // Validate what is stored in the database as it comes out, and if it doesn't conform to what is currently valid
            // for a name then assume it is one of these snippets and simply don't return it.

            var validator = (IValidator)new RegexValidator(RegularExpressions.CompleteJobAdSearchName, Constants.JobAdSearchNameMinLength, Constants.JobAdSearchNameMaxLength);
            var name = validator.IsValid(search.displayText) ? search.displayText : null;

            return new JobAdSearch
            {
                Id = search.id,
                OwnerId = search.ownerId,
                Name = name,
                Criteria = search.JobSearchCriteriaSetEntity.MapTo<JobSearchCriteriaSetEntity, JobSearchCriteriaEntity, JobAdSearchCriteria>(criteriaPersister, true)
            };
        }

        public static JobSearchEntity Map(this JobAdSearchExecution execution, int maxResults, ICriteriaPersister criteriaPersister)
        {
            return new JobSearchEntity
            {
                id = execution.Id,
                context = execution.Context,
                startTime = execution.StartTime == null ? DateTime.Now : execution.StartTime.Value,
                duration = execution.Duration == null ? 0 : execution.Duration.Value.Ticks,
                searcherId = execution.SearcherId,
                searcherIp = execution.SearcherIp,
                savedSearchId = execution.SearchId,
                channelId = execution.ChannelId,
                appId = execution.AppId,
                JobSearchCriteriaSetEntity = execution.Criteria.MapTo<JobSearchCriteriaSetEntity, JobSearchCriteriaEntity, JobAdSearchCriteria>(criteriaPersister, true),
                JobSearchResultSetEntity = execution.Results.Map(maxResults),
            };
        }

        public static JobAdSearchExecution Map(this JobSearchEntity entity, ICriteriaPersister criteriaPersister)
        {
            return new JobAdSearchExecution
            {
                Id = entity.id,
                Context = entity.context,
                StartTime = entity.startTime,
                Duration = new TimeSpan(entity.duration),
                SearcherId = entity.searcherId,
                SearcherIp = entity.searcherIp,
                SearchId = entity.savedSearchId,
                ChannelId = entity.channelId,
                AppId = entity.appId,
                Criteria = entity.JobSearchCriteriaSetEntity.MapTo<JobSearchCriteriaSetEntity, JobSearchCriteriaEntity, JobAdSearchCriteria>(criteriaPersister, true),
                Results = entity.JobSearchResultSetEntity.Map(),
            };
        }

        private static JobSearchResultSetEntity Map(this JobAdSearchResults results, int maxCount)
        {
            var entity = new JobSearchResultSetEntity
            {
                id = results.Id,
                count = results.TotalMatches,
                JobSearchResultEntities = new EntitySet<JobSearchResultEntity>()
            };

            entity.JobSearchResultEntities.AddRange(
                from r in Enumerable.Range(1, Math.Min(results.JobAdIds.Count, maxCount))
                select new JobSearchResultEntity
                {
                    rank = (short)r,
                    jobAdId = results.JobAdIds[r - 1]
                });

            return entity;
        }

        private static JobAdSearchResults Map(this JobSearchResultSetEntity entity)
        {
            return new JobAdSearchResults
            {
                Id = entity.id,
                TotalMatches = entity.count,
                JobAdIds = (from e in entity.JobSearchResultEntities orderby e.rank select e.jobAdId).Take(entity.count).ToList(),
            };
        }
    }
}
