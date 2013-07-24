using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Reports.Employers.Queries
{
    public interface IEmployerReportsQuery
    {
        IList<EmployerReport> GetReports(Guid clientId);
        EmployerReport GetReport(Guid clientId, string type);

        IList<EmployerReport> GetReportsToRun(DateTime startDate, DateTime endDate);
    }
}