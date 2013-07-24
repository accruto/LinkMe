using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Registration.Data
{
    public class RegistrationRepository
        : Repository, IRegistrationRepository
    {
        private static readonly Func<RegistrationDataContext, Guid, EmailVerificationEntity> GetEmailVerificationEntity
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid id)
                => (from v in dc.EmailVerificationEntities
                    where v.id == id
                    select v).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, Guid, string, EmailVerification> GetEmailVerificationByUser
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid userId, string emailAddress)
                => (from v in dc.EmailVerificationEntities
                    where v.userId == userId && v.emailAddress == emailAddress
                    select v.Map()).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, string, EmailVerification> GetEmailVerificationByCode
            = CompiledQuery.Compile((RegistrationDataContext dc, string verificationCode)
                => (from v in dc.EmailVerificationEntities
                    where v.verificationCode == verificationCode
                    select v.Map()).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, string, IQueryable<Guid>> GetEmailAddressUserIds
            = CompiledQuery.Compile((RegistrationDataContext dc, string emailAddress)
                => (from u in dc.RegisteredUserEntities
                    where (u.emailAddress == emailAddress || u.secondaryEmailAddress == emailAddress)
                    select u.id).Distinct());

        private static readonly Func<RegistrationDataContext, Guid, RegisteredUserEntity> GetRegisteredUserEntity
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    select u).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, Guid, AffiliationReferral> GetAffiliationReferral
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid refereeId)
                => (from r in dc.JoinReferralEntities
                    where r.userId == refereeId
                    select r.Map()).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, Guid, MemberEntity> GetMemberEntity
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid id)
                => (from m in dc.MemberEntities
                    where m.id == id
                    select m).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, IQueryable<ExternalReferralSource>> GetExternalReferralSources
            = CompiledQuery.Compile((RegistrationDataContext dc)
                => from s in dc.ReferralSourceEntities
                   orderby s.displayName
                   select s.Map());

        private static readonly Func<RegistrationDataContext, int, ExternalReferralSource> GetExternalReferralSource
            = CompiledQuery.Compile((RegistrationDataContext dc, int id)
                => (from s in dc.ReferralSourceEntities
                    where s.id == id
                    select s.Map()).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, string, ExternalReferralSource> GetExternalReferralSourceByName
            = CompiledQuery.Compile((RegistrationDataContext dc, string name)
                => (from s in dc.ReferralSourceEntities
                    where s.displayName == name
                    select s.Map()).SingleOrDefault());

        private static readonly Func<RegistrationDataContext, Guid, ExternalReferral> GetExternalReferral
            = CompiledQuery.Compile((RegistrationDataContext dc, Guid userId)
                => (from m in dc.MemberEntities
                    where m.id == userId
                    && m.enteredReferralSourceId != null
                    select m.Map()).SingleOrDefault());

        public RegistrationRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IRegistrationRepository.CreateEmailVerification(EmailVerification emailVerification)
        {
            using (var dc = CreateContext())
            {
                dc.EmailVerificationEntities.InsertOnSubmit(emailVerification.Map());
                dc.SubmitChanges();
            }
        }

        void IRegistrationRepository.DeleteEmailVerification(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEmailVerificationEntity(dc, id);
                if (entity != null)
                {
                    dc.EmailVerificationEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        EmailVerification IRegistrationRepository.GetEmailVerification(Guid userId, string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmailVerificationByUser(dc, userId, emailAddress);
            }
        }

        EmailVerification IRegistrationRepository.GetEmailVerification(string verificationCode)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmailVerificationByCode(dc, verificationCode);
            }
        }

        IList<Guid> IRegistrationRepository.GetUserIds(string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmailAddressUserIds(dc, emailAddress).ToList();
            }
        }

        void IRegistrationRepository.VerifyEmailAddress(Guid userId, string emailAddress)
        {
            UpdateIsVerified(userId, emailAddress, true);
        }

        void IRegistrationRepository.UnverifyEmailAddress(Guid userId, string emailAddress)
        {
            UpdateIsVerified(userId, emailAddress, false);
        }

        void IRegistrationRepository.CreateAffiliationReferral(AffiliationReferral affiliationReferral)
        {
            using (var dc = CreateContext())
            {
                dc.JoinReferralEntities.InsertOnSubmit(affiliationReferral.Map());
                dc.SubmitChanges();
            }
        }

        AffiliationReferral IRegistrationRepository.GetAffiliationReferral(Guid refereeId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAffiliationReferral(dc, refereeId);
            }
        }

        void IRegistrationRepository.CreateExternalReferral(ExternalReferral externalReferral)
        {
            UpdateExternalReferral(externalReferral.UserId, externalReferral.SourceId);
        }

        void IRegistrationRepository.UpdateExternalReferral(ExternalReferral externalReferral)
        {
            UpdateExternalReferral(externalReferral.UserId, externalReferral.SourceId);
        }

        void IRegistrationRepository.DeleteExternalReferral(Guid userId)
        {
            UpdateExternalReferral(userId, null);
        }

        IList<ExternalReferralSource> IRegistrationRepository.GetExternalReferralSources()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExternalReferralSources(dc).ToList();
            }
        }

        ExternalReferralSource IRegistrationRepository.GetExternalReferralSource(int id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExternalReferralSource(dc, id);
            }
        }

        ExternalReferralSource IRegistrationRepository.GetExternalReferralSource(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExternalReferralSourceByName(dc, name);
            }
        }

        ExternalReferral IRegistrationRepository.GetExternalReferral(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExternalReferral(dc, userId);
            }
        }

        private void UpdateExternalReferral(Guid userId, int? sourceId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetMemberEntity(dc, userId);
                if (entity != null)
                {
                    entity.enteredReferralSourceId = (byte?)sourceId;
                    dc.SubmitChanges();
                }
            }
        }

        private void UpdateIsVerified(Guid userId, string emailAddress, bool isVerified)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRegisteredUserEntity(dc, userId);
                if (entity != null)
                {
                    if (string.Equals(entity.emailAddress, emailAddress, StringComparison.InvariantCultureIgnoreCase))
                        entity.emailAddressVerified = isVerified;
                    else if (string.Equals(entity.secondaryEmailAddress, emailAddress, StringComparison.InvariantCultureIgnoreCase))
                        entity.secondaryEmailAddressVerified = isVerified;

                    dc.SubmitChanges();
                }
            }
        }

        private RegistrationDataContext CreateContext()
        {
            return CreateContext(c => new RegistrationDataContext(c));
        }
    }
}
