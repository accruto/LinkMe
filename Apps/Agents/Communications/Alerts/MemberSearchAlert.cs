using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public class MemberSearchAlert
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid MemberSearchId { get; set; }
        [DefaultNow]
        public DateTime LastRunTime { get; set; }
        public AlertType AlertType { get; set; }
    }
}
