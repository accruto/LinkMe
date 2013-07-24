using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Resumes
{
    public class ParsedAddress
    {
        public string Street { get; set; }
        public string Location { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (ParsedAddress))
                return false;
            return Equals((ParsedAddress) obj);
        }

        public bool Equals(ParsedAddress other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.Street, Street)
                && Equals(other.Location, Location);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Street != null ? Street.GetHashCode() : 0)*397) ^ (Location != null ? Location.GetHashCode() : 0);
            }
        }
    }

    public class ParsedResume
        : IHaveEmailAddresses, IHavePhoneNumbers
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ParsedAddress Address { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
        public IList<EmailAddress> EmailAddresses { get; set; }
        public PartialDate? DateOfBirth { get; set; }
        [Prepare, Validate]
        public Resume Resume { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(FirstName)
                    && string.IsNullOrEmpty(LastName)
                    && (Address == null || (string.IsNullOrEmpty(Address.Street) && string.IsNullOrEmpty(Address.Location)))
                    && (PhoneNumbers == null || PhoneNumbers.Count == 0)
                    && (EmailAddresses == null || EmailAddresses.Count == 0)
                    && DateOfBirth == null
                    && (Resume == null || Resume.IsEmpty);
            }
        }

        public bool HasVerifiedEmail
        {
            get { return (EmailAddresses != null && EmailAddresses.Where(e => e.IsVerified) != null); }
        }
    }
}