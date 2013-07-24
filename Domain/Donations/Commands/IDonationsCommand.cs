namespace LinkMe.Domain.Donations.Commands
{
    public interface IDonationsCommand
    {
        void CreateRequest(DonationRequest request);
        void CreateDonation(Donation donation);
    }
}