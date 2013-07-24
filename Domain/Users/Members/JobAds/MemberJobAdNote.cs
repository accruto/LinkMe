using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds
{
    public class MemberJobAdNote
        : JobAdNote
    {
        public Guid MemberId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }
    }
}
