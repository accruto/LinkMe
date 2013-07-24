using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class OrderReceiptEmail
        : EmployerEmail
    {
        private readonly Order _order;
        private readonly Product[] _products;
        private readonly Credit[] _credits;
        private readonly ProductCreditAdjustment[] _adjustments;
        private readonly PurchaseReceipt _receipt;

        public OrderReceiptEmail(ICommunicationUser to, Order order, IEnumerable<Product> orderProducts, IEnumerable<Credit> orderPrimaryCredits, IEnumerable<ProductCreditAdjustment> orderPrimaryAdjustments, PurchaseReceipt receipt)
            : base(to)
        {
            _order = order;
            _products = orderProducts.ToArray();
            _credits = orderPrimaryCredits.ToArray();
            _adjustments = orderPrimaryAdjustments.ToArray();
            _receipt = receipt;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);

            properties.Add("Order", _order);
            properties.Add("Products", _products);
            properties.Add("Credits", _credits);
            properties.Add("Adjustments", _adjustments);
            properties.Add("Receipt", _receipt);
        }
    }
}
