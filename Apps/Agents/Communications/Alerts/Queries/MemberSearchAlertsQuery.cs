using System;
using System.Collections.Generic;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.Apps.Agents.Communications.Alerts.Queries
{
    public class MemberSearchAlertsQuery
        : IMemberSearchAlertsQuery
    {
        private readonly ISearchAlertsRepository _repository;
        private readonly IMemberSearchesQuery _memberSearchesQuery;

        public MemberSearchAlertsQuery(ISearchAlertsRepository repository, IMemberSearchesQuery memberSearchesQuery)
        {
            _repository = repository;
            _memberSearchesQuery = memberSearchesQuery;
        }

        IList<MemberSearch> IMemberSearchAlertsQuery.GetMemberSearches(Guid ownerId)
        {
            return _memberSearchesQuery.GetMemberSearches(ownerId);
        }

        MemberSearch IMemberSearchAlertsQuery.GetMemberSearch(Guid ownerId, string name)
        {
            return _memberSearchesQuery.GetMemberSearch(ownerId, name);
        }

        MemberSearch IMemberSearchAlertsQuery.GetMemberSearch(Guid searchId)
        {
            return _memberSearchesQuery.GetMemberSearch(searchId);
        }

        MemberSearchAlert IMemberSearchAlertsQuery.GetMemberSearchAlert(Guid searchId, AlertType alertType)
        {
            return _repository.GetMemberSearchAlert(searchId, alertType);
        }

        MemberSearchAlert IMemberSearchAlertsQuery.GetMemberSearchAlert(Guid id)
        {
            return _repository.GetMemberSearchAlert(id);
        }

        IList<MemberSearchAlert> IMemberSearchAlertsQuery.GetMemberSearchAlerts(Guid searchId, AlertType? alertType)
        {
            return _repository.GetMemberSearchAlerts(new [] {searchId}, alertType);
        }

        IList<MemberSearchAlert> IMemberSearchAlertsQuery.GetMemberSearchAlerts(IEnumerable<Guid> searchIds, AlertType? alertType)
        {
            return _repository.GetMemberSearchAlerts(searchIds, alertType);
        }

        IList<MemberSearchAlert> IMemberSearchAlertsQuery.GetMemberSearchAlerts(AlertType alertType)
        {
            return _repository.GetMemberSearchAlerts(alertType);
        }


        int IMemberSearchAlertsQuery.GetBadgeCount(Guid ownerId)
        {
            return _repository.GetBadgeCount(ownerId);
        }

        IList<SavedResumeSearchAlertResult> IMemberSearchAlertsQuery.GetUnviewedCandidates(Guid ownerId)
        {
            return _repository.GetUnviewedCandidates(ownerId);
        }

        IList<SavedResumeSearchAlertResult> IMemberSearchAlertsQuery.GetAlertResults(Guid savedSearchAlertId)
        {
            return _repository.GetAlertResults(savedSearchAlertId);
        }

        IList<SavedResumeSearchAlertResult> IMemberSearchAlertsQuery.GetUnviewedCandidates(Guid ownerId, Guid savedSearchAlertId)
        {
            return _repository.GetUnviewedCandidates(ownerId, savedSearchAlertId);
        }

        SavedResumeSearchAlertResult IMemberSearchAlertsQuery.LastAlert(Guid memberId, Guid savedSearchAlertId)
        {
            return _repository.LastAlert(memberId, savedSearchAlertId);
        }
    }
}