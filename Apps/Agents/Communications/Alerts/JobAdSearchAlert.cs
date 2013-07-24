using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Communications.Alerts
{
    public class JobAdSearchAlert
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid JobAdSearchId { get; set; }
        [DefaultNow]
        public DateTime LastRunTime { get; set; }
    }
}