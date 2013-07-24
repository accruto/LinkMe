using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.JobAds
{
    [Serializable]
    public class JobAdPermissionsException
        : PermissionsException
    {
        private readonly Guid _jobAdId;

        public JobAdPermissionsException(IHasId<Guid> user, Guid jobAdId)
            : base(user)
        {
            _jobAdId = jobAdId;
        }

        public Guid JobAdId
        {
            get { return _jobAdId; }
        }
    }
}