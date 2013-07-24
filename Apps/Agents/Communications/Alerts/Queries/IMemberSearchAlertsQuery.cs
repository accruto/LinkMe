using System;
using System.Collections.Generic;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Agents.Communications.Alerts.Queries
{
    public interface IMemberSearchAlertsQuery
    {
        IList<MemberSearch> GetMemberSearches(Guid ownerId);
        MemberSearch GetMemberSearch(Guid ownerId, string name);
        MemberSearch GetMemberSearch(Guid memberSearchId);
        MemberSearchAlert GetMemberSearchAlert(Guid memberSearchId, AlertType alertType);
        MemberSearchAlert GetMemberSearchAlert(Guid id);
        IList<MemberSearchAlert> GetMemberSearchAlerts(IEnumerable<Guid> memberSearchIds, AlertType? alertType);
        IList<MemberSearchAlert> GetMemberSearchAlerts(Guid memberSearchId, AlertType? alertType);
        IList<MemberSearchAlert> GetMemberSearchAlerts(AlertType alertType);

        int GetBadgeCount(Guid ownerId);
        IList<SavedResumeSearchAlertResult> GetUnviewedCandidates(Guid ownerId);
        IList<SavedResumeSearchAlertResult> GetUnviewedCandidates(Guid ownerId, Guid savedSearchAlertId);
        SavedResumeSearchAlertResult LastAlert(Guid memberId, Guid savedSearchAlertId);
        IList<SavedResumeSearchAlertResult> GetAlertResults(Guid savedSearchAlertId);
    }
}