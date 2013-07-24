using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public class CandidateFlagListsPermissionsException
        : PermissionsException
    {
        private readonly Guid _flagListId;

        public CandidateFlagListsPermissionsException(IUser employer, Guid flagListId)
            : base(employer)
        {
            _flagListId = flagListId;
        }

        public Guid FlagListId
        {
            get { return _flagListId; }
        }
    }
}