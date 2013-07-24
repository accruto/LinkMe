using System;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public class SavedResumeSearchAlertResult
        : SavedSearchAlertResult, ISavedResumeSearchAlertResult
    {
        public Guid SavedResumeSearchAlertId
        {
            get { return SavedSearchAlertId; }
            set { SavedSearchAlertId = value; }
        }

        public Guid CandidateId
        {
            get { return SearchResultId; }
            set { SearchResultId = value; }
        }
    }
}
