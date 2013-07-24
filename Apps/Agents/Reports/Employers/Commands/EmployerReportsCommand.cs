using System;
using LinkMe.Apps.Agents.Reports.Commands;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    public class EmployerReportsCommand
        : IEmployerReportsCommand
    {
        private readonly IReportsCommand _reportsCommand;

        public EmployerReportsCommand(IReportsCommand reportsCommand)
        {
            _reportsCommand = reportsCommand;
        }

        T IEmployerReportsCommand.CreateReportTemplate<T>(Guid clientId)
        {
            return CreateReportTemplate(clientId, typeof(T).Name) as T;
        }

        EmployerReport IEmployerReportsCommand.CreateReportTemplate(Guid clientId, string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("No report type was specified.", "type");

            return CreateReportTemplate(clientId, type);
        }

        void IEmployerReportsCommand.CreateReport(EmployerReport report)
        {
            _reportsCommand.CreateReport(report);
        }

        void IEmployerReportsCommand.UpdateReport(EmployerReport report)
        {
            _reportsCommand.UpdateReport(report);
        }

        void IEmployerReportsCommand.CreateReportRun(ReportRun reportRun)
        {
            _reportsCommand.CreateReportRun(reportRun);
        }

        private static EmployerReport CreateReportTemplate(Guid clientId, string type)
        {
            switch (type.ToLower())
            {
                case "jobboardactivityreport":
                    return new JobBoardActivityReport {ClientId = clientId};

                case "candidatecarereport":
                    return new CandidateCareReport {ClientId = clientId};

                case "resumesearchactivityreport":
                    return new ResumeSearchActivityReport {ClientId = clientId};
            }

            throw new ApplicationException("The report type, '" + type + "', cannot be found.");
        }
    }
}