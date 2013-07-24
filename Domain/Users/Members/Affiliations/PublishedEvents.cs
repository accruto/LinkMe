using System;

namespace LinkMe.Domain.Users.Members.Affiliations
{
    public static class PublishedEvents
    {
        public const string MemberChanged = "LinkMe.Domain.Users.Members.Affiliations.MemberChanged";
    }

    public class MemberEventArgs
        : EventArgs
    {
        public readonly Guid MemberId;
        public readonly Guid? AffiliateId;

        public MemberEventArgs(Guid memberId, Guid? affiliateId)
        {
            MemberId = memberId;
            AffiliateId = affiliateId;
        }
    }
}