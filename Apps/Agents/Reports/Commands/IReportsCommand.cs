namespace LinkMe.Apps.Agents.Reports.Commands
{
    public interface IReportsCommand
    {
        void CreateReport(Report report);
        void UpdateReport(Report report);
        void CreateReportRun(ReportRun reportRun);
    }
}