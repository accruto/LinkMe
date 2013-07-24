using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public interface ISearchAlertsRepository
    {
        void CreateMemberSearchAlert(MemberSearchAlert alert);
        void DeleteMemberSearchAlert(Guid memberSearchId, AlertType alertType);
        void DeleteMemberSearchAlerts(Guid memberSearchId);
        void UpdateMemberSearchLastRunTime(Guid memberSearchId, DateTime time, AlertType alertType);
        MemberSearchAlert GetMemberSearchAlert(Guid memberSearchId, AlertType alertType);
        MemberSearchAlert GetMemberSearchAlert(Guid id);
        IList<MemberSearchAlert> GetMemberSearchAlerts(AlertType alertType);
        IList<MemberSearchAlert> GetMemberSearchAlerts(IEnumerable<Guid> memberSearchIds, AlertType? alertType);

        void CreateJobAdSearchAlert(JobAdSearchAlert alert);
        void DeleteJobAdSearchAlert(Guid jobAdSearchId);
        void UpdateJobAdSearchLastRunTime(Guid jobAdSearchId, DateTime time);
        JobAdSearchAlert GetJobAdSearchAlert(Guid jobAdSearchId);
        IList<JobAdSearchAlert> GetJobAdSearchAlerts();
        IList<JobAdSearchAlert> GetJobAdSearchAlerts(IEnumerable<Guid> jobAdSearchIds);

        void CreateDesiredJobAdSearchId(Guid ownerId, Guid jobAdSearchId);
        void DeleteDesiredJobAdSearchId(Guid ownerId);
        Guid? GetDesiredJobAdSearchId(Guid ownerId);

        int GetBadgeCount(Guid ownerId);
        void AddResults(IList<SavedResumeSearchAlertResult> results);
        IList<SavedResumeSearchAlertResult> GetUnviewedCandidates(Guid ownerId);
        IList<SavedResumeSearchAlertResult> GetUnviewedCandidates(Guid ownerId, Guid savedSearchAlertId);
        SavedResumeSearchAlertResult LastAlert(Guid memberId, Guid savedSearchAlertId);
        IList<SavedResumeSearchAlertResult> GetAlertResults(Guid savedSearchAlertId);

        void MarkAsViewed(Guid savedResumeSearchAlertId);
    }
}
