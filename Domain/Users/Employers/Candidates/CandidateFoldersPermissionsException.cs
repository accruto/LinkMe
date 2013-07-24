using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public class CandidateFoldersPermissionsException
        : PermissionsException
    {
        private readonly Guid _folderId;

        public CandidateFoldersPermissionsException(IUser employer, Guid folderId)
            : base(employer)
        {
            _folderId = folderId;
        }

        public Guid FolderId
        {
            get { return _folderId; }
        }
    }
}