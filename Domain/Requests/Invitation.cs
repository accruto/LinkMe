using System;

namespace LinkMe.Domain.Requests
{
    public class Invitation
        : Request
    {
        public Guid InviterId { get; set; }
        public Guid? InviteeId { get; set; }
        public string InviteeEmailAddress { get; set; }
    }
}
