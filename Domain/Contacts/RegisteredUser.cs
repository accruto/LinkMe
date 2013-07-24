using System;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public abstract class RegisteredUser
        : IRegisteredUser, ICommunicationUser
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        public abstract UserType UserType { get; }

        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName.CombineLastName(LastName); }
        }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActivated { get; set; }

        Guid? IRegisteredUser.AffiliateId
        {
            get { return GetAffiliateId(); }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return GetAffiliateId(); }
        }

        string ICommunicationRecipient.EmailAddress
        {
            get
            {
                var address = GetCommunicationEmailAddress();
                return address == null ? null : address.Address;
            }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get
            {
                var address = GetCommunicationEmailAddress();
                return address == null ? false : address.IsVerified;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (RegisteredUser))
                return false;
            return Equals((RegisteredUser)obj);
        }

        public bool Equals(RegisteredUser other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.FirstName, FirstName)
                && Equals(other.LastName, LastName)
                && other.IsEnabled.Equals(IsEnabled)
                && other.IsActivated.Equals(IsActivated);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (FirstName != null ? FirstName.GetHashCode() : 0);
                result = (result * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                result = (result * 397) ^ IsEnabled.GetHashCode();
                result = (result * 397) ^ IsActivated.GetHashCode();
                return result;
            }
        }
        protected abstract Guid? GetAffiliateId();
        protected abstract EmailAddress GetCommunicationEmailAddress();
    }
}
