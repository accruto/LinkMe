using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Criterias.Data;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Users.Members.Data;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Data
{
    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal partial class AddressEntity
        : IAddressEntity<LocationReferenceEntity>
    {
    }

    internal partial class MemberEntity
        : IMemberEntity<AddressEntity, LocationReferenceEntity>
    {
        DateTime? IHavePartialDateEntity.date
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        byte? IHavePartialDateEntity.dateParts
        {
            get { return dateOfBirthParts; }
            set { dateOfBirthParts = value; }
        }
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity
    {
    }

    internal partial class CommunityMemberEntity
        : ICommunityMemberEntity
    {
    }

    internal partial class ResumeSearchCriteriaEntity
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

    internal partial class ResumeSearchCriteriaSetEntity
        : ICriteriaSetEntity<ResumeSearchCriteriaEntity>
    {
        public EntitySet<ResumeSearchCriteriaEntity> Entities
        {
            get { return ResumeSearchCriteriaEntities; }
            set { ResumeSearchCriteriaEntities = value; }
        }
    }

    internal static class Mappings
    {
        public static SavedResumeSearchEntity Map(this MemberSearch search, ICriteriaPersister criteriaPersister)
        {
            var entity = new SavedResumeSearchEntity {id = search.Id};
            search.MapTo(entity, criteriaPersister);
            return entity;
        }

        public static void MapTo(this MemberSearch search, SavedResumeSearchEntity entity, ICriteriaPersister criteriaPersister)
        {
            entity.createdTime = search.CreatedTime;
            entity.name = search.Name;
            entity.ownerId = search.OwnerId;
            entity.ResumeSearchCriteriaSetEntity = search.Criteria.MapTo<ResumeSearchCriteriaSetEntity, ResumeSearchCriteriaEntity, MemberSearchCriteria>(criteriaPersister, true);
        }

        public static MemberSearch Map(this SavedResumeSearchEntity search, ICriteriaPersister criteriaPersister)
        {
            var memberSearch = new MemberSearch
            {
                Id = search.id,
                CreatedTime = search.createdTime,
                Name = search.name,
                OwnerId = search.ownerId,
                Criteria = search.ResumeSearchCriteriaSetEntity.MapTo<ResumeSearchCriteriaSetEntity, ResumeSearchCriteriaEntity, MemberSearchCriteria>(criteriaPersister, true)
            };

            // For some reason some database entries have a value of 2.

            var criteria = memberSearch.Criteria;
            if ((int)criteria.JobTitlesToSearch == 2)
                criteria.JobTitlesToSearch = JobsToSearch.RecentJobs;

            return memberSearch;
        }

        public static ResumeSearchEntity Map(this MemberSearchExecution execution, int maxResults, ICriteriaPersister criteriaPersister)
        {
            return new ResumeSearchEntity
            {
                id = execution.Id,
                context = execution.Context,
                startTime = execution.StartTime == null ? DateTime.Now : execution.StartTime.Value,
                duration = execution.Duration == null ? 0 : execution.Duration.Value.Ticks,
                searcherId = execution.SearcherId,
                savedSearchId = execution.SearchId,
                searcherIp = execution.SearcherIp,
                channelId = execution.ChannelId,
                appId = execution.AppId,
                ResumeSearchCriteriaSetEntity = execution.Criteria.MapTo<ResumeSearchCriteriaSetEntity, ResumeSearchCriteriaEntity, MemberSearchCriteria>(criteriaPersister, true),
                ResumeSearchResultSetEntity = execution.Results.Map(maxResults),
            };
        }

        private static ResumeSearchResultSetEntity Map(this MemberSearchResults results, int maxCount)
        {
            var entity = new ResumeSearchResultSetEntity
            {
                id = results.Id,
                count = results.TotalMatches,
                ResumeSearchResultEntities = new EntitySet<ResumeSearchResultEntity>()
            };

            entity.ResumeSearchResultEntities.AddRange(
                from r in Enumerable.Range(1, Math.Min(results.MemberIds.Count, maxCount))
                select new ResumeSearchResultEntity
                {
                    rank = (short) r,
                    resumeId = results.MemberIds[r - 1]
                });

            return entity;
        }

        public static MemberSearchExecution Map(this ResumeSearchEntity entity, ICriteriaPersister criteriaPersister)
        {
            return new MemberSearchExecution
            {
                Id = entity.id,
                Context = entity.context,
                StartTime = entity.startTime,
                Duration = entity.duration == null ? (TimeSpan?)null : new TimeSpan(entity.duration.Value),
                SearcherId = entity.searcherId,
                SearchId = entity.savedSearchId,
                Criteria = entity.ResumeSearchCriteriaSetEntity.MapTo<ResumeSearchCriteriaSetEntity, ResumeSearchCriteriaEntity, MemberSearchCriteria>(criteriaPersister, true),
                Results = entity.ResumeSearchResultSetEntity.Map(),
            };
        }

        private static MemberSearchResults Map(this ResumeSearchResultSetEntity entity)
        {
            return new MemberSearchResults
            {
                Id = entity.id,
                TotalMatches = entity.count,
                MemberIds = (from e in entity.ResumeSearchResultEntities orderby e.rank select e.resumeId).Take(entity.count).ToList(),
            };
        }
    }
}
