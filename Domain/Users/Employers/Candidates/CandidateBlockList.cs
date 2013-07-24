using System;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public enum BlockListType
    {
        Temporary = 4,
        Permanent = 5,
    }

    public class CandidateBlockList
        : ContenderList
    {
        public Guid RecruiterId
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

    public class CandidateBlockListEntry
        : ContenderListEntry
    {
        public Guid BlockListId
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
