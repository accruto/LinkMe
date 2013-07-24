using System;
using System.Linq;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class ContactDetailsExtensions
    {
        private const string PrimaryEmailAddressSeparator = ";";
        private static readonly string[] EmailAddressSeparators = new[] {PrimaryEmailAddressSeparator, ","};

        public static void ParseEmailAddresses(this ContactDetails contactDetails, string emailAddresses)
        {
            if (contactDetails == null)
                return;

            if (string.IsNullOrEmpty(emailAddresses))
            {
                contactDetails.EmailAddress = null;
                contactDetails.SecondaryEmailAddresses = null;
                return;
            }

            var splitEmailAddresses = emailAddresses.Trim().Split(EmailAddressSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (splitEmailAddresses.Length == 0)
            {
                contactDetails.EmailAddress = null;
                contactDetails.SecondaryEmailAddresses = null;
                return;
            }

            // First entry is email address.

            contactDetails.EmailAddress = splitEmailAddresses[0].Trim();

            // The rest are the secondary email addresses.

            contactDetails.SecondaryEmailAddresses = splitEmailAddresses.Length == 1
                ? null
                : string.Join(PrimaryEmailAddressSeparator, (from s in splitEmailAddresses.Skip(1) where !string.IsNullOrEmpty(s.Trim()) select s.Trim()).ToArray());
        }

        public static string GetEmailAddressesDisplayText(this ContactDetails contactDetails)
        {
            if (contactDetails == null)
                return null;

            if (string.IsNullOrEmpty(contactDetails.EmailAddress))
                return string.IsNullOrEmpty(contactDetails.SecondaryEmailAddresses)
                    ? null
                    : contactDetails.SecondaryEmailAddresses;

            if (string.IsNullOrEmpty(contactDetails.SecondaryEmailAddresses))
                return contactDetails.EmailAddress;

            return contactDetails.EmailAddress + PrimaryEmailAddressSeparator + contactDetails.SecondaryEmailAddresses;
        }
    }
}
