using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace LinkMe.Apps.Agents.Reports.Data
{
    internal static class Mappings
    {
        public static ReportDefinitionEntity Map(this Report report)
        {
            var entity = new ReportDefinitionEntity
            {
                id = report.Id,
                organisationId = report.ClientId,
                reportType = report.Type,
            };
            report.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Report report, ReportDefinitionEntity entity)
        {
            entity.toAccountManager = report.SendToAccountManager;
            entity.toClient = report.SendToClient;
            entity.ReportParameterSetEntity = report.Parameters == null || report.Parameters.Count == 0
                ? null
                : report.Parameters.Map();
        }

        private static ReportParameterSetEntity Map(this IEnumerable<ReportParameter> parameters)
        {
            var entities = new EntitySet<ReportParameterEntity>();
            entities.AddRange(from p in parameters select p.Map());

            return new ReportParameterSetEntity
            {
                id = Guid.NewGuid(),
                ReportParameterEntities = entities,
            };
        }

        private static ReportParameterEntity Map(this ReportParameter parameter)
        {
            return new ReportParameterEntity
            {
                id = Guid.NewGuid(),
                type = parameter.Name,
                value = Convert.ToString(parameter.Value),
            };
        }

        public static T MapTo<T>(this ReportDefinitionEntity entity)
            where T : Report, new()
        {
            if (entity == null)
                return null;

            var t = new T
            {
                Id = entity.id,
                ClientId = entity.organisationId,
                SendToAccountManager = entity.toAccountManager,
                SendToClient = entity.toClient,
            };

            t.SetParameters(entity.ReportParameterSetEntity == null ? new List<ReportParameter>() : entity.ReportParameterSetEntity.Map());
            return t;
        }

        private static IList<ReportParameter> Map(this ReportParameterSetEntity entity)
        {
            var list = new List<ReportParameter>();
            list.AddRange(from e in entity.ReportParameterEntities select e.Map());
            return list;
        }

        private static ReportParameter Map(this ReportParameterEntity entity)
        {
            return new ReportParameter
            {
                Name = entity.type,
                Value = entity.value,
            };
        }

        public static SentReportEntity Map(this ReportRun reportRun)
        {
            return new SentReportEntity
            {
                id = reportRun.Id,
                reportDefinitionId = reportRun.ReportId,
                sentTime = reportRun.SentTime,
                periodStart = reportRun.PeriodStart,
                periodEnd = reportRun.PeriodEnd,
                sentToAccountManagerId = reportRun.SentToAccountManagerId,
                sentToClientEmail = reportRun.SentToClientEmail,
            };
        }
    }
}
