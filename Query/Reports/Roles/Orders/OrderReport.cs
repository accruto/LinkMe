using System;

namespace LinkMe.Query.Reports.Roles.Orders
{
    public class OrderReport
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string OrganisationName { get; set; }
        public string[] Products { get; set; }
        public decimal Price { get; set; }
    }
}
