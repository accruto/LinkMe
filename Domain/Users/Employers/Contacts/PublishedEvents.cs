using System;

namespace LinkMe.Domain.Users.Employers.Contacts
{
    public static class PublishedEvents
    {
        public const string MessageCreated = "LinkMe.Domain.Users.Employers.Contacts.MessageCreated";
    }

    public class MessageCreatedEventArgs
        : EventArgs
    {
        public Guid EmployerId { get; private set; }
        public Guid MemberId { get; private set; }
        public Guid? RepresentativeId { get; private set; }
        public MemberMessage Message { get; private set; }

        public MessageCreatedEventArgs(Guid employerId, Guid memberId, Guid? representativeId, MemberMessage message)
        {
            EmployerId = employerId;
            MemberId = memberId;
            RepresentativeId = representativeId;
            Message = message;
        }
    }
}
