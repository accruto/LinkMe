using System;

namespace LinkMe.Apps.Agents.Users.Members
{
    public static class PublishedEvents
    {
        public const string MemberCreated = "LinkMe.Apps.Agents.Users.Members.MemberCreated";
        public const string MemberUpdated = "LinkMe.Apps.Agents.Users.Members.MemberUpdated";
    }

    public class MemberCreatedEventArgs
        : EventArgs
    {
        public Guid MemberId { get; private set; }

        public MemberCreatedEventArgs(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}
