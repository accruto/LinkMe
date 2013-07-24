using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Apps.Agents.Reports.Data
{
    public class ReportsRepository
        : Repository, IReportsRepository
    {
        private static readonly DataLoadOptions ReportLoadOptions = DataOptions.CreateLoadOptions<ReportDefinitionEntity, ReportParameterSetEntity>(r => r.ReportParameterSetEntity, s => s.ReportParameterEntities);

        private static readonly Func<ReportsDataContext, Guid, ReportDefinitionEntity> GetReportDefinitionEntityQuery
            = CompiledQuery.Compile((ReportsDataContext dc, Guid id)
                => (from r in dc.ReportDefinitionEntities
                    where r.id == id
                    select r).SingleOrDefault());

        private static readonly Func<ReportsDataContext, Guid, string, ReportDefinitionEntity> GetReportDefinitionEntityByClientQuery
            = CompiledQuery.Compile((ReportsDataContext dc, Guid clientId, string type)
                => (from r in dc.ReportDefinitionEntities
                    where r.organisationId == clientId
                    && r.reportType == type
                    select r).SingleOrDefault());

        private static readonly Func<ReportsDataContext, string, DateTime, DateTime, IQueryable<ReportDefinitionEntity>> GetReportDefinitionEntitiesToRunQuery
            = CompiledQuery.Compile((ReportsDataContext dc, string type, DateTime startDate, DateTime endDate)
                => from r in dc.ReportDefinitionEntities
                   where (r.toAccountManager || r.toClient)
                   && r.reportType == type
                   && !(from s in dc.SentReportEntities where s.reportDefinitionId == r.id && s.periodStart == startDate && s.periodEnd == endDate select s).Any()
                   select r);

        public ReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IReportsRepository.CreateReport(Report report)
        {
            using (var dc = CreateContext())
            {
                dc.ReportDefinitionEntities.InsertOnSubmit(report.Map());
                dc.SubmitChanges();
            }
        }

        void IReportsRepository.UpdateReport(Report report)
        {
            using (var dc = CreateContext())
            {
                var entity = GetReportDefinitionEntity(dc, report.Id);
                if (entity != null)
                {
                    if (entity.ReportParameterSetEntity != null)
                    {
                        dc.ReportParameterEntities.DeleteAllOnSubmit(entity.ReportParameterSetEntity.ReportParameterEntities);
                        dc.ReportParameterSetEntities.DeleteOnSubmit(entity.ReportParameterSetEntity);
                    }

                    report.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        T IReportsRepository.GetReport<T>(Guid clientId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetReport<T>(dc, clientId);
            }
        }

        void IReportsRepository.CreateReportRun(ReportRun reportRun)
        {
            using (var dc = CreateContext())
            {
                dc.SentReportEntities.InsertOnSubmit(reportRun.Map());
                dc.SubmitChanges();
            }
        }

        IList<T> IReportsRepository.GetReportsToRun<T>(DateTime startDate, DateTime endDate)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetReportsToRun<T>(dc, startDate, endDate).ToList();
            }
        }

        private static ReportDefinitionEntity GetReportDefinitionEntity(ReportsDataContext dc, Guid id)
        {
            dc.LoadOptions = ReportLoadOptions;
            return GetReportDefinitionEntityQuery(dc, id);
        }

        private static T GetReport<T>(ReportsDataContext dc, Guid clientId)
            where T : Report, new()
        {
            dc.LoadOptions = ReportLoadOptions;
            return GetReportDefinitionEntityByClientQuery(dc, clientId, typeof(T).Name).MapTo<T>();
        }

        private static IQueryable<T> GetReportsToRun<T>(ReportsDataContext dc, DateTime startDate, DateTime endDate)
            where T : Report, new()
        {
            dc.LoadOptions = ReportLoadOptions;
            return from r in GetReportDefinitionEntitiesToRunQuery(dc, typeof(T).Name, startDate, endDate) select r.MapTo<T>();
        }

        private ReportsDataContext CreateContext()
        {
            return CreateContext(c => new ReportsDataContext(c));
        }
    }
}
