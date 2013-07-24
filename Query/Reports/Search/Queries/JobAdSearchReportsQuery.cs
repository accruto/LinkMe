using LinkMe.Domain;

namespace LinkMe.Query.Reports.Search.Queries
{
    public class JobAdSearchReportsQuery
        : IJobAdSearchReportsQuery
    {
        private readonly IJobAdSearchReportsRepository _repository;

        public JobAdSearchReportsQuery(IJobAdSearchReportsRepository repository)
        {
            _repository = repository;
        }

        int IJobAdSearchReportsQuery.GetJobAdSearches(DateTimeRange timeRange)
        {
            return _repository.GetJobAdSearches(timeRange);
        }

        int IJobAdSearchReportsQuery.GetJobAdSearchAlerts()
        {
            return _repository.GetJobAdSearchAlerts();
        }
    }
}