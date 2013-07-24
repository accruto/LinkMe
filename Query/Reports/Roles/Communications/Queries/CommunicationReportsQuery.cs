using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Communications.Queries
{
    public class CommunicationReportsQuery
        : ICommunicationReportsQuery
    {
        private readonly ICommunicationsRepository _repository;

        public CommunicationReportsQuery(ICommunicationsRepository repository)
        {
            _repository = repository;
        }

        IDictionary<string, CommunicationReport> ICommunicationReportsQuery.GetCommunicationReport(DayRange day)
        {
            return _repository.GetCommunicationReport(day);
        }
    }
}