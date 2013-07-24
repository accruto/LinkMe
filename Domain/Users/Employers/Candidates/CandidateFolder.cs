using System;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public enum FolderType
    {
        Private = 0,
        Shortlist = 1,
        Shared = 3,
        Mobile = 7,
    }

    public class CandidateFolder
        : ContenderList
    {
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public Guid RecruiterId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        public Guid? OrganisationId
        {
            get { return SharedWithId; }
            set { SharedWithId = value; }
        }

        public FolderType FolderType
        {
            get { return (FolderType) ListType; }
            set { ListType = (int) value; }
        }
    }

    public class CandidateFolderEntry
        : ContenderListEntry
    {
        public Guid FolderId
        {
            get { return ListId; }
            set { ListId = value; }
        }

        public Guid CandidateId
        {
            get { return ContenderId; }
            set { ContenderId = value; }
        }
    }
}
