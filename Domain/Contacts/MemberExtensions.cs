using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Contacts
{
    public static class MemberExtensions
    {
        public static EmailAddress GetBestEmailAddress(this IMember member)
        {
            // Try to find the first verified email address.

            if (member.EmailAddresses == null || member.EmailAddresses.Count == 0)
                return null;

            var emailAddress = (from a in member.EmailAddresses where a.IsVerified select a).FirstOrDefault();
            if (emailAddress != null)
                return emailAddress;

            // None are verified so just return the first.

            return member.EmailAddresses.FirstOrDefault();
        }

        public static EmailAddress GetPrimaryEmailAddress(this IMember member)
        {
            return member.EmailAddresses == null ? null : member.EmailAddresses.FirstOrDefault();
        }

        public static EmailAddress GetSecondaryEmailAddress(this IMember member)
        {
            return member.EmailAddresses == null ? null : member.EmailAddresses.Skip(1).FirstOrDefault();
        }

        public static PhoneNumber GetBestPhoneNumber(this IMember member)
        {
            return member.GetPrimaryPhoneNumber();
        }

        public static PhoneNumber GetPrimaryPhoneNumber(this IMember member)
        {
            return member.PhoneNumbers == null ? null : member.PhoneNumbers.FirstOrDefault();
        }

        public static PhoneNumber GetSecondaryPhoneNumber(this IMember member)
        {
            return member.PhoneNumbers == null ? null : member.PhoneNumbers.Skip(1).FirstOrDefault();
        }

        public static void UpdateSecondaryEmailAddress(this Member member, EmailAddress emailAddress)
        {
            // Make an effort to update the second email address but if there are none
            // then simply add it to the list.

            if (member.EmailAddresses == null)
                member.EmailAddresses = new List<EmailAddress>();
            if (member.EmailAddresses.Count > 1)
                member.EmailAddresses[1] = emailAddress;
            else
                member.EmailAddresses.Add(emailAddress);
        }

        public static PhoneNumber GetTertiaryPhoneNumber(this IMember member)
        {
            return member.PhoneNumbers == null ? null : member.PhoneNumbers.Skip(2).FirstOrDefault();
        }

        public static void UpdateSecondaryPhoneNumber(this Member member, PhoneNumber phoneNumber)
        {
            // Make an effort to update the second phone number but if there are no phone numbers
            // then simply add it to the list.
            
            if (member.PhoneNumbers == null)
                member.PhoneNumbers = new List<PhoneNumber>();
            if (member.PhoneNumbers.Count > 1)
                member.PhoneNumbers[1] = phoneNumber;
            else
                member.PhoneNumbers.Add(phoneNumber);
        }

        public static PhoneNumber GetPhoneNumber(this IMember member, PhoneNumberType type)
        {
            if (member.PhoneNumbers == null)
                return null;
            var phoneNumber = (from p in member.PhoneNumbers where p.Type == type select p).FirstOrDefault();
            return phoneNumber == null || string.IsNullOrEmpty(phoneNumber.Number) ? null : phoneNumber;
        }
    }
}
