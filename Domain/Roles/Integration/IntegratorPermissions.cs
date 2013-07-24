using System;

namespace LinkMe.Domain.Roles.Integration
{
    [Flags]
    public enum IntegratorPermissions
    {
        None = 0,
        PostJobAds = 1,
        GetJobApplication = 2,
        GetJobAds = 4
    }
}
