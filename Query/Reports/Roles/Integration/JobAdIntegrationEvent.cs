using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Query.Reports.Roles.Integration
{
    public abstract class JobAdIntegrationEvent
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }

        public bool Success { get; set; }
        public Guid IntegratorUserId { get; set; }
        public int JobAds { get; set; }
    }

    public class JobAdExportFeedEvent
        : JobAdIntegrationEvent
    {
    }

    public class JobAdExportFeedIdEvent
        : JobAdIntegrationEvent
    {
    }

    public class JobAdImportPostEvent
        : JobAdIntegrationEvent
    {
        public Guid PosterId { get; set; }
        public int Failed { get; set; }
        public int Posted { get; set; }
        public int Closed { get; set; }
        public int Updated { get; set; }
        public int Duplicates { get; set; }
        public int Ignored { get; set; }
    }

    public class JobAdImportCloseEvent
        : JobAdIntegrationEvent
    {
        public int Failed { get; set; }
        public int Closed { get; set; }
        public int NotFound { get; set; }
    }

    public class JobAdExportPostEvent
        : JobAdIntegrationEvent
    {
        public int Failed { get; set; }
        public int Posted { get; set; }
        public int Updated { get; set; }
    }

    public class JobAdExportCloseEvent
        : JobAdIntegrationEvent
    {
        public int Failed { get; set; }
        public int Closed { get; set; }
        public int NotFound { get; set; }
    }
}
