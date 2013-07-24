using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.JobAds
{
    public class JobAdBlockListsPermissionsException
        : PermissionsException
    {
        private readonly Guid _blockListId;

        public JobAdBlockListsPermissionsException(IUser member, Guid blockListId)
            : base(member)
        {
            _blockListId = blockListId;
        }

        public Guid BlockListId
        {
            get { return _blockListId; }
        }
    }
}