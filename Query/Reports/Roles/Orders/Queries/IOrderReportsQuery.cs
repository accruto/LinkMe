using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Orders.Queries
{
    public interface IOrderReportsQuery
    {
        IList<OrderReport> GetOrderReports(DateTimeRange timeRange);
    }
}