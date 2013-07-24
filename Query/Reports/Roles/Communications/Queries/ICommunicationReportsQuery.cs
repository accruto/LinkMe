using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Communications.Queries
{
    public interface ICommunicationReportsQuery
    {
        IDictionary<string, CommunicationReport> GetCommunicationReport(DayRange day);
    }
}