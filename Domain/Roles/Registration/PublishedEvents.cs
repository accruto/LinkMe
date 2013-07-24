using System;

namespace LinkMe.Domain.Roles.Registration
{
    public static class PublishedEvents
    {
        public const string EmailAddressUnverified = "LinkMe.Domain.Users.Members.EmailAddressUnverified";
        public const string EmailAddressVerified = "LinkMe.Domain.Users.Members.EmailAddressVerified";
    }

    public class EmailAddressUnverifiedEventArgs
        : EventArgs
    {
        public Guid UserId { get; private set; }
        public string EmailAddress { get; private set; }
        public string Reason { get; private set; }

        public EmailAddressUnverifiedEventArgs(Guid userId, string emailAddress, string reason)
        {
            UserId = userId;
            EmailAddress = emailAddress;
            Reason = reason;
        }
    }

    public class EmailAddressVerifiedEventArgs
        : EventArgs
    {
        public Guid UserId { get; private set; }
        public string EmailAddress { get; private set; }

        public EmailAddressVerifiedEventArgs(Guid userId, string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }
    }
}
