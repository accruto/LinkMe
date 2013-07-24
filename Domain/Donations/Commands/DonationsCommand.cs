using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Donations.Commands
{
    public class DonationsCommand
        : IDonationsCommand
    {
        private readonly IDonationsRepository _repository;

        public DonationsCommand(IDonationsRepository repository)
        {
            _repository = repository;
        }

        void IDonationsCommand.CreateRequest(DonationRequest request)
        {
            // Try to find first.

            var existingRequest = _repository.GetRequest(request.RecipientId, request.Amount);
            if (existingRequest == null)
            {
                request.Prepare();
                _repository.CreateRequest(request);
            }
            else
            {
                request.Id = existingRequest.Id;
            }
        }

        void IDonationsCommand.CreateDonation(Donation donation)
        {
            donation.Prepare();
            donation.Validate();
            _repository.CreateDonation(donation);
        }
    }
}