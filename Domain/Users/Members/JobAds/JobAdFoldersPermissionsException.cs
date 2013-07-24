using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public class JobAdFoldersPermissionsException
        : PermissionsException
    {
        private readonly Guid _folderId;

        public JobAdFoldersPermissionsException(IUser member, Guid folderId)
            : base(member)
        {
            _folderId = folderId;
        }

        public Guid FolderId
        {
            get { return _folderId; }
        }
    }
}