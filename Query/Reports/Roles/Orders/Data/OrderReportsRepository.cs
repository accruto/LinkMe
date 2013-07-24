using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Orders.Data
{
    public class OrderReportsRepository
        : ReportsRepository<OrdersDataContext>, IOrderReportsRepository
    {
        public OrderReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<OrderReport> IOrderReportsRepository.GetOrderReports(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from o in dc.ProductOrderEntities
                        join r in dc.ProductReceiptEntities on o.id equals r.orderId
                        join u in dc.RegisteredUserEntities on o.purchaserId equals u.id
                        join e in dc.EmployerEntities on u.id equals e.id
                        join g in dc.OrganisationEntities on e.organisationId equals g.id
                        where o.time >= timeRange.Start && o.time < timeRange.End
                        select new OrderReport
                        {
                            Id = o.id,
                            ClientName = u.firstName + ' ' + u.lastName,
                            OrganisationName = g.displayName,
                            Products = (from i in dc.ProductOrderItemEntities
                                        join p in dc.ProductEntities on i.productId equals p.id
                                        where o.id == i.orderId
                                        select p.name).ToArray(),
                            Price = o.priceInclTax
                        }).ToList();
            }
        }

        protected override OrdersDataContext CreateDataContext(IDbConnection connection)
        {
            return new OrdersDataContext(connection);
        }
    }
}
