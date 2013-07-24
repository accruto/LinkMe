using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public enum FolderType
    {
        Private = 0,
        Mobile = 7,
    }

    public class JobAdFolder
        : JobAdList
    {
        public Guid MemberId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        public FolderType FolderType
        {
            get { return (FolderType)ListType; }
            set { ListType = (int)value; }
        }
    }

    public class JobAdFolderEntry
        : JobAdListEntry
    {
        public Guid FolderId
        {
            get { return ListId; }
            set { ListId = value; }
        }
    }
}
