using System;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public enum FlagListType
    {
        Flagged = 2,
    }

    public class CandidateFlagList
        : ContenderList
    {
        public Guid RecruiterId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        public FlagListType FlagListType
        {
            get { return (FlagListType) ListType; }
            set { ListType = (int) value; }
        }
    }

    public class CandidateFlagListEntry
        : ContenderListEntry
    {
        public Guid FlagListId
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
