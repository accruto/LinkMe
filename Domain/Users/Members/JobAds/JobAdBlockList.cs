using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public enum BlockListType
    {
        Permanent = 5,
    }

    public class JobAdBlockList
        : JobAdList
    {
        public Guid MemberId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        public BlockListType BlockListType
        {
            get { return (BlockListType)ListType; }
            set { ListType = (int)value; }
        }
    }

    public class JobAdBlockListEntry
        : JobAdListEntry
    {
        public Guid BlockListId
        {
            get { return ListId; }
            set { ListId = value; }
        }
    }
}
