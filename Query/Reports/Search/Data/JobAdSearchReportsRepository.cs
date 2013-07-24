using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Search.Data
{
    public class JobAdSearchReportsRepository
        : ReportsRepository<SearchDataContext>, IJobAdSearchReportsRepository
    {
        public JobAdSearchReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int IJobAdSearchReportsRepository.GetJobAdSearches(DateTimeRange dateRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from s in dc.JobSearchEntities
                        where s.startTime >= dateRange.Start && s.startTime < dateRange.End
                              && (from c in dc.JobSearchCriteriaEntities
                                  where c.setId == s.criteriaSetId
                                        && c.name != "SortOrder"
                                  select c).Any()
                        select s).Count();
            }
        }

        int IJobAdSearchReportsRepository.GetJobAdSearchAlerts()
        {
            using (var dc = CreateDataContext(true))
            {
                return (from a in dc.SavedJobSearchAlertEntities
                        join s in dc.SavedJobSearchEntities on a.id equals s.alertId
                        select a).Count();
            }
        }

        protected override SearchDataContext CreateDataContext(IDbConnection connection)
        {
            return new SearchDataContext(connection);
        }
    }
}
