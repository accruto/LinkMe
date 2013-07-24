using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Users.Employers.Queries;

namespace LinkMe.Apps.Agents.Domain.Roles.Orders.Handlers
{
    public class OrdersHandler
        : IOrdersHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IProductsQuery _productsQuery;

        public OrdersHandler(IEmailsCommand emailsCommand, IEmployersQuery employersQuery, ICreditsQuery creditsQuery, IProductsQuery productsQuery)
        {
            _emailsCommand = emailsCommand;
            _employersQuery = employersQuery;
            _creditsQuery = creditsQuery;
            _productsQuery = productsQuery;
        }

        void IOrdersHandler.OnOrderPurchased(Order order, PurchaseReceipt receipt)
        {
            var allProducts = _productsQuery.GetProducts();

            // Reorder the items by primary credit description just to introduce some certainty.

            order.Items = (from i in order.Items
                           join p in allProducts on i.ProductId equals p.Id
                           let a = p.GetPrimaryCreditAdjustment()
                           let c = a == null ? null : _creditsQuery.GetCredit(a.CreditId)
                           orderby c == null ? "" : c.Description
                           select i).ToList();

            // Grab the list of products selected.

            var orderProducts = (from i in order.Items
                                 join p in allProducts on i.ProductId equals p.Id
                                 select p).ToList();

            // Grab the list of primary properties.

            var orderPrimaryCredits = (from p in orderProducts
                                       let a = p.GetPrimaryCreditAdjustment()
                                       where a != null
                                       select _creditsQuery.GetCredit(a.CreditId)).ToList();
            var orderPrimaryAdjustments = (from p in orderProducts
                                           let a = p.GetPrimaryCreditAdjustment()
                                           where a != null
                                           select a).ToList();

            // Grab the purchaser, who is being sent the email.

            var purchaser = _employersQuery.GetEmployer(order.PurchaserId);

            _emailsCommand.TrySend(new OrderReceiptEmail(purchaser, order, orderProducts, orderPrimaryCredits, orderPrimaryAdjustments, receipt));
        }
    }
}