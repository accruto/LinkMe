using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Agents.Communications.Alerts.Commands
{
    public interface IMemberSearchAlertsCommand
    {
        void CreateMemberSearch(IUser owner, MemberSearch search);
        void CreateMemberSearchAlert(IUser owner, MemberSearch search, params AlertType[] alertTypes);

        void UpdateMemberSearch(IUser owner, MemberSearch search);
        void UpdateMemberSearch(IUser owner, MemberSearch search, params Tuple<AlertType, bool>[] alertMethods);
        void DeleteMemberSearch(IUser owner, MemberSearch search);
        void DeleteMemberSearchAlert(IUser owner, MemberSearch search, AlertType alertType);
        void DeleteMemberSearchAlerts(IUser owner, MemberSearch search);

        void UpdateLastRunTime(Guid searchId, DateTime time, AlertType alertType);

        void AddResults(Guid employerId, IList<SavedResumeSearchAlertResult> results);
        void MarkAsViewed(Guid employerId, Guid candidateId);
    }
}