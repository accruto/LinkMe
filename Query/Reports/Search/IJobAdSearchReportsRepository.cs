using LinkMe.Domain;

namespace LinkMe.Query.Reports.Search
{
    public interface IJobAdSearchReportsRepository
    {
        int GetJobAdSearches(DateTimeRange timeRange);
        int GetJobAdSearchAlerts();
    }
}
