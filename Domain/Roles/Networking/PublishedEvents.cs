using System;

namespace LinkMe.Domain.Roles.Networking
{
    public static class PublishedEvents
    {
        public const string FirstDegreeContactMade = "LinkMe.Domain.Roles.Networking.FirstDegreeContactMade";
    }

    public class FirstDegreeContactEventArgs
        : EventArgs
    {
        public readonly Guid FromId;
        public readonly Guid ToId;

        public FirstDegreeContactEventArgs(Guid fromId, Guid toId)
        {
            FromId = fromId;
            ToId = toId;
        }
    }
}
