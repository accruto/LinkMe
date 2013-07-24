using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public interface ISavedResumeSearchAlertResult
    {
        Guid Id { get; set; }
        Guid SavedResumeSearchAlertId { get; set; }
        Guid CandidateId { get; set; }
        bool Viewed { get; set; }
        DateTime CreatedTime { get; set; }
    }
}
