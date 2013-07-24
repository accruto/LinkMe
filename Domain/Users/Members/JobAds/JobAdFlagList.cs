using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public enum FlagListType
    {
        Flagged = 2,
    }

    public class JobAdFlagList : JobAdList
    {
        public Guid MemberId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        public FlagListType FlagListType
        {
            get { return (FlagListType)ListType; }
            set { ListType = (int)value; }
        }
    }

    public class JobAdFlagListEntry : JobAdListEntry
    {
        public Guid FlagListId
        {
            get { return ListId; }
            set { ListId = value; }
        }
    }
}
