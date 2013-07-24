using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Registration
{
    public interface IRegistrationReportsRepository
    {
        IDictionary<string, int> GetJoinReferrals(DateTimeRange timeRange);
    }
}
