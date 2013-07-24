using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Orders.Queries
{
    public class OrderReportsQuery
        : IOrderReportsQuery
    {
        private readonly IOrderReportsRepository _repository;

        public OrderReportsQuery(IOrderReportsRepository repository)
        {
            _repository = repository;
        }

        IList<OrderReport> IOrderReportsQuery.GetOrderReports(DateTimeRange timeRange)
        {
            return _repository.GetOrderReports(timeRange);
        }
    }
}