using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Integration.Data
{
    public class IntegrationReportsRepository
        : ReportsRepository<IntegrationDataContext>, IIntegrationReportsRepository
    {
        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdExportFeedReport>>> GetJobAdExportFeedReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdExportFeedEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdExportFeedReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                   }));

        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdExportFeedIdReport>>> GetJobAdExportFeedIdReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdExportFeedIdEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdExportFeedIdReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                   }));

        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdExportPostReport>>> GetJobAdExportPostReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdExportPostEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdExportPostReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                       Failed = a.Sum(x => x.JobAdExportPostEventEntity.failed),
                       Posted = a.Sum(x => x.JobAdExportPostEventEntity.posted),
                       Updated = a.Sum(x => x.JobAdExportPostEventEntity.updated),
                   }));

        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdExportCloseReport>>> GetJobAdExportCloseReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdExportCloseEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdExportCloseReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                       Closed = a.Sum(x => x.JobAdExportCloseEventEntity.closed),
                       Failed = a.Sum(x => x.JobAdExportCloseEventEntity.failed),
                       NotFound = a.Sum(x => x.JobAdExportCloseEventEntity.notFound),
                   }));

        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdImportPostReport>>> GetJobAdImportPostReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdImportPostEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdImportPostReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                       Closed = a.Sum(x => x.JobAdImportPostEventEntity.closed),
                       Duplicates = a.Sum(x => x.JobAdImportPostEventEntity.duplicates),
                       Failed = a.Sum(x => x.JobAdImportPostEventEntity.failed),
                       Ignored = a.Sum(x => x.JobAdImportPostEventEntity.ignored),
                       Posted = a.Sum(x => x.JobAdImportPostEventEntity.posted),
                       Updated = a.Sum(x => x.JobAdImportPostEventEntity.updated),
                   }));

        private static readonly Func<IntegrationDataContext, DateTimeRange, IQueryable<Tuple<Guid, JobAdImportCloseReport>>> GetJobAdImportCloseReports
            = CompiledQuery.Compile((IntegrationDataContext dc, DateTimeRange timeRange)
                => from e in dc.JobAdIntegrationEventEntities
                   join o in dc.JobAdImportCloseEventEntities on e.id equals o.id
                   where e.time >= timeRange.Start.Value && e.time < timeRange.End.Value
                   group e by e.integratorUserId into a
                   select Tuple.Create(a.Key, new JobAdImportCloseReport
                   {
                       Events = a.Count(),
                       Successes = a.Count(x => x.success),
                       JobAds = a.Sum(x => x.jobAds),
                       Closed = a.Sum(x => x.JobAdImportCloseEventEntity.closed),
                       Failed = a.Sum(x => x.JobAdImportCloseEventEntity.failed),
                       NotFound = a.Sum(x => x.JobAdImportCloseEventEntity.notFound),
                   }));

        public IntegrationReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IIntegrationReportsRepository.CreateJobAdIntegrationEvent(JobAdIntegrationEvent evt)
        {
            using (var dc = CreateDataContext(false))
            {
                if (evt is JobAdImportCloseEvent)
                    dc.JobAdImportCloseEventEntities.InsertOnSubmit(((JobAdImportCloseEvent)evt).Map());
                else if (evt is JobAdImportPostEvent)
                    dc.JobAdImportPostEventEntities.InsertOnSubmit(((JobAdImportPostEvent)evt).Map());
                if (evt is JobAdExportCloseEvent)
                    dc.JobAdExportCloseEventEntities.InsertOnSubmit(((JobAdExportCloseEvent)evt).Map());
                else if (evt is JobAdExportPostEvent)
                    dc.JobAdExportPostEventEntities.InsertOnSubmit(((JobAdExportPostEvent)evt).Map());
                else if (evt is JobAdExportFeedEvent)
                    dc.JobAdExportFeedEventEntities.InsertOnSubmit(((JobAdExportFeedEvent)evt).Map());
                else if (evt is JobAdExportFeedIdEvent)
                    dc.JobAdExportFeedIdEventEntities.InsertOnSubmit(((JobAdExportFeedIdEvent)evt).Map());
                dc.SubmitChanges();
            }
        }

        IDictionary<Guid, JobAdIntegrationReport> IIntegrationReportsRepository.GetJobAdIntegrationReports(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                var exportFeedReports = GetJobAdExportFeedReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);
                var exportFeedIdReports = GetJobAdExportFeedIdReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);
                var exportPostReports = GetJobAdExportPostReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);
                var exportCloseReports = GetJobAdExportCloseReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);
                var importPostReports = GetJobAdImportPostReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);
                var importCloseReports = GetJobAdImportCloseReports(dc, timeRange).ToDictionary(x => x.Item1, x => x.Item2);

                var integratorUserIds = exportFeedReports.Keys
                    .Concat(exportFeedIdReports.Keys)
                    .Concat(exportPostReports.Keys)
                    .Concat(exportCloseReports.Keys)
                    .Concat(importPostReports.Keys)
                    .Concat(importCloseReports.Keys).Distinct();

                return (from id in integratorUserIds
                        select new
                        {
                            id,
                            Report = new JobAdIntegrationReport
                            {
                                ExportFeedReport = GetReport(id, exportFeedReports),
                                ExportFeedIdReport = GetReport(id, exportFeedIdReports),
                                ExportPostReport = GetReport(id, exportPostReports),
                                ExportCloseReport = GetReport(id, exportCloseReports),
                                ImportPostReport = GetReport(id, importPostReports),
                                ImportCloseReport = GetReport(id, importCloseReports),
                            }
                        }).ToDictionary(x => x.id, x => x.Report);
            }
        }

        private static TReport GetReport<TReport>(Guid id, IDictionary<Guid, TReport> reports)
            where TReport : new()
        {
            return reports.ContainsKey(id)
                ? reports[id]
                : new TReport();
        }

        protected override IntegrationDataContext CreateDataContext(IDbConnection connection)
        {
            return new IntegrationDataContext(connection);
        }
    }
}
