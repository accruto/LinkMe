using System;
using System.IO;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.Roles.JobAds.Queries;

namespace LinkMe.TaskRunner.Tasks
{
    public class ExternalJobRedirectCountTask
        : Task
    {
        private static readonly EventSource<ExternalJobRedirectCountTask> EventSource = new EventSource<ExternalJobRedirectCountTask>();

        private readonly IIntegrationQuery _integrationQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;

        public ExternalJobRedirectCountTask(IJobAdReportsQuery jobAdReportsQuery, IIntegrationQuery integrationQuery)
            : base(EventSource)
        {
            _jobAdReportsQuery = jobAdReportsQuery;
            _integrationQuery = integrationQuery;
        }

        public override void ExecuteTask(string[] args)
        {
            var startTime = DateTime.Parse(args[0]);
            var endTime = DateTime.Parse(args[1]);
            var integratorName = args[2];
            if (string.IsNullOrEmpty(integratorName))
                throw new ArgumentException("Integrator name required.", "integratorName");

            var startDate = startTime.Date;
            var endDate = endTime.Date;
            var integratorUser = _integrationQuery.GetIntegratorUser(integratorName);

            var filePath = Path.Combine(Path.GetTempPath(), integratorName + ".csv");
            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine("Date, Count");

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var count = _jobAdReportsQuery.GetExternalApplications(integratorUser.Id, new DateTimeRange(date, date.AddDays(1)));
                    writer.WriteLine("\"{0}\", {1}", date.ToShortDateString(), count);
                }
            }
        }
    }
}
