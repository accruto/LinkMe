namespace LinkMe.Domain.Roles.Registration.Data
{
    internal static class Mappings
    {
        public static EmailVerificationEntity Map(this EmailVerification emailVerification)
        {
            return new EmailVerificationEntity
            {
                id = emailVerification.Id,
                createdTime = emailVerification.CreatedTime,
                emailAddress = emailVerification.EmailAddress,
                userId = emailVerification.UserId,
                verificationCode = emailVerification.VerificationCode
            };
        }

        public static EmailVerification Map(this EmailVerificationEntity entity)
        {
            return new EmailVerification
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                EmailAddress = entity.emailAddress,
                UserId = entity.userId,
                VerificationCode = entity.verificationCode
            };
        }

        public static JoinReferralEntity Map(this AffiliationReferral affiliationReferral)
        {
            return new JoinReferralEntity
            {
                userId = affiliationReferral.RefereeId,
                promotionCode = affiliationReferral.PromotionCode,
                refererUrl = affiliationReferral.RefererUrl,
                referralCode = affiliationReferral.ReferralCode,
            };
        }

        public static AffiliationReferral Map(this JoinReferralEntity entity)
        {
            return new AffiliationReferral
            {
                RefereeId = entity.userId,
                PromotionCode = entity.promotionCode,
                RefererUrl = entity.refererUrl,
                ReferralCode = entity.referralCode,
            };
        }

        public static ExternalReferralSource Map(this ReferralSourceEntity entity)
        {
            return new ExternalReferralSource
            {
                Id = entity.id,
                Name = entity.displayName,
            };
        }

        public static ExternalReferral Map(this MemberEntity entity)
        {
            return new ExternalReferral
            {
                UserId = entity.id,
                SourceId = entity.enteredReferralSourceId.Value,
            };
        }
    }
}
