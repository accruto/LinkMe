using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Communications
{
    public interface ICommunicationsRepository
    {
        IDictionary<string, CommunicationReport> GetCommunicationReport(DayRange day);
    }
}
