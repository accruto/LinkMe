using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Donations
{
    public interface IDonationsRepository
    {
        IList<DonationRecipient> GetRecipients();
        DonationRecipient GetRecipient(Guid id);
        DonationRecipient GetRecipient(string name);

        void CreateRequest(DonationRequest request);
        DonationRequest GetRequest(Guid id);
        DonationRequest GetRequest(Guid recipientId, decimal amount);

        void CreateDonation(Donation donation);
        Donation GetDonation(Guid id);
        IList<Donation> GetDonations(IEnumerable<Guid> ids);
    }
}
