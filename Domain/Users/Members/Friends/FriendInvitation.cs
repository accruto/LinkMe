using System;
using LinkMe.Domain.Roles.Networking;

namespace LinkMe.Domain.Users.Members.Friends
{
    public class FriendInvitation
        : NetworkingInvitation
    {
        public Guid? DonationRequestId { get; set; }
    }
}