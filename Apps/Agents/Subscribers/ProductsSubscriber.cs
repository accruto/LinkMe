using LinkMe.Apps.Agents.Domain.Credits.Handlers;
using LinkMe.Apps.Agents.Domain.Roles.Orders.Handlers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class ProductsSubscriber
    {
        private readonly ICreditsHandler _creditsHandler;
        private readonly IOrdersHandler _ordersHandler;

        public ProductsSubscriber(ICreditsHandler creditsHandler, IOrdersHandler ordersHandler)
        {
            _creditsHandler = creditsHandler;
            _ordersHandler = ordersHandler;
        }

        [SubscribesTo(LinkMe.Domain.Credits.PublishedEvents.CreditExercised)]
        public void OnCreditExercised(object sender, CreditExercisedEventArgs args)
        {
            _creditsHandler.OnCreditExercised(args.CreditId, args.OwnerId, args.AllocationAdjusted);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Orders.PublishedEvents.OrderPurchased)]
        public void OnOrderPurchased(object sender, OrderPurchasedEventArgs args)
        {
            _ordersHandler.OnOrderPurchased(args.Order, args.Receipt);
        }
    }
}