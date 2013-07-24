using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Reports.Queries
{
    public class ReportsQuery
        : IReportsQuery
    {
        private readonly IReportsRepository _repository;

        public ReportsQuery(IReportsRepository repository)
        {
            _repository = repository;
        }

        T IReportsQuery.GetReport<T>(Guid clientId)
        {
            return _repository.GetReport<T>(clientId);
        }

        IList<T> IReportsQuery.GetReportsToRun<T>(DateTime startDate, DateTime endDate)
        {
            return _repository.GetReportsToRun<T>(startDate, endDate);
        }
    }
}