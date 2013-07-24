using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Web.Areas.Employers.Models
{
    public class OrderDetailsModel
    {
        public Order Order { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Product> OrderProducts { get; set; }
        public IDictionary<Guid, Credit> Credits { get; set; }
    }

    public class OrderSummaryModel
    {
        public OrderDetailsModel OrderDetails { get; set; }
        public IRegisteredUser Purchaser { get; set; }
        public CreditCardReceipt Receipt { get; set; }
    }
}