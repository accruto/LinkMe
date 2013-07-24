using LinkMe.Domain;

namespace LinkMe.Query.Reports.Search.Queries
{
    public interface IJobAdSearchReportsQuery
    {
        int GetJobAdSearches(DateTimeRange timeRange);
        int GetJobAdSearchAlerts();
    }
}