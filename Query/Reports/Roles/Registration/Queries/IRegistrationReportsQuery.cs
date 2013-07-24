using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Registration.Queries
{
    public interface IRegistrationReportsQuery
    {
        IDictionary<string, PromotionCodeReport> GetPromotionCodeReports(DayRange day);
    }
}