namespace LinkMe.Domain.Donations.Data
{
    internal static class Mappings
    {
        public static DonationRecipient Map(this DonationRecipientEntity entity)
        {
            return new DonationRecipient
            {
                Id = entity.id,
                IsActive = entity.isActive,
                Name = entity.displayName,
            };
        }

        public static DonationRequestEntity Map(this DonationRequest request)
        {
            return new DonationRequestEntity
            {
                amount = request.Amount,
                donationRecipientId = request.RecipientId,
                id = request.Id,
            };
        }

        public static DonationRequest Map(this DonationRequestEntity entity)
        {
            return new DonationRequest
            {
                Id = entity.id,
                Amount = entity.amount,
                RecipientId = entity.donationRecipientId
            };
        }

        public static Donation Map(this NetworkInvitationEntity entity)
        {
            return new Donation {Id = entity.id, RequestId = entity.donationRequestId.Value};
        }
    }
}
