using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Donations.Queries
{
    public class DonationsQuery
        : IDonationsQuery
    {
        private readonly IDonationsRepository _repository;

        public DonationsQuery(IDonationsRepository repository)
        {
            _repository = repository;
        }

        IList<DonationRecipient> IDonationsQuery.GetRecipients()
        {
            return _repository.GetRecipients();
        }

        DonationRecipient IDonationsQuery.GetRecipient(Guid id)
        {
            return _repository.GetRecipient(id);
        }

        DonationRecipient IDonationsQuery.GetRecipient(string name)
        {
            return _repository.GetRecipient(name);
        }

        DonationRequest IDonationsQuery.GetRequest(Guid id)
        {
            return _repository.GetRequest(id);
        }

        DonationRequest IDonationsQuery.GetRequest(Guid recipientId, decimal amount)
        {
            return _repository.GetRequest(recipientId, amount);
        }

        Donation IDonationsQuery.GetDonation(Guid id)
        {
            return _repository.GetDonation(id);
        }

        IList<Donation> IDonationsQuery.GetDonations(IEnumerable<Guid> ids)
        {
            return _repository.GetDonations(ids);
        }
    }
}