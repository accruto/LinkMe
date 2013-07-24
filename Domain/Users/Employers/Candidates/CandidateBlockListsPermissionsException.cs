using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public class CandidateBlockListsPermissionsException
        : PermissionsException
    {
        private readonly Guid _blockListId;

        public CandidateBlockListsPermissionsException(IUser employer, Guid blockListId)
            : base(employer)
        {
            _blockListId = blockListId;
        }

        public Guid BlockListId
        {
            get { return _blockListId; }
        }
    }
}