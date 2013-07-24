using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Apps.Agents.Communications.Alerts.Data
{
    public class SearchAlertsRepository
        : Repository, ISearchAlertsRepository
    {
        private static readonly Func<AlertsDataContext, Guid, AlertType, SavedResumeSearchAlertEntity> GetSavedResumeSearchAlertEntity
            = CompiledQuery.Compile((AlertsDataContext dc, Guid memberSearchId, AlertType alertType)
                => (from s in dc.SavedResumeSearchEntities
                    join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                    where s.id == memberSearchId
                    && a.alertType == (byte)alertType
                    select a).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, IQueryable<SavedResumeSearchAlertEntity>> GetSavedResumeSearchAlertEntities
            = CompiledQuery.Compile((AlertsDataContext dc, Guid memberSearchId)
                => (from s in dc.SavedResumeSearchEntities
                    join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                    where s.id == memberSearchId
                    select a));

        private static readonly Func<AlertsDataContext, Guid, AlertType, MemberSearchAlert> GetMemberSearchAlertBySearchId
            = CompiledQuery.Compile((AlertsDataContext dc, Guid memberSearchId, AlertType alertType)
                => (from a in dc.SavedResumeSearchAlertEntities
                    where a.savedResumeSearchId == memberSearchId
                    && a.alertType == (byte)alertType
                    select a.Map()).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, MemberSearchAlert> GetMemberSearchAlert
            = CompiledQuery.Compile((AlertsDataContext dc, Guid id)
                => (from a in dc.SavedResumeSearchAlertEntities
                    where a.id == id
                    select a.Map()).SingleOrDefault());

        private static readonly Func<AlertsDataContext, AlertType, IQueryable<MemberSearchAlert>> GetMemberSearchAlerts
            = CompiledQuery.Compile((AlertsDataContext dc, AlertType alertType)
                => (from s in dc.SavedResumeSearchEntities
                    join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                    where a.alertType == (byte) alertType
                    select a.Map()));

        private static readonly Func<AlertsDataContext, string, IQueryable<MemberSearchAlert>> GetMemberSearchAlertsSubset
            = CompiledQuery.Compile((AlertsDataContext dc, string memberSearchIds)
                => (from s in dc.SavedResumeSearchEntities
                    join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberSearchIds) on s.id equals i.value
                    select a.Map()));

        private static readonly Func<AlertsDataContext, string, AlertType, IQueryable<MemberSearchAlert>> GetFilteredMemberSearchAlerts
            = CompiledQuery.Compile((AlertsDataContext dc, string memberSearchIds, AlertType alertType)
                => (from s in dc.SavedResumeSearchEntities
                    join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberSearchIds) on s.id equals i.value
                    where a.alertType == (byte)alertType
                    select a.Map()));

        private static readonly Func<AlertsDataContext, Guid, SavedJobSearchEntity> GetSavedJobSearchEntity
            = CompiledQuery.Compile((AlertsDataContext dc, Guid id)
                => (from s in dc.SavedJobSearchEntities
                    where s.id == id
                    select s).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, SavedJobSearchAlertEntity> GetSavedJobSearchAlertEntity
            = CompiledQuery.Compile((AlertsDataContext dc, Guid memberSearchId)
                => (from s in dc.SavedJobSearchEntities
                    join a in dc.SavedJobSearchAlertEntities on s.alertId equals a.id
                    where s.id == memberSearchId
                    select a).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, JobAdSearchAlert> GetJobAdSearchAlert
            = CompiledQuery.Compile((AlertsDataContext dc, Guid memberSearchId)
                => (from s in dc.SavedJobSearchEntities
                    join a in dc.SavedJobSearchAlertEntities on s.alertId equals a.id
                    where s.id == memberSearchId
                    select a.Map(memberSearchId)).SingleOrDefault());

        private static readonly Func<AlertsDataContext, IQueryable<JobAdSearchAlert>> GetJobAdSearchAlerts
            = CompiledQuery.Compile((AlertsDataContext dc)
                => (from s in dc.SavedJobSearchEntities
                    join a in dc.SavedJobSearchAlertEntities on s.alertId equals a.id
                    select a.Map(s.id)));

        private static readonly Func<AlertsDataContext, string, IQueryable<JobAdSearchAlert>> GetFilteredJobAdSearchAlerts
            = CompiledQuery.Compile((AlertsDataContext dc, string memberSearchIds)
                => (from s in dc.SavedJobSearchEntities
                    join a in dc.SavedJobSearchAlertEntities on s.alertId equals a.id
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberSearchIds) on s.id equals i.value
                    select a.Map(s.id)));

        private static readonly Func<AlertsDataContext, Guid, Guid?> GetDesiredJobAdSearchId
            = CompiledQuery.Compile((AlertsDataContext dc, Guid ownerId)
                => (from c in dc.CandidateEntities
                    where c.id == ownerId
                    select c.desiredJobSavedSearchId).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, SavedSearchAlertResultEntity> GetSavedSearchAlertResultEntity
            = CompiledQuery.Compile((AlertsDataContext dc, Guid savedSearchAlertResultId)
                => (from s in dc.SavedSearchAlertResultEntities
                    where s.id == savedSearchAlertResultId
                    select s).SingleOrDefault());

        private static readonly Func<AlertsDataContext, Guid, IQueryable<SavedResumeSearchAlertResult>> GetUnViewedSearchResultsByOwnerQuery
            = CompiledQuery.Compile((AlertsDataContext dc, Guid ownerId)
                => from e in dc.SavedSearchAlertResultEntities
                   join a in dc.SavedResumeSearchAlertEntities on e.savedSearchAlertId equals a.id
                   join ss in dc.SavedResumeSearchEntities on a.savedResumeSearchId equals ss.id
                   where !e.viewed
                   && ss.ownerId == ownerId
                   select e.MapTo<SavedResumeSearchAlertResult>());

        private static readonly Func<AlertsDataContext, Guid, Guid, IQueryable<SavedResumeSearchAlertResult>> GetUnViewedSearchResultsByOwnerAndAlertQuery
            = CompiledQuery.Compile((AlertsDataContext dc, Guid ownerId, Guid savedSearchAlertId)
                => from e in dc.SavedSearchAlertResultEntities
                   join a in dc.SavedResumeSearchAlertEntities on e.savedSearchAlertId equals a.id
                   join ss in dc.SavedResumeSearchEntities on a.savedResumeSearchId equals ss.id
                   where !e.viewed
                   && ss.ownerId == ownerId
                   && a.id == savedSearchAlertId
                   select e.MapTo<SavedResumeSearchAlertResult>());

        private static readonly Func<AlertsDataContext, Guid, IQueryable<SavedResumeSearchAlertResult>> GetAlertResultsQuery
            = CompiledQuery.Compile((AlertsDataContext dc, Guid savedSearchAlertId)
                => from e in dc.SavedSearchAlertResultEntities
                   where e.savedSearchAlertId == savedSearchAlertId
                   select e.MapTo<SavedResumeSearchAlertResult>());

        private static readonly Func<AlertsDataContext, Guid, Guid, SavedResumeSearchAlertResult> GetMostRecentAlertResultQuery
            = CompiledQuery.Compile((AlertsDataContext dc, Guid searchResultId, Guid savedSearchAlertId)
                => (from e in dc.SavedSearchAlertResultEntities
                   where e.savedSearchAlertId == savedSearchAlertId
                   && e.searchResultId == searchResultId
                   orderby e.createdTime descending
                   select e.MapTo<SavedResumeSearchAlertResult>()).FirstOrDefault());

        public SearchAlertsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void ISearchAlertsRepository.CreateMemberSearchAlert(MemberSearchAlert alert)
        {
            using (var dc = CreateContext())
            {
                dc.SavedResumeSearchAlertEntities.InsertOnSubmit(alert.Map());

                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.DeleteMemberSearchAlert(Guid memberSearchId, AlertType alertType)
        {
            using (var dc = CreateContext())
            {
                // Delete the entity

                var alertEntity = GetSavedResumeSearchAlertEntity(dc, memberSearchId, alertType);
                if (alertEntity != null)
                    dc.SavedResumeSearchAlertEntities.DeleteOnSubmit(alertEntity);

                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.DeleteMemberSearchAlerts(Guid memberSearchId)
        {
            using (var dc = CreateContext())
            {
                // Delete the entity

                var alertEntities = GetSavedResumeSearchAlertEntities(dc, memberSearchId);
                if (alertEntities != null)
                    dc.SavedResumeSearchAlertEntities.DeleteAllOnSubmit(alertEntities);

                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.UpdateMemberSearchLastRunTime(Guid memberSearchId, DateTime time, AlertType alertType)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedResumeSearchAlertEntity(dc, memberSearchId, alertType);
                entity.lastRunTime = time;
                dc.SubmitChanges();
            }
        }

        MemberSearchAlert ISearchAlertsRepository.GetMemberSearchAlert(Guid memberSearchId, AlertType alertType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchAlertBySearchId(dc, memberSearchId, alertType);
            }
        }

        MemberSearchAlert ISearchAlertsRepository.GetMemberSearchAlert(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchAlert(dc, id);
            }
        }

        IList<MemberSearchAlert> ISearchAlertsRepository.GetMemberSearchAlerts(AlertType alertType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberSearchAlerts(dc, alertType).ToList();
            }
        }

        IList<MemberSearchAlert> ISearchAlertsRepository.GetMemberSearchAlerts(IEnumerable<Guid> memberSearchIds, AlertType? alertType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return alertType.HasValue
                    ? GetFilteredMemberSearchAlerts(dc, new SplitList<Guid>(memberSearchIds).ToString(), alertType.Value)
                        .ToList()
                    : GetMemberSearchAlertsSubset(dc, new SplitList<Guid>(memberSearchIds).ToString()).
                        ToList();
            }
        }

        void ISearchAlertsRepository.CreateJobAdSearchAlert(JobAdSearchAlert alert)
        {
            using (var dc = CreateContext())
            {
                dc.SavedJobSearchAlertEntities.InsertOnSubmit(alert.Map());

                // Need to update the corresponding search entity.

                var entity = GetSavedJobSearchEntity(dc, alert.JobAdSearchId);
                entity.alertId = alert.Id;

                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.DeleteJobAdSearchAlert(Guid jobAdSearchId)
        {
            using (var dc = CreateContext())
            {
                // Delete the entity and update the search.

                var searchEntity = GetSavedJobSearchEntity(dc, jobAdSearchId);
                if (searchEntity != null)
                    searchEntity.alertId = null;

                var alertEntity = GetSavedJobSearchAlertEntity(dc, jobAdSearchId);
                if (alertEntity != null)
                    dc.SavedJobSearchAlertEntities.DeleteOnSubmit(alertEntity);

                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.UpdateJobAdSearchLastRunTime(Guid jobAdSearchId, DateTime time)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedJobSearchAlertEntity(dc, jobAdSearchId);
                entity.lastRunTime = time;
                dc.SubmitChanges();
            }
        }

        JobAdSearchAlert ISearchAlertsRepository.GetJobAdSearchAlert(Guid jobAdSearchId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchAlert(dc, jobAdSearchId);
            }
        }

        IList<JobAdSearchAlert> ISearchAlertsRepository.GetJobAdSearchAlerts()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdSearchAlerts(dc).ToList();
            }
        }

        IList<JobAdSearchAlert> ISearchAlertsRepository.GetJobAdSearchAlerts(IEnumerable<Guid> jobAdSearchIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredJobAdSearchAlerts(dc, new SplitList<Guid>(jobAdSearchIds).ToString()).ToList();
            }
        }

        void ISearchAlertsRepository.CreateDesiredJobAdSearchId(Guid ownerId, Guid jobAdSearchId)
        {
            using (var dc = CreateContext())
            {
                var entity = new CandidateEntity {id = ownerId, desiredJobSavedSearchId = null};
                dc.CandidateEntities.Attach(entity);
                entity.desiredJobSavedSearchId = jobAdSearchId;
                dc.SubmitChanges();
            }
        }

        void ISearchAlertsRepository.DeleteDesiredJobAdSearchId(Guid ownerId)
        {
            using (var dc = CreateContext())
            {
                // Set it to something so that it gets updated.

                var entity = new CandidateEntity { id = ownerId, desiredJobSavedSearchId = Guid.NewGuid() };
                dc.CandidateEntities.Attach(entity);
                entity.desiredJobSavedSearchId = null;
                dc.SubmitChanges();
            }
        }

        Guid? ISearchAlertsRepository.GetDesiredJobAdSearchId(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDesiredJobAdSearchId(dc, ownerId);
            }
        }

        int ISearchAlertsRepository.GetBadgeCount(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUnViewedSearchResultsByOwnerQuery(dc, ownerId).ToList().Select(r => r.CandidateId).Distinct().Count();
            }
        }

        void ISearchAlertsRepository.AddResults(IList<SavedResumeSearchAlertResult> results)
        {
            using (var dc = CreateContext())
            {
                dc.SavedSearchAlertResultEntities.InsertAllOnSubmit(from r in results select r.Map());

                dc.SubmitChanges();
            }
        }

        IList<SavedResumeSearchAlertResult> ISearchAlertsRepository.GetUnviewedCandidates(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUnViewedSearchResultsByOwnerQuery(dc, ownerId).ToList();
            }
        }

        IList<SavedResumeSearchAlertResult> ISearchAlertsRepository.GetUnviewedCandidates(Guid ownerId, Guid savedSearchAlertId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUnViewedSearchResultsByOwnerAndAlertQuery(dc, ownerId, savedSearchAlertId).ToList();
            }
        }

        IList<SavedResumeSearchAlertResult> ISearchAlertsRepository.GetAlertResults(Guid savedSearchAlertId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAlertResultsQuery(dc, savedSearchAlertId).ToList();
            }
        }

        SavedResumeSearchAlertResult ISearchAlertsRepository.LastAlert(Guid memberId, Guid savedSearchAlertId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMostRecentAlertResultQuery(dc, memberId, savedSearchAlertId);
            }
        }

        void ISearchAlertsRepository.MarkAsViewed(Guid savedResumeSearchAlertId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetSavedSearchAlertResultEntity(dc, savedResumeSearchAlertId);
                entity.viewed = true;
                dc.SubmitChanges();
            }
        }

        private AlertsDataContext CreateContext()
        {
            return CreateContext(c => new AlertsDataContext(c));
        }
    }
}
