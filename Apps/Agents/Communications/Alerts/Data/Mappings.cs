using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Apps.Agents.Communications.Alerts.Data
{
    internal static class Mappings
    {
        public static SavedResumeSearchAlertEntity Map(this MemberSearchAlert alert)
        {
            return new SavedResumeSearchAlertEntity
            {
                id = alert.Id,
                savedResumeSearchId = alert.MemberSearchId,
                lastRunTime = alert.LastRunTime,
                sendUpdatedResults = true,
                alertType = (byte) alert.AlertType,
            };
        }

        public static MemberSearchAlert Map(this SavedResumeSearchAlertEntity entity)
        {
            return new MemberSearchAlert
            {
                Id = entity.id,
                LastRunTime = entity.lastRunTime,
                MemberSearchId = entity.savedResumeSearchId.Value,
                AlertType = (AlertType)entity.alertType,
            };
        }

        public static SavedJobSearchAlertEntity Map(this JobAdSearchAlert alert)
        {
            return new SavedJobSearchAlertEntity
            {
                id = alert.Id,
                lastRunTime = alert.LastRunTime,
            };
        }

        public static JobAdSearchAlert Map(this SavedJobSearchAlertEntity entity, Guid jobAdSearchId)
        {
            return new JobAdSearchAlert
            {
                Id = entity.id,
                LastRunTime = entity.lastRunTime,
                JobAdSearchId = jobAdSearchId,
            };
        }

        internal static IList<T> Map<T>(this IEnumerable<SavedSearchAlertResultEntity> entities)
    where T : SavedSearchAlertResult, new()
        {
            return (from e in entities
                    select e.MapTo<T>()).ToList();
        }

        internal static T MapTo<T>(this SavedSearchAlertResultEntity entity)
            where T : SavedSearchAlertResult, new()
        {
            return new T
            {
                Id = entity.id,
                SavedSearchAlertId = entity.savedSearchAlertId,
                SearchResultId = entity.searchResultId,
                Viewed = entity.viewed,
                CreatedTime = entity.createdTime,
            };
        }

        internal static SavedSearchAlertResultEntity Map(this SavedSearchAlertResult result)
        {
            var entity = new SavedSearchAlertResultEntity();
            result.Map(entity);

            return entity;
        }

        internal static void Map(this SavedSearchAlertResult result, SavedSearchAlertResultEntity entity)
        {
            entity.id = result.Id;
            entity.savedSearchAlertId = result.SavedSearchAlertId;
            entity.searchResultId = result.SearchResultId;
            entity.viewed = result.Viewed;
            entity.createdTime = result.CreatedTime;
        }
    }
}
