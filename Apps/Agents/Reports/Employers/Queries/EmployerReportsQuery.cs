using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Reports.Queries;

namespace LinkMe.Apps.Agents.Reports.Employers.Queries
{
    public class EmployerReportsQuery
        : IEmployerReportsQuery
    {
        private readonly IReportsQuery _reportsQuery;

        public EmployerReportsQuery(IReportsQuery reportsQuery)
        {
            _reportsQuery = reportsQuery;
        }

        IList<EmployerReport> IEmployerReportsQuery.GetReports(Guid clientId)
        {
            return (from r in new[]
                                  {
                                      (EmployerReport) _reportsQuery.GetReport<CandidateCareReport>(clientId),
                                      _reportsQuery.GetReport<ResumeSearchActivityReport>(clientId),
                                      _reportsQuery.GetReport<JobBoardActivityReport>(clientId)
                                  }
            where r != null
            select r).ToList();
        }

        EmployerReport IEmployerReportsQuery.GetReport(Guid clientId, string type)
        {
            switch (type.ToLower())
            {
                case "jobboardactivityreport":
                    return _reportsQuery.GetReport<JobBoardActivityReport>(clientId);

                case "candidatecarereport":
                    return _reportsQuery.GetReport<CandidateCareReport>(clientId);

                case "resumesearchactivityreport":
                    return _reportsQuery.GetReport<ResumeSearchActivityReport>(clientId);

                default:
                    return null;
            }
        }

        IList<EmployerReport> IEmployerReportsQuery.GetReportsToRun(DateTime startDate, DateTime endDate)
        {
            return _reportsQuery.GetReportsToRun<CandidateCareReport>(startDate, endDate).Cast<EmployerReport>()
                .Concat(_reportsQuery.GetReportsToRun<JobBoardActivityReport>(startDate, endDate))
                .Concat(_reportsQuery.GetReportsToRun<ResumeSearchActivityReport>(startDate, endDate)).ToList();
        }
    }
}