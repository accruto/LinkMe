using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products.Commands;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Web.Areas.Employers.Models.Products;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.Products
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class ProductsController
        : EmployersController
    {
        private readonly IEmployerOrdersQuery _employerOrdersQuery;
        private readonly IProductsCommand _productsCommand;
        private readonly IOrdersQuery _ordersQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;

        public ProductsController(IEmployerOrdersQuery employerOrdersQuery, IProductsCommand productsCommand, IOrdersQuery ordersQuery, ICreditsQuery creditsQuery, IAllocationsQuery allocationsQuery, IRecruitersQuery recruitersQuery, IOrganisationsQuery organisationsQuery)
        {
            _employerOrdersQuery = employerOrdersQuery;
            _productsCommand = productsCommand;
            _ordersQuery = ordersQuery;
            _creditsQuery = creditsQuery;
            _allocationsQuery = allocationsQuery;
            _recruitersQuery = recruitersQuery;
            _organisationsQuery = organisationsQuery;
        }

        public ActionResult Credits()
        {
            var employerId = User.Id().Value;

            var credits = _creditsQuery.GetCredits();

            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employerId);
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(hierarchy);

            var orders = _ordersQuery.GetOrders(from a in allocations.SelectMany(b => b.Value) where a.ReferenceId != null select a.ReferenceId.Value);
            return View(new EmployerCreditsModel
            {
                Employer = CurrentEmployer,
                OrganisationHierarchy = _organisationsQuery.GetOrganisations(hierarchy.Skip(1)),
                Credits = credits,
                Allocations = allocations.ToDictionary(a => a.Key, a => (IList<Allocation>)a.Value),
                Orders = orders
            });
        }

        public ActionResult Orders()
        {
            return View(_ordersQuery.GetPurchasedOrders(User.Id().Value));
        }

        public ActionResult Order(Guid id)
        {
            var order = _ordersQuery.GetPurchasedOrder(id);
            if (order == null)
                return NotFound("order", "id", id);

            // Can only view your own order.

            return order.OwnerId == User.Id().Value
                ? GetOrderSummaryView(order)
                : NotFound("order", "id", id);
        }

        public ActionResult Print(Guid id)
        {
            var order = _ordersQuery.GetPurchasedOrder(id);
            if (order == null)
                return NotFound("order", "id", id);

            return GetOrderSummaryView(order);
        }

        private ActionResult GetOrderSummaryView(Order order)
        {
            // Ensure the viewer has access.

            var userId = User.Id().Value;
            if (order.PurchaserId != userId)
                return RedirectToRoute(ProductsRoutes.Orders);

            // Use the products command because the products may now be disabled.

            return View(_employerOrdersQuery.GetOrderSummary(_creditsQuery, _ordersQuery, order, _productsCommand.GetProducts(), CurrentRegisteredUser));
        }
    }
}