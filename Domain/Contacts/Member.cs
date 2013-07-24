using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class Member
        : RegisteredUser, IMember, IHaveEmailAddresses, IHavePhoneNumbers
    {
        public DateTime LastUpdatedTime { get; set; }

        [Required, ListLength(1, 2), EmailAddresses]
        public IList<EmailAddress> EmailAddresses { get; set; }
        ReadOnlyCollection<EmailAddress> IMember.EmailAddresses { get { return EmailAddresses == null ? null : EmailAddresses.ToReadOnlyCollection(); } }

        public IList<PhoneNumber> PhoneNumbers { get; set; }
        ReadOnlyCollection<PhoneNumber> IMember.PhoneNumbers { get { return PhoneNumbers == null ? null : PhoneNumbers.ToReadOnlyCollection(); } }

        public Gender Gender { get; set; }
        public PartialDate? DateOfBirth { get; set; }
        [Prepare, Validate]
        public Address Address { get; set; }
        public Guid? PhotoId { get; set; }

        public VisibilitySettings VisibilitySettings { get; set; }
        public EthnicStatus EthnicStatus { get; set; }

        public Member()
        {
            VisibilitySettings = new VisibilitySettings();
        }

        public override UserType UserType
        {
            get { return UserType.Member; }
        }

        public Guid? AffiliateId { get; set; }

        protected override Guid? GetAffiliateId()
        {
            return AffiliateId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Member))
                return false;
            return Equals((Member) obj);
        }

        public bool Equals(Member other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.EmailAddresses.NullableSequenceEqual(EmailAddresses)
                && other.PhoneNumbers.NullableSequenceEqual(PhoneNumbers)
                && Equals(other.Gender, Gender)
                && Equals(other.DateOfBirth, DateOfBirth)
                && Equals(other.Address, Address)
                && Equals(other.PhotoId, PhotoId)
                && Equals(other.VisibilitySettings, VisibilitySettings)
                && Equals(other.EthnicStatus, EthnicStatus)
                && Equals(other.AffiliateId, AffiliateId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ EmailAddresses.GetCollectionHashCode();
                result = (result * 397) ^ PhoneNumbers.GetCollectionHashCode();
                result = (result * 397) ^ Gender.GetHashCode();
                result = (result * 397) ^ (DateOfBirth.HasValue ? DateOfBirth.Value.GetHashCode() : 0);
                result = (result * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                result = (result * 397) ^ (PhotoId.HasValue ? PhotoId.Value.GetHashCode() : 0);
                result = (result * 397) ^ (VisibilitySettings != null ? VisibilitySettings.GetHashCode() : 0);
                result = (result * 397) ^ EthnicStatus.GetHashCode();
                result = (result * 397) ^ (AffiliateId.HasValue ? AffiliateId.Value.GetHashCode() : 0);
                return result;
            }
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return this.GetBestEmailAddress();
        }
    }

    public class UnregisteredMember
        : RegisteredUser, IMember
    {
        public IList<EmailAddress> EmailAddresses { get; set; }
        ReadOnlyCollection<EmailAddress> IMember.EmailAddresses { get { return EmailAddresses == null ? null : EmailAddresses.ToReadOnlyCollection(); } }

        public IList<PhoneNumber> PhoneNumbers { get; set; }
        ReadOnlyCollection<PhoneNumber> IMember.PhoneNumbers { get { return PhoneNumbers == null ? null : PhoneNumbers.ToReadOnlyCollection(); } }

        public DateTime LastUpdatedTime { get; set; }
        public Gender Gender { get; set; }
        public PartialDate? DateOfBirth { get; set; }
        [Prepare, Validate]
        public Address Address { get; set; }
        public Guid? PhotoId { get; set; }

        public VisibilitySettings VisibilitySettings { get; set; }
        public EthnicStatus EthnicStatus { get; set; }

        public override UserType UserType
        {
            get { return UserType.Anonymous; }
        }

        public Guid? AffiliateId { get; set; }

        protected override Guid? GetAffiliateId()
        {
            return AffiliateId;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddresses == null ? null : EmailAddresses.FirstOrDefault();
        }
    }
}
