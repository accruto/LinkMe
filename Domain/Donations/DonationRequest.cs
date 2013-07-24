using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Donations
{
    public class DonationRequest
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public Decimal Amount { get; set; }
        public Guid RecipientId { get; set; }
    }
}
