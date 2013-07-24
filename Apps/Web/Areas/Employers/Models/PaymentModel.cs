using LinkMe.Domain.Products;

namespace LinkMe.Web.Areas.Employers.Models
{
    public class PaymentModel
    {
        public bool UseDiscount { get; set; }
        public string CouponCode { get; set; }
        public OrderDetailsModel OrderDetails { get; set; }
        public CreditCard CreditCard { get; set; }
        public bool AuthoriseCreditCard { get; set; }
    }
}