using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.JobAds
{
    public class JobAdFlagListsPermissionsException
        : PermissionsException
    {
        private readonly Guid _flagListId;

        public JobAdFlagListsPermissionsException(IUser member, Guid flagListId)
            : base(member)
        {
            _flagListId = flagListId;
        }

        public Guid FlagListId
        {
            get { return _flagListId; }
        }
    }
}