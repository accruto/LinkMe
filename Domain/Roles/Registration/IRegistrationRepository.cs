using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Registration
{
    public interface IRegistrationRepository
    {
        void CreateEmailVerification(EmailVerification emailVerification);
        void DeleteEmailVerification(Guid id);

        EmailVerification GetEmailVerification(Guid userId, string emailAddress);
        EmailVerification GetEmailVerification(string verificationCode);

        IList<Guid> GetUserIds(string emailAddress);
        void VerifyEmailAddress(Guid userId, string emailAddress);
        void UnverifyEmailAddress(Guid userId, string emailAddress);

        void CreateAffiliationReferral(AffiliationReferral affiliationReferral);
        AffiliationReferral GetAffiliationReferral(Guid refereeId);

        void CreateExternalReferral(ExternalReferral externalReferral);
        void UpdateExternalReferral(ExternalReferral externalReferral);
        void DeleteExternalReferral(Guid userId);
        ExternalReferral GetExternalReferral(Guid userId);

        IList<ExternalReferralSource> GetExternalReferralSources();
        ExternalReferralSource GetExternalReferralSource(int id);
        ExternalReferralSource GetExternalReferralSource(string name);
    }
}
