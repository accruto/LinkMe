using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Orders
{
    public interface IOrderReportsRepository
    {
        IList<OrderReport> GetOrderReports(DateTimeRange timeRange);
    }
}
