using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Reports.Queries
{
    public interface IReportsQuery
    {
        T GetReport<T>(Guid clientId) where T : Report, new();
        IList<T> GetReportsToRun<T>(DateTime startDate, DateTime endDate) where T : Report, new();
    }
}