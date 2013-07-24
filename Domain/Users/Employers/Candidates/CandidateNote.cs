using System;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public class CandidateNote
        : ContenderNote
    {
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

        public Guid CandidateId
        {
            get { return ContenderId; }
            set { ContenderId = value; }
        }

        public bool IsShared
        {
            get { return SharedWithId.HasValue; }
        }
    }
}
