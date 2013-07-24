using System;
using System.Collections.Generic;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Web.Models.Credits
{
    public abstract class CreditsModel
    {
        public IList<Credit> Credits { get; set; }
        public IDictionary<Guid, IList<Allocation>> Allocations { get; set; }
        public IList<Order> Orders { get; set; }
    }
}