using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Reports.Commands
{
    public class ReportsCommand
        : IReportsCommand
    {
        private readonly IReportsRepository _repository;

        public ReportsCommand(IReportsRepository repository)
        {
            _repository = repository;
        }

        void IReportsCommand.CreateReport(Report report)
        {
            report.Prepare();
            report.Validate();
            _repository.CreateReport(report);
        }

        void IReportsCommand.UpdateReport(Report report)
        {
            report.Validate();
            _repository.UpdateReport(report);
        }

        void IReportsCommand.CreateReportRun(ReportRun reportRun)
        {
            reportRun.Prepare();
            reportRun.Validate();
            _repository.CreateReportRun(reportRun);
        }
    }
}