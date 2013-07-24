using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearchesPermissionsException
        : PermissionsException
    {
        private readonly Guid _searchId;

        public JobAdSearchesPermissionsException(Guid userId, Guid searchId)
            : base(userId)
        {
            _searchId = searchId;
        }

        public Guid SearchId
        {
            get { return _searchId; }
        }
    }
}