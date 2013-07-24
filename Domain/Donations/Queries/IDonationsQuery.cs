using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Donations.Queries
{
    public interface IDonationsQuery
    {
        IList<DonationRecipient> GetRecipients();
        DonationRecipient GetRecipient(Guid id);
        DonationRecipient GetRecipient(string name);

        DonationRequest GetRequest(Guid id);
        DonationRequest GetRequest(Guid recipientId, decimal amount);

        Donation GetDonation(Guid id);
        IList<Donation> GetDonations(IEnumerable<Guid> ids);
    }
}