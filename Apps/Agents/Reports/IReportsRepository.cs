using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Reports
{
    public interface IReportsRepository
    {
        void CreateReport(Report report);
        void UpdateReport(Report report);
        T GetReport<T>(Guid clientId) where T : Report, new();

        void CreateReportRun(ReportRun reportRun);
        IList<T> GetReportsToRun<T>(DateTime startDate, DateTime endDate) where T : Report, new();
    }
}
