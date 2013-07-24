namespace LinkMe.Query.Reports.Roles.Integration.Data
{
    internal static class Mappings
    {
        public static JobAdImportCloseEventEntity Map(this JobAdImportCloseEvent evt)
        {
            return new JobAdImportCloseEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent) evt).Map(),
                failed = evt.Failed,
                closed = evt.Closed,
                notFound = evt.NotFound,
            };
        }

        public static JobAdImportPostEventEntity Map(this JobAdImportPostEvent evt)
        {
            return new JobAdImportPostEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent)evt).Map(),
                posterId = evt.PosterId,
                failed = evt.Failed,
                posted = evt.Posted,
                closed = evt.Closed,
                updated = evt.Updated,
                duplicates = evt.Duplicates,
                ignored = evt.Ignored,
            };
        }

        public static JobAdExportCloseEventEntity Map(this JobAdExportCloseEvent evt)
        {
            return new JobAdExportCloseEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent)evt).Map(),
                failed = evt.Failed,
                closed = evt.Closed,
                notFound = evt.NotFound,
            };
        }

        public static JobAdExportPostEventEntity Map(this JobAdExportPostEvent evt)
        {
            return new JobAdExportPostEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent)evt).Map(),
                failed = evt.Failed,
                posted = evt.Posted,
                updated = evt.Updated,
            };
        }

        public static JobAdExportFeedEventEntity Map(this JobAdExportFeedEvent evt)
        {
            return new JobAdExportFeedEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent)evt).Map(),
            };
        }

        public static JobAdExportFeedIdEventEntity Map(this JobAdExportFeedIdEvent evt)
        {
            return new JobAdExportFeedIdEventEntity
            {
                id = evt.Id,
                JobAdIntegrationEventEntity = ((JobAdIntegrationEvent)evt).Map(),
            };
        }

        private static JobAdIntegrationEventEntity Map(this JobAdIntegrationEvent evt)
        {
            return new JobAdIntegrationEventEntity
            {
                id = evt.Id,
                time = evt.Time,
                integratorUserId = evt.IntegratorUserId,
                success = evt.Success,
                jobAds = evt.JobAds,
            };
        }
    }
}
