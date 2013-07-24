using System;
using LinkMe.Domain;
using LinkMe.Domain.Credits;

namespace LinkMe.Query.Reports.Credits.Queries
{
    public interface ICreditReportsQuery
    {
        CreditReport GetCreditReport<T>(Guid ownerId, DateRange dateRange) where T : Credit;
    }
}