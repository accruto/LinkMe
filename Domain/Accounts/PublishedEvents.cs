using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Accounts
{
    public static class PublishedEvents
    {
        public const string UserEnabled = "LinkMe.Domain.Accounts.UserEnabled";
        public const string UserDisabled = "LinkMe.Domain.Accounts.UserDisabled";
        public const string UserActivated = "LinkMe.Domain.Accounts.UserActivated";
        public const string UserDeactivated = "LinkMe.Domain.Accounts.UserDeactivated";
    }

    public class UserAccountEventArgs
        : EventArgs
    {
        public Guid UserId { get; private set; }
        public UserType UserType { get; private set; }
        public Guid ActorId { get; private set; }

        public UserAccountEventArgs(Guid userId, UserType userType, Guid actorId)
        {
            UserId = userId;
            UserType = userType;
            ActorId = actorId;
        }
    }

    public class UserDeactivatedEventArgs
        : UserAccountEventArgs
    {
        public DeactivationReason Reason { get; private set; }
        public string Comments { get; private set; }

        public UserDeactivatedEventArgs(Guid userId, UserType userType, Guid actorId, DeactivationReason reason, string comments)
            : base(userId, userType, actorId)
        {
            Reason = reason;
            Comments = comments;
        }
    }
}