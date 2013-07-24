using System;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Credits
{
    public interface ICreditReportsRepository
    {
        CreditReport GetCreditReport(Guid ownerId, Guid creditId, DateRange dateRange);
    }
}