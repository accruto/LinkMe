using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Products;
using LinkMe.Web.Areas.Employers.Models.Accounts;

namespace LinkMe.Web.Areas.Employers.Models.Products
{
    public class NewOrderChooseModel
        : PageflowModel
    {
        public Guid SelectedContactProductId { get; set; }
        public OrderDetailsModel OrderDetails { get; set; }
        public IList<Product> ContactProducts { get; set; }
    }

    public class NewOrderAccountModel
        : PageflowModel
    {
        public AccountModel Account { get; set; }
        public OrderDetailsModel OrderDetails { get; set; }
    }

    public class NewOrderPaymentModel
        : PageflowModel
    {
        public PaymentModel Payment { get; set; }
        public Guid ProductId { get; set; }
    }

    public class NewOrderReceiptModel
        : PageflowModel
    {
        public OrderSummaryModel OrderSummary { get; set; }
    }
}