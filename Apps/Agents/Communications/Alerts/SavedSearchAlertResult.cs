using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public abstract class SavedSearchAlertResult
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        internal Guid SavedSearchAlertId { get; set; }
        internal Guid SearchResultId { get; set; }
        public bool Viewed { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}
