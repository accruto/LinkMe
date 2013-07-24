using System;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    public interface IEmployerReportsCommand
    {
        T CreateReportTemplate<T>(Guid clientId) where T : EmployerReport;
        EmployerReport CreateReportTemplate(Guid clientId, string type);

        void CreateReport(EmployerReport report);
        void UpdateReport(EmployerReport report);

        void CreateReportRun(ReportRun reportRun);
    }
}