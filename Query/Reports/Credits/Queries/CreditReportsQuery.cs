using System;
using LinkMe.Domain;
using LinkMe.Domain.Credits.Queries;

namespace LinkMe.Query.Reports.Credits.Queries
{
    public class CreditReportsQuery
        : ICreditReportsQuery
    {
        private readonly ICreditReportsRepository _repository;
        private readonly ICreditsQuery _creditsQuery;

        public CreditReportsQuery(ICreditReportsRepository repository, ICreditsQuery creditsQuery)
        {
            _repository = repository;
            _creditsQuery = creditsQuery;
        }

        CreditReport ICreditReportsQuery.GetCreditReport<T>(Guid ownerId, DateRange dateRange)
        {
            return _repository.GetCreditReport(ownerId, _creditsQuery.GetCredit<T>().Id, dateRange);
        }
    }
}