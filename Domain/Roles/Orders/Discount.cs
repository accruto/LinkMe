namespace LinkMe.Domain.Roles.Orders
{
    public class Discount
    {
        public decimal Percentage { get; set; }
    }

    public class BundleDiscount
        : Discount
    {
    }
}
